using BankStartWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankStartWeb.Pages.User
{
    [Authorize(Roles = "Admin")]
    public class RegisterUserModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterUserModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "Enter an email address")]
        [EmailAddress(ErrorMessage = "Enter an valid email address")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Enter a password")]
        [Compare(nameof(Email), ErrorMessage = "Email doesn´t match")]
        public string RepeatEmail { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public void OnGet()
        {
        }


        
    }
}
