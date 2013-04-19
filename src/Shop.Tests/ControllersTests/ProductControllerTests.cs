using System;
using System.Web.Mvc;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Shop.Controllers;
using Shop.Models;
using Shop.Services.Domain;
using Shop.Services.Domain.Dto;

namespace Shop.Tests.ControllersTests
{
    [TestFixture]
    public class ProductControllerTests
    {
        private ProductController _productController;
        private IProductRepository _productRepository;

        [SetUp]
        public void SetUp()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _productController = new ProductController(_productRepository);
        }

        [Test]
        public void Create_should_show_create_product_view()
        {
            var actionResult = _productController.Create();

            actionResult.Should().BeOfType<ViewResult>();
            actionResult.As<ViewResult>().ViewName.Should().Be("CreateOrEdit");
        }

        [Test]
        public void Edit_should_show_product_with_given_id()
        {
            _productRepository.FindById(1).Returns(new Product());

            var actionResult = _productController.Edit(1);

            actionResult.Should().BeOfType<ViewResult>();
            actionResult.As<ViewResult>().ViewName.Should().Be("CreateOrEdit");
            actionResult.As<ViewResult>().Model.Should().BeOfType<EditProduct>();
        }

        [Test]
        public void Edit_should_return_not_found_when_product_with_given_id_is_not_exists()
        {
            _productRepository.FindById(1).Returns((Product) null);

            var actionResult = _productController.Edit(1);

            actionResult.Should().BeOfType<HttpNotFoundResult>();
        }

        [Test]
        public void Edit_should_show_edit_product_view()
        {
            _productRepository.FindById(1).Returns(new Product());

            var actionResult = _productController.Edit(1);

            actionResult.Should().BeOfType<ViewResult>();
            actionResult.As<ViewResult>().ViewName.Should().Be("CreateOrEdit");
        }

        [TestCase(0)]
        [TestCase(1)]
        public void Save_should_save_product(int id)
        {
            _productController.Save(new EditProduct {Id = id});

            _productRepository.Received(1).Save(Arg.Is<Product>(x => x.Id == id));
        }

        [TestCase(0, 1)]
        [TestCase(1, 1)]
        public void Save_should_redirect_to_edit_when_save_exists_product(int id, int returnedId)
        {
            _productRepository.Save(Arg.Any<Product>()).Returns(returnedId);

            var actionResult = _productController.Save(new EditProduct { Id = id });

            actionResult.Should().BeOfType<RedirectToRouteResult>();
            var routeResult = actionResult.As<RedirectToRouteResult>();
            routeResult.RouteValues["id"].Should().Be(returnedId);
            routeResult.RouteValues["action"].Should().Be("Edit");
            routeResult.RouteValues["controller"].Should().Be("Product");
        }

        [TestCase(0)]
        [TestCase(1)]
        public void Save_should_redirect_to_view_when_model_is_invalid(int id)
        {
            _productController.ModelState.AddModelError("", "some error");
            var model = new EditProduct {Id = id};

            var actionResult = _productController.Save(model);

            actionResult.Should().BeOfType<ViewResult>();
            actionResult.As<ViewResult>().Model.Should().BeSameAs(model);
            actionResult.As<ViewResult>().ViewName.Should().Be("CreateOrEdit");
        }

        [TestCase(0)]
        [TestCase(1)]
        public void Save_should_set_notice(int id)
        {
            _productController.Save(new EditProduct {Id = id});

            _productController.TempData.Should().ContainKey("notice");
            _productController.TempData["notice"].Should().NotBeNull();
        }

        [TestCase(0)]
        [TestCase(1)]
        public void Save_should_show_error_message_when_product_saving_failed(int id)
        {
            _productRepository.WhenForAnyArgs(x => x.Save(null))
                           .Do(x => { throw new Exception("Some error"); });
            var model = new EditProduct {Id = id};

            var actionResult = _productController.Save(model);

            _productController.ModelState.IsValid.Should().BeFalse();
            actionResult.Should().BeOfType<ViewResult>();
            actionResult.As<ViewResult>().Model.Should().BeSameAs(model);
            actionResult.As<ViewResult>().ViewName.Should().Be("CreateOrEdit");
        }
    }
}