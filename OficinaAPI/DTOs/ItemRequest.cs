using System.Collections.Generic;

namespace OficinaAPI.DTOs
{
    public class ItemRequest
    {
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
    }
}