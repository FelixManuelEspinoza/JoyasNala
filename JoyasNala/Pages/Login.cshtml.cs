using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JoyasNala.Models;
using System.Linq;

namespace JoyasNala.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public string Message { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == Username && u.Contrasena == Password);
            if (usuario != null)
            {
                // Autenticación exitosa
                if (usuario.EsAdministrador)
                {
                    return RedirectToPage("/Admin/Dashboard");
                }
                else
                {
                    return RedirectToPage("/Index");
                }
            }
            else
            {
                Message = "Nombre de usuario o contraseña incorrectos";
                return Page();
            }
        }
    }
}
