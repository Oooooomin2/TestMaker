using System;
using System.Linq;
using DDD.Domain.Model.Interface.Tests;
using DDD.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var t = _testRepository.GetContent(o => o.TestId == id);
            return View(_testRepository.GetContent(o => o.TestId == id));
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
        public IActionResult Create(
            [Bind("TestId,Title,Number,CreatedTime,UpdatedTime,UserId,User,Questions")] Test test)
        {
            if (ModelState.IsValid)
            {
                test.CreatedTime = DateTime.Now;
                test.UpdatedTime = DateTime.Now;
                _testRepository.Create(test);
                return RedirectToAction("Index", "Home");
            }
            ViewData["Title"] = test.Title;
            ViewData["Number"] = test.Number;
            return View(test);
        }

        // GET: Test/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            ViewData["Title"] = "Edit";
            ViewData["Action"] = "Edit";
            ViewData["Controller"] = "Test";
            return View(_testRepository.GetContent(o => o.TestId == id));
        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            int id,
            [Bind("TestId,Title,Number,CreatedTime,UpdatedTime,UserId,User,Questions")] Test test)
        {
            if (id != test.TestId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    test.UpdatedTime = DateTime.Now;
                    _testRepository.Update(test);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_testRepository.Exists(o => o.TestId == test.TestId))
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
            return View(test);
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
        public IActionResult DeleteConfirmed(int id)
        {
            _testRepository.Delete(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Score(
            int id,
            [Bind("TestId,Title,Number,CreatedTime,UpdatedTime,UserId,User,Questions")] Test test)
        {
            var questions = _testRepository.GetQuestion(id);
            var correctCount = 0;
            foreach (var t in test.Questions)
            {
                var question = questions.SingleOrDefault(o => o.QuestionId == t.QuestionId);
                if (t.Choices.Count > 1)
                {
                    var answers = question.Choices
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
                    var answer = question.Choices
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
            return View("Details", _testRepository.GetContent(o => o.TestId == id));
        }
    }
}
