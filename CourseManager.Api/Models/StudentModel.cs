using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CourseManager.Api.Models;

public class StudentModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "Id do Aluno")]
    public int StudentId { get; set; }

    [Required]
    [MaxLength(200)]
    [Display(Name = "Nome do Aluno", Prompt = "Insira o nome do aluno")]
    public string? StudentName { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Endereço de email inválido")]
    [MaxLength(200)]
    [Display(Name = "Email", Prompt = "Insira o email do aluno")]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Date, ErrorMessage = "Insira uma data válida no formato dd/mm/yyyy")]
    [Display(Name = "Data de Matrícula", Prompt = "Insira a data de matrícula do aluno")]
    public DateTime EnrollmentDate { get; set; }

    [JsonIgnore]
    [Display(Name = "Cursos Matriculados", Prompt = "Insira os cursos matriculados pelo aluno")]
    public List<CourseModel>? Courses { get; set; } = new();
}