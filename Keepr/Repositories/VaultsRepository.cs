using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Vault GetById(int id)
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id
      WHERE v.id = @id AND v.isPrivate = 0
      ;";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, prof) =>
      {
        vault.Creator = prof;
        return vault;
      }, new { id }).FirstOrDefault();
    }

    internal Vault Create(Vault newVault)
    {
      string sql = @"
      INSERT INTO vaults
      (name, description, isPrivate, creatorId)
      VALUES
      (@Name, @Description, @IsPrivate, @CreatorId);
      SELECT LAST_INSERT_ID() 
      ;";
      int id = _db.ExecuteScalar<int>(sql, newVault);
      newVault.Id = id;
      return newVault;
    }

    internal void Edit(Vault oldVault)
    {
      string sql = @"
      UPDATE vaults
      SET
      name = @Name,
      description = @description,
      isPrivate = @IsPrivate
      WHERE id = @Id
      ;";
      _db.Execute(sql, oldVault);
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM vaults WHERE id = @id LIMIT 1
      ;";
      _db.Execute(sql, new { id });
    }
  }
}