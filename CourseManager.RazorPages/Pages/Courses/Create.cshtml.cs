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

namespace CourseManager.RazorPages.Pages.Courses;

public class Create : PageModel
{
    [BindProperty]
    public CourseModel Course { get; set; } = new();

    public async Task<IActionResult> OnPostAsync(CourseModel course)
    {
        Uri uri = new("http://localhost:5259/api/Course/PostCourse");
        string json = JsonConvert.SerializeObject(course);
        using StringContent content = new(json, Encoding.UTF8, "application/json");
        using HttpResponseMessage response = await new HttpClient().PostAsync(uri, content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage("View");
        }
        return Page();
    }
}
