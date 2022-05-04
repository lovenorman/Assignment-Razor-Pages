using BankStartWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BankStartWeb.Pages.User
{
    [Authorize(Roles = "Admin")]
    public class RegisterUserModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterUserModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
        [BindProperty]
        public bool EmailConfirmed { get; set; }
        [BindProperty]
        public IList<string> Roles { get; set; }
        [BindProperty]
        public List<SelectListItem> AllRoles { get; set; }

        public void OnGet()
        {
            SetRoles();
        }

        //public IActionResult OnPost()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new IdentityUser();
        //        {
        //            user.Email = Email;
        //            user.PasswordHash = Password;
        //            user.EmailConfirmed = EmailConfirmed;
        //        };

        //        _userManager.CreateAsync(user, Password).Wait();
        //        _userManager.AddToRoleAsync(user, Roles).Wait();

        //        return RedirectToPage("CustomersList");
        //    }

        //    SetRoles();
        //    return Page();
        //}
        
        private void SetRoles()
        {
            AllRoles = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value = "Cashier",
                    Text = "Cashier"
                },
                new SelectListItem()
                {
                    Value = "Admin",
                    Text = "Admin"
                }
            };
        }
    }
}
