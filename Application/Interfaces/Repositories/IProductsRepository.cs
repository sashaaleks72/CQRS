using Application.Common.Models;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IProductsRepository
    {
        Task<PaginatedData<TeapotEntity>> GetProductsAsync(int? page, int? limit, bool? isAsc, string? sort);
        Task<TeapotEntity?> GetProductByIdAsync(Guid id);
        Task<bool> AddProductAsync(TeapotEntity newTeapot);
        Task<bool> DeleteProductByIdAsync(Guid id);
        Task<bool> UpdateProductAsync(TeapotEntity teapotToUpdate);
    }
}
