namespace checkpoint_8_keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Keep CreateKeep(Keep keepData)
    {
      string sql = @"
      INSERT INTO keeps
      (name, description, img, views, creatorId)
      VALUES
      (@name, @description, @img, @views, @creatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, keepData);
      keepData.Id = id;
      return keepData;
    }

    internal List<Keep> GetAll()
    {
      string sql = @"
      SELECT
      kp.*,
      acct.*
      FROM keeps kp
      JOIN accounts acct ON kp.creatorId = acct.id;
      ";
      List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keeps, prof) =>
      {
        keeps.Creator = prof;
        return keeps;
      }).ToList();
      return keeps;
    }

    internal List<Keep> GetAllByProfileId(string userId)
    {
      string sql = @"
      SELECT
      kp.*,
      acct.*
      FROM keeps kp
      JOIN accounts acct ON kp.creatorId = acct.id
      WHERE kp.creatorId = @userId;
      ";
      List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keeps, prof) =>
      {
        keeps.Creator = prof;
        return keeps;
      }, new { userId }).ToList();
      return keeps;
    }

    internal Keep GetOne(int id)
    {
      string sql = @"
      SELECT
      kp.*,
      acct.*
      FROM keeps kp
      JOIN accounts acct ON kp.creatorId = acct.id
      WHERE kp.id = @id;";
      Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, prof) =>
      {
        keep.Creator = prof;
        return keep;
      }, new { id }).FirstOrDefault();
      return keep;
    }

    internal bool Remove(int id)
    {
      string sql = @"
      DELETE FROM keeps WHERE id = @id;
      ";
      int rows = _db.Execute(sql, new { id });
      return rows == 1;
    }

    internal int Update(Keep original)
    {
      string sql = @"
      UPDATE keeps
      SET

      name = @name,
      description = @description,
      views = @views
      
      WHERE id = @id;
      ";
      int rows = _db.Execute(sql, original);
      return rows;
    }
  }
}