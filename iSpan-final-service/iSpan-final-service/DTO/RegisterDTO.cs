namespace iSpan_final_service.DTO
{
    public class RegisterDTO
    {
        public int MemberId { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string? Mobile { get; set; }
        public bool? Gender { get; set; }
        public string? Email { get; set; }
        public int? Authority { get; set; }

    }
}
