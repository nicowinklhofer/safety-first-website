using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class EmailModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;

    public EmailModel(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [BindProperty]
    public string NewEmail { get; set; }

    public string Message { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound();

        var token = await _userManager.GenerateChangeEmailTokenAsync(user, NewEmail);
        var result = await _userManager.ChangeEmailAsync(user, NewEmail, token);
        if (result.Succeeded)
        {
            Message = "E-Mail erfolgreich geändert.";
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return Page();
    }
}
