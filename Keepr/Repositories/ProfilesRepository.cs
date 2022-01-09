using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile GetById(string id)
    {
      string sql = @"
      SELECT * FROM accounts WHERE id = @id
      ;";
      return _db.QueryFirstOrDefault<Profile>(sql, new { id });
    }

    internal Profile Edit(Profile original)
    {
      string sql = @"
            UPDATE accounts
            SET 
              name = @Name,
              picture = @Picture
            WHERE id = @Id;";
      _db.Execute(sql, original);
      return original;
    }


  }
}