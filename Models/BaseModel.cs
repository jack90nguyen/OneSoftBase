
namespace OneSoft.Models;

public class FieldModel
{
  /// <summary>ID hoặc value</summary>
  public string id { get; set; }

  /// <summary>Tên hoặc tiêu đề</summary>
  public string name { get; set; }
}


public class DataModel
{
  /// <summary>ID</summary>
  public string id { get; set; }

  /// <summary>Thông báo</summary>
  public string message { get; set; }

  /// <summary>Dữ liệu trả về</summary>
  public Dictionary<string, string> data { get; set; } = new();
}


public class NavModel
{
  public string name { get; set; }

  public string link { get; set; }

  public string icon { get; set; }

  public bool active { get; set; }

  public List<NavModel> childs { get; set; }

  public NavModel() { }
  
  public NavModel(string name, string link, string icon)
  {
    this.name = name;
    this.link = link;
    this.icon = icon;
  }

}

public class FileModel
{
  public string id { get; set; }

  public string link { get; set; }

  public string name { get; set; }

  public string format { get; set; }

  public long size { get; set; }

  public long date { get; set; }
}
