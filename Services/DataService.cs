using System.Text;
using System.Reflection;
using System.Runtime.Caching;
using Newtonsoft.Json;

namespace OneSoft.Services;

public class DataService
{
  /// <summary>
  /// Tạo bản sao cho object
  /// </summary>
  public static T ObjectClone<T>(T model)
  {
    var serialized = JsonConvert.SerializeObject(model);
    return JsonConvert.DeserializeObject<T>(serialized);
  }

  /// <summary>
  /// In ra dữ liệu trong model
  /// </summary>
  public static void ObjectConsole(object obj)
  {
    Type t = obj.GetType();
    Console.WriteLine("Type is: {0}", t.Name);
    PropertyInfo[] props = t.GetProperties();
    foreach (var prop in props)
    {
      try
      {
        if (prop.GetIndexParameters().Length == 0)
          Console.WriteLine("{0, 20} = {2, -40} ({1})", prop.Name, prop.PropertyType.Name, prop.GetValue(obj));
      }
      catch (System.Exception)
      {
        
      }
    }
  }

  /// <summary>
  /// Xóa dữ liệu trong Cache
  /// </summary>
  public static void CacheRemove(string key)
  {
    ObjectCache cache = MemoryCache.Default;
    cache.Remove(key);
    Console.WriteLine($"Cache clear: {key}");
  }

  /// <summary>
  /// Danh sách nhân viên, có dùng cahce
  /// </summary>
  public static List<T> CacheData<T>(string key, List<T> data)
  {
    ObjectCache cache = MemoryCache.Default;
    CacheItemPolicy policy = new()
    {
      AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
    };

    List<T> results;
    if (cache[key] == null)
    {
      results = data;
      cache.Add(key, results, policy, null);
      Console.WriteLine($"Cache set: {key}");
    }
    else
    {
      results = (List<T>)cache.Get(key);
      Console.WriteLine($"Cache get: {key}");
    }
    return results;
  }
}

