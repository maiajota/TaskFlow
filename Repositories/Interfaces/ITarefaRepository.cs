using TaskFlow.Models;

namespace TaskFlow.Repositories.Interfaces;

public interface ITarefaRepository
{
    public IEnumerable<Tarefa> Buscar();
    public bool NomeJaExiste(string nome);
    public Task<bool> CadastrarAsync(string nome, DateTime dataVencimento);
    public Task ExcluirAsync(int id);
    public Task EditarAsync(int id,string nome, DateTime dataVencimento);
}
