﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WordReminder.Data;
using WordReminder.Data.Model;

namespace WordReminder.Data.Migrations
{
    [DbContext(typeof(WordReminderContext))]
    [Migration("20180112183435_1")]
    partial class _1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WordReminder.Data.Model.Keyword", b =>
                {
                    b.Property<int>("KeywordId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Word")
                        .IsRequired();

                    b.HasKey("KeywordId");

                    b.ToTable("Keyword");
                });

            modelBuilder.Entity("WordReminder.Data.Model.KeywordMeaning", b =>
                {
                    b.Property<int>("KeywordMeaningId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("KeywordId");

                    b.Property<int>("KeywordType");

                    b.Property<string>("Word")
                        .IsRequired();

                    b.HasKey("KeywordMeaningId");

                    b.HasIndex("KeywordId");

                    b.ToTable("KeywordMeaning");
                });

            modelBuilder.Entity("WordReminder.Data.Model.KeywordMeaningSentence", b =>
                {
                    b.Property<int>("KeywordMeaningSentenceId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("KeywordMeaningId");

                    b.Property<string>("Sentence")
                        .IsRequired();

                    b.HasKey("KeywordMeaningSentenceId");

                    b.HasIndex("KeywordMeaningId");

                    b.ToTable("KeywordMeaningSentence");
                });

            modelBuilder.Entity("WordReminder.Data.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Fullname");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Password");

                    b.Property<int>("UserType");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WordReminder.Data.Model.UserWord", b =>
                {
                    b.Property<int>("UserWordId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("KeywordId");

                    b.Property<int>("UserId");

                    b.HasKey("UserWordId");

                    b.ToTable("UserWord");
                });

            modelBuilder.Entity("WordReminder.Data.Model.KeywordMeaning", b =>
                {
                    b.HasOne("WordReminder.Data.Model.Keyword", "Keyword")
                        .WithMany("Meanings")
                        .HasForeignKey("KeywordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WordReminder.Data.Model.KeywordMeaningSentence", b =>
                {
                    b.HasOne("WordReminder.Data.Model.KeywordMeaning", "KeywordMeaning")
                        .WithMany("KeywordMeaningSentences")
                        .HasForeignKey("KeywordMeaningId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
