using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Conversations.Commands.SendMessage;
using Rossgram.Application.Posts.Commands.UploadPost;
using Rossgram.Domain;
using Rossgram.Domain.Enumerations;

namespace Rossgram.WebApi.Configurations;

public static partial class AppConfiguration
{
    public static IServiceCollection AddConfiguredControllers(this IServiceCollection services)
    {
        services.AddControllers()
            .AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                new List<JsonConverter>
                    {
                        new StringEnumConverter(new CamelCaseNamingStrategy()),
                        new StringEnumerationConverter(),
                        new AbstractClassConverter<UploadPostCommand.AttachmentDTO, AttachmentType>(
                            typePropertyLambda: y => y.AttachmentType,
                            typeChooser: new Dictionary<AttachmentType, Type>()
                            {
                                [AttachmentType.File] = typeof(UploadPostCommand.AttachmentFileDTO),
                            }),
                        new AbstractClassConverter<SendMessageCommand.AttachmentDTO, AttachmentType>(
                            typePropertyLambda: y => y.AttachmentType,
                            typeChooser: new Dictionary<AttachmentType, Type>()
                            {
                                [AttachmentType.File] = typeof(SendMessageCommand.AttachmentFileDTO),
                                [AttachmentType.Link] = typeof(SendMessageCommand.AttachmentLinkDTO),
                                [AttachmentType.Post] = typeof(SendMessageCommand.AttachmentPostDTO),
                            })
                    }
                    .ForEach(x.SerializerSettings.Converters.Add);

                x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
        return services;
    }

    public class StringEnumerationConverter : JsonConverter
    {
        public override bool CanWrite => true;
        
        public override bool CanConvert(Type objectType)
        {
            return typeof(Enumeration).IsAssignableFrom(objectType);
        }
        
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is not null)
            {
                Enumeration enumeration = (Enumeration) value;
                writer.WriteValue(enumeration.ToString());
            }
            else writer.WriteNull();
        }
        
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.Value is not string enumerationCode) return null;

            Enumeration result = (Enumeration) typeof(Enumeration)
                .GetMethod(nameof(Enumeration.GetByCode))
                !.MakeGenericMethod(objectType)
                .Invoke(null, new object?[] {enumerationCode})!;
            
            return result;
        }
    }
    
    public class AbstractClassConverter<TBase, TEnumeration> : JsonConverter
        where TEnumeration : Enumeration
    {
        public override bool CanWrite => true;

        private readonly string _typePropertyName;
        private readonly Dictionary<TEnumeration, Type> _typeChooser;

        public AbstractClassConverter(
            Expression<Func<TBase, TEnumeration>> typePropertyLambda,
            Dictionary<TEnumeration, Type> typeChooser)
        {
            _typePropertyName = typePropertyLambda.GetMemberName();
            _typeChooser = typeChooser;
        }
        
        public override bool CanConvert(Type objectType)
        {
            return typeof(TBase) == objectType;
        }
        
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is not null) writer.WriteValue(value);
            else writer.WriteNull();
        }
        
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            JProperty? typeProperty = jObject.Properties()
                .FirstOrDefault(x => x.Name.ToLower() == _typePropertyName.ToLower());

            string? typeString = (typeProperty?.Value as JValue)?.Value<string>();
            
            if (string.IsNullOrEmpty(typeString)) throw new NullReferenceException(
                $"Cannot read '{_typePropertyName}' property to choose derived type");
            
            TEnumeration typeEnumeration = Enumeration.GetByCode<TEnumeration>(typeString);
            Type requiredType = _typeChooser[typeEnumeration];
            
            return serializer.Deserialize(jObject.CreateReader(), requiredType);
        }
    }
}