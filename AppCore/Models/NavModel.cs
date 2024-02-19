
namespace AppCore.Models
{
  public class NavModel
  {
    public string Name { get; set; }

    public string Url { get; set; }

    public string Icon { get; set; }

    public bool Active { get; set; }

    public List<NavModel> Childs { get; set; } = new();
  }
}