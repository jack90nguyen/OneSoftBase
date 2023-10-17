using System.Text;
using System.Security.Cryptography;

namespace OneSoft.Helpers;

public class StringHelper
{
  private static readonly string[] VietnameseSigns =
  new string[]
    {
      "aAeEoOuUiIdDyY",
      "áàạảãâấầậẩẫăắằặẳẵ",
      "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
      "éèẹẻẽêếềệểễ",
      "ÉÈẸẺẼÊẾỀỆỂỄ",
      "óòọỏõôốồộổỗơớờợởỡ",
      "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
      "úùụủũưứừựửữ",
      "ÚÙỤỦŨƯỨỪỰỬỮ",
      "íìịỉĩ",
      "ÍÌỊỈĨ",
      "đ",
      "Đ",
      "ýỳỵỷỹ",
      "ÝỲỴỶỸ"
    };


  /// <summary>
  /// Kiểm tra dữ liệu rỗng
  /// </summary>
  public static bool IsEmpty(string value)
  {
    if (string.IsNullOrEmpty(value))
      return true;
    if (string.IsNullOrEmpty(value.Trim()))
      return true;
    return false;
  }


  /// <summary>
  /// Kiểm tra nội dung có chưa từ khóa không ?
  /// </summary>
  public static bool Search(string keyword, string content)
  {
    if (!string.IsNullOrEmpty(keyword))
    {
      content = content.ToLower();
      var keyChar = keyword.ToLower().Trim().Split(' ');
      for (int i = 0; i < keyChar.Length; i++)
      {
        if (!content.Contains(keyChar[i]))
          return false;
      }

      return true;
    }
    else
      return true;
  }

  /// <summary>
  /// String to MD5
  /// </summary>
  public static string CreateMD5(string input)
  {
    // Use input string to calculate MD5 hash
    using MD5 md5 = MD5.Create();
    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
    byte[] hashBytes = MD5.HashData(inputBytes);

    // Convert the byte array to hexadecimal string
    StringBuilder sb = new();
    for (int i = 0; i < hashBytes.Length; i++)
      sb.Append(hashBytes[i].ToString("X2"));

    return sb.ToString();
  }



  /// <summary>
  /// Loại bỏ ký tự đặt biệt, không xóa dấu cách
  /// </summary>
  private static string RemoveSpecial(string text)
  {
    //Loại bỏ ký tự đặc biệt
    //text = text.Replace("-", ""); Không bỏ ký tự -
    text = text.Replace("?", "");
    text = text.Replace("'", "");
    text = text.Replace("/", "");
    text = text.Replace("\"", "");
    text = text.Replace("’", "");
    text = text.Replace(",", "");
    text = text.Replace(":", "");
    text = text.Replace(";", "");
    text = text.Replace("!", "");
    text = text.Replace(".", "");
    text = text.Replace("~", "");
    text = text.Replace("`", "");
    text = text.Replace("#", "");
    text = text.Replace("@", "");
    text = text.Replace("$", "");
    text = text.Replace("®", "");
    text = text.Replace("%", "");
    text = text.Replace("^", "");
    text = text.Replace("&", "");
    text = text.Replace("*", "");
    text = text.Replace("(", "");
    text = text.Replace(")", "");
    text = text.Replace("<", "");
    text = text.Replace(">", "");
    text = text.Replace("[", "");
    text = text.Replace("]", "");
    text = text.Replace("{", "");
    text = text.Replace("}", "");
    text = text.Replace("=", "");
    text = text.Replace("–", "");
    text = text.Replace("+", "");
    text = text.Replace("|", "");
    return text.Trim();
  }


  // <summary>
  /// Chuyển chuỗi tiếng Việt có dấu thành chuỗi không dấu
  /// </summary>
  public static string VnNoSigns(string text)
  {
    if (!string.IsNullOrEmpty(text))
    {
      // Chuyển thành chuỗi không dấu
      for (int i = 1; i < VietnameseSigns.Length; i++)
      {
        for (int j = 0; j < VietnameseSigns[i].Length; j++)
          text = text.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
      }

      // Loại bỏ ký tự đặc biệt
      text = RemoveSpecial(text);

      // Chuyển UTF8 thành ASCII
      text = ConvertASCII(text);

      // Xóa 2 dấu cách liền kề
      while (text.Contains("  "))
        text = text.Replace("  ", " ");

      return text.Trim();
    }
    else
      return string.Empty;
  }


  /// <summary>
  /// Đổi tên file thành tiếng việt không dấu
  /// </summary>
  public static string RenameFile(string fileName)
  {
    if (!string.IsNullOrEmpty(fileName))
    {
      // Xóa 2 dấu cách liền kề
      while (fileName.Contains("  "))
        fileName = fileName.Replace("  ", " ");
      // Thay dấu cách thành dấu -
      fileName = fileName.Replace(" ", "-");

      // Chuyển thành chuỗi không dấu
      for (int i = 1; i < VietnameseSigns.Length; i++)
      {
        for (int j = 0; j < VietnameseSigns[i].Length; j++)
          fileName = fileName.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
      }

      return fileName.Trim().ToLower();
    }
    else
      return string.Empty;
  }

  /// <summary>
  /// Chuyển UTF8 thành ASCII
  /// </summary>
  private static string ConvertASCII(string text)
  {
    byte[] strBytes = Encoding.UTF8.GetBytes(text);

    byte[] asciiBytes = Encoding.Convert(Encoding.UTF8, Encoding.ASCII, strBytes);

    string asciiStr = Encoding.ASCII.GetString(asciiBytes);

    if (asciiStr.Contains('?'))
      return asciiStr.Replace("?", "");
    else
      return text;
  }
}