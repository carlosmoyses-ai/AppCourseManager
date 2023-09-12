using CourseManager.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CourseManager.RazorPages.Pages.Students;

public class View : PageModel
{
    public List<StudentModel> Students { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        Uri uri = new Uri("http://localhost:5259/api/Student/GetStudents");
        using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
        using HttpResponseMessage response = await new HttpClient().SendAsync(request); ;
        if (response.IsSuccessStatusCode)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            Students = JsonConvert.DeserializeObject<List<StudentModel>>(responseContent) ?? new();
        }

        return Page();
    }
}
