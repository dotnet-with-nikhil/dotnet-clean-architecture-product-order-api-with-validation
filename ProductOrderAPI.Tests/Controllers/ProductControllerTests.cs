using dotnet_example_clean_arch_with_entity_framework.Controllers;
using dotnet_example_clean_arch_with_entity_framework.DOTs;
using dotnet_example_clean_arch_with_entity_framework.Models;
using dotnet_example_clean_arch_with_entity_framework.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductOrderAPI.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _serviceMock;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _serviceMock = new Mock<IProductService>();
            _controller = new ProductController(_serviceMock.Object);
        }

        //GET
        [Fact]
        public async Task Get_Should_Return_OK()
        {
            _serviceMock.Setup(x => x.GetAll())
                        .ReturnsAsync(new List<Products>());

            var result = await _controller.Get();
            result.Should().BeOfType<OkObjectResult>();

        }

        //POST
        [Fact]
        public async Task POST_Should_Return_Created()
        {
            var dto = new ProductDto { Name = "Laptop-HP", Price = 80000 };

            var result = await _controller.Post(dto);
            result.Should().BeOfType<CreatedAtActionResult>();
        }

        //PUT
        [Fact]
        public async Task Put_Should_Return_NoContent()
        {
            var dto = new ProductDto { Name = "Laptop-HP", Price = 80000 };

            var result = await _controller.Put(1, dto);
            result.Should().BeOfType<NoContentResult>();
        }

        //PATCH
        [Fact]
        public async Task Patch_Should_Return_Ok()
        {
            var dto = new UpdateProductDto { Name = "Laptop-HP-Updated" };

            var result = await _controller.Patch(1, dto);
            result.Should().BeOfType<OkObjectResult>();
        }

        //DELETE
        [Fact]
        public async Task Delete_Should_Return_NoContent()
        {
            var result = await _controller.Delete(1);
            result.Should().BeOfType<OkObjectResult>();
        }

        //HEAD
        [Fact]
        public async Task Head_Should_Return_Ok_When_Exists()
        {
            _serviceMock.Setup(x => x.IsExists(1)).ReturnsAsync(true);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            var result = await _controller.Head(1);
            result.Should().BeOfType<OkResult>();
        }

        //HEAD
        [Fact]
        public async Task Head_Should_Return_NotFound_When_Not_Exists()
        {
            _serviceMock.Setup(x => x.IsExists(1)).ReturnsAsync(false);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            var result = await _controller.Head(1);
            result.Should().BeOfType<NotFoundResult>();
        }


        //OPTIONS
        [Fact]
        public async Task Option_Should_Return_Ok()
        {
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            var result = _controller.Options();
            result.Should().BeOfType<OkResult>();
        }
    }
}
