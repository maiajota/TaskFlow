using TaskFlow.Models;

namespace TaskFlow.ViewModel;

public class CadastroTarefaViewModel
{
    public string Nome { get; set; }
    public DateTime DataVencimento { get; set; }
    public IEnumerable<Tarefa> Tarefas { get; set; }
}
