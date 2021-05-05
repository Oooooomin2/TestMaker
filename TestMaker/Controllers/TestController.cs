﻿using System;
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
using TestMaker.Models.ViewModels;

namespace TestMaker.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private readonly TestMakerContext _context;

        public TestController(TestMakerContext context)
        {
            _context = context;
        }

        // GET: Test/Index/5
        public IActionResult Index(int? id)
        {
            var index = new UserTestViewModel
            {
                Tests = _context.Tests.Where(m => m.UserId == id).ToList(),
                User = _context.Users
                    .FirstOrDefault(m => m.UserId == id)
            };
            return View(index);
        }

        // GET: Test/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var test = new TestViewModel
            {
                Tests = _context.Tests
                    .FirstOrDefault(m => m.TestId == id),
                Questions = _context.Questions
                    .Where(m => m.TestId == id).ToList(),
                Choices = _context.Choices
                    .Where(m => m.Question.TestId == id).ToList()
            };
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        public IActionResult SetSettings()
        {
            return View();
        }

        // GET: Test/Create
        public IActionResult Create([FromQuery]string title,[FromQuery]string number)
        {
            ViewData["Title"] = title;
            ViewData["Number"] = number;
            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Tests")]TestViewModel testViewModel)
        {
            ViewData["Title"] = testViewModel.Tests.Title;
            ViewData["Number"] = testViewModel.Tests.Number;
            if (ModelState.IsValid)
            {
                testViewModel.Tests.CreatedTime = DateTime.Now;
                testViewModel.Tests.UpdatedTime = DateTime.Now;
                _context.Tests.Add(testViewModel.Tests);
                foreach(var q in testViewModel.Tests.Questions)
                {
                    _context.Questions.Add(q);
                    foreach(var c in q.Choices)
                    {
                        _context.Choices.Add(c);
                    }
                }
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(testViewModel);
        }

        // GET: Test/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = new TestViewModel
            {
                Tests = _context.Tests
                    .FirstOrDefault(m => m.TestId == id),
                Questions = _context.Questions
                    .Where(m => m.TestId == id).ToList(),
                Choices = _context.Choices
                    .Where(m => m.Question.TestId == id).ToList()
            };
            if (test == null)
            {
                return NotFound();
            }
            return View(test);
        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Tests")]TestViewModel testViewModel)
        {
            if (id != testViewModel.Tests.TestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    testViewModel.Tests.UpdatedTime = DateTime.Now;
                    _context.Tests.Update(testViewModel.Tests);
                    foreach (var q in testViewModel.Tests.Questions)
                    {
                        _context.Questions.Update(q);
                        foreach (var c in q.Choices)
                        {
                            _context.Choices.Update(c);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestExists(testViewModel.Tests.TestId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(testViewModel);
        }

        // GET: Test/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Tests
                .FirstOrDefaultAsync(m => m.TestId == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var test = await _context.Tests.FindAsync(id);
            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Score(int id, [Bind("Tests")]TestViewModel testViewModel)
        {
            var questions = _context.Questions.Where(o => o.TestId == id);
            foreach (var question in questions)
            {
                question.Choices = _context.Choices.Where(o => o.QuestionId == question.QuestionId).ToList();
            }
            var correctCount = 0;
            foreach (var t in testViewModel.Tests.Questions)
            {
                var q = questions.SingleOrDefault(o => o.QuestionId == t.QuestionId);
                if (t.Choices.Count > 1)
                {
                    var answers = q.Choices.Where(o => o.IsAnswer).ToList();
                    var answersCollection = new List<int>();
                    foreach (var answer in answers)
                    {
                        answersCollection.Add(answer.ChoiceId);
                    }
                    var usersAnswers = t.Choices.Where(o => o.IsUsersAnswerCheck).ToList();
                    var usersAnswersCollection = new List<int>();
                    foreach (var usersAnswer in usersAnswers)
                    {
                        usersAnswersCollection.Add(usersAnswer.ChoiceId);
                    }
                    if (answersCollection.SequenceEqual(usersAnswersCollection))
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
                        .SingleOrDefault(o => o.IsAnswer).ChoiceId;
                    var usersAnswer = t.Choices.FirstOrDefault().IsUsersAnswerRadio;
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
            var test = new TestViewModel
            {
                Tests = _context.Tests
                    .FirstOrDefault(m => m.TestId == id),
                Questions = _context.Questions
                    .Where(m => m.TestId == id).ToList(),
                Choices = _context.Choices
                    .Where(m => m.Question.TestId == id).ToList()
            };
            ViewData["Score"] = "Score";
            ViewData["CorrectCount"] = correctCount;
            return View("Details", test);
        }

        private bool TestExists(int id)
        {
            return _context.Tests.Any(e => e.TestId == id);
        }
    }
}
