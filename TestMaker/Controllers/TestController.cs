﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestMaker.Data;
using TestMaker.Models;
using TestMaker.Models.ViewModels;

namespace TestMaker.Controllers
{
    public class TestController : Controller
    {
        private readonly TestMakerContext _context;

        public TestController(TestMakerContext context)
        {
            _context = context;
        }

        // GET: Test
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tests.ToListAsync());
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
            if (ModelState.IsValid)
            {
                ViewData["Title"] = testViewModel.Tests.Title;
                ViewData["Number"] = testViewModel.Tests.Number;
                testViewModel.Tests.CreatedTime = DateTime.Now;
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Tests.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("TestId,Question,Answer")] Question question)
        {
            if (id != question.QuestionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestExists(question.QuestionId))
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
            return View(question);
        }

        // GET: Test/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Questions
                .FirstOrDefaultAsync(m => m.QuestionId == id);
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

        private bool TestExists(int id)
        {
            return _context.Tests.Any(e => e.TestId == id);
        }
    }
}
