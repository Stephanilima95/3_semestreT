using FilmesTorloni.WebAPI.BdContextFilme;
using FilmesTorloni.WebAPI.Interfaces;
using FilmesTorloni.WebAPI.Models;
using FilmesTorloni.WebAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace FilmesTorloni.WebAPI.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly FilmeContext _context;
    private bool senhaValida;

    public UsuarioRepository(FilmeContext context)
        {
            _context = context;
    }

    public void Cadastrar(Usuario novoUsuario)
    {
        try
        {
            novoUsuario.IdUsuario = Guid.NewGuid().ToString();
            novoUsuario.Senha = Criptografia.GerarHash
                (novoUsuario.Senha!);

            _context.Usuarios.Add(novoUsuario);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            // Lidar com a exceção, por exemplo, logar o erro
            Console.WriteLine($"Erro ao cadastrar usuário: {ex.Message}");
            throw; // Re-throw a exceção para que possa ser tratada em outro lugar, se necessário
        }
    }

    public Usuario BuscarPorId(Guid id)
    {
        try
        {
            Usuario usuarioBuscado = _context.Usuarios.Find(id.ToString())!;
            if(usuarioBuscado != null)
            {
                return usuarioBuscado;
            }
            return null!;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public Usuario BuscarPorEmailSenha(string email, string senha)
    {
        try
        {
            Usuario usuarioBuscado = _context.Usuarios.FirstOrDefault(u => u.Email == email)!;

            if(usuarioBuscado != null)
            {     bool confere = Criptografia.CompararHash(senha, usuarioBuscado.Senha!);

                if (confere)
            {
                return usuarioBuscado;
            }

        }

            return null!;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
