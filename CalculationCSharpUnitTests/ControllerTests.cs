﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CalculationCSharp;
using CalculationCSharp.Controllers;

namespace CalculationCSharpUnitTests
{
    [TestClass]
    public class ControllerTests
    {
        [TestMethod]
        public void Index()
        {
            //Arrange
            HomeController controller = new HomeController();


            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
