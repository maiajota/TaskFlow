using Microsoft.AspNetCore.Mvc;

namespace TaskFlow.Controllers;

public class LoginController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
