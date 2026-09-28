namespace AulaAuth01.Models;

public class Usuario
{
    public string User { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }

    public Usuario()
    {
        
    }

    public Usuario(string user, string password, string role)
    {
        User = user;
        Password = password;
        Role = role;
    }
}