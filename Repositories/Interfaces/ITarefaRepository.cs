using TaskFlow.Models;
using TaskFlow.ViewModel;

namespace TaskFlow.Repositories.Interfaces;

public interface ITarefaRepository
{
    public IEnumerable<Tarefa> Buscar();
    public Task CadastrarAsync(CadastroTarefaViewModel model);
    public Task ExcluirAsync(int id);
}
