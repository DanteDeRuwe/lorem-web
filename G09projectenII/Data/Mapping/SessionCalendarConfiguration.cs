using G09projectenII.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G09projectenII.Data.Mapping
{
    public class SessionCalendarConfiguration : IEntityTypeConfiguration<SessionCalendar>
    {
        public void Configure(EntityTypeBuilder<SessionCalendar> builder)
        {
            builder.HasKey(e => e.CalendarId);

            builder.ToTable("SESSIONCALENDAR");

            builder.Property(e => e.CalendarId)
                .HasColumnName("CALENDAR_ID")
                .HasColumnType("numeric(19, 0)");

            builder.Property(e => e.Enddate)
                .HasColumnName("ENDDATE")
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Startdate)
                .HasColumnName("STARTDATE")
                .HasMaxLength(255)
                .IsUnicode(false);
        }
    }
}
