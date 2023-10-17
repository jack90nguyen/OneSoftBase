
namespace OneSoft.Helpers;

public class FileHelper
{
  private static readonly bool isMacOS = Environment.CurrentDirectory.Contains('/');

  /// <summary>
  /// Lấy folder chính xác
  /// </summary>
  public static string GetPath(string path)
  {
    if (isMacOS)
      return path.Replace("\\", "/");
    else
      return path.Replace("/", "\\");
  }

  /// <summary>
  /// Lấy tên file file
  /// </summary>
  /// <returns>.jpg, .png...</returns>
  public static string FileName(string link)
  {
    if (!string.IsNullOrEmpty(link))
    {
      var fileName = new FileInfo(link).Name;
      if (fileName.Contains('_'))
        return fileName.Split("_")[1];
      else
        return fileName;
    }
    return string.Empty;
  }

  /// <summary>
  /// Lấy định đạng file
  /// </summary>
  /// <returns>.jpg, .png...</returns>
  public static string FileFormat(string fileName)
  {
    if (!string.IsNullOrEmpty(fileName))
      return Path.GetExtension(fileName);
    return string.Empty;
  }

  /// <summary>
  /// Lấy nới chứa file
  /// </summary>
  public static string FilePath(string link)
  {
    var filePath = GetPath(Environment.CurrentDirectory + "\\wwwroot" + link);

    if (File.Exists(filePath))
      return filePath;

    return string.Empty;
  }

  /// <summary>
  /// Lưu file vào hosting
  /// </summary>
  /// <returns>Trả về link hình</returns> 
  public static async Task<string> UploadFile(StreamContent inputStream, string fileName)
  {
    try
    {
      string format = FileFormat(fileName);
      string folder = "upload\\" + DateTime.Today.ToString("yyyyMM");
      string filePath = GetPath(Environment.CurrentDirectory + "\\wwwroot\\" + folder);

      if (!Directory.Exists(filePath))
        Directory.CreateDirectory(filePath);

      string fullPath = Path.Combine(filePath, fileName);

      await using FileStream fs = new(fullPath, FileMode.Create);
      await inputStream.CopyToAsync(fs);
      inputStream.Dispose();
      fs.Dispose();

      string result = $"/{folder.Replace("\\", "/")}/{fileName}";

      Console.WriteLine($"Upload file to: {result}");

      return result;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Can't upload file: {fileName} \n{ex.Message}");
      return null;
    }
  }

  /// <summary>
  /// Đọc file Text
  /// </summary>
  public static string ReadText(string link)
  {
    try
    {
      var filePath = GetPath(Environment.CurrentDirectory + "\\wwwroot" + link);

      if (File.Exists(filePath))
        return File.ReadAllText(filePath);
    }
    catch (System.Exception ex)
    {
      Console.WriteLine("ReadText: " + ex.Message);
    }
    return "";
  }
}
