using System.Text;
using System.Reflection;
using Newtonsoft.Json;

namespace AppCore.Services;

public class DataService
{
  public static readonly int PageSize = 25;
  public static readonly int FileSize = 10;
  public static readonly long MaxFileSize = 10 * 1024000;
  public static readonly string AvatarUrl = 
    "https://ui-avatars.com/api/?background=random&length=1&font-size=0.6&name=";
  private static readonly string MongoConnection = 
    "mongodb+srv://onetez_serverless:DQNgWhvg4wNjJfGt@onetezserverless.7d1poc5.mongodb.net/?retryWrites=true&w=majority";

  #region MongoDB

  /// <summary>
  /// Kết nối MongoDB
  /// </summary>
  public static IMongoDatabase DbConnect()
  {
    var client = new MongoClient(MongoConnection);
    return client.GetDatabase("ToolSpy");
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


  #region Tạo ID ngẫu nhiên

  private static readonly Random random = new Random();
  private static readonly string Characters = "0123456789abcdefghijklmnopqrstuvwxyz";

  // <summary>
  /// Tạo một số ngẫu nhiên từ min đến max
  /// </summary>
  public static int RandomInt(int min, int max)
  {
    return random.Next(min, max + 1);
  }

  /// <summary>
  /// Hàm tạo mã tùy chọn độ dài
  /// </summary>
  public static string RandomCode(int length)
  {
    var result = new StringBuilder();
    for (int i = 0; i < length; i++)
      result.Append(Characters[RandomInt(0, 35)]);
    return result.ToString().ToUpper();
  }

  /// <summary>
  /// Hàm tạo ID tùy chọn độ dài
  /// </summary>
  public static string RandomId(int length)
  {
    var date = DateTime.Today;
    var result = new StringBuilder();
    result.Append(date.ToString("yyMMdd"));
    result.Append(RandomCode(length - 4));

    return result.ToString().ToUpper();
  }

  /// <summary>
  /// Hàm tạo ID mặc định: 6[Date] + 6[Random]
  /// </summary>
  public static string RandomId()
  {
    return RandomId(12);
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