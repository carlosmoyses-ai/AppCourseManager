using System.Text;
using CourseManager.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CourseManager.RazorPages.Pages.Students
{
    public class Delete : PageModel
    {
        [BindProperty]
        public StudentModel Student { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Uri uri = new($"http://localhost:5259/api/Student/GetStudent/{id}");
            using HttpResponseMessage response = await new HttpClient().GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                Student = JsonConvert.DeserializeObject<StudentModel>(json) ?? new();
                return Page();
            }
            return RedirectToPage("View");
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Uri uri = new($"http://localhost:5259/api/Student/DeleteStudent/{id}");
            string json = JsonConvert.SerializeObject(Student);
            using StringContent content = new(json, Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await new HttpClient().DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return RedirectToPage("View");
            }
            return Page();
        }
    }
}