using G09projectenII.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace G09projectenII.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Announcement> Announcement { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<SessionAttendees> SessionAttendees { get; set; }
        public virtual DbSet<SessionRegistrees> SessionRegistrees { get; set; }
        public virtual DbSet<SessionCalendar> SessionCalendars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.ToTable("ANNOUNCEMENT");

                entity.Property(e => e.AnnouncementId)
                    .HasColumnName("ANNOUNCEMENT_ID")
                    .HasColumnType("numeric(19, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AuthorMemberId)
                    .HasColumnName("AUTHOR_MEMBER_ID")
                    .HasColumnType("numeric(19, 0)");

                entity.Property(e => e.SessionId)
                    .HasColumnName("session_id")
                    .HasColumnType("numeric(19, 0)");

                entity.Property(e => e.Text)
                    .HasColumnName("TEXT")
                    .HasColumnType("text");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("TIMESTAMP")
                    .HasMaxLength(255)
                    .HasConversion(Util.DateConverter())
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasColumnName("TITLE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Announcements)
                    .HasForeignKey(d => d.AuthorMemberId)
                    .HasConstraintName("NNUNCEMENTTHORMEMBERID");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.Announcements)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ANNOUNCEMENTsession_id");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("MEMBER");

                entity.Property(e => e.MemberId)
                    .HasColumnName("MEMBER_ID")
                    .HasColumnType("numeric(19, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Firstname)
                    .HasColumnName("FIRSTNAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasColumnName("LASTNAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Memberstatus).HasColumnName("MEMBERSTATUS");

                entity.Property(e => e.Membertype).HasColumnName("MEMBERTYPE");

                entity.Property(e => e.Password)
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Profilepicpath)
                    .HasColumnName("PROFILEPICPATH")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("SESSION");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("numeric(19, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CalendarId)
                    .HasColumnName("calendar_id")
                    .HasColumnType("numeric(19, 0)");

                entity.Property(e => e.Capacity).HasColumnName("CAPACITY");

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasColumnType("text");

                entity.Property(e => e.Endtime)
                    .HasColumnName("ENDTIME")
                    .HasMaxLength(255)
                    .HasConversion(Util.DateConverter())
                    .IsUnicode(false);

                entity.Property(e => e.Externallink)
                    .HasColumnName("EXTERNALLINK")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasColumnName("LOCATION")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MemberId)
                    .HasColumnName("member_id")
                    .HasColumnType("numeric(19, 0)");

                entity.Property(e => e.SessionState)
                    .HasColumnName("SESSIONSTATUS")
                    .HasConversion(
                        state => state.toInt(),
                        storedInt => SessionState.fromInt(storedInt)
                    );

                entity.Property(e => e.Speakername)
                    .HasColumnName("SPEAKERNAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Starttime)
                    .HasColumnName("STARTTIME")
                    .HasMaxLength(255)
                    .HasConversion(Util.DateConverter())
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasColumnName("TITLE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("TYPE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Calendar)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.CalendarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SESSION_calendar_id");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SESSION_member_id");
            });

            modelBuilder.Entity<SessionAttendees>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.Id,
                    e.MemberId
                })
                    .HasName("PK__SESSION___693A506C9E0B59CA");

                entity.ToTable("SESSION_ATTENDEES");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(19, 0)");

                entity.Property(e => e.MemberId)
                                .HasColumnName("member_id")
                                .HasColumnType("numeric(19, 0)");

                entity.HasOne(d => d.Session)
                                .WithMany(p => p.SessionAttendees)
                                .HasForeignKey(d => d.Id)
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("SESSION_ATTENDEES_id");

                entity.HasOne(d => d.Member)
                                .WithMany(p => p.SessionAttendees)
                                .HasForeignKey(d => d.MemberId)
                                .OnDelete(DeleteBehavior.ClientSetNull)
                                .HasConstraintName("SSSIONATTENDEESmmberid");
            });

            modelBuilder.Entity<SessionRegistrees>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.MemberId })
                    .HasName("PK__SESSION___693A506CF9DDAA43");

                entity.ToTable("SESSION_REGISTREES");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(19, 0)");

                entity.Property(e => e.MemberId)
                                    .HasColumnName("member_id")
                                    .HasColumnType("numeric(19, 0)");

                entity.HasOne(d => d.Session)
                                    .WithMany(p => p.SessionRegistrees)
                                    .HasForeignKey(d => d.Id)
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName("SESSION_REGISTREES_id");

                entity.HasOne(d => d.Member)
                                    .WithMany(p => p.SessionRegistrees)
                                    .HasForeignKey(d => d.MemberId)
                                    .OnDelete(DeleteBehavior.ClientSetNull)
                                    .HasConstraintName("SSSONREGISTREESmmberid");
            });

            modelBuilder.Entity<SessionCalendar>(entity =>
            {
                entity.HasKey(e => e.CalendarId)
                    .HasName("PK__SESSIONC__EB730BD478604F52");

                entity.ToTable("SESSIONCALENDAR");

                entity.Property(e => e.CalendarId)
                    .HasColumnName("CALENDAR_ID")
                    .HasColumnType("numeric(19, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Enddate)
                                    .HasColumnName("ENDDATE")
                                    .HasMaxLength(255)
                                    .IsUnicode(false);

                entity.Property(e => e.Startdate)
                                    .HasColumnName("STARTDATE")
                                    .HasMaxLength(255)
                                    .IsUnicode(false);
            });

        }
    }
}
