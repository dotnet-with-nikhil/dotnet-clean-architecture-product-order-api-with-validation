using dotnet_example_clean_arch_with_entity_framework.DOTs;
using dotnet_example_clean_arch_with_entity_framework.Models;
using dotnet_example_clean_arch_with_entity_framework.Repositories.Interfaces;
using dotnet_example_clean_arch_with_entity_framework.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductOrderAPI.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _repoMock;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _repoMock = new Mock<IProductRepository>();
            _service = new ProductService(_repoMock.Object);
        }


        //TestCase for Get
        [Fact]
        public async Task GetAll_Should_Return_List_Of_Products()
        {
            var products = new List<Products>
            {
                new Products {Id =1, Name = "Laptop", Price = 50000 }
            };
            _repoMock.Setup(x => x.GetAll()).ReturnsAsync(products);
            var result = await _service.GetAll();
            result.Should().HaveCount(1);
        }

        //TestCase for Add
        [Fact]
        public async Task Add_Should_Call_Repository_Add()
        {
            var dto = new Products { Name = "Laptop", Price = 50000 };
            await _service.Add(dto);
            _repoMock.Verify(x => x.Add(It.IsAny<Products>()), Times.Once);
        }

        //TestCase for Update
        [Fact]
        public async Task Update_Should_Modify_Product()
        {
            var product = new Products { Id = 1, Name = "Laptop", Price = 100000 };
            _repoMock.Setup(x => x.GetById(1)).ReturnsAsync(product);

            var dto = new Products { Name = "Laptop-HP", Price = 50000 };
            await _service.Update(1, dto);

            product.Name.Should().Be("Laptop-HP");
            product.Price.Should().Be(50000);
        }

        //TestCase for Patch
        [Fact]
        public async Task Patch_Should_Update_Only_Provided_Fields()
        {
            var product = new Products { Id = 1, Name = "Laptop", Price = 50000 };
            _repoMock.Setup(x => x.GetById(1)).ReturnsAsync(product);
            var dto = new UpdateProductDto { Name = "Laptop-HP-UpdatedName" };
            await _service.Patch(1, dto);
            product.Name.Should().Be("Laptop-HP-UpdatedName");
            product.Price.Should().Be(50000); //unchanged
        }

        //TestCase for Delete
        [Fact]
        public async Task Delete_Should_Call_Repository_Delete()
        {
            var product = new Products { Id = 1, Name = "Laptop", Price = 100000 };
            _repoMock.Setup(x => x.GetById(1)).ReturnsAsync(product);

            await _service.Detele(1);
            _repoMock.Verify(x => x.Delete(product), Times.Once);
        }
    }
}
