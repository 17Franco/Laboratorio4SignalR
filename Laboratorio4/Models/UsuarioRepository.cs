namespace Laboratorio4.Models;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly List<Usuario> _usuarios = new()
    {
        new Usuario("fran@example.com", "1234"),
        new Usuario("ana@example.com", "1234"),
        new Usuario("juan@example.com", "1234")
    };

    public void Aniadir(Usuario usuario)
    {
        _usuarios.Add(usuario);
    }

    public Usuario? Buscar(string email)
    {
        return _usuarios.FirstOrDefault(usuario =>
            string.Equals(usuario.Email, email,
                StringComparison.OrdinalIgnoreCase));
    }

    public List<Usuario> Listar()
    {
        return _usuarios.ToList();
    }
}