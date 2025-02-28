using Microsoft.EntityFrameworkCore;
using TaskFlow.Models;

namespace TaskFlow.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }
}
