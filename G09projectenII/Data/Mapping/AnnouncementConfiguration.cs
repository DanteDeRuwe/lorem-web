using G09projectenII.Models;
using G09projectenII.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G09projectenII.Data.Mapping
{
    public class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
    {
        public void Configure(EntityTypeBuilder<Announcement> builder)
        {
            builder.ToTable("ANNOUNCEMENT");

            builder.Property(e => e.AnnouncementId)
                .HasColumnName("ANNOUNCEMENT_ID")
                .HasColumnType("numeric(19, 0)");

            builder.Property(e => e.AuthorMemberId)
                .HasColumnName("AUTHOR_MEMBER_ID")
                .HasColumnType("numeric(19, 0)");

            builder.Property(e => e.SessionId)
                .HasColumnName("session_id")
                .HasColumnType("numeric(19, 0)");

            builder.Property(e => e.Text)
                .HasColumnName("TEXT")
                .HasColumnType("text");

            builder.Property(e => e.Timestamp)
                .HasColumnName("TIMESTAMP")
                .HasMaxLength(255)
                .HasConversion(Util.DateConverter)
                .IsUnicode(false);

            builder.Property(e => e.Title)
                .HasColumnName("TITLE")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.HasOne(d => d.Author)
                .WithMany(p => p.Announcements)
                .HasForeignKey(d => d.AuthorMemberId);

            builder.HasOne(d => d.Session)
                .WithMany(p => p.Announcements)
                .HasForeignKey(d => d.SessionId);
        }
    }
}
