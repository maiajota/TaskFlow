using TaskFlow.Models;

namespace TaskFlow.Repositories.Interfaces;

public interface IUsuarioRepository
{
    public IEnumerable<Usuario> Buscar();
    public Task Criar(Usuario usuario);
    public Task Excluir(int id);
}
