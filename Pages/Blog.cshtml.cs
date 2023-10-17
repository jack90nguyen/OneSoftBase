using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OneSoft.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class BlogModel : PageModel
{
  public string Title { get; set; }
  public string Link { get; set; }

  public async Task OnGetAsync(string link)
  {
    Link = link;
    Title = "Blog " + link.Replace("-", " ");
    //DataService.ObjectConsole(this.Request);
  }
}