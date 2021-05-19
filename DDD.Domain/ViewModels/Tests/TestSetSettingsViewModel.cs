using System.ComponentModel.DataAnnotations;

namespace DDD.Domain.ViewModels.Tests
{
    public sealed class TestSetSettingsViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [Range(1,100)]
        public int Number { get; set; }
    }
}
