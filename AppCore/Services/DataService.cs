using System.Text;
using System.Reflection;
using Newtonsoft.Json;

namespace AppCore.Services;

public class DataService
{
  public static readonly int PageSize = 25;
  public static readonly int FileSize = 10;
  public static readonly long MaxFileSize = 10 * 1024000;
  public static readonly string AppTitle = "OneSoft";
  private static readonly string MongoConnection = "mongodb://127.0.0.1:27017/";

  #region MongoDB

  /// <summary>
  /// Kết nối MongoDB
  /// </summary>
  public static IMongoDatabase DbConnect()
  {
    var client = new MongoClient(MongoConnection);
    return client.GetDatabase("OneSoft");
  }

  /// <summary>
  /// Tạo bản sao cho Model
  /// </summary>
  /// <param name="model">Dữ liệu cần nhân bản</param>
  public static T Clone<T>(T model)
  {
    var serialized = JsonConvert.SerializeObject(model);
    return JsonConvert.DeserializeObject<T>(serialized);
  }

  /// <summary>
  /// In ra dữ liệu trong model
  /// </summary>
  public static void ConsoleObject(Object obj)
  {
    Type t = obj.GetType();
    Console.WriteLine("Type is: {0}", t.Name);
    PropertyInfo[] props = t.GetProperties();
    foreach (var prop in props)
    {
      if (prop.GetIndexParameters().Length == 0)
        Console.WriteLine("{0, 20} = {2, -40} ({1})", prop.Name, prop.PropertyType.Name, prop.GetValue(obj));
    }
  }

  #endregion
}


public static class DataExtension
{
  #region string extension
  
  /// <summary>
  /// Kiểm tra string có rỗng không
  /// </summary>
  public static bool IsEmpty(this string value)
  {
    if (string.IsNullOrEmpty(value))
      return true;
    if (string.IsNullOrEmpty(value.Trim()))
      return true;
    return false;
  }

  /// <summary>
  /// Kiểm tra string có chứa keyword không?
  /// </summary>
  public static bool Search(this string value, string keyword)
  {
    if (!keyword.IsEmpty())
    {
      string content = value.ToLower();
      string[] keys = keyword.ToLower().Trim().Split(' ');
      for (int i = 0; i < keys.Length; i++)
      {
        if (!content.Contains(keys[i]))
          return false;
      }
    }
    return true;
  }
  
  /// <summary>
  /// So sánh 2 chuỗi có giống nhau không
  /// </summary>
  public static bool Compare(this string value, string text)
  {
    if (string.IsNullOrEmpty(value) && string.IsNullOrEmpty(text))
      return true;
    if (value.Trim().ToLower() == text.Trim().ToLower())
      return true;
    return false;
  }

  #endregion
}