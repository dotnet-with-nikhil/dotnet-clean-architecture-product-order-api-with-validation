using dotnet_example_clean_arch_with_entity_framework.Models;

namespace dotnet_example_clean_arch_with_entity_framework.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Products>> GetAll();
        Task<Products> GetById(int id);
        Task<int> Add(Products product);
        Task Update(Products product);
        Task Delete(Products product);
        Task<bool> Exists(int id);
    }
}
