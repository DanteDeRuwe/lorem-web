using G09projectenII.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G09projectenII.Data.Mapping
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("MEMBER");

            builder.Property(e => e.MemberId)
                .HasColumnName("MEMBER_ID")
                .HasColumnType("numeric(19, 0)");

            builder.Property(e => e.Firstname)
                .HasColumnName("FIRSTNAME")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Lastname)
                .HasColumnName("LASTNAME")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Memberstatus).HasColumnName("MEMBERSTATUS");

            builder.Property(e => e.Membertype).HasColumnName("MEMBERTYPE");

            builder.Property(e => e.Password)
                .HasColumnName("PASSWORD")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Profilepicpath)
                .HasColumnName("PROFILEPICPATH")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Username)
                .HasColumnName("USERNAME")
                .HasMaxLength(255)
                .IsUnicode(false);
        }
    }
}
