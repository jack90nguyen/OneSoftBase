
namespace AppCore.Services;

public class UserService
{
  /// <summary>
  /// Hàm đăng nhập
  /// </summary>
  public static async Task<UserModel> Login(string username, string password)
  {
    var model = await UserData.GetByUsername(username);
    if (model != null && model.IsActive)
    {
      if (model.Password == StringHelper.CreateMD5(password))
        return model;
    }
    return null;
  }
}