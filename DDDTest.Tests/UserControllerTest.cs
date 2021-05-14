using DDDTest.Tests.ViewModelData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Controllers;
using TestMaker.Data;
using TestMaker.Models;
using TestMaker.Models.Interface;
using TestMaker.Models.Repository;
using TestMaker.Models.ViewModels;
using Xunit;

namespace DDDTest.Tests
{
    public class UserControllerTest
    {
        [Fact]
        public void Access_UserIndex_db()
        {
            var model = UserModelTestData.UserModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_UserIndex_db")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Users.Add(model);
            _context.SaveChanges();
            Assert.Equal(model.LoginId, _context.Users.FirstOrDefault().LoginId);
            Assert.Equal(model.UserName, _context.Users.FirstOrDefault().UserName);
        }

        [Fact]
        public async Task Access_UserIndex_check_viewData()
        {
            var model = UserModelTestData.UserModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_UserIndex_check_viewData")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Users.Add(model);
            _context.SaveChanges();
            IUserRepository userRepository = new UserRepository(_context);
            var controller = new UserController(userRepository);
            var view = await controller.Index() as ViewResult;
            Assert.Equal("Index", view.ViewData["Title"]);
            Assert.Equal("Index", view.ViewData["Action"]);
            Assert.Equal("User", view.ViewData["Controller"]);
        }

        [Fact]
        public async Task Access_UserDetails_check_whenIdNull_BeNotFound()
        {
            var model = UserModelTestData.UserModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_UserDetails_check_whenIdNull_BeNotFound")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Users.Add(model);
            _context.SaveChanges();
            IUserRepository userRepository = new UserRepository(_context);
            var controller = new UserController(userRepository);
            var actionResult = await controller.Details(null);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async Task Access_UserDetails_db()
        {
            var model = UserModelTestData.UserModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_UserDetails_db")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Users.Add(model);
            _context.SaveChanges();
            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == 1);
            Assert.Equal(model.UserId, user.UserId);
            Assert.Equal(model.LoginId, user.LoginId);
            Assert.Equal(model.UserName, user.UserName);
            Assert.Equal(model.Icon, user.Icon);
            Assert.Equal(model.SelfIntroduction, user.SelfIntroduction);
        }

        [Fact]
        public async Task Access_UserDetails_check_viewData()
        {
            var model = UserModelTestData.UserModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_UserDetails_check_viewData")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Users.Add(model);
            _context.SaveChanges();
            IUserRepository userRepository = new UserRepository(_context);
            var controller = new UserController(userRepository);
            var view = await controller.Details(1) as ViewResult;
            Assert.Equal("User's information", view.ViewData["Title"]);
            Assert.Equal("Details", view.ViewData["Action"]);
            Assert.Equal("User", view.ViewData["Controller"]);
        }

        [Fact]
        public async Task Access_UserCreate_Check_DuplicateUser()
        {
            var model = UserModelTestData.UserModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_UserCreate_Check_DuplicateUser")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Users.Add(model);
            _context.SaveChanges();
            IUserRepository userRepository = new UserRepository(_context);
            var controller = new UserController(userRepository);
            var view = await controller.Create(model) as ViewResult;
            Assert.False(view.ViewData.ModelState.IsValid);
            Assert.Equal("The LoginId is unregistered", view.ViewData.ModelState["LoginId"].Errors[0].ErrorMessage);
        }

        [Fact]
        public async Task Access_UserCreated_Check_RedirectToActionResult()
        {
            var model = UserModelTestData.UserModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_UserCreated_Check_RedirectToActionResult")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Users.Add(model);
            _context.SaveChanges();
            IUserRepository userRepository = new UserRepository(_context);
            var controller = new UserController(userRepository);
            var actionResult = await controller.Create(UserModelTestData.UserCreateData()) as RedirectToActionResult;
            Assert.Equal("Login", actionResult.ActionName);
            Assert.Equal("Account", actionResult.ControllerName);
        }

        [Fact]
        public void Access_UserCreate_check_viewData()
        {
            var controller = new UserController(null);
            var view = controller.Create() as ViewResult;
            Assert.Equal("Create", view.ViewData["Title"]);
            Assert.Equal("Create", view.ViewData["Action"]);
            Assert.Equal("User", view.ViewData["Controller"]);
        }

        [Fact]
        public void Access_UserEdit_check_whenIdNull_BeNotFound()
        {
            var model = UserModelTestData.UserModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_UserEdit_check_whenIdNull_BeNotFound")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Users.Add(model);
            _context.SaveChanges();
            IUserRepository userRepository = new UserRepository(_context);
            var controller = new UserController(userRepository);
            var actionResult = controller.Edit(null);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public void Access_UserEdit_check_viewData()
        {
            var model = UserModelTestData.UserModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_UserEdit_check_viewData")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Users.Add(model);
            _context.SaveChanges();
            IUserRepository userRepository = new UserRepository(_context);
            var controller = new UserController(userRepository);
            var view = controller.Edit(1) as ViewResult;
            Assert.Equal("Edit", view.ViewData["Title"]);
            Assert.Equal("Edit", view.ViewData["Action"]);
            Assert.Equal("User", view.ViewData["Controller"]);
        }

        [Fact]
        public void Access_UserEdit_db()
        {
            var model = UserModelTestData.UserModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_UserEdit_db")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Users.Add(model);
            _context.SaveChanges();
            var user = _context.Users.Find(1);
            Assert.Equal(model.UserId, user.UserId);
            Assert.Equal(model.LoginId, user.LoginId);
            Assert.Equal(model.UserName, user.UserName);
            Assert.Equal(model.Password, user.Password);
            Assert.Equal(model.Salt, user.Salt);
            Assert.Equal(model.SelfIntroduction, user.SelfIntroduction);
            Assert.Equal(model.Icon, user.Icon);
        }

        [Fact]
        public async Task Access_UserEdited_Check_WhenIdDifferentPostedData_BeNotFound()
        {
            var model = UserModelTestData.UserModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_UserEdited_Check_WhenIdDifferentPostedData_BeNotFound")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Users.Add(model);
            _context.SaveChanges();
            IUserRepository userRepository = new UserRepository(_context);
            var controller = new UserController(userRepository);
            var actionResult = await controller.Edit(999999, model);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async Task Access_UserDelete_check_whenIdNull_BeNotFound()
        {
            var model = UserModelTestData.UserModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_UserDelete_check_whenIdNull_BeNotFound")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Users.Add(model);
            _context.SaveChanges();
            IUserRepository userRepository = new UserRepository(_context);
            var controller = new UserController(userRepository);
            var actionResult = await controller.Details(null);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async Task Access_UserDelete_Check_viewData()
        {
            var model = UserModelTestData.UserModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_UserDelete_Check_viewData")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Users.Add(model);
            _context.SaveChanges();
            IUserRepository userRepository = new UserRepository(_context);
            var controller = new UserController(userRepository);
            var view = await controller.Delete(1) as ViewResult;
            Assert.Equal("Delete", view.ViewData["Title"]);
            Assert.Equal("Delete", view.ViewData["Action"]);
            Assert.Equal("User", view.ViewData["Controller"]);
        }

        [Fact]
        public async Task Access_UserDeleteConfirmed_check_RedirectToActionResult()
        {
            var model = UserModelTestData.UserModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_UserDeleteConfirmed_check_RedirectToActionResult")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Users.Add(model);
            _context.SaveChanges();
            IUserRepository userRepository = new UserRepository(_context);
            var controller = new UserController(userRepository);
            var actionResult = await controller.DeleteConfirmed(1) as RedirectToActionResult;
            Assert.Equal("Index", actionResult.ActionName);
            Assert.Equal("Home", actionResult.ControllerName);
        }

        [Fact]
        public async Task Access_UserDeleteConfirmed_Check_db()
        {
            var model = UserModelTestData.UserModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_UserDeleteConfirmed_Check_db")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Users.Add(model);
            _context.SaveChanges();
            Assert.Equal(1, _context.Users.Count());
            IUserRepository userRepository = new UserRepository(_context);
            var controller = new UserController(userRepository);
            await controller.DeleteConfirmed(1);
            Assert.Equal(0, _context.Users.Count());
        }

        [Fact]
        public async Task Access_UserChangePassword_db()
        {
            var model = UserModelTestData.UserModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_UserChangePassword_db")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Users.Add(model);
            _context.SaveChanges();
            var user = await _context.Users.FindAsync(1);
            Assert.Equal(model.LoginId, user.LoginId);
            Assert.Equal(model.Password, user.Password);
        }

        [Fact]
        public async Task Access_UserChangePassword_Check_ModelStateIsValid()
        {
            var model = UserModelTestData.UserModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_UserChangePassword_Check_ModelStateIsValid")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Users.Add(model);
            _context.SaveChanges();
            IUserRepository userRepository = new UserRepository(_context);
            var controller = new UserController(userRepository);
            controller.ModelState.AddModelError("error", "some error");
            var view = await controller.ChangePassword(model) as ViewResult;
            Assert.Equal("Change password", view.ViewData["Title"]);
            Assert.Equal("ChangePassword", view.ViewData["Action"]);
            Assert.Equal("User", view.ViewData["Controller"]);
        }
    }
}
