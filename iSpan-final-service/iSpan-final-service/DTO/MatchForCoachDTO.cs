namespace iSpan_final_service.DTO
{
    public class MatchForCoachDTO
    {
        public int MatchId { get; set; }
        public int? ObjectId { get; set; }
        public int? OrdererId { get; set; }
        public int? Payment { get; set; }
        public bool? IsPaid { get; set; }
        public bool? IsConfirmed { get; set; }
        public string? Content { get; set; }
        public DateTime? OrderTime { get; set; } //Match裡的預約日
        public DateTime? DealTime { get; set; } //付錢時間
        public DateTime? StartDateTime { get; set; }//課程開始時間

    }
}

