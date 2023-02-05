﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace iSpan_final_service.Models
{
    public partial class WOBContext : DbContext
    {
        public WOBContext()
        {
        }

        public WOBContext(DbContextOptions<WOBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<Award> Award { get; set; }
        public virtual DbSet<BackgroundManagement> BackgroundManagement { get; set; }
        public virtual DbSet<Board> Board { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Calendar> Calendar { get; set; }
        public virtual DbSet<ChatRoom> ChatRoom { get; set; }
        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<County> County { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<FavoriteBoard> FavoriteBoard { get; set; }
        public virtual DbSet<Gym> Gym { get; set; }
        public virtual DbSet<GymOrder> GymOrder { get; set; }
        public virtual DbSet<GymOrderEquipment> GymOrderEquipment { get; set; }
        public virtual DbSet<Inbody> Inbody { get; set; }
        public virtual DbSet<License> License { get; set; }
        public virtual DbSet<Match> Match { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Notice> Notice { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Reply> Reply { get; set; }
        public virtual DbSet<Score> Score { get; set; }
        public virtual DbSet<SurviceArea> SurviceArea { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>(entity =>
            {
                entity.Property(e => e.AreaId).HasColumnName("areaId");

                entity.Property(e => e.CountyId).HasColumnName("countyId");

                entity.Property(e => e.Village)
                    .HasMaxLength(50)
                    .HasColumnName("village");

                entity.HasOne(d => d.County)
                    .WithMany(p => p.Area)
                    .HasForeignKey(d => d.CountyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Area_County");
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.Property(e => e.ArticleId).HasColumnName("articleId");

                entity.Property(e => e.BoardId).HasColumnName("boardId");

                entity.Property(e => e.Context)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .HasColumnName("context");

                entity.Property(e => e.MemberId).HasColumnName("memberId");

                entity.Property(e => e.NumNice).HasColumnName("numNice");

                entity.Property(e => e.NumReply).HasColumnName("numReply");

                entity.Property(e => e.Picture)
                    .HasMaxLength(200)
                    .HasColumnName("picture");

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasColumnName("time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.Property(e => e.Validity)
                    .IsRequired()
                    .HasColumnName("validity")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Board)
                    .WithMany(p => p.Article)
                    .HasForeignKey(d => d.BoardId)
                    .HasConstraintName("FK_Article_Board");
            });

            modelBuilder.Entity<Award>(entity =>
            {
                entity.Property(e => e.AwardId).HasColumnName("awardId");

                entity.Property(e => e.AwardName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("awardName");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.MemberId).HasColumnName("memberId");
            });

            modelBuilder.Entity<BackgroundManagement>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("backgroundManagement");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Account)
                    .HasMaxLength(50)
                    .HasColumnName("account");

                entity.Property(e => e.Authority).HasColumnName("authority");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<Board>(entity =>
            {
                entity.Property(e => e.BoardId).HasColumnName("boardId");

                entity.Property(e => e.BoardName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("boardName");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.BrandId).HasColumnName("brandId");

                entity.Property(e => e.BrandName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("brandName");
            });

            modelBuilder.Entity<Calendar>(entity =>
            {
                entity.Property(e => e.CalendarId).HasColumnName("calendarId");

                entity.Property(e => e.IsConfirmed).HasColumnName("isConfirmed");

                entity.Property(e => e.MyContent)
                    .HasMaxLength(200)
                    .HasColumnName("myContent");

                entity.Property(e => e.ObjectId).HasColumnName("objectId");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("orderDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrdererId).HasColumnName("ordererId");

                entity.Property(e => e.StartDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("startDateTime");
            });

            modelBuilder.Entity<ChatRoom>(entity =>
            {
                entity.HasKey(e => e.ChatId);

                entity.Property(e => e.ChatId).HasColumnName("chatId");

                entity.Property(e => e.Context)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("context");

                entity.Property(e => e.MemberCoachId).HasColumnName("memberCoachId");

                entity.Property(e => e.MemberStudentId).HasColumnName("memberStudentId");

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasColumnName("time")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.Property(e => e.ClassId).HasColumnName("classId");

                entity.Property(e => e.ClassName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("className");
            });

            modelBuilder.Entity<County>(entity =>
            {
                entity.Property(e => e.CountyId).HasColumnName("countyId");

                entity.Property(e => e.CountyName)
                    .HasMaxLength(50)
                    .HasColumnName("countyName");
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.Property(e => e.EquipmentId).HasColumnName("equipmentId");

                entity.Property(e => e.EquipmentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("equipmentName");

                entity.Property(e => e.GymId).HasColumnName("gymId");
            });

            modelBuilder.Entity<FavoriteBoard>(entity =>
            {
                entity.HasKey(e => new { e.BoardId, e.MemberId });

                entity.Property(e => e.BoardId).HasColumnName("boardId");

                entity.Property(e => e.MemberId).HasColumnName("memberId");
            });

            modelBuilder.Entity<Gym>(entity =>
            {
                entity.Property(e => e.GymId).HasColumnName("gymId");

                entity.Property(e => e.Account)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("account");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.AreaId).HasColumnName("areaId");

                entity.Property(e => e.GymName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("gymName");

                entity.Property(e => e.ImgPath)
                    .HasMaxLength(500)
                    .HasColumnName("imgPath");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone")
                    .IsFixedLength();

                entity.Property(e => e.Validity)
                    .IsRequired()
                    .HasColumnName("validity")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Web)
                    .HasMaxLength(200)
                    .HasColumnName("web");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Gym)
                    .HasForeignKey(d => d.AreaId)
                    .HasConstraintName("FK_Gym_Area");
            });

            modelBuilder.Entity<GymOrder>(entity =>
            {
                entity.Property(e => e.GymOrderId).HasColumnName("gymOrderId");

                entity.Property(e => e.GymId).HasColumnName("gymId");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.HasOne(d => d.Gym)
                    .WithMany(p => p.GymOrder)
                    .HasForeignKey(d => d.GymId)
                    .HasConstraintName("FK_GymOrder_Gym");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.GymOrder)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_GymOrder_Match");
            });

            modelBuilder.Entity<GymOrderEquipment>(entity =>
            {
                entity.HasKey(e => new { e.GymOrderId, e.EquipmentId })
                    .HasName("PK_areaOrderEquipment");

                entity.Property(e => e.GymOrderId).HasColumnName("gymOrderId");

                entity.Property(e => e.EquipmentId).HasColumnName("equipmentId");

                entity.Property(e => e.Num).HasColumnName("num");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.GymOrderEquipment)
                    .HasForeignKey(d => d.EquipmentId)
                    .HasConstraintName("FK_GymOrderEquipment_Equipment");

                entity.HasOne(d => d.GymOrder)
                    .WithMany(p => p.GymOrderEquipment)
                    .HasForeignKey(d => d.GymOrderId)
                    .HasConstraintName("FK_GymOrderEquipment_GymOrder");
            });

            modelBuilder.Entity<Inbody>(entity =>
            {
                entity.Property(e => e.InbodyId).HasColumnName("inbodyId");

                entity.Property(e => e.Bfm)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("bfm");

                entity.Property(e => e.Bmi)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("bmi");

                entity.Property(e => e.Bmr)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("bmr");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ffm)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("ffm");

                entity.Property(e => e.Height)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("height");

                entity.Property(e => e.MemberId).HasColumnName("memberId");

                entity.Property(e => e.Smm)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("smm");

                entity.Property(e => e.Tbw)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("tbw");

                entity.Property(e => e.Weight)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("weight");

                entity.Property(e => e.Whr)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("whr");
            });

            modelBuilder.Entity<License>(entity =>
            {
                entity.Property(e => e.LicenseId).HasColumnName("licenseId");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.LicenseName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("licenseName");

                entity.Property(e => e.MemberId).HasColumnName("memberId");
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.Property(e => e.MatchId).HasColumnName("matchId");

                entity.Property(e => e.CalendarId).HasColumnName("calendarId");

                entity.Property(e => e.DealTime)
                    .HasColumnType("datetime")
                    .HasColumnName("dealTime");

                entity.Property(e => e.IsPaid).HasColumnName("isPaid");

                entity.Property(e => e.OrderTime)
                    .HasColumnType("datetime")
                    .HasColumnName("orderTime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Payment).HasColumnName("payment");

                entity.HasOne(d => d.Calendar)
                    .WithMany(p => p.Match)
                    .HasForeignKey(d => d.CalendarId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Match_Calendar");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.MemberId).HasColumnName("memberId");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("account");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Authority).HasColumnName("authority");

                entity.Property(e => e.Credit).HasColumnName("credit");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .HasColumnName("mobile")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Picture)
                    .HasMaxLength(200)
                    .HasColumnName("picture");

                entity.Property(e => e.Points).HasColumnName("points");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.SelfIntro)
                    .HasMaxLength(200)
                    .HasColumnName("selfIntro");

                entity.Property(e => e.Skill)
                    .HasMaxLength(50)
                    .HasColumnName("skill");

                entity.Property(e => e.Validity)
                    .HasColumnName("validity")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Notice>(entity =>
            {
                entity.HasKey(e => e.MessageId);

                entity.Property(e => e.MessageId).HasColumnName("messageId");

                entity.Property(e => e.IsClicked).HasColumnName("isClicked");

                entity.Property(e => e.MemberId).HasColumnName("memberId");

                entity.Property(e => e.NoticeLink)
                    .IsRequired()
                    .HasColumnName("noticeLink");

                entity.Property(e => e.NoticeTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("noticeTitle");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.MemberId).HasColumnName("memberId");

                entity.Property(e => e.OrderTime)
                    .HasColumnType("datetime")
                    .HasColumnName("orderTime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId });

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Sum).HasColumnName("sum");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderId_OrderDetailId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductId_OrderDetailId");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.BrandId).HasColumnName("brandId");

                entity.Property(e => e.ClassId).HasColumnName("classId");

                entity.Property(e => e.Describe)
                    .HasMaxLength(200)
                    .HasColumnName("describe");

                entity.Property(e => e.Discount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("discount");

                entity.Property(e => e.Image)
                    .HasMaxLength(200)
                    .HasColumnName("image");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("productName");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.Property(e => e.Validity).HasColumnName("validity");
            });

            modelBuilder.Entity<Reply>(entity =>
            {
                entity.Property(e => e.ReplyId).HasColumnName("replyId");

                entity.Property(e => e.ArticleId).HasColumnName("articleId");

                entity.Property(e => e.Context)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("context");

                entity.Property(e => e.IsRereply).HasColumnName("isRereply");

                entity.Property(e => e.MemberId).HasColumnName("memberId");

                entity.Property(e => e.NumNice).HasColumnName("numNice");

                entity.Property(e => e.Picture)
                    .HasMaxLength(200)
                    .HasColumnName("picture");

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasColumnName("time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Validity)
                    .IsRequired()
                    .HasColumnName("validity")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Score>(entity =>
            {
                entity.HasKey(e => new { e.ObjectId, e.MemberId });

                entity.Property(e => e.ObjectId).HasColumnName("objectId");

                entity.Property(e => e.MemberId).HasColumnName("memberId");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Score1).HasColumnName("score");

                entity.Property(e => e.Validity).HasColumnName("validity");
            });

            modelBuilder.Entity<SurviceArea>(entity =>
            {
                entity.Property(e => e.SurviceAreaId).HasColumnName("surviceAreaId");

                entity.Property(e => e.AreaId).HasColumnName("areaId");

                entity.Property(e => e.MemberId).HasColumnName("memberId");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.SurviceArea)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_SurviceArea_Member");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}