using EventPlusTorloni.WebAPI.BdContextEvent;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;
using EventPlusTorloni.WebAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace EventPlusTorloni.WebAPI.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly EventContext _context;

    // Construtor para injetar o contexto do banco de dados
    public UsuarioRepository(EventContext context)
    {
        _context = context;
    }


    /// <summary>
    /// Retrieves a user matching the specified email and password, if such a user exists.
    /// </summary>
    /// <remarks>The password is compared using a secure hash verification. If no user with the specified
    /// email exists or the password does not match, the method returns <see langword="null"/>.</remarks>
    /// <param name="email">The email address of the user to search for. Cannot be null or empty.</param>
    /// <param name="senha">The password to verify for the user with the specified email. Cannot be null or empty.</param>
    /// <returns>A <see cref="Usuario"/> object representing the user if the email and password match; otherwise, <see
    /// langword="null"/>.</returns>
    public Usuario BuscarPorEmailESenha(string email, string senha, Guid IdTipoUsuario)
    {

        var usuarioBuscado = _context.Usuarios
            .Include(Usuario => Usuario.IdTipoUsuarioNavigation)
            .FirstOrDefault(usuario => usuario.Email == email);
        if (usuarioBuscado != null)
        {
            bool confere = Criptografia.ComprarHash(senha, usuarioBuscado.Senha);

            if (confere)
            {               
                return usuarioBuscado;
            }
        }
        return null!;

    }

    /// <summary>
    /// Retrieves a user entity by its unique identifier.
    /// </summary>
    /// <remarks>The returned user includes related data from the user type navigation property. If no user
    /// with the specified identifier exists, the method returns null.</remarks>
    /// <param name="id">The unique identifier of the user to retrieve.</param>
    /// <returns>The user entity that matches the specified identifier, or null if no user is found.</returns>
    public Usuario BuscarPorId(Guid id)
    {
        return _context.Usuarios
            .Include(Usuario => Usuario.IdTipoUsuarioNavigation)
            .FirstOrDefault(usuario => usuario.IdUsuario == id)!;
    }

    /// <summary>
    /// Registers a new user in the system by securely hashing the user's password before saving.
    /// </summary>
    /// <remarks>The method hashes the user's password using a secure algorithm before persisting the user to
    /// the database. Ensure that the provided user object contains all required fields for registration. This method
    /// commits the changes immediately to the underlying data store.</remarks>
    /// <param name="usuario">The user to be registered. The user's password must be provided in plain text; it will be hashed before storage.
    /// Cannot be null.</param>
    public void Cadastrar(Usuario usuario)
    {
        usuario.Senha = Criptografia.GerarHash(usuario.Senha);

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }
}
