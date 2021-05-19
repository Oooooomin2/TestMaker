using DDD.Domain.Data;
using DDD.Domain.Model.Interface.Tests;
using DDD.Domain.Models.Repository.Tests;
using DDD.Domain.ViewModels.Tests;
using DDDTest.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TestMaker.Controllers;
using Xunit;

namespace DDDTest.Tests
{
    public class TestControllerTest
    {
        [Fact]
        public void Access_TestIndex_db()
        {
            var testData = TestModelData.TestData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestIndex_db")
                .Options;
            using var _context = new TestMakerContext(options);
            foreach (var t in testData)
            {
                _context.Tests.Add(t);
            }
            _context.Users.Add(testData[0].User);
            _context.SaveChanges();
            ITestRepository testRepository = new TestRepository(_context);
            var testInfoTest = testRepository.GetAll(1);
            Assert.Equal(testData[0].TestId, testInfoTest.FirstOrDefault().TestId);
            Assert.Equal(testData[0].Title, testInfoTest.FirstOrDefault().Title);
            Assert.Equal(testData[0].CreatedTime, testInfoTest.FirstOrDefault().CreatedTime);
            Assert.Equal(testData[0].UpdatedTime, testInfoTest.FirstOrDefault().UpdatedTime);
            Assert.Equal(testData[0].UserId, testInfoTest.FirstOrDefault().UserId);
            Assert.Equal(testData[0].Questions[0].QuestionText, testInfoTest.FirstOrDefault().Questions[0].QuestionText);
            Assert.Equal(testData[0].Questions[0].TestId, testInfoTest.FirstOrDefault().Questions[0].TestId);
            Assert.Equal(testData[0].Questions[0].Choices[0].ChoiceId, testInfoTest.FirstOrDefault().Questions[0].Choices[0].ChoiceId);
            Assert.Equal(testData[0].Questions[0].Choices[0].ChoiceText, testInfoTest.FirstOrDefault().Questions[0].Choices[0].ChoiceText);
            Assert.Equal(testData[0].Questions[0].Choices[0].IsAnswer, testInfoTest.FirstOrDefault().Questions[0].Choices[0].IsAnswer);
            Assert.Equal(testData[0].Questions[0].Choices[0].IsUsersAnswerCheck, testInfoTest.FirstOrDefault().Questions[0].Choices[0].IsUsersAnswerCheck);
            Assert.Equal(testData[0].Questions[0].Choices[0].IsUsersAnswerRadio, testInfoTest.FirstOrDefault().Questions[0].Choices[0].IsUsersAnswerRadio);
            Assert.Equal(testData[0].Questions[0].Choices[0].QuestionId, testInfoTest.FirstOrDefault().Questions[0].Choices[0].QuestionId);
            Assert.Equal(testData[0].Questions[0].Choices[0].ChoiceText, testInfoTest.FirstOrDefault().Questions[0].Choices[0].ChoiceText);
            Assert.Equal(testData[0].User.LoginId, testInfoTest.FirstOrDefault().User.LoginId);
            Assert.Equal(testData[0].User.UserName, testInfoTest.FirstOrDefault().User.UserName);
            Assert.Equal(testData[0].User.Password, testInfoTest.FirstOrDefault().User.Password);
            Assert.Equal(testData[0].User.Salt, testInfoTest.FirstOrDefault().User.Salt);
            Assert.Equal(testData[0].User.SelfIntroduction, testInfoTest.FirstOrDefault().User.SelfIntroduction);
            Assert.Equal(testData[0].User.Icon, testInfoTest.FirstOrDefault().User.Icon);
        }

        [Fact]
        public void Access_TestIndex_check_viewData()
        {
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
            var testData = TestModelData.TestData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestDetails_db")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Tests.Add(testData[0]);
            foreach(var q in testData[0].Questions)
            {
                _context.Questions.Add(q);
            }
            foreach (var c in testData[0].Questions[0].Choices)
            {
                _context.Choices.Add(c);
            }
            _context.SaveChanges();
            ITestRepository testRepository = new TestRepository(_context);
            var testInfoTest = testRepository.GetContent(o => o.TestId == 1);
            Assert.Equal(testData[0].TestId, testInfoTest.TestId);
            Assert.Equal(testData[0].Title, testInfoTest.Title);
            Assert.Equal(testData[0].CreatedTime, testInfoTest.CreatedTime);
            Assert.Equal(testData[0].UpdatedTime, testInfoTest.UpdatedTime);
            Assert.Equal(testData[0].UserId, testInfoTest.UserId);
            Assert.Equal(testData[0].Questions[0].QuestionText, testInfoTest.Questions[0].QuestionText);
            Assert.Equal(testData[0].Questions[0].TestId, testInfoTest.Questions[0].TestId);
            Assert.Equal(testData[0].Questions[0].Choices[0].ChoiceId, testInfoTest.Questions[0].Choices[0].ChoiceId);
            Assert.Equal(testData[0].Questions[0].Choices[0].ChoiceText, testInfoTest.Questions[0].Choices[0].ChoiceText);
            Assert.Equal(testData[0].Questions[0].Choices[0].IsAnswer, testInfoTest.Questions[0].Choices[0].IsAnswer);
            Assert.Equal(testData[0].Questions[0].Choices[0].IsUsersAnswerCheck, testInfoTest.Questions[0].Choices[0].IsUsersAnswerCheck);
            Assert.Equal(testData[0].Questions[0].Choices[0].IsUsersAnswerRadio, testInfoTest.Questions[0].Choices[0].IsUsersAnswerRadio);
            Assert.Equal(testData[0].Questions[0].Choices[0].QuestionId, testInfoTest.Questions[0].Choices[0].QuestionId);
            Assert.Equal(testData[0].Questions[0].Choices[0].ChoiceText, testInfoTest.Questions[0].Choices[0].ChoiceText);
            Assert.Equal(testData[0].Questions[0].Choices[0].QuestionId, testInfoTest.Questions[0].Choices[0].QuestionId);
            Assert.Equal(testData[0].Questions[0].Choices[0].IsAnswer, testInfoTest.Questions[0].Choices[0].IsAnswer);
        }

        [Fact]
        public void Access_TestDetails_check_viewData()
        {
            var testData = TestModelData.TestData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestDetails_check_viewData")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Tests.Add(testData[0]);
            foreach (var q in testData[0].Questions)
            {
                _context.Questions.Add(q);
            }
            foreach (var c in testData[0].Questions[0].Choices)
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
            var view = controller.Create(new TestSetSettingsViewModel
            {
                Title = "CreateMethodTest",
                Number= 1
            }) as ViewResult;
            Assert.Equal("CreateMethodTest", view.ViewData["Title"]);
            Assert.Equal(1, view.ViewData["Number"]);
            Assert.Equal("Create", view.ViewData["Action"]);
            Assert.Equal("Test", view.ViewData["Controller"]);
        }

        [Fact]
        public void Access_TestCreated_check_RedirectToActionResult()
        {
            var testData = TestModelData.TestData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestCreated_check_viewData")
                .Options;
            using var _context = new TestMakerContext(options);
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            var actionResult = controller.Create(testData[0]) as RedirectToActionResult;
            Assert.Equal("Index", actionResult.ActionName);
            Assert.Equal("Home", actionResult.ControllerName);
        }

        [Fact]
        public void Access_TestCreated_Check_db()
        {
            var testData = TestModelData.TestData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestCreated_Check_db")
                .Options;
            using var _context = new TestMakerContext(options);
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            var actionResult = controller.Create(testData[0]);
            Assert.Equal(testData[0].TestId, _context.Tests.FirstOrDefault().TestId);
            Assert.Equal(testData[0].Title, _context.Tests.FirstOrDefault().Title);
            Assert.Equal(testData[0].CreatedTime, _context.Tests.FirstOrDefault().CreatedTime);
            Assert.Equal(testData[0].UpdatedTime, _context.Tests.FirstOrDefault().UpdatedTime);
            Assert.Equal(testData[0].UserId, _context.Tests.FirstOrDefault().UserId);
            Assert.Equal(testData[0].Questions[0].QuestionText, _context.Tests.FirstOrDefault().Questions[0].QuestionText);
            Assert.Equal(testData[0].Questions[0].TestId, _context.Tests.FirstOrDefault().Questions[0].TestId);
            Assert.Equal(testData[0].Questions[0].Choices[0].ChoiceId, _context.Tests.FirstOrDefault().Questions[0].Choices[0].ChoiceId);
            Assert.Equal(testData[0].Questions[0].Choices[0].ChoiceText, _context.Tests.FirstOrDefault().Questions[0].Choices[0].ChoiceText);
            Assert.Equal(testData[0].Questions[0].Choices[0].IsAnswer, _context.Tests.FirstOrDefault().Questions[0].Choices[0].IsAnswer);
            Assert.Equal(testData[0].Questions[0].Choices[0].IsUsersAnswerCheck, _context.Tests.FirstOrDefault().Questions[0].Choices[0].IsUsersAnswerCheck);
            Assert.Equal(testData[0].Questions[0].Choices[0].IsUsersAnswerRadio, _context.Tests.FirstOrDefault().Questions[0].Choices[0].IsUsersAnswerRadio);
            Assert.Equal(testData[0].Questions[0].Choices[0].QuestionId, _context.Tests.FirstOrDefault().Questions[0].Choices[0].QuestionId);
            Assert.Equal(testData[0].Questions[0].Choices[0].ChoiceText, _context.Tests.FirstOrDefault().Questions[0].Choices[0].ChoiceText);
            Assert.Equal(testData[0].Questions[0].Choices[0].QuestionId, _context.Choices.FirstOrDefault().QuestionId);
            Assert.Equal(testData[0].Questions[0].Choices[0].IsAnswer, _context.Choices.FirstOrDefault().IsAnswer);
        }

        [Fact]
        public void Access_TestCreated_Check_GivenInvalidModel()
        {
            var testData = TestModelData.TestData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestCreated_Check_GivenInvalidModel")
                .Options;
            using var _context = new TestMakerContext(options);
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            controller.ModelState.AddModelError("error", "some error");
            var view = controller.Create(testData[0]) as ViewResult;
            Assert.Equal(testData[0].Title, view.ViewData["Title"]);
            Assert.Equal(testData[0].Number, view.ViewData["Number"]);
        }

        [Fact]
        public void Access_TestEdit_Check_viewData()
        {
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
            var testData = TestModelData.TestData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestEdited_Check_WhenIdDifferentPostedData_BeNotFound")
                .Options;
            using var _context = new TestMakerContext(options);
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            var actionResult = controller.Edit(999999, testData[0]);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public void Access_TestEdited_Check_GivenInvalidModel()
        {
            var testData = TestModelData.TestData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestEdited_Check_GivenInvalidModel")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Tests.Add(testData[0]);
            foreach (var q in testData[0].Questions)
            {
                _context.Questions.Add(q);
            }
            foreach (var c in testData[0].Questions[0].Choices)
            {
                _context.Choices.Add(c);
            }
            _context.SaveChanges();
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            controller.ModelState.AddModelError("error", "some error");
            var view = controller.Edit(1, testData[0]) as ViewResult;
            Assert.Null(view.ViewName);
        }

        [Fact]
        public void Access_TestDelete_check_whenIdNull_BeNotFound()
        {
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
        public void Access_TestDeleteConfirmed_check_RedirectToActionResult()
        {
            var testData = TestModelData.TestData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestDeleteConfirmed_check_RedirectToActionResult")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Tests.Add(testData[0]);
            _context.SaveChanges();
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            var actionResult = controller.DeleteConfirmed(1) as RedirectToActionResult;
            Assert.Equal("Index", actionResult.ActionName);
            Assert.Equal("Home", actionResult.ControllerName);
        }

        [Fact]
        public void Access_TestDeleteConfirmed_Check_db()
        {
            var testData = TestModelData.TestData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestDeleteConfirmed_Check_db")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Tests.Add(testData[0]);
            _context.SaveChanges();
            Assert.Equal(1, _context.Tests.Count());
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            controller.DeleteConfirmed(1);
            Assert.Equal(0, _context.Tests.Count());
        }

        [Fact]
        public void Access_TestScore_Check_MultiFailResult()
        {
            var testData = TestModelData.TestData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestScore_Check_MultiCorrectResult0")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Tests.Add(testData[0]);
            _context.SaveChanges();
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            var view = controller.Score(1, testData[0]) as ViewResult;
            Assert.Equal(0, view.ViewData["CorrectCount"]);
            Assert.Equal("Score", view.ViewData["Score"]);
            Assert.Equal("Details", view.ViewName);
        }

        [Fact]
        public void Access_TestScore_Check_MultiCorrectResult()
        {
            var testData = TestModelData.TestDataForScoreMultiCorrectCheck();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestScore_Check_MultiCorrectResult1")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Tests.Add(testData);
            _context.SaveChanges();
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            var view = controller.Score(1, testData) as ViewResult;
            Assert.Equal(1, view.ViewData["CorrectCount"]);
            Assert.Equal("Score", view.ViewData["Score"]);
            Assert.Equal("Details", view.ViewName);
        }

        [Fact]
        public void Access_TestScore_Check_SingleFailResult()
        {
            var testData = TestModelData.TestDataForScoreSingleFailCheck();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestScore_Check_SingleCorrectResult0")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Tests.Add(testData);
            _context.SaveChanges();
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            var view = controller.Score(1, testData) as ViewResult;
            Assert.Equal(0, view.ViewData["CorrectCount"]);
            Assert.Equal("Score", view.ViewData["Score"]);
            Assert.Equal("Details", view.ViewName);
        }

        [Fact]
        public void Access_TestScore_Check_SingleCorrectResult()
        {
            var testData = TestModelData.TestDataForScoreSingleCorrectCheck();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_TestScore_Check_SingleCorrectResult1")
                .Options;
            using var _context = new TestMakerContext(options);
            _context.Tests.Add(testData);
            _context.SaveChanges();
            ITestRepository testRepository = new TestRepository(_context);
            var controller = new TestController(testRepository);
            var view = controller.Score(1, testData) as ViewResult;
            Assert.Equal(1, view.ViewData["CorrectCount"]);
            Assert.Equal("Score", view.ViewData["Score"]);
            Assert.Equal("Details", view.ViewName);
        }
    }
}
