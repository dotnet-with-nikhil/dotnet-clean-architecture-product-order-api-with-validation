using dotnet_example_clean_arch_with_entity_framework.DOTs;
using dotnet_example_clean_arch_with_entity_framework.Models;

namespace dotnet_example_clean_arch_with_entity_framework.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Products>> GetAll();
        Task<Products> GetById(int id);
        Task<int> Add(Products product);
        Task Update(int id, Products dto);
        Task Patch(int id, UpdateProductDto product);
        Task Detele(int id);
        Task<bool> IsExists(int id);
    }
}
