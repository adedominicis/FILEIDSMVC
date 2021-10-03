using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FILEIDSMVC;
using FILEIDSMVC.Controllers;

namespace FILEIDSMVC.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RegistrarArchivo()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.RegistrarArchivo() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void ListarArchivos()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.ListarArchivos() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
