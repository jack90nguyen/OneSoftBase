using System.Text;

namespace AppCore.Helpers;

public class RandomHelper
{
  private static readonly Random _random = new();
  private static readonly string _characters = "0123456789abcdefghijklmnopqrstuvwxyz";

  // <summary>
  /// Tạo một số ngẫu nhiên từ min đến max
  /// </summary>
  public static int Number(int min, int max)
  {
    return _random.Next(min, max + 1);
  }

  /// <summary>
  /// Hàm tạo ID: [12-time]+[6-random]
  /// </summary>
  public static long NumberID()
  {
    return Convert.ToInt64(ID());
  }

  /// <summary>
  /// Hàm tạo ID: [12-time]+[6-random]
  /// </summary>
  public static string ID()
  {
    return DateTime.Now.ToString("yyMMddHHmmss") + Number(100000, 999999);
  }

  /// <summary>
  /// Hàm tạo mã tùy chọn độ dài
  /// </summary>
  public static string Code(int length)
  {
    var result = new StringBuilder();
    for (int i = 0; i < length; i++)
      result.Append(_characters[Number(0, 35)]);
    return result.ToString().ToUpper();
  }

  /// <summary>
  /// Hàm tạo mã màu ngẫu nhiên
  /// </summary>
  public static string Guid()
  {
    return System.Guid.NewGuid().ToString().Replace("-", "");
  }
}
