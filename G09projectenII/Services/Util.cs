using G09projectenII.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace G09projectenII.Services
{
    public static class Util
    {

        public static ValueConverter<DateTime, string> DateConverter =>
            // Gives a value converter for dates to string (for use in the mappers)
            new ValueConverter<DateTime, string>(
                v => v.ToString("O"),
                v => DateTime.Parse(v)
            );

        public static string ExtraSessionInfo(Session session) => session.SessionState switch
        {
            CreatedSessionState css => $"{session.Capacity} inschrijvingsplaatsen",
            OpenSessionState oss => $"{session.NumberOfRegistrees}/{session.Capacity} inschrijvingen",
            ClosedSessionState css => $"{session.NumberOfAttendees}/{session.NumberOfRegistrees} ingeschrevenen aanwezig",
            FinishedSessionState fss => $"{session.NumberOfAttendees}/{session.NumberOfRegistrees} ingeschrevenen aanwezig",
            _ => ""
        };
    }
}
