using System;
using System.Collections.Generic;

namespace DDD.Domain.Models
{
    public class Test
    {
        public int TestId { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IList<Question> Questions { get; set; }
    }
}
