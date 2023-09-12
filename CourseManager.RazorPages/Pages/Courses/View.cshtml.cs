using CourseManager.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CourseManager.RazorPages.Pages.Courses
{
    public class View : PageModel
    {
        public List<CourseModel> Courses { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            Uri uri = new Uri("http://localhost:5259/api/Course/GetCourses");
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            using HttpResponseMessage response = await new HttpClient().SendAsync(request);;
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                Courses = JsonConvert.DeserializeObject<List<CourseModel>>(responseContent) ?? new();
            }

            return Page();
        }
    }
}