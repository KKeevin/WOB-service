namespace iSpan_final_service.DTO
{
    public class LoginDTO
    {
        public int MemberId { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string? Name { get; set; }

        public string? Email { get; set; }
    }
}
