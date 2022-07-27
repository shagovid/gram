using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Interfaces.Configs;
using Rossgram.Application.Common.Services;
using Rossgram.Domain.Entities;
using Rossgram.Domain.Entities.Posts;
using Rossgram.Domain.Enumerations;
using Rossgram.WebApi.Configurations;
using Serilog;
using Serilog.Debugging;

namespace Rossgram.WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        IHost webHost = CreateHostBuilder(args).Build();
        using (IServiceScope scope = webHost.Services.CreateScope())
        {
            // Migrate database
            IAppDbContext context = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
            await context.AutoMigrationAsync();

            // Initialize logger
            IDatabaseConfig databaseConfig = scope.ServiceProvider.GetRequiredService<IDatabaseConfig>();
            Log.Logger = AppConfiguration.CreateLogger(databaseConfig.ConnectionString);
#if DEBUG
            SelfLog.Enable(x => Debug.WriteLine(x));
            await InsertDatabaseSeed(context, scope);
#endif
        }

        Log.Information("Application started");
        await webHost.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel(o =>
                {
                    o.ListenAnyIP(EnvironmentVariable.KestrelPort);
                });
                webBuilder.UseStartup<Startup>();
            });

    private static async Task InsertDatabaseSeed(IAppDbContext context, IServiceScope scope)
    {
        PasswordController passwordController = scope.ServiceProvider.GetRequiredService<PasswordController>();

        Account account1 = new Account(
            id: default,
            role: Role.User,
            nickname: "testuser",
            password: passwordController.Create("testuser"),
            email: "testuser@ross.ru",
            name: "Test Testing",
            bio: "Just for test",
            avatarId: null,
            avatar: null,
            isVerified: false,
            uploadedFiles: default!,
            histories: default!,
            posts: default!,
            postsLikes: default!,
            postsComments: default!,
            postCommentsLikes: default!,
            followers: default!,
            followings: default!,
            privateConversationsAsNewer: default!,
            privateConversationsAsOlder: default!,
            groupsConversationsMember: default!,
            sentMessages: default!,
            messagesLikes: default!
        );
        
        UploadedFile avaUploadedFile = new UploadedFile(
            id: default,
            ownerId: default,
            owner: default!,
            fullName: "emptyava.png",
            type: FileType.Photo, 
            objectsStorageKey: "images/2022/6/3/637898758084756586824a62d7b9c260eff993a7b52bd3b3cb/emptyava.png");
        List<UploadedFile> uploadedFiles2 = Enumerable.Range(0, 100).Select(x => new UploadedFile(
            id: default,
            ownerId: default,
            owner: default!,
            fullName: "11.jpg",
            type: FileType.Photo,
            objectsStorageKey: "images/2022/5/26/6378919993582857005162d02a0dd8d4494df6d62b86053b5a/11.jpg"
        )).Concat(new [] {avaUploadedFile}).ToList();
        Account account2 = new Account(
            id: default,
            role: Role.User,
            nickname: "testuser2",
            password: passwordController.Create("testuser2"),
            email: "testuser2@ross.ru",
            name: "Test Testing 2",
            bio: "Just for test 2",
            avatarId: null,
            avatar: null!,
            isVerified: false,
            uploadedFiles: uploadedFiles2,
            histories: default!,
            posts: default!,
            postsLikes: default!,
            postsComments: default!,
            postCommentsLikes: default!,
            followers: default!,
            followings: default!,
            privateConversationsAsNewer: default!,
            privateConversationsAsOlder: default!,
            groupsConversationsMember: default!,
            sentMessages: default!,
            messagesLikes: default!
        );
        await context.Accounts.AddAsync(account1);
        await context.Accounts.AddAsync(account2);
        await context.SaveChangesAsync();

        account2.AvatarId = avaUploadedFile.Id;
        context.Accounts.Update(account2);
        await context.SaveChangesAsync();

        Following following1To2 = new Following(
            id: default,
            accountId: account2.Id,
            account: default!,
            followerId: account1.Id,
            follower: default!
        );
        await context.Followings.AddAsync(following1To2);
        await context.SaveChangesAsync();
        
        List<Post> posts2 = uploadedFiles2.Select(x => new Post(
            id: default,
            ownerId: account2.Id,
            owner: default!,
            timeStamp: DateTimeOffset.UtcNow.AddMinutes(x.Id - (uploadedFiles2.Count + 5)),
            attachments: new List<PostAttachment>(new[]
                {
                    new PostAttachmentFile(
                        id: default,
                        postId: default,
                        post: default!,
                        order: 1,
                        uploadedFileId: x.Id,
                        uploadedFile: default!
                    )
                }
            ),
            comment: "Вот мы :)",
            likes: default!,
            comments: default!,
            messagesAttachments: default!
        )).ToList();
        await context.Posts.AddRangeAsync(posts2);
        await context.SaveChangesAsync();

        List<PostComment> comments = posts2
            .SelectMany(x => Enumerable.Range(0, 20)
                .Select(y => new PostComment(
                    id: default,
                    ownerId: y % 2 == 0 ? account1.Id : account2.Id,
                    owner: default!,
                    postId: x.Id,
                    post: default!,
                    timeStamp: x.TimeStamp.AddMinutes(y),
                    text: string.Join(" ", Enumerable.Repeat("test", 10)),
                    likes: default!)))
            .ToList();
        await context.PostsComments.AddRangeAsync(comments);
        await context.SaveChangesAsync();
    }
}