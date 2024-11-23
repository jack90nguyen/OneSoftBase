using MongoDB.Bson.Serialization.Attributes;

namespace AppCore.Models;

public class UserModel
{
  [BsonId]
  public long Id { get; set; }

  /// <summary>Password</summary>
  public string Password { get; set; }

  /// <summary>Username</summary>
  public string Username { get; set; }

  /// <summary>Avatar</summary>
  public string Avatar { get; set; }

  /// <summary>Session</summary>
  public string Session { get; set; }

  /// <summary>IsActive</summary>
  public bool IsActive { get; set; } = true;

  /// <summary>Role</summary>
  public RoleEnum Role { get; set; }


  public enum RoleEnum
  {
    Admin,
    User
  }
}