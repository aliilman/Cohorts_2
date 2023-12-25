
using Microsoft.AspNetCore.Mvc;

//Bonus 1: Fake bir kullanıcı giriş sistemi yapın ve custom bir attribute ile bunu kontrol edin.
public class LoginController : Controller
{
    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        // Kullanıcı giriş kontrolü
        if (username == "Admin" && password == "Admin")
        {
            return Ok();
        }
        return BadRequest();
    }
}
