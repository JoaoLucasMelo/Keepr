using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Keep> GetAll()
    {
      string sql = @"
      SELECT
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      ;";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, prof) =>
      {
        keep.Creator = prof;
        return keep;
      }).ToList();
    }

    internal Keep GetById(int id)
    {
      string sql = @"
      SELECT
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.id = @id
      ;";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, prof) =>
      {
        keep.Creator = prof;
        return keep;
      }, new { id }).FirstOrDefault();
    }

    internal List<Keep> Search(string search)
    {
      string sql = @"
      SELECT
      k.*,
      a.*
      FROM
      keeps k
      JOIN accounts a ON k.creatorId = a.id AND k.name LIKE @search
      ;";
      return _db.Query<Keep, Account, Keep>(sql, (keep, acct) =>
              {
                keep.Creator = acct;
                return keep;
              }, new { search }, splitOn: "id").ToList();
    }

    internal void AddKeep(int id)
    {
      string sql = @"
      UPDATE keeps
      SET keeps = keeps+1
      WHERE
      id = @id;
    ;";
      _db.Execute(sql, new { id });
    }
    internal Keep Create(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps
      (name, description, img, creatorId)
      VALUES
      (@Name, @Description, @Img, @CreatorId);
      SELECT LAST_INSERT_ID() 
      ;";
      int id = _db.ExecuteScalar<int>(sql, newKeep);
      newKeep.Id = id;
      return newKeep;
    }

    internal void AddView(int id)
    {
      string sql = @"
      UPDATE keeps
      SET views = views+1
      WHERE
      id = @id;
    ;";
      _db.Execute(sql, new { id });
    }

    internal void RemoveKeep(int id)
    {
      string sql = @"
      UPDATE keeps
      SET keeps = keeps-1
      WHERE
      id = @id;
    ;";
      _db.Execute(sql, new { id });
    }

    internal List<Keep> GetByProfileId(string id)
    {
      string sql = @"
      SELECT
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.creatorId = @id
      ;";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, prof) =>
      {
        keep.Creator = prof;
        return keep;
      }, new { id }).ToList();
    }

    internal List<VaultKeepViewModal> GetKeepsByVaultId(int id)
    {
      string sql = @"
      SELECT
      k.*,
      k.id AS keepId,
      vk.id AS VaultKeepId,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      JOIN vaultkeeps vk ON vk.vaultId = @id AND vk.keepId = k.id
      WHERE vk.vaultId = @id
      ;";
      return _db.Query<VaultKeepViewModal, Profile, VaultKeepViewModal>(sql, (vkvm, prof) =>
      {
        vkvm.Creator = prof;
        return vkvm;
      }, new { id }).ToList();
    }

    internal void Edit(Keep oldKeep)
    {
      string sql = @"
      UPDATE keeps
      SET
      name = @Name,
      description = @description,
      img = @Img
      WHERE id = @Id
      ;";
      _db.Execute(sql, oldKeep);
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM keeps WHERE id = @id LIMIT 1
      ;";
      _db.Execute(sql, new { id });
    }
  }
}