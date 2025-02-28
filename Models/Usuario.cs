using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskFlow.Models;

[Table("Usuario")]
public class Usuario
{
    [Key]
    public int Id { get; set ; }

    [Required]
    [StringLength(120)]
    public string Email { get; set; }

    [Required]
    [StringLength(50)]
    public string Nome { get; set; }

    [Required]
    [StringLength(50)]
    public string Sobrenome { get; set; }

    [Required]
    [StringLength(60)]
    public string Senha { get; set; }

    [Required]
    public DateTime DataCriacao { get; set; }
} 
