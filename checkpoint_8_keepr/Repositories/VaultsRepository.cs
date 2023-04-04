namespace checkpoint_8_keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Vault CreateVault(Vault vaultData)
    {
      string sql = @"
      INSERT INTO vaults
      (name, description, img, isPrivate, creatorId)
      VALUES
      (@name, @description, @img, @isPrivate, @creatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, vaultData);
      vaultData.Id = id;
      return vaultData;
    }

    internal List<Vault> GetAllByProfileId(string userId)
    {
      string sql = @"
      SELECT
      vlt.*,
      acct.*
      FROM vaults vlt
      JOIN accounts acct ON vlt.creatorId = acct.id
      WHERE vlt.creatorId = @userId;
      ";
      List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, (vaults, prof) =>
      {
        vaults.Creator = prof;
        return vaults;
      }, new { userId }).ToList();
      return vaults;
    }

    internal Vault GetOneVault(int id)
    {
      string sql = @"
      SELECT
      vlt.*,
      acct.*
      FROM vaults vlt
      JOIN accounts acct ON vlt.creatorId = acct.id
      WHERE vlt.id = @id;";
      Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, prof) =>
      {
        vault.Creator = prof;
        return vault;
      }, new { id }).FirstOrDefault();
      return vault;

    }

    internal bool Remove(int id)
    {
      string sql = @"
      DELETE FROM vaults WHERE id = @id;
      ";
      int rows = _db.Execute(sql, new { id });
      return rows == 1;
    }

    internal int Update(Vault original)
    {
      string sql = @"
      UPDATE vaults
      SET
      name = @name,
      isPrivate = @isPrivate
      WHERE id = @id;
      ";
      int rows = _db.Execute(sql, original);
      return rows;
    }
  }
}