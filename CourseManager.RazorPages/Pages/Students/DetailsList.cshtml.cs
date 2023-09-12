using CourseManager.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CourseManager.RazorPages.Pages.Students;
public class DetailsList : PageModel
{
    [BindProperty]
    public StudentModel Student { get; set; } = new();
    public List<CourseModel> Courses { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Uri uri = new($"http://localhost:5259/api/Student/GetStudent/{id}/Courses");
        using HttpResponseMessage response = await new HttpClient().GetAsync(uri);
        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            Courses = JsonConvert.DeserializeObject<List<CourseModel>>(json) ?? new();
            return Page();
        }
        return RedirectToPage("View");
    }
}