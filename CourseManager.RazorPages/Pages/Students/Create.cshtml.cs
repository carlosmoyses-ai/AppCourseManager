using System.Text;
using CourseManager.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CourseManager.RazorPages.Pages.Students;
public class Create : PageModel
{
    [BindProperty]
    public StudentModel Students { get; set; } = new();

    public async Task<IActionResult> OnPostAsync()
    {
        Uri uri = new("http://localhost:5259/api/Student/PostStudent");
        string json = JsonConvert.SerializeObject(Students);
        Students.EnrollmentDate = DateTime.Now;
        using StringContent content = new(json, Encoding.UTF8, "application/json");
        using HttpResponseMessage response = await new HttpClient().PostAsync(uri, content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage("View");
        }
        return Page();
    }
}
