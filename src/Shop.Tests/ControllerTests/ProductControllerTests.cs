using System;
using System.Web.Mvc;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Shop.Controllers;
using Shop.Domain;
using Shop.Domain.Model;
using Shop.Domain.Repositories;
using Shop.EntityFramework;
using Shop.Models;

namespace Shop.Tests.ControllerTests
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
            _productController = new ProductController(_productRepository, Substitute.For<IUnitOfWorkScope>());
        }
        
        [Test]
        public void Create_should_show_create_new_product_view()
        {
            var actionResult = _productController.Create();

            actionResult.Should().BeOfType<ViewResult>();
            actionResult.As<ViewResult>().ViewName.Should().Be("CreateOrEdit");
        }

        [Test]
        public void Edit_should_return_not_found_if_product_with_given_id_is_not_exists()
        {
            _productRepository.FindById(Arg.Any<int>()).Returns((Product) null);

            var actionResult = _productController.Edit(3);

            actionResult.Should().BeOfType<HttpNotFoundResult>();
        }

        [Test]
        public void Edit_should_show_edit_product_view_if_product_exists()
        {
            _productRepository.FindById(Arg.Any<int>())
                              .Returns(x => new Product {Id = x.Arg<int>()});

            var actionResult = _productController.Edit(1);

            actionResult.Should().BeOfType<ViewResult>();
            actionResult.As<ViewResult>().ViewName.Should().Be("CreateOrEdit");
        }

        [Test]
        public void Edit_should_map_founded_product_to_view_model()
        {
            const string category = "dsfhdjk";
            const string name = "the name";
            const string vendor = "samsung";
            const string description = "Описание ";
            _productRepository.FindById(Arg.Any<int>())
                              .Returns(x => new Product
                                  {
                                      Id          = x.Arg<int>(),
                                      Category    = category,
                                      Name        = name,
                                      Vendor      = vendor,
                                      Description = description
                                  });

            var actionResult = _productController.Edit(1);

            var model = (EditProduct) actionResult.As<ViewResult>().Model;
            model.Id.Should().Be(1);
            model.Category.Should().Be(category);
            model.Name.Should().Be(name);
            model.Vendor.Should().Be(vendor);
            model.Description.Should().Be(description);
        }

        [TestCase(0, 1, 1)]
        [TestCase(2, 2, 2)]
        [TestCase(2, 3, 3)]
        public void Save_should_show_edit_view_with_generated_id(int givenId,
                                                                 int genratedId,
                                                                 int returnedId)
        {
            _productRepository.Save(Arg.Do<Product>(x => x.Id = genratedId));

            var actionResult = _productController.Save(new EditProduct {Id = givenId});

            var result = (RedirectToRouteResult) actionResult;
            result.RouteValues["action"].Should().Be("Edit");
            result.RouteValues["id"].Should().Be(returnedId);
        }

        [TestCase(0)]
        [TestCase(1)]
        public void Save_should_show_error_message_when_save_product_failed(int id)
        {
            _productRepository.When(x => x.Save(Arg.Any<Product>()))
                              .Do(_ => { throw new Exception("Some error"); });

            var actionResult = _productController.Save(new EditProduct {Id = id});

            actionResult.Should().BeOfType<ViewResult>();
            actionResult.As<ViewResult>().ViewName.Should().Be("CreateOrEdit");
            _productController.ModelState.IsValid.Should().BeFalse();
        }

        [TestCase(0)]
        [TestCase(1)]
        public void Save_should_show_validation_error_message_when_model_is_invalid(int id)
        {
            _productController.ModelState.AddModelError("sdfdsfds", "some error");

            var actionResult = _productController.Save(new EditProduct {Id = id});

            actionResult.Should().BeOfType<ViewResult>();
            actionResult.As<ViewResult>().ViewName.Should().Be("CreateOrEdit");
        }

        [TestCase(0)]
        [TestCase(1)]
        public void Save_should_not_save_product_when_model_is_invalid(int id)
        {
            _productController.ModelState.AddModelError("sdfdsfds", "some error");

            _productController.Save(new EditProduct { Id = id });

            _productRepository.DidNotReceive().Save(Arg.Any<Product>());
        }
    }
}
