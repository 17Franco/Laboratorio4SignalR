namespace Laboratorio4.Models;

public class Usuario
{
    public String Email { get; set; }
    public String Password { get; set; }


    public Usuario(String email, String pass) {
        this.Email = email;
        this.Password = pass;
    }

   
}