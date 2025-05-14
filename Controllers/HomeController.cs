using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var users = _userManager.Users.ToList();
        return View(users);
    }
    public IActionResult Contact()
    {
        return View();
    }

    // POST: Kontaktformular absenden
    [HttpPost]
    public IActionResult Contact(string Name, string Email, string Message)
    {
        // Hier kannst du später E-Mail senden oder speichern
        ViewBag.MessageSent = "Vielen Dank für Ihre Nachricht! Wir melden uns schnellstmöglich.";
        return View();
    }




}
