﻿namespace G09projectenII.Models
{
    public class SessionRegistrees
    {
        public decimal Id { get; set; }
        public decimal MemberId { get; set; }

        public virtual Session Session { get; set; }
        public virtual Member Member { get; set; }
    }
}
