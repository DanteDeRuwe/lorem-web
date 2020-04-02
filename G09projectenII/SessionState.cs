namespace G09projectenII.Models
{
    public abstract class SessionState
    {

        /// <summary>
        /// For storing in the database
        /// </summary>
        public int toInt() =>
            this switch
            {
                CreatedSessionState s => 0,
                OpenSessionState s => 1,
                ClosedSessionState s => 2,
                FinishedSessionState s => 3,
            };

        /// <summary>
        /// For instantiating the correct state from the database
        /// </summary>
        public static SessionState fromInt(int v) =>
            v switch
            {
                0 => new CreatedSessionState(),
                1 => new OpenSessionState(),
                2 => new ClosedSessionState(),
                3 => new FinishedSessionState()
            };
    }
}