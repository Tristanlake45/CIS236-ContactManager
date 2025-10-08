using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace ContactList.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Phone")]
        [StringLength(50)]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; }

        [Display(Name = "Organization")]
        public string Organization { get; set; }

        // Foreign key - use Range to ensure selection > 0
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // Only set by code on create
        public DateTime DateAdded { get; set; }

        // Slug for friendly URLs (not stored in DB)
        [NotMapped]
        public string Slug => MakeSlug($"{FirstName} {LastName}");

        private string MakeSlug(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            var lower = input.Trim().ToLowerInvariant();
            lower = Regex.Replace(lower, @"[^a-z0-9\s-]", ""); // remove invalid chars
            lower = Regex.Replace(lower, @"\s+", "-"); // spaces to hyphens
            lower = Regex.Replace(lower, @"-+", "-"); // collapse dashes
            return lower;
        }
    }
}

