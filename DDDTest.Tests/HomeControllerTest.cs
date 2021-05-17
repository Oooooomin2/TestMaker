using DDD.Domain.Data;
using DDD.Domain.Model.Interface;
using DDD.Domain.Model.Repository;
using DDDTest.Tests.ViewModelData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMaker.Controllers;
using Xunit;

namespace DDDTest.Tests
{
    public class HomeControllerTest
    {
        [Fact]
        public void Access_homeIndex_db()
        {
            var viewModel = HomeViewModelTestData.HomeIndexViewModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_homeIndex_db")
                .Options;
            using var _context = new TestMakerContext(options);
            foreach (var t in viewModel.Tests)
            {
                _context.Tests.Add(t);
            }
            foreach (var u in viewModel.Users)
            {
                _context.Users.Add(u);
            }
            _context.SaveChanges();
            IHomeRepository homeRepository = new HomeRepository(_context);
            var homeInfoTest = homeRepository.GetAll();
            Assert.Equal(viewModel.Tests[0].TestId, homeInfoTest.Tests[0].TestId);
            Assert.Equal(viewModel.Tests[0].Title, homeInfoTest.Tests[0].Title);
            Assert.Equal(viewModel.Tests[0].CreatedTime, homeInfoTest.Tests[0].CreatedTime);
            Assert.Equal(viewModel.Tests[0].UpdatedTime, homeInfoTest.Tests[0].UpdatedTime);
            Assert.Equal(viewModel.Tests[0].UserId, homeInfoTest.Tests[0].UserId);
            Assert.Equal(viewModel.Tests[0].Questions[0].QuestionText, homeInfoTest.Tests[0].Questions[0].QuestionText);
            Assert.Equal(viewModel.Tests[0].Questions[0].TestId, homeInfoTest.Tests[0].Questions[0].TestId);
            Assert.Equal(viewModel.Tests[0].Questions[0].Choices[0].ChoiceId, homeInfoTest.Tests[0].Questions[0].Choices[0].ChoiceId);
            Assert.Equal(viewModel.Tests[0].Questions[0].Choices[0].ChoiceText, homeInfoTest.Tests[0].Questions[0].Choices[0].ChoiceText);
            Assert.Equal(viewModel.Tests[0].Questions[0].Choices[0].IsAnswer, homeInfoTest.Tests[0].Questions[0].Choices[0].IsAnswer);
            Assert.Equal(viewModel.Tests[0].Questions[0].Choices[0].IsUsersAnswerCheck, homeInfoTest.Tests[0].Questions[0].Choices[0].IsUsersAnswerCheck);
            Assert.Equal(viewModel.Tests[0].Questions[0].Choices[0].IsUsersAnswerRadio, homeInfoTest.Tests[0].Questions[0].Choices[0].IsUsersAnswerRadio);
            Assert.Equal(viewModel.Tests[0].Questions[0].Choices[0].QuestionId, homeInfoTest.Tests[0].Questions[0].Choices[0].QuestionId);
            Assert.Equal(viewModel.Tests[0].Questions[0].Choices[0].ChoiceText, homeInfoTest.Tests[0].Questions[0].Choices[0].ChoiceText);
            Assert.Equal(viewModel.Tests[0].Questions[0].Choices[0].ChoiceText, homeInfoTest.Tests[0].Questions[0].Choices[0].ChoiceText);
            Assert.Equal(viewModel.Users[0].LoginId, homeInfoTest.Users[0].LoginId);
            Assert.Equal(viewModel.Users[0].UserName, homeInfoTest.Users[0].UserName);
            Assert.Equal(viewModel.Users[0].Password, homeInfoTest.Users[0].Password);
            Assert.Equal(viewModel.Users[0].Salt, homeInfoTest.Users[0].Salt);
            Assert.Equal(viewModel.Users[0].SelfIntroduction, homeInfoTest.Users[0].SelfIntroduction);
            Assert.Equal(viewModel.Users[0].Icon, homeInfoTest.Users[0].Icon);
        }

        [Fact]
        public void Access_homeIndex_check_viewData()
        {
            var viewModel = HomeViewModelTestData.HomeIndexViewModelData();
            var options = new DbContextOptionsBuilder<TestMakerContext>()
                .UseInMemoryDatabase(databaseName: "Access_homeIndex_check_viewData")
                .Options;
            using var _context = new TestMakerContext(options);
            foreach (var t in viewModel.Tests)
            {
                _context.Tests.Add(t);
            }
            foreach (var u in viewModel.Users)
            {
                _context.Users.Add(u);
            }
            _context.SaveChanges();
            IHomeRepository homeRepository = new HomeRepository(_context);
            var controller = new HomeController(homeRepository);
            var view = controller.Index() as ViewResult;
            Assert.Equal("Home Page", view.ViewData["Title"]);
            Assert.Equal("Index", view.ViewData["Action"]);
            Assert.Equal("Home", view.ViewData["Controller"]);
        }

        [Fact]
        public void Access_descriptionIndex_check_viewData()
        {
            var controller = new HomeController(null);
            var view = controller.Description() as ViewResult;
            Assert.Equal("What's the Test Maker?", view.ViewData["Title"]);
            Assert.Equal("Description", view.ViewData["Action"]);
            Assert.Equal("Home", view.ViewData["Controller"]);
        }
    }
}
