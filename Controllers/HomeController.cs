using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Models;
using TaskFlow.Repositories.Interfaces;
using TaskFlow.ViewModel;

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
        var tarefaViewModel = new CadastroTarefaViewModel();
        tarefaViewModel.Tarefas = _tarefaRepository.Buscar();

        return View(tarefaViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarTarefa(CadastroTarefaViewModel model)
    {
        if(!ModelState.IsValid)
            return View(model);
                
        await _tarefaRepository.CadastrarAsync(model);
        return RedirectToAction("Index");
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

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
