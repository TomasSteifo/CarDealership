using Application.CarIssues.Commands;
using Application.CarIssues.Dtos;
using Application.Interfaces.CarIssueInterface;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CarIssues.Handlers
{
    public class CreateCarIssueCommandHandler : IRequestHandler<CreateCarIssueCommand, CarIssueDto>
    {
       private readonly IMapper _mapper;
       private readonly ICarIssueRepository _repository;
        public CreateCarIssueCommandHandler(IMapper mapper, ICarIssueRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CarIssueDto> Handle(CreateCarIssueCommand request, CancellationToken cancellationToken)
        {
            var carIssue = new CarIssue
            {
                Description = request.Description,
                CarId = request.CarId,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.CreateAsync(carIssue, cancellationToken);

            return _mapper.Map<CarIssueDto>(carIssue);
        }
        
    }
}