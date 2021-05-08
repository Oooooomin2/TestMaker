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
    public class HomeIndexViewModelTest
    {
        [Fact]
        public void Access_homeIndex_db()
        {
            var viewModel = HomeIndexViewModelTestData.HomeIndexViewModelData();
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
            var homeInfoTest = new HomeIndexViewModel().ShowHomeInfo(_context);
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
            var viewModel = HomeIndexViewModelTestData.HomeIndexViewModelData();
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
            var controller = new HomeController(_context);
            var view = controller.Index() as ViewResult;
            Assert.Equal("Home Page", view.ViewData["Title"]);
            Assert.Equal("Index", view.ViewData["Action"]);
            Assert.Equal("Home", view.ViewData["Controller"]);
        }
    }
}
