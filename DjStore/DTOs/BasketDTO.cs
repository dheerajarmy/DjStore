using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DjStore.DTOs
{
    public class BasketDTO
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<BasketItemsDTO> BasketItems { get; set; }
    }
}