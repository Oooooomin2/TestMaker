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
using TestMaker.Models.ViewModels;
using Xunit;

namespace DDDTest.Tests
{
    public class TestViewModelTest
    {
        [Fact]
        public void Access_TestIndex_db()
        {
            var viewModel = UserTestViewModelTestData.UserTestViewModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestIndex_db")
                .Options;
            using var _context = new TestMakerContext(options);
            foreach (var t in viewModel.Tests)
            {
                _context.Tests.Add(t);
            }
            _context.Users.Add(viewModel.User);
            _context.SaveChanges();
            var testInfoTest = new UserTestViewModel().ShowTestIndexInfo(1, _context);
            Assert.Equal(viewModel.Tests[0].TestId, testInfoTest.Tests[0].TestId);
            Assert.Equal(viewModel.Tests[0].Title, testInfoTest.Tests[0].Title);
            Assert.Equal(viewModel.Tests[0].CreatedTime, testInfoTest.Tests[0].CreatedTime);
            Assert.Equal(viewModel.Tests[0].UpdatedTime, testInfoTest.Tests[0].UpdatedTime);
            Assert.Equal(viewModel.Tests[0].UserId, testInfoTest.Tests[0].UserId);
            Assert.Equal(viewModel.Tests[0].Questions[0].QuestionText, testInfoTest.Tests[0].Questions[0].QuestionText);
            Assert.Equal(viewModel.Tests[0].Questions[0].TestId, testInfoTest.Tests[0].Questions[0].TestId);
            Assert.Equal(viewModel.Tests[0].Questions[0].Choices[0].ChoiceId, testInfoTest.Tests[0].Questions[0].Choices[0].ChoiceId);
            Assert.Equal(viewModel.Tests[0].Questions[0].Choices[0].ChoiceText, testInfoTest.Tests[0].Questions[0].Choices[0].ChoiceText);
            Assert.Equal(viewModel.Tests[0].Questions[0].Choices[0].IsAnswer, testInfoTest.Tests[0].Questions[0].Choices[0].IsAnswer);
            Assert.Equal(viewModel.Tests[0].Questions[0].Choices[0].IsUsersAnswerCheck, testInfoTest.Tests[0].Questions[0].Choices[0].IsUsersAnswerCheck);
            Assert.Equal(viewModel.Tests[0].Questions[0].Choices[0].IsUsersAnswerRadio, testInfoTest.Tests[0].Questions[0].Choices[0].IsUsersAnswerRadio);
            Assert.Equal(viewModel.Tests[0].Questions[0].Choices[0].QuestionId, testInfoTest.Tests[0].Questions[0].Choices[0].QuestionId);
            Assert.Equal(viewModel.Tests[0].Questions[0].Choices[0].ChoiceText, testInfoTest.Tests[0].Questions[0].Choices[0].ChoiceText);
            Assert.Equal(viewModel.User.LoginId, testInfoTest.User.LoginId);
            Assert.Equal(viewModel.User.UserName, testInfoTest.User.UserName);
            Assert.Equal(viewModel.User.Password, testInfoTest.User.Password);
            Assert.Equal(viewModel.User.Salt, testInfoTest.User.Salt);
            Assert.Equal(viewModel.User.SelfIntroduction, testInfoTest.User.SelfIntroduction);
            Assert.Equal(viewModel.User.Icon, testInfoTest.User.Icon);
        }

        [Fact]
        public void Access_TestIndex_check_viewData()
        {
            var viewModel = UserTestViewModelTestData.UserTestViewModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestIndex_check_viewData")
                .Options;
            using var _context = new TestMakerContext(options);
            foreach (var t in viewModel.Tests)
            {
                _context.Tests.Add(t);
            }
            _context.Users.Add(viewModel.User);
            _context.SaveChanges();
            var controller = new TestController(_context);
            var view = controller.Index(1) as ViewResult;
            Assert.Equal("Index", view.ViewData["Title"]);
            Assert.Equal("Index", view.ViewData["Action"]);
            Assert.Equal("Test", view.ViewData["Controller"]);
        }

        [Fact]
        public void Access_TestDetails_check_whenIdNull_BeNotFound()
        {
            var viewModel = UserTestViewModelTestData.UserTestViewModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestDetails_check_whenIdNull_BeNotFound")
                .Options;
            using var _context = new TestMakerContext(options);
            foreach (var t in viewModel.Tests)
            {
                _context.Tests.Add(t);
            }
            _context.Users.Add(viewModel.User);
            _context.SaveChanges();

            var controller = new TestController(_context);
            var actionResult = controller.Details(null);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public void Access_TestDetails_db()
        {
            var viewModel = TestViewModelTestData.TestViewModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestDetails_db")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Tests.Add(viewModel.Tests);
            foreach(var q in viewModel.Questions)
            {
                _context.Questions.Add(q);
            }
            foreach (var c in viewModel.Choices)
            {
                _context.Choices.Add(c);
            }
            _context.SaveChanges();
            var testInfoTest = new TestViewModel().ShowTestDetailsInfo(1, _context);
            Assert.Equal(viewModel.Tests.TestId, testInfoTest.Tests.TestId);
            Assert.Equal(viewModel.Tests.Title, testInfoTest.Tests.Title);
            Assert.Equal(viewModel.Tests.CreatedTime, testInfoTest.Tests.CreatedTime);
            Assert.Equal(viewModel.Tests.UpdatedTime, testInfoTest.Tests.UpdatedTime);
            Assert.Equal(viewModel.Tests.UserId, testInfoTest.Tests.UserId);
            Assert.Equal(viewModel.Tests.Questions[0].QuestionText, testInfoTest.Tests.Questions[0].QuestionText);
            Assert.Equal(viewModel.Tests.Questions[0].TestId, testInfoTest.Tests.Questions[0].TestId);
            Assert.Equal(viewModel.Tests.Questions[0].Choices[0].ChoiceId, testInfoTest.Tests.Questions[0].Choices[0].ChoiceId);
            Assert.Equal(viewModel.Tests.Questions[0].Choices[0].ChoiceText, testInfoTest.Tests.Questions[0].Choices[0].ChoiceText);
            Assert.Equal(viewModel.Tests.Questions[0].Choices[0].IsAnswer, testInfoTest.Tests.Questions[0].Choices[0].IsAnswer);
            Assert.Equal(viewModel.Tests.Questions[0].Choices[0].IsUsersAnswerCheck, testInfoTest.Tests.Questions[0].Choices[0].IsUsersAnswerCheck);
            Assert.Equal(viewModel.Tests.Questions[0].Choices[0].IsUsersAnswerRadio, testInfoTest.Tests.Questions[0].Choices[0].IsUsersAnswerRadio);
            Assert.Equal(viewModel.Tests.Questions[0].Choices[0].QuestionId, testInfoTest.Tests.Questions[0].Choices[0].QuestionId);
            Assert.Equal(viewModel.Tests.Questions[0].Choices[0].ChoiceText, testInfoTest.Tests.Questions[0].Choices[0].ChoiceText);
            Assert.Equal(viewModel.Choices[0].QuestionId, testInfoTest.Choices[0].QuestionId);
            Assert.Equal(viewModel.Choices[0].IsAnswer, testInfoTest.Choices[0].IsAnswer);
        }

        [Fact]
        public void Access_TestDetails_check_viewData()
        {
            var viewModel = TestViewModelTestData.TestViewModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestDetails_check_viewData")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Tests.Add(viewModel.Tests);
            foreach (var q in viewModel.Questions)
            {
                _context.Questions.Add(q);
            }
            foreach (var c in viewModel.Choices)
            {
                _context.Choices.Add(c);
            }
            _context.SaveChanges();
            var controller = new TestController(_context);
            var view = controller.Details(1) as ViewResult;
            Assert.Equal("Details", view.ViewData["Title"]);
            Assert.Equal("Details", view.ViewData["Action"]);
            Assert.Equal("Test", view.ViewData["Controller"]);
        }
    }
}
