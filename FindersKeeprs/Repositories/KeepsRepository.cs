using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using FindersKeeprs.Models;

namespace FindersKeeprs.Repositories
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
      JOIN accounts a on k.creatorId = a.id";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) => 
      {
        k.Creator = p;
        return k;
      }).ToList();
    }

    internal Keep Create(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps
      (name, description, img, creatorId)
      VALUES
      (@Name, @Description, @Img, @CreatorId);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newKeep);
      newKeep.Id = id;
      return newKeep;
    }
  }
}