using MediatR;

namespace Rossgram.Application.Comments.Queries.GetComments;

public class GetCommentsQuery
    : IRequest<GetCommentsResponseDTO>
{
    public long PostId { get; set; }
    public int? Offset { get; set; }
    public int? Limit { get; set; }
}