using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Models;
using TaskFlow.Models.Enums;
using TaskFlow.Repositories.Interfaces;

namespace TaskFlow.Controllers;

public class HomeController : Controller
{

    private readonly ITarefaRepository _tarefaRepository;

    public HomeController(ITarefaRepository tarefaRepository)
    {
        _tarefaRepository = tarefaRepository;
    }

    public IActionResult Index()
    {
        var tarefas = _tarefaRepository.Buscar();

        return View(tarefas);
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarTarefa(string nome, DateTime dataVencimento, bool urgente)
    {
        if(!ModelState.IsValid)
            return Json(new { success = StatusEnum.DadosInvalidos, message = "Dados inválidos." });

        var dataAtual = DateTime.Now;
        var dataAtualFormatada = new DateTime(dataAtual.Year, dataAtual.Month, dataAtual.Day);

        if(dataVencimento < dataAtualFormatada)
            return Json(new { status = StatusEnum.DataInvalida, message = "A data de vencimento está inválida." });

        if(_tarefaRepository.NomeJaExiste(nome))
            return Json(new {status = StatusEnum.NomeExistente, message = "O nome da tarefa já existe." });
                
        var resultado = await _tarefaRepository.CadastrarAsync(nome, dataVencimento, urgente);

        if(!resultado)
            return Json(new { status = StatusEnum.Erro, message = "Não foi possível cadastrar a tarefa." });

        return Json(new { status = StatusEnum.Sucesso, message = "Tarefa criada com sucesso." });
    }

    [HttpPost]
    public async Task<IActionResult> ExcluirTarefa(int id)
    {
        try
        {
            var teste = id;
            await _tarefaRepository.ExcluirAsync(id);
            return RedirectToAction("Index");
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditarTarefa(int id, string nome, DateTime dataVencimento, bool urgente)
    {
        if(!ModelState.IsValid)
            return Json(new { success = StatusEnum.DadosInvalidos, message = "Dados inválidos." });

        if(_tarefaRepository.NomeJaExiste(nome, id))
            return Json(new { status = StatusEnum.NomeExistente, message = "O nome da tarefa já existe." });

        await _tarefaRepository.EditarAsync(id, nome, dataVencimento, urgente);

        return Json(new { status = StatusEnum.Sucesso, message = "Tarefa editada com sucesso." });
    
    }

    [HttpGet]
    public IActionResult AtualizarTabelaTarefas()
    {
        var tarefas = _tarefaRepository.Buscar();

        return PartialView("_TabelaTarefas", tarefas);
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
