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
  }
}