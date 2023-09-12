using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseManager.RazorPages.Models;
using Newtonsoft.Json;

namespace CourseManager.RazorPages.Pages.Students;

public class Details : PageModel
{
    [BindProperty]
    public StudentModel Student { get; set; } = new();
    public IList<CourseModel> Courses { get; set; } = new List<CourseModel>();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Uri uri = new($"http://localhost:5259/api/Student/GetStudent/{id}");
        Uri uri2 = new(uri.OriginalString + "/Courses");
        using HttpResponseMessage response = await new HttpClient().GetAsync(uri);
        using HttpResponseMessage response2 = await new HttpClient().GetAsync(uri2);
        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            string json2 = await response2.Content.ReadAsStringAsync();
            Student = JsonConvert.DeserializeObject<StudentModel>(json) ?? new();
            Courses = JsonConvert.DeserializeObject<List<CourseModel>>(json2) ?? new();
            return Page();
        }

        return RedirectToPage("View");
    }
}

