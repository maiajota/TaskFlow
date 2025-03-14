using TaskFlow.Context;
using TaskFlow.Models;
using TaskFlow.Repositories.Interfaces;
using TaskFlow.ViewModel;

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
            })
            .OrderBy(t => t.DataVencimento);
   } 

   public async Task CadastrarAsync(CadastroTarefaViewModel model)
   {
        if(_context.Tarefas.Any(t => t.Nome == model.Nome)) {
            return;
        } 

        var tarefa = new Tarefa(idUsuario: 1, model.Nome, model.DataVencimento);
        _context.Tarefas.Add(tarefa);

        await _context.SaveChangesAsync();
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
