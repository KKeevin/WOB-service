namespace iSpan_final_service.DTO
{
    public class ArticleDTO
    {
        public int ArticleId { get; set; }
        public int MemberId { get; set; }
        public string? Account { get; set; }
        public int BoardId { get; set; }
        public string? BoardName { get; set; }
        public string? Title { get; set; }
        public string? Context { get; set; }
        public DateTime Time { get; set; }
        public int NumNice { get; set; }
        public int NumReply { get; set; }
        public string? Picture { get; set; }
        //public bool? Visibility { get; set; }
    }
}
