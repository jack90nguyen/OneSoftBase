
namespace AppCore.Helpers;

public class SharedHelper
{
  /// <summary>
  /// Hàm tính phân trang
  /// </summary>
  public static int Paging(int total, int size)
  {
    if (total <= size)
      return 0;
    int col = total / size;
    float tp = total / (float)size;
    if (total % size != 0 && tp > (col))
      col = total / size + 1;
    else
      col = total / size;
    return col;
  }

  /// <summary>
  /// Get Avatar URL
  /// </summary>
  public static string Avatar(string text)
  {
    string link = "https://ui-avatars.com/api/?background=random&length=1&font-size=0.6&name=";
    return link + text.Replace(" ", "+");
  }
}
