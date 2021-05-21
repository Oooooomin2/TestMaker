using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace TestMakerProject.Controllers.Resources
{
    public class TestResource
    {
        public int TestId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Range(1, 100)]
        public int Number { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int UserId { get; set; }
        public ICollection<QuestionResource> Questions { get; set; }
    }
}
