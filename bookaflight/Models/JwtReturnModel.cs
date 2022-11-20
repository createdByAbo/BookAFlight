namespace BookAFlight.Models;

public class JwtReturnModel
{
    public string? Jwt { get; }
    public DateTime Expirity { get; }

    public JwtReturnModel(string jwt, string expirity)
    {
        Jwt = jwt;
        Expirity = DateTime.Parse(expirity);
    }
}