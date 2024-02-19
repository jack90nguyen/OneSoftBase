using SkiaSharp;
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
        fileName = DataService.RandomId() + format;
      else
      {
        fileName = StringHelper.RenameFile(fileName);
        if (File.Exists(Path.Combine(filePath, fileName)))
          fileName = DataService.RandomInt(100, 999) + StringHelper.RenameFile(fileName);
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
  /// 
  /// </summary>
  private static async Task<FileStream> ConvertToFileStream(StreamContent inputStream)
  {
    await using FileStream fileStream = new(null, FileMode.Open);
    await inputStream.CopyToAsync(fileStream);
    inputStream.Dispose();
    fileStream.Dispose();

    return fileStream;
  }


  /// <summary>
  /// Thay đổi kích thướt hình ảnh
  /// </summary>
  /// <returns>Trả về link hình</returns>
  public static async Task<string> ResizeImage(StreamContent inputStream, string outputPath, int size)
  {
    int quality = 100;
    var fileInfo = new FileInfo(outputPath);
    var format = fileInfo.Extension;
    var folderPath = fileInfo.DirectoryName;
    var rootPath = Environment.CurrentDirectory + "\\wwwroot";

    if (isMacOS)
      rootPath = rootPath.Replace("\\", "/");

    if (!Directory.Exists(folderPath))
      Directory.CreateDirectory(folderPath);

    await using FileStream fileStream = new(null, FileMode.Open);
    await inputStream.CopyToAsync(fileStream);

    using (var sKStream = new SKManagedStream(fileStream))
    {
      using (var original = SKBitmap.Decode(sKStream))
      {
        int width, height;
        if (original.Width <= size || original.Height <= size)
        {
          width = original.Width;
          height = original.Height;
        }
        else if (original.Width > original.Height)
        {
          width = size;
          height = original.Height * size / original.Width;
        }
        else
        {
          height = size;
          width = original.Width * size / original.Height;
        }

        using (var resized = original.Resize(new SKImageInfo(width, height), SKFilterQuality.High))
        {
          using (var image = SKImage.FromBitmap(resized))
          {
            using (var output = File.OpenWrite(outputPath))
            {
              if (format == ".png")
                image.Encode(SKEncodedImageFormat.Png, quality).SaveTo(output);
              else if (format == ".gif")
                image.Encode(SKEncodedImageFormat.Gif, quality).SaveTo(output);
              else
                image.Encode(SKEncodedImageFormat.Jpeg, quality).SaveTo(output);
            }
          }
        }
      }
    }

    string link = outputPath.Replace(rootPath, "/").Replace("\\", "/");

    Console.WriteLine($"Resize file to: {link}");

    return link;
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
