using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestMaker.Data;
using TestMaker.Models;
using TestMaker.Models.Interface;
using TestMaker.Models.ViewModels;

namespace TestMaker.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private readonly ITestRepository _testRepository;

        public TestController(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        // GET: Test/Index/5
        public IActionResult Index(int? id)
        {
            ViewData["Title"] = "Index";
            ViewData["Action"] = "Index";
            ViewData["Controller"] = "Test";
            return View(_testRepository.GetAll(id));
        }

        // GET: Test/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            ViewData["Title"] = "Details";
            ViewData["Action"] = "Details";
            ViewData["Controller"] = "Test";
            return View(_testRepository.GetContent(id));
        }

        public IActionResult SetSettings()
        {
            ViewData["Title"] = "Settings";
            ViewData["Action"] = "Settings";
            ViewData["Controller"] = "Test";
            return View();
        }

        // GET: Test/Create
        public IActionResult Create([FromQuery]string title,[FromQuery]string number)
        {
            ViewData["Title"] = title;
            ViewData["Number"] = number;
            ViewData["Action"] = "Create";
            ViewData["Controller"] = "Test";
            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Tests")]TestViewModel testViewModel)
        {
            if (ModelState.IsValid)
            {
                testViewModel.Tests.CreatedTime = DateTime.Now;
                testViewModel.Tests.UpdatedTime = DateTime.Now;
                _testRepository.Update(testViewModel);
                return RedirectToAction("Index", "Home");
            }
            ViewData["Title"] = testViewModel.Tests.Title;
            ViewData["Number"] = testViewModel.Tests.Number;
            return View(testViewModel);
        }

        // GET: Test/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            ViewData["Title"] = "Edit";
            ViewData["Action"] = "Edit";
            ViewData["Controller"] = "Test";
            return View(_testRepository.GetContent(id));
        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Tests")]TestViewModel testViewModel)
        {
            if (id != testViewModel.Tests.TestId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    testViewModel.Tests.UpdatedTime = DateTime.Now;
                    _testRepository.Update(testViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_testRepository.TestExists(testViewModel.Tests.TestId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(testViewModel);
        }

        // GET: Test/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            ViewData["Title"] = "Delete";
            ViewData["Action"] = "Delete";
            ViewData["Controller"] = "Test";
            return View(_testRepository.GetDeleteContent(id));
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _testRepository.DeleteAsync(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Score(int id, [Bind("Tests")]TestViewModel testViewModel)
        {
            var questions = _testRepository.GetQuestion(id);
            var correctCount = 0;
            foreach (var t in testViewModel.Tests.Questions)
            {
                var q = questions.SingleOrDefault(o => o.QuestionId == t.QuestionId);
                if (t.Choices.Count > 1)
                {
                    var answers = q.Choices
                        .Where(o => o.IsAnswer)
                        .Select(o => o.ChoiceId);
                    var usersAnswers = t.Choices
                        .Where(o => o.IsUsersAnswerCheck)
                        .Select(o => o.ChoiceId);
                    if (answers.SequenceEqual(usersAnswers))
                    {
                        ViewData[t.QuestionId.ToString()] = "Correct!!";
                        correctCount++;
                    }
                    else
                    {
                        ViewData[t.QuestionId.ToString()] = "Incorrect!!";
                    }
                }
                else
                {
                    var answer = q.Choices
                        .Where(o => o.QuestionId == t.QuestionId)
                        .SingleOrDefault(o => o.IsAnswer)
                        .ChoiceId;
                    var usersAnswer = t.Choices
                        .SingleOrDefault()
                        .IsUsersAnswerRadio;
                    if (answer == usersAnswer)
                    {
                        ViewData[t.QuestionId.ToString()] = "Correct!!";
                        correctCount++;
                    }
                    else
                    {
                        ViewData[t.QuestionId.ToString()] = "Incorrect!!";
                    }
                }
            }
            ViewData["Score"] = "Score";
            ViewData["CorrectCount"] = correctCount;
            return View("Details", _testRepository.GetContent(id));
        }
    }
}
