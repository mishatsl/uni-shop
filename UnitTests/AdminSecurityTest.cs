using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebUI.Controllers;
using WebUI.Infrastructure.Abstract;
using WebUI.Models;

namespace UnitTests
{
    [TestClass]
    public class AdminSecurityTest
    {
        [TestMethod]
        public void Can_Login_With_Valid_Credentials()
        {
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);

            LoginViewModel model = new LoginViewModel { UserName = "admin", Password = "secret" };

            AccountController_Past_ver target = new AccountController_Past_ver(mock.Object);

            ActionResult result = target.Login(model,"/myUrl");

            Assert.IsInstanceOfType(result,typeof(RedirectResult));
            Assert.AreEqual(((RedirectResult)result).Url, "/myUrl");
        }

        [TestMethod]
        public void Can_Login_With_InValid_Credentials()
        {
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);

            LoginViewModel model = new LoginViewModel { UserName = "badAdmin", Password = "BadSecret" };

            AccountController_Past_ver target = new AccountController_Past_ver(mock.Object);

            ActionResult result = target.Login(model, "/myUrl");

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }   
}
