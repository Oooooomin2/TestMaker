using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Data;

namespace TestMaker.Models.ViewModels
{
    public class UserTestViewModel
    {
        public IList<Test> Tests { get; set; }
        public User User { get; set; }

        public UserTestViewModel ShowTestIndexInfo(int? id, TestMakerContext _context)
        {
            return new UserTestViewModel
            {
                Tests = _context.Tests
                    .Where(m => m.UserId == id)
                    .ToList(),
                User = _context.Users
                    .FirstOrDefault(m => m.UserId == id)
            };
        }
    }
}
