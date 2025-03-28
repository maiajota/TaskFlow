using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskFlow.Models;

[Table("Tarefa")]
public class Tarefa
{
    public Tarefa(int idUsuario, string nome, DateTime dataVencimento, bool urgente)
    {
        IdUsuario = idUsuario;
        Nome = nome;
        DataCriacao = DateTime.Now;
        DataVencimento = dataVencimento;
        Urgente = urgente;
    }

    public Tarefa()
    {
    }

    [Key]
    public int Id { get; set; }

    [ForeignKey("Usuario")]
    [Column("IdUsuario")]
    public int IdUsuario { get; set; }
    public virtual Usuario Usuario { get; set; }

    [Required]
    [StringLength(50)]
    public string Nome { get; set; }

    [Required]
    public DateTime DataCriacao { get; set; }

    [Required]
    public DateTime DataVencimento { get; set; }

    [Required]
    [Column(TypeName = "bit")]
    public bool Urgente { get; set; } = false;
}
