namespace iSpan_final_service.DTO
{
    public class CalendarDTO
    {
        public int CalendarId { get; set; }
        public int ObjectId { get; set; }
        public int? OrdererId { get; set; }
        public DateTime Start { get; set; }
        public bool? IsConfirmed { get; set; }

    }
}
