
namespace AppCore.Data
{
  public class UserData
  {
    private static IMongoDatabase _db = DataService.DbConnect();
    private static string _collection = "Users";

    public static async Task<UserModel> Create(UserModel model)
    {
      model.Id = DataService.RandomId();
      model.Avatar = DataService.AvatarUrl + model.Username.Replace(" ", "+");
      model.Username = model.Username.Trim().ToLower();
      model.Password = StringHelper.CreateMD5(model.Password);

      var collection = _db.GetCollection<UserModel>(_collection);

      await collection.InsertOneAsync(model);

      return model;
    }


    public static async Task<bool> Delete(string id)
    {
      var collection = _db.GetCollection<UserModel>(_collection);

      var result = await collection.DeleteOneAsync(x => x.Id == id);

      if (result.DeletedCount > 0)
        return true;
      else
        return false;
    }


    public static async Task<UserModel> Update(UserModel model)
    {
      var collection = _db.GetCollection<UserModel>(_collection);

      var option = new ReplaceOptions { IsUpsert = false };

      var result = await collection.ReplaceOneAsync(x => x.Id == model.Id, model, option);

      return model;
    }

    /// <summary>
    /// Cập nhật Session
    /// </summary>
    public static async Task UpdateSession(string id, string session)
    {
      var collection = _db.GetCollection<UserModel>(_collection);

       var update = Builders<UserModel>.Update
        .Set(x => x.Session, session);

      var result = await collection.UpdateOneAsync(x => x.Id == id, update);
    }


    public static async Task<UserModel> GetById(string id)
    {
      var collection = _db.GetCollection<UserModel>(_collection);

      return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public static async Task<UserModel> GetByUsername(string username)
    {
      var collection = _db.GetCollection<UserModel>(_collection);

      return await collection.Find(x => x.Username == username).FirstOrDefaultAsync();
    }

    public static async Task<UserModel> GetBySession(string session)
    {
      var collection = _db.GetCollection<UserModel>(_collection);

      return await collection.Find(x => x.Session == session && x.IsActive).FirstOrDefaultAsync();
    }


    /// <summary>
    /// Get all users
    /// </summary>
    public static async Task<List<UserModel>> GetAll()
    {
      var collection = _db.GetCollection<UserModel>(_collection);

      var results = await collection.Find(x => true).ToListAsync();

      return (from x in results orderby x.Role, x.Username select x).ToList();
    }
  }
}