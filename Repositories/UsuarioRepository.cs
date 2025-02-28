using TaskFlow.Context;
using TaskFlow.Models;
using TaskFlow.Repositories.Interfaces;

namespace TaskFlow.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Usuario> Buscar()
    {
        return _context.Usuarios
            .Select(u => new Usuario
            {
                Nome = u.Nome,
                Sobrenome = u.Sobrenome,
                Email = u.Email
            });
    }

    public async Task Criar(Usuario usuario)
    {
        usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

        _context.Usuarios.Add(usuario);

        await _context.SaveChangesAsync();
    }

    public async Task Excluir(int id)
    {
        var usuario = _context.Usuarios
            .Where(u => u.Id == id)
            .FirstOrDefault();

        _context.Usuarios.Remove(usuario);

        await _context.SaveChangesAsync();
    }
}
