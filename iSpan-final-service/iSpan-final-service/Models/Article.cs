// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace iSpan_final_service.Models
{
    public partial class Article
    {
        public int ArticleId { get; set; }
        public int MemberId { get; set; }
        public int BoardId { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public DateTime Time { get; set; }
        public int NumNice { get; set; }
        public int NumReply { get; set; }
        public bool? Validity { get; set; }
        public string Picture { get; set; }

        public virtual Board Board { get; set; }
    }
}