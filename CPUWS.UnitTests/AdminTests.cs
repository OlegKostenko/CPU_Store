using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CPUWS.DAL.Abstruct;
using CPUWS.DAL.Entities;
using CPUWS.WebsUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CPUWS.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                 new Product {ProductId = 1, Name = "P1"},
                 new Product {ProductId = 2, Name = "P2"},
                 new Product {ProductId = 3, Name = "P3"},
                 }.AsQueryable());
            // Arrange - create a controller
            AdminController target = new AdminController(mock.Object);
            // Action
            Product[] result =
           ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray();
            // Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
        }

        [TestMethod]
        public void Can_Edit_Product()
        {
            // Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                 new Product {ProductId = 1, Name = "P1"},
                 new Product {ProductId = 2, Name = "P2"},
                 new Product {ProductId = 3, Name = "P3"},
                 }.AsQueryable());
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Act
            Product p1 = target.Edit(1).ViewData.Model as Product;
            Product p2 = target.Edit(2).ViewData.Model as Product;
            Product p3 = target.Edit(3).ViewData.Model as Product;
            // Assert
            Assert.AreEqual(1, p1.ProductId);
            Assert.AreEqual(2, p2.ProductId);
            Assert.AreEqual(3, p3.ProductId);
        }
        [TestMethod]
        public void Cannot_Edit_Nonexistent_Product()
        {
            // Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                 new Product {ProductId = 1, Name = "P1"},
                 new Product {ProductId = 2, Name = "P2"},
                 new Product {ProductId = 3, Name = "P3"},
                 }.AsQueryable());
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Act
            Product result = (Product)target.Edit(4).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            AdminController target = new AdminController(mock.Object);
            
            Product product = new Product { Name = "Test" };
            
            ActionResult result = target.Edit(product);
            
            mock.Verify(m => m.SaveProduct(product));
            
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            
            AdminController target = new AdminController(mock.Object);
            
            Product product = new Product { Name = "Test" };
            
            target.ModelState.AddModelError("error", "error");
            
            ActionResult result = target.Edit(product);
            
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Products()
        {
            // Arrange - create a Product
            Product prod = new Product { ProductId = 2, Name = "Test" };
            // Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                 new Product {ProductId = 1, Name = "P1"},
                 prod,
                 new Product {ProductId = 3, Name = "P3"},
                 }.AsQueryable());
            // Arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // Act - delete the product
            target.Delete(prod.ProductId);
            // Assert - ensure that the repository delete method was
            // called with the correct Product
            mock.Verify(m => m.DeleteProduct(prod.ProductId));
        }
    }
}
