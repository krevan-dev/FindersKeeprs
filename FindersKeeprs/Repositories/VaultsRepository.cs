using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using FindersKeeprs.Models;

namespace FindersKeeprs.Repositories
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
        JOIN accounts a on v.creatorId = a.id
        WHERE v.id = @id";
        return _db.Query<Vault, Profile, Vault>(sql, (v, p) => 
        {
            v.Creator = p;
            return v;
        }, new {id}).FirstOrDefault();
    }

    internal Vault Create(Vault newVault)
    {
        string sql = @"
        INSERT INTO vaults
          (name, description, isPrivate, creatorId)
        VALUES
          (@Name, @Description, @isPrivate, @CreatorId);
        SELECT LAST_INSERT_ID();";
        int id = _db.ExecuteScalar<int>(sql, newVault);
        newVault.Id = id;
        return newVault;
    }

    internal void Edit(Vault original)
    {
        string sql = @"
        UPDATE vaults
        SET
        name = @Name,
        description = @Description,
        isPrivate = @isPrivate
        WHERE id = @Id";
        _db.Execute(sql, original);
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM vaults
      WHERE id = @id
      LIMIT 1";
      _db.Execute(sql, new {id});
    }

    internal List<VaultKeepViewModel> GetKeepsByVaultId(int id)
    {
      string sql = @"
      SELECT
      k.*,
      vk.id AS vaultKeepId,
      a.*,
      vk.vaultId,
      vk.keepId
      FROM vault_keeps vk
      JOIN keeps k ON k.id = vk.keepId
      JOIN accounts a ON a.id = k.creatorId
      WHERE vk.vaultId = @id;";
      return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (k, p) =>
      {
        k.Creator = p;
        return k;
      }, new { id }).ToList();
    }

    internal List<Vault> GetVaultsByAccount(string userId)
    {
      string sql = @"
      SELECT * FROM vaults v
      WHERE v.creatorId = @userId;";
      return _db.Query<Vault>(sql, new { userId }).ToList();
    }

    internal List<Vault> GetVaultsByUserId(string id)
    {
      string sql = @"
      SELECT * FROM vaults v
      WHERE v.creatorId = @id;";
      return _db.Query<Vault>(sql, new { id }).ToList();
    }
  }
}