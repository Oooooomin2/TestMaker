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
    public class TestControllerTest
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
            ITestRepository testRepository = new TestRepository(_context);
            var testInfoTest = testRepository.GetAll(1);
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
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
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
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
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
            ITestRepository testRepository = new TestRepository(_context);
            var testInfoTest = testRepository.GetContent(1);
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
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            var view = controller.Details(1) as ViewResult;
            Assert.Equal("Details", view.ViewData["Title"]);
            Assert.Equal("Details", view.ViewData["Action"]);
            Assert.Equal("Test", view.ViewData["Controller"]);
        }

        [Fact]
        public void Access_TestSettings_check_viewData()
        {
            var controller = new TestController(null);
            var view = controller.SetSettings() as ViewResult;
            Assert.Equal("Settings", view.ViewData["Title"]);
            Assert.Equal("Settings", view.ViewData["Action"]);
            Assert.Equal("Test", view.ViewData["Controller"]);
        }

        [Fact]
        public void Access_TestCreate_check_viewData()
        {
            var controller = new TestController(null);
            var view = controller.Create("CreateMethodTest", "1") as ViewResult;
            Assert.Equal("CreateMethodTest", view.ViewData["Title"]);
            Assert.Equal("1", view.ViewData["Number"]);
            Assert.Equal("Create", view.ViewData["Action"]);
            Assert.Equal("Test", view.ViewData["Controller"]);
        }

        [Fact]
        public void Access_TestCreated_check_RedirectToActionResult()
        {
            var viewModel = TestViewModelTestData.TestViewModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestCreated_check_viewData")
                .Options;
            using var _context = new TestMakerContext(options);
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            var actionResult = controller.Create(viewModel) as RedirectToActionResult;
            Assert.Equal("Index", actionResult.ActionName);
            Assert.Equal("Home", actionResult.ControllerName);
        }

        [Fact]
        public void Access_TestCreated_Check_db()
        {
            var viewModel = TestViewModelTestData.TestViewModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestCreated_Check_db")
                .Options;
            using var _context = new TestMakerContext(options);
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            var actionResult = controller.Create(viewModel);
            Assert.Equal(viewModel.Tests.TestId, _context.Tests.FirstOrDefault().TestId);
            Assert.Equal(viewModel.Tests.Title, _context.Tests.FirstOrDefault().Title);
            Assert.Equal(viewModel.Tests.CreatedTime, _context.Tests.FirstOrDefault().CreatedTime);
            Assert.Equal(viewModel.Tests.UpdatedTime, _context.Tests.FirstOrDefault().UpdatedTime);
            Assert.Equal(viewModel.Tests.UserId, _context.Tests.FirstOrDefault().UserId);
            Assert.Equal(viewModel.Tests.Questions[0].QuestionText, _context.Tests.FirstOrDefault().Questions[0].QuestionText);
            Assert.Equal(viewModel.Tests.Questions[0].TestId, _context.Tests.FirstOrDefault().Questions[0].TestId);
            Assert.Equal(viewModel.Tests.Questions[0].Choices[0].ChoiceId, _context.Tests.FirstOrDefault().Questions[0].Choices[0].ChoiceId);
            Assert.Equal(viewModel.Tests.Questions[0].Choices[0].ChoiceText, _context.Tests.FirstOrDefault().Questions[0].Choices[0].ChoiceText);
            Assert.Equal(viewModel.Tests.Questions[0].Choices[0].IsAnswer, _context.Tests.FirstOrDefault().Questions[0].Choices[0].IsAnswer);
            Assert.Equal(viewModel.Tests.Questions[0].Choices[0].IsUsersAnswerCheck, _context.Tests.FirstOrDefault().Questions[0].Choices[0].IsUsersAnswerCheck);
            Assert.Equal(viewModel.Tests.Questions[0].Choices[0].IsUsersAnswerRadio, _context.Tests.FirstOrDefault().Questions[0].Choices[0].IsUsersAnswerRadio);
            Assert.Equal(viewModel.Tests.Questions[0].Choices[0].QuestionId, _context.Tests.FirstOrDefault().Questions[0].Choices[0].QuestionId);
            Assert.Equal(viewModel.Tests.Questions[0].Choices[0].ChoiceText, _context.Tests.FirstOrDefault().Questions[0].Choices[0].ChoiceText);
            Assert.Equal(viewModel.Choices[0].QuestionId, _context.Choices.FirstOrDefault().QuestionId);
            Assert.Equal(viewModel.Choices[0].IsAnswer, _context.Choices.FirstOrDefault().IsAnswer);
        }

        [Fact]
        public void Access_TestCreated_Check_GivenInvalidModel()
        {
            var viewModel = TestViewModelTestData.TestViewModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestCreated_Check_GivenInvalidModel")
                .Options;
            using var _context = new TestMakerContext(options);
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            controller.ModelState.AddModelError("error", "some error");
            var view = controller.Create(viewModel) as ViewResult;
            Assert.Equal(viewModel.Tests.Title, view.ViewData["Title"]);
            Assert.Equal(viewModel.Tests.Number, view.ViewData["Number"]);
        }

        [Fact]
        public void Access_TestEdit_Check_viewData()
        {
            var viewModel = TestViewModelTestData.TestViewModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestEdit_Check_viewData")
                .Options;
            using var _context = new TestMakerContext(options);
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            var view = controller.Edit(1) as ViewResult;
            Assert.Equal("Edit", view.ViewData["Title"]);
            Assert.Equal("Edit", view.ViewData["Action"]);
            Assert.Equal("Test", view.ViewData["Controller"]);
        }

        [Fact]
        public void Access_TestEdited_Check_WhenIdDifferentPostedData_BeNotFound()
        {
            var viewModel = TestViewModelTestData.TestViewModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestEdited_Check_WhenIdDifferentPostedData_BeNotFound")
                .Options;
            using var _context = new TestMakerContext(options);
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            var actionResult = controller.Edit(999999, viewModel);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public void Access_TestEdited_Check_GivenInvalidModel()
        {
            var viewModel = TestViewModelTestData.TestViewModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestEdited_Check_GivenInvalidModel")
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
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            controller.ModelState.AddModelError("error", "some error");
            var view = controller.Edit(1, viewModel) as ViewResult;
            Assert.Null(view.ViewName);
        }

        [Fact]
        public void Access_TestDelete_check_whenIdNull_BeNotFound()
        {
            var viewModel = UserTestViewModelTestData.UserTestViewModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestDelete_check_whenIdNull_BeNotFound")
                .Options;
            using var _context = new TestMakerContext(options);
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            var actionResult = controller.Details(null);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public void Access_TestDelete_Check_viewData()
        {
            var viewModel = TestViewModelTestData.TestViewModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestDelete_Check_viewData")
                .Options;
            using var _context = new TestMakerContext(options);
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            var view = controller.Delete(1) as ViewResult;
            Assert.Equal("Delete", view.ViewData["Title"]);
            Assert.Equal("Delete", view.ViewData["Action"]);
            Assert.Equal("Test", view.ViewData["Controller"]);
        }

        [Fact]
        public async Task Access_TestDeleteConfirmed_check_RedirectToActionResult()
        {
            var viewModel = TestViewModelTestData.TestViewModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestDeleteConfirmed_check_RedirectToActionResult")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Tests.Add(viewModel.Tests);
            _context.SaveChanges();
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            var actionResult = await controller.DeleteConfirmed(1) as RedirectToActionResult;
            Assert.Equal("Index", actionResult.ActionName);
            Assert.Equal("Home", actionResult.ControllerName);
        }

        [Fact]
        public async Task Access_TestDeleteConfirmed_Check_db()
        {
            var viewModel = TestViewModelTestData.TestViewModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestDeleteConfirmed_Check_db")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Tests.Add(viewModel.Tests);
            _context.SaveChanges();
            Assert.Equal(1, _context.Tests.Count());
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            await controller.DeleteConfirmed(1);
            Assert.Equal(0, _context.Tests.Count());
        }

        [Fact]
        public void Access_TestScore_Check_MultiCorrectResult0()
        {
            var viewModel = TestViewModelTestData.TestViewModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestScore_Check_MultiCorrectResult0")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Tests.Add(viewModel.Tests);
            _context.SaveChanges();
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            var view = controller.Score(1, viewModel) as ViewResult;
            Assert.Equal(0, view.ViewData["CorrectCount"]);
            Assert.Equal("Score", view.ViewData["Score"]);
            Assert.Equal("Details", view.ViewName);
        }

        [Fact]
        public void Access_TestScore_Check_MultiCorrectResult1()
        {
            var viewModel = TestViewModelTestData.TestViewModelDataForScoreTestMultiCorrectCheck();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestScore_Check_MultiCorrectResult1")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Tests.Add(viewModel.Tests);
            _context.SaveChanges();
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            var view = controller.Score(1, viewModel) as ViewResult;
            Assert.Equal(1, view.ViewData["CorrectCount"]);
            Assert.Equal("Score", view.ViewData["Score"]);
            Assert.Equal("Details", view.ViewName);
        }

        [Fact]
        public void Access_TestScore_Check_SingleCorrectResult0()
        {
            var viewModel = TestViewModelTestData.TestViewModelDataForScoreTestSingleFailCheck();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestScore_Check_SingleCorrectResult0")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Tests.Add(viewModel.Tests);
            _context.SaveChanges();
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            var view = controller.Score(1, viewModel) as ViewResult;
            Assert.Equal(0, view.ViewData["CorrectCount"]);
            Assert.Equal("Score", view.ViewData["Score"]);
            Assert.Equal("Details", view.ViewName);
        }

        [Fact]
        public void Access_TestScore_Check_SingleCorrectResult1()
        {
            var viewModel = TestViewModelTestData.TestViewModelDataForScoreTestSingleCorrectCheck();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestScore_Check_SingleCorrectResult1")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Tests.Add(viewModel.Tests);
            _context.SaveChanges();
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            var view = controller.Score(1, viewModel) as ViewResult;
            Assert.Equal(1, view.ViewData["CorrectCount"]);
            Assert.Equal("Score", view.ViewData["Score"]);
            Assert.Equal("Details", view.ViewName);
        }
    }
}
