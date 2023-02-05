namespace iSpan_final_service.DTO
{
    public class ReplyDTO

    {
        public int? ReplyId { get; set; }
        public int? ArticleId { get; set; }
        public int? MemberId { get; set; }
        public string? Account { get; set; }
        public string? Context { get; set; }
        public DateTime? Time { get; set; }
        public int? NumNice { get; set; }
        public string? Picture { get; set; }
        //public bool? Visibility { get; set; }
        //public bool IsRereply { get; set; }
    }
}
