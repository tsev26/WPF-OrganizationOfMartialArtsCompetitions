﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OOMAC.EF;

namespace OOMAC.EF.Migrations
{
    [DbContext(typeof(OOMACDBContext))]
    [Migration("20220312183742_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BracketMatch", b =>
                {
                    b.Property<int>("BracketsId")
                        .HasColumnType("int");

                    b.Property<int>("MatchesId")
                        .HasColumnType("int");

                    b.HasKey("BracketsId", "MatchesId");

                    b.HasIndex("MatchesId");

                    b.ToTable("BracketMatch");
                });

            modelBuilder.Entity("ContestantTournament", b =>
                {
                    b.Property<int>("ContestansId")
                        .HasColumnType("int");

                    b.Property<int>("TournamentsId")
                        .HasColumnType("int");

                    b.HasKey("ContestansId", "TournamentsId");

                    b.HasIndex("TournamentsId");

                    b.ToTable("ContestantTournament");
                });

            modelBuilder.Entity("MatchPool", b =>
                {
                    b.Property<int>("MatchesId")
                        .HasColumnType("int");

                    b.Property<int>("PoolsId")
                        .HasColumnType("int");

                    b.HasKey("MatchesId", "PoolsId");

                    b.HasIndex("PoolsId");

                    b.ToTable("MatchPool");
                });

            modelBuilder.Entity("OOMAC.Domain.Models.Bracket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BrcId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Round")
                        .HasColumnType("int");

                    b.Property<int>("TournamentId")
                        .HasColumnType("int")
                        .HasColumnName("BrcTrnId");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("Bracker");
                });

            modelBuilder.Entity("OOMAC.Domain.Models.Contestant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ConId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int")
                        .HasColumnName("ConAge");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ConEmail");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ConFName");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ConLName");

                    b.Property<int>("TechnicalSkill")
                        .HasColumnType("int")
                        .HasColumnName("ConTechnicalSkill");

                    b.HasKey("Id");

                    b.ToTable("Contestant");
                });

            modelBuilder.Entity("OOMAC.Domain.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MatId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("WinnerId")
                        .HasColumnType("int")
                        .HasColumnName("MatWinId");

                    b.HasKey("Id");

                    b.HasIndex("WinnerId");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("OOMAC.Domain.Models.MatchupEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MaeId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContestantId")
                        .HasColumnType("int")
                        .HasColumnName("MaeConId");

                    b.Property<int?>("MatchId")
                        .HasColumnType("int");

                    b.Property<string>("ScoreString")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MaeScore");

                    b.HasKey("Id");

                    b.HasIndex("ContestantId");

                    b.HasIndex("MatchId");

                    b.ToTable("MatchupEntry");
                });

            modelBuilder.Entity("OOMAC.Domain.Models.Pool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PolId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TournamentId")
                        .HasColumnType("int")
                        .HasColumnName("PolTrnId");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("Pool");
                });

            modelBuilder.Entity("OOMAC.Domain.Models.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TrnId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaxAge")
                        .HasColumnType("int")
                        .HasColumnName("TrnMaxAge");

                    b.Property<int>("MaxTechnicalSkill")
                        .HasColumnType("int")
                        .HasColumnName("TrnMaxTechSkill");

                    b.Property<int>("MinAge")
                        .HasColumnType("int")
                        .HasColumnName("TrnMinAge");

                    b.Property<int>("MinTechnicalSkill")
                        .HasColumnType("int")
                        .HasColumnName("TrnMinTechSkill");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TrnName");

                    b.HasKey("Id");

                    b.ToTable("Tournament");
                });

            modelBuilder.Entity("BracketMatch", b =>
                {
                    b.HasOne("OOMAC.Domain.Models.Bracket", null)
                        .WithMany()
                        .HasForeignKey("BracketsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OOMAC.Domain.Models.Match", null)
                        .WithMany()
                        .HasForeignKey("MatchesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ContestantTournament", b =>
                {
                    b.HasOne("OOMAC.Domain.Models.Contestant", null)
                        .WithMany()
                        .HasForeignKey("ContestansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OOMAC.Domain.Models.Tournament", null)
                        .WithMany()
                        .HasForeignKey("TournamentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MatchPool", b =>
                {
                    b.HasOne("OOMAC.Domain.Models.Match", null)
                        .WithMany()
                        .HasForeignKey("MatchesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OOMAC.Domain.Models.Pool", null)
                        .WithMany()
                        .HasForeignKey("PoolsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OOMAC.Domain.Models.Bracket", b =>
                {
                    b.HasOne("OOMAC.Domain.Models.Tournament", "Tournament")
                        .WithMany("Brackets")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("OOMAC.Domain.Models.Match", b =>
                {
                    b.HasOne("OOMAC.Domain.Models.Contestant", "Winner")
                        .WithMany()
                        .HasForeignKey("WinnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Winner");
                });

            modelBuilder.Entity("OOMAC.Domain.Models.MatchupEntry", b =>
                {
                    b.HasOne("OOMAC.Domain.Models.Contestant", "Contestant")
                        .WithMany()
                        .HasForeignKey("ContestantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OOMAC.Domain.Models.Match", null)
                        .WithMany("Entries")
                        .HasForeignKey("MatchId");

                    b.Navigation("Contestant");
                });

            modelBuilder.Entity("OOMAC.Domain.Models.Pool", b =>
                {
                    b.HasOne("OOMAC.Domain.Models.Tournament", "Tournament")
                        .WithMany("Pools")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("OOMAC.Domain.Models.Match", b =>
                {
                    b.Navigation("Entries");
                });

            modelBuilder.Entity("OOMAC.Domain.Models.Tournament", b =>
                {
                    b.Navigation("Brackets");

                    b.Navigation("Pools");
                });
#pragma warning restore 612, 618
        }
    }
}
