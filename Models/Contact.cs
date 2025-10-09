using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace ContactList.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required, Display(Name = "First Name"), StringLength(100)]
        public string FirstName { get; set; } = "";

        [Required, Display(Name = "Last Name"), StringLength(100)]
        public string LastName { get; set; } = "";

        [Required, Display(Name = "Phone"), StringLength(50)]
        public string Phone { get; set; } = "";

        [Required, EmailAddress, StringLength(200)]
        public string Email { get; set; } = "";

        // Optional per rubric — make it nullable so MVC doesn't implicitly require it
        public string? Organization { get; set; }

        // Keep non-nullable int + Range to enforce selection > 0
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }  // nav can be null until included

        // Set only by code on create
        public DateTime DateAdded { get; set; }

        [NotMapped]
        public string Slug => MakeSlug($"{FirstName} {LastName}");
        private static string MakeSlug(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            var lower = input.Trim().ToLowerInvariant();
            lower = Regex.Replace(lower, @"[^a-z0-9\s-]", "");
            lower = Regex.Replace(lower, @"\s+", "-");
            lower = Regex.Replace(lower, @"-+", "-");
            return lower;
        }
    }
}