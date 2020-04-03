using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace G09projectenII.Services
{
    public class Util
    {
        public static ValueConverter<DateTime, string> DateConverter() => new ValueConverter<DateTime, string>(
    v => v.ToString("O"),
    v => DateTime.Parse(v));
    }
}
