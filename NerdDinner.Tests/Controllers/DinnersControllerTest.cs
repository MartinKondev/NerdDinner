using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NerdDinner.Controllers;
using NerdDinner.Models;
using NerdDinner.Tests.Fakes;

namespace NerdDinner.Tests.Controllers
{
    [TestClass]
    public class DinnersControllerTest
    {
        List<Dinner> CreateDinnersList()
        {
            var dinnersList = new List<Dinner>();
            for (int i = 0; i < 100; i++)
            {
                var dinner = new Dinner()
                    {
                        DinnerId = i,
                        Title = "Sample Dinner",
                        HostedBy = "SomeUser",
                        Address = "Some Address",
                        Country = "USA",
                        ContactPhone = "425-555-1212",
                        Description = "Some description",
                        EventDate = DateTime.Now.AddDays(i),
                        Lattitude = 99,
                        Longitude = -99
                    };
                dinnersList.Add(dinner);
            }
            return dinnersList;
        }

        DinnersController CreateDinnersController()
        {
            var rep = new FakeDinnerRepository(CreateDinnersList());
            return new DinnersController(rep);
        }

        DinnersController CreateDinnersControllerAs(string userName)
        {
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(userName);
            mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            var controller = CreateDinnersController();
            controller.ControllerContext = mock.Object;
            return controller;
        }

        [TestMethod]
        public void Details_Action_Should_Return_View_For_Dinner()
        {
            //Arrange
            var controller = CreateDinnersController();

            //Act
            var result = controller.Details(1);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Details_Action_Should_Return_Not_Found_View_For_Bogus_Dinner()
        {
            //Arrange
            var controller = CreateDinnersController();

            //Act
            var result = controller.Details(999) as ViewResult;

            //Assert
            Assert.AreEqual("NotFound", result.ViewName);
        }

        [TestMethod]
        public void Edit_Method_Should_Return_View_For_Valid_Dinner()
        {
            //Arrange
            var controller = CreateDinnersControllerAs("Martin");

            //Act
            var result = controller.Edit(1) as ViewResult;

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
