namespace JoyasNala.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public bool EsAdministrador { get; set; }
    }
}
