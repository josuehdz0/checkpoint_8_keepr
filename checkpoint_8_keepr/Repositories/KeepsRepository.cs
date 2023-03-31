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


  }
}