using G09projectenII.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G09projectenII.Data.Mapping
{
    public class SessionRegistreesConfiguration : IEntityTypeConfiguration<SessionRegistrees>
    {
        public void Configure(EntityTypeBuilder<SessionRegistrees> builder)
        {
            builder.HasKey(e => new { e.Id, e.MemberId })
                .HasName("PK__SESSION___693A506CF9DDAA43");

            builder.ToTable("SESSION_REGISTREES");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("numeric(19, 0)");

            builder.Property(e => e.MemberId)
                .HasColumnName("member_id")
                .HasColumnType("numeric(19, 0)");

            builder.HasOne(d => d.Session)
                .WithMany(p => p.SessionRegistrees)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SESSION_REGISTREES_id");

            builder.HasOne(d => d.Member)
                .WithMany(p => p.SessionRegistrees)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SSSONREGISTREESmmberid");
        }
    }
}
