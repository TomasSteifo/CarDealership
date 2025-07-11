using CarDealership.Application.Interfaces.CarIssueInterface;
using CarDealership.Application.Interfaces.IAppDbContext;
using CarDealership.Infrastructure.Persistence;
using CarDealership.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealership.Infrastructure.Repositories
{
    public class CarIssueRepository : ICarIssueRepository
    {
        private readonly CarDealershipDbContext _context;

        public CarIssueRepository(CarDealershipDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<CarIssue>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.CarIssues
                .Include(ci => ci.Car)
                .ToListAsync(cancellationToken);
        }

        public async Task<CarIssue?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.CarIssues
                .FirstOrDefaultAsync(c => c.CarIssueId == id, cancellationToken);
        }
        public async Task CreateAsync(CarIssue carIssue, CancellationToken cancellationToken)
        {
            await _context.CarIssues.AddAsync(carIssue, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(CarIssue carIssue, CancellationToken cancellationToken)
        {
            _context.CarIssues.Update(carIssue);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(CarIssue carIssue, CancellationToken cancellationToken)
        {
            _context.CarIssues.Remove(carIssue);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}