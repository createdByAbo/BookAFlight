namespace BookAFlight.Exceptions
{
    public class UserNotActivated : Exception
    {
        public UserNotActivated(string message) : base(message) { }
    }
}