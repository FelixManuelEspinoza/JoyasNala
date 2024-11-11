namespace JoyasNala.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
    }
}
