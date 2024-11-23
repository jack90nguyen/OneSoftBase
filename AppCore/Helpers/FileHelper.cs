namespace AppCore.Helpers;

public class FileHelper
{
  private static bool isMacOS = Environment.CurrentDirectory.Contains("/");

  /// <summary>
  /// Lưu file vào hosting
  /// </summary>
  /// <returns>Trả về link hình</returns> 
  public static async Task<string> UploadFile(StreamContent inputStream, string fileName,
    bool isRename)
  {
    try
    {
      string folder = "upload\\" + DateTime.Now.ToString("yyyyMMdd");
      string filePath = Environment.CurrentDirectory + "\\wwwroot\\" + folder;
      string format = FileExtension(fileName);

      if (isMacOS)
        filePath = filePath.Replace("\\", "/");

      if (!Directory.Exists(filePath))
        Directory.CreateDirectory(filePath);

      if (isRename)
        fileName = RandomHelper.ID() + format;
      else
      {
        fileName = StringHelper.RenameFile(fileName);
        if (File.Exists(Path.Combine(filePath, fileName)))
          fileName = RandomHelper.ID() + StringHelper.RenameFile(fileName);
      }

      string fullPath = Path.Combine(filePath, fileName);

      await using FileStream fs = new(fullPath, FileMode.Create);
      await inputStream.CopyToAsync(fs);
      inputStream.Dispose();
      fs.Dispose();

      string link = $"/{folder.Replace("\\", "/")}/{fileName}";

      Console.WriteLine($"Upload file to: {link}");

      return link;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Can't upload file: {fileName} \n{ex.Message}");
      return null;
    }
  }


  /// <summary>
  /// Hàm xóa file
  /// </summary>
  public static void DeleteFile(string link)
  {
    if (string.IsNullOrEmpty(link) || !link.StartsWith("/upload"))
      return;

    new Task(() =>
    {
      try
      {
        var filePath = Environment.CurrentDirectory + "\\wwwroot" + link.Replace("/", "\\");

        if (isMacOS)
          filePath = filePath.Replace("\\", "/");

        if (File.Exists(filePath))
          File.Delete(filePath);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Can't delete file: {link} \n{ex.Message}");
      }
    }).Start();
  }


  /// <summary>
  /// Lấy định đạng file
  /// </summary>
  /// <returns>.jpg, .png...</returns>
  public static string FileExtension(string file)
  {
    if (!string.IsNullOrEmpty(file))
      return new FileInfo(file).Extension;
    return string.Empty;
  }


  /// <summary>
  /// Lấy tên file file
  /// </summary>
  /// <returns>filename.jpg, filename.png...</returns>
  public static string FileName(string file)
  {
    if (!string.IsNullOrEmpty(file))
      return new FileInfo(file).Name;
    return string.Empty;
  }

}
