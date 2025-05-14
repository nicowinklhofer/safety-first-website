using Microsoft.AspNetCore.Mvc;

public class ContactController : Controller
{
    // Zeigt das Kontaktformular
    public IActionResult Index()
    {
        return View();
    }

    // Verarbeitet das Kontaktformular
    [HttpPost]
    public IActionResult Index(string Name, string Email, string Message)
    {
        // Hier kannst du später z. B. Mail versenden
        ViewBag.MessageSent = "Vielen Dank für Ihre Nachricht! Wir melden uns schnellstmöglich.";
        return View();
    }
}
