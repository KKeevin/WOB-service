using System;

namespace iSpan_final_service.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int MemberId { get; set; }
       
        public string? Address { get; set; }
    }
}
