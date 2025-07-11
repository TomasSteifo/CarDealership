using CarDealership.Application.CarIssues.Dtos;
using CarDealership.Application.CarIssues.Queries;
using CarDealership.Application.Interfaces.IAppDbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace CarDealership.Application.CarIssues.Handlers
{
    public class GetCarIssueByIdQueryHandler : IRequestHandler<GetCarIssueByIdQuery, CarIssueDto>
    {
        private readonly IAppDbContext _context;

        public GetCarIssueByIdQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<CarIssueDto> Handle(GetCarIssueByIdQuery request, CancellationToken cancellationToken)
        {
            
            var issue = await _context.CarIssues
                .Where(i => i.CarIssueId == request.CarIssueId)
                .Select(issue => new CarIssueDto
                {
                    CarIssueId = issue.CarIssueId,
                    Description = issue.Description,
                    OptionalComment = issue.OptionalComment,
                    AIAnalysis = issue.AIAnalysis,
                    CreatedAt = issue.CreatedAt,
                    CarId = issue.CarId
                })
                .FirstOrDefaultAsync(cancellationToken);

            return issue;
        }
    }
}