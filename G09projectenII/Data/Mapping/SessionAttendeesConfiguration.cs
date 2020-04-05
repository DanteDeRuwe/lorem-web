using G09projectenII.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G09projectenII.Data.Mapping
{
    public class SessionAttendeesConfiguration : IEntityTypeConfiguration<SessionAttendees>
    {
        public void Configure(EntityTypeBuilder<SessionAttendees> builder)
        {
            builder.HasKey(e => new
            {
                e.Id,
                e.MemberId
            })
                .HasName("PK__SESSION___693A506C9E0B59CA");

            builder.ToTable("SESSION_ATTENDEES");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("numeric(19, 0)");

            builder.Property(e => e.MemberId)
                .HasColumnName("member_id")
                .HasColumnType("numeric(19, 0)");

            builder.HasOne(d => d.Session)
                .WithMany(p => p.SessionAttendees)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SESSION_ATTENDEES_id");

            builder.HasOne(d => d.Member)
                .WithMany(p => p.SessionAttendees)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SSSIONATTENDEESmmberid");
        }
    }
}
