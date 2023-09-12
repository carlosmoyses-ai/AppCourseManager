using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CourseManager.RazorPages.Models;

public class CourseModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "Id do Curso")]
    public int CourseId { get; set; }

    [Required]
    [MaxLength(200)]
    [Display(Name = "Nome do Curso", Prompt = "Insira o nome do curso")]
    public string? CourseName { get; set; }
    
    [MaxLength(500)]
    [Display(Name = "Descrição do Curso", Prompt = "Insira a descrição do curso")]
    public string? Description { get; set; }

    [Required]
    [DataType(DataType.Date, ErrorMessage = "Insira uma data válida no formato dd/mm/yyyy")]
    [Display(Name = "Data de Início", Prompt = "Insira a data de início do curso")]
    public DateTime StartDate { get; set; }
    [Required]
    [DataType(DataType.Date, ErrorMessage = "Insira uma data válida no formato dd/mm/yyyy")]
    [Display(Name = "Data de Término", Prompt = "Insira a data de término do curso")]
    public DateTime EndDate { get; set; }

    [JsonIgnore]
    [Display(Name = "Alunos Matriculados", Prompt = "Insira os alunos matriculados no curso")]
    public List<StudentModel>? Students { get; set; } = new();
}