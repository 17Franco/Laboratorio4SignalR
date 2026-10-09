namespace Laboratorio4.Models;

public interface IUsuarioRepository
{
    void Aniadir(Usuario usuario);
    Usuario? Buscar(string email);
    List<Usuario> Listar();
} 
