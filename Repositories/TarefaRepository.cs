using TaskFlow.Context;
using TaskFlow.Models;
using TaskFlow.Repositories.Interfaces;

namespace TaskFlow.Repositories;

public class TarefaRepository : ITarefaRepository
{
    private readonly AppDbContext _context;

    public TarefaRepository(AppDbContext context)
    {
        _context = context;
    }
   public IEnumerable<Tarefa> Buscar()
   {
        return _context.Tarefas
            .Select(t => new Tarefa
            {
                Id = t.Id,
                Nome = t.Nome,
                DataVencimento = t.DataVencimento,
                Urgente = t.Urgente
            })
            .OrderBy(t => t.DataVencimento);
   }

   public bool NomeJaExiste(string nome)
   {
    if(_context.Tarefas.Any(t => t.Nome == nome))
        return true;

    return false;
   }

   public async Task<bool> CadastrarAsync(string nome, DateTime dataVencimento, bool urgente)
   {
        if(NomeJaExiste(nome))
            return false;
        
        var tarefa = new Tarefa(idUsuario: 1, nome, dataVencimento, urgente);
        _context.Tarefas.Add(tarefa);

        await _context.SaveChangesAsync();

        return true;
   }

   public async Task ExcluirAsync(int id)
   {
        var tarefa = _context.Tarefas
            .FirstOrDefault(t => t.Id == id);

        _context.Tarefas.Remove(tarefa);

        await _context.SaveChangesAsync();
   }

   public async Task EditarAsync(int id, string nome, DateTime dataVencimento)
   {
        var tarefa = _context.Tarefas
            .FirstOrDefault(t => t.Id == id);

        tarefa.Nome = nome;
        tarefa.DataVencimento = dataVencimento;

        _context.Tarefas.Update(tarefa);

        await _context.SaveChangesAsync();  
   }
}
