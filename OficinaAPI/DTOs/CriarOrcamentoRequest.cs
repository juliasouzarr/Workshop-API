using System.Collections.Generic;

namespace OficinaAPI.DTOs
{
    public class CriarOrcamentoRequest
    {
        public int ClienteId { get; set; }
        public int VeiculoId { get; set; }
        public List<ItemRequest> Itens { get; set; }
    }
}