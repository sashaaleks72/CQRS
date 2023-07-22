using Application.Common.Models;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Modules.Test.Extensions;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductsRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedData<TeapotEntity>> GetProductsAsync(int? page, int? limit, bool? isAsc, string? sort)
        {
            var totalQuantity = await _dbContext.Teapots.CountAsync();
            var query = _dbContext.Teapots.AsQueryable();

            if (page != null && limit != null) 
            {
                query = query.Skip((page.Value - 1) * limit.Value).Take(limit.Value);
            }

            query = query.OrderByParams(sort, isAsc);

            var receivedTeapots = await query.ToListAsync();

            return new PaginatedData<TeapotEntity>
            {
                Data = receivedTeapots,
                TotalCount = totalQuantity,
                Page = page != null ? page.Value : 0,
                Limit = limit != null ? limit.Value : 0
            };
        }

        public async Task<TeapotEntity?> GetProductByIdAsync(Guid id)
        {
            var receivedTeapot = await _dbContext.Teapots.SingleOrDefaultAsync(x => x.Id == id);
            return receivedTeapot;
        }

        public async Task<bool> AddProductAsync(TeapotEntity newTeapot)
        {
            await _dbContext.Teapots.AddAsync(newTeapot);
            var quantityOfAddedRows = await _dbContext.SaveChangesAsync();

            return quantityOfAddedRows > 0;
        }

        public async Task<bool> DeleteProductByIdAsync(Guid id)
        {
            var teapotToDelete = new TeapotEntity { Id = id };

            _dbContext.Entry(teapotToDelete).State = EntityState.Deleted;
            _dbContext.Teapots.Remove(teapotToDelete);

            var quantityOfDeletedRows = await _dbContext.SaveChangesAsync();

            return quantityOfDeletedRows > 0;
        }

        public async Task<bool> UpdateProductAsync(TeapotEntity teapotToUpdate)
        {
            _dbContext.Entry(teapotToUpdate).State = EntityState.Modified;
            _dbContext.Teapots.Update(teapotToUpdate);

            var quantityOfUpdatedRows = await _dbContext.SaveChangesAsync();

            return quantityOfUpdatedRows > 0;
        }
    }
}
