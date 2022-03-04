using System.Data;
using System.Linq;
using Dapper;
using FindersKeeprs.Models;

namespace FindersKeeprs.Repositories
{
  public class VaultKeepsRepository
  {
      private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      string sql = @"
      UPDATE keeps
      SET keeps = keeps + 1
      WHERE id = @KeepId;
      INSERT INTO vault_keeps
        (vaultId, keepId, creatorId)
      VALUES
        (@VaultId, @KeepId, @CreatorId);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      newVaultKeep.Id = id;
      return newVaultKeep;
    }

    internal VaultKeep GetById(int id)
    {
      string sql = @"
      SELECT * FROM vault_keeps
      WHERE id = @id;";
      return _db.Query<VaultKeep>(sql, new {id}).FirstOrDefault();
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM vault_keeps
      WHERE id = @id
      LIMIT 1;";
      _db.Execute(sql, new {id});
    }
  }
}