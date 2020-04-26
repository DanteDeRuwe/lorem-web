using G09projectenII.Models;
using G09projectenII.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G09projectenII.Data.Mapping
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("SESSION");

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasColumnType("numeric(19, 0)")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CalendarId)
                .HasColumnName("calendar_id")
                .HasColumnType("numeric(19, 0)");

            builder.Property(e => e.Capacity).HasColumnName("CAPACITY");

            builder.Property(e => e.Description)
                .HasColumnName("DESCRIPTION")
                .HasColumnType("text");

            builder.Property(e => e.EndDateTime)
                .HasColumnName("ENDTIME")
                .HasMaxLength(255)
                .HasConversion(Util.DateConverter())
                .IsUnicode(false);

            builder.Property(e => e.Externallink)
                .HasColumnName("EXTERNALLINK")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Location)
                .HasColumnName("LOCATION")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.MemberId)
                .HasColumnName("member_id")
                .HasColumnType("numeric(19, 0)");

            builder.Property(e => e.SessionState)
                .HasColumnName("SESSIONSTATUS")
                .HasConversion(
                    state => state.ToInt(),
                    storedInt => SessionState.FromInt(storedInt)
                );

            builder.Property(e => e.Speakername)
                .HasColumnName("SPEAKERNAME")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.StartDateTime)
                .HasColumnName("STARTTIME")
                .HasMaxLength(255)
                .HasConversion(Util.DateConverter())
                .IsUnicode(false);

            builder.Property(e => e.Title)
                .HasColumnName("TITLE")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Type)
                .HasColumnName("TYPE")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.HasOne(d => d.Calendar)
                .WithMany(p => p.Sessions)
                .HasForeignKey(d => d.CalendarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SESSION_calendar_id");

            builder.HasOne(d => d.Member)
                .WithMany(p => p.Sessions)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SESSION_member_id");
        }
    }
}
