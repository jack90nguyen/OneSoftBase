
using MongoDB.Bson;

namespace AppCore.Data
{
  public class UserData
  {
    private static IMongoDatabase _db = DataService.DbConnect();
    private static readonly IMongoCollection<UserModel> _collection = _db.GetCollection<UserModel>("Users");

    public static async Task<UserModel> Create(UserModel model)
    {
      model.Id = RandomHelper.NumberID();
      model.Avatar = SharedHelper.Avatar(model.Username);
      model.Username = model.Username.Trim().ToLower();
      model.Password = StringHelper.CreateMD5(model.Password);

      await _collection.InsertOneAsync(model);

      return model;
    }


    public static async Task<bool> Delete(long id)
    {
      var result = await _collection.DeleteOneAsync(x => x.Id == id);

      if (result.DeletedCount > 0)
        return true;
      else
        return false;
    }


    public static async Task<UserModel> Update(UserModel model)
    {
      var option = new ReplaceOptions { IsUpsert = false };

      var result = await _collection.ReplaceOneAsync(x => x.Id == model.Id, model, option);

      return model;
    }

    /// <summary>
    /// Cập nhật Session
    /// </summary>
    public static async Task UpdateSession(long id, string session)
    {
       var update = Builders<UserModel>.Update
        .Set(x => x.Session, session);

      var result = await _collection.UpdateOneAsync(x => x.Id == id, update);
    }


    public static async Task<UserModel> GetById(long id)
    {
      return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public static async Task<UserModel> GetByUsername(string username)
    {
      return await _collection.Find(x => x.Username == username).FirstOrDefaultAsync();
    }

    public static async Task<UserModel> GetBySession(string session)
    {
      return await _collection.Find(x => x.Session == session && x.IsActive).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Get all users
    /// </summary>
    public static async Task<List<UserModel>> GetAll()
    {
      var results = await _collection.Find(x => true).ToListAsync();

      return (from x in results orderby x.Role, x.Username select x).ToList();
    }

    /// <summary>
    /// Search users
    /// </summary>
    public static async Task<List<UserModel>> GetList(string keyword)
    {
      var builder = Builders<UserModel>.Filter;

      var filtered = builder.Empty;

      if(!keyword.IsEmpty())
      filtered &= builder.Or(
        builder.Regex(x => x.Id, new BsonRegularExpression(keyword, "i")),
        builder.Regex(x => x.Username, new BsonRegularExpression(keyword, "i"))
      );

      return await _collection.Find(filtered)
        .SortBy(x => x.Role).ThenBy(x => x.Id)
        .ToListAsync();
    }
  }
}