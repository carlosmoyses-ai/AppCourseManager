using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManager.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CourseManager.RazorPages.Pages.Students
{
    public class Edit : PageModel
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
            Uri uri = new($"http://localhost:5259/api/Student/PutStudent/{id}");
            string json = JsonConvert.SerializeObject(Student);
            Student.EnrollmentDate = DateTime.Now;
            using StringContent content = new(json, Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await new HttpClient().PutAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return RedirectToPage("View");
            }
            return Page();
        }
    }
}