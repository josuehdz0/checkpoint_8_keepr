namespace checkpoint_8_keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
      string sql = @"
      INSERT INTO vaultkeeps
      (creatorId, vaultId, keepId)
      VALUES
      (@creatorId, @vaultId, @keepId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
      vaultKeepData.Id = id;
      return vaultKeepData;
    }

    internal VaultKeep GetOne(int id)
    {
      string sql = @"
      SELECT
      *
      FROM vaultkeeps
      WHERE id = @id;
      ";
      VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { id }).FirstOrDefault();
      return vaultKeep;
    }

    internal List<VaultedKeep> GetVaultedKeeps(int vaultId)
    {
      string sql = @"
      SELECT
      kps.*,
      vltkps.*,
      vlts.*,
      creator.*
      FROM vaultkeeps vltkps
      JOIN keeps kps ON vltkps.keepId = kps.id
      JOIN vaults vlts ON vltkps.vaultId = vlts.id
      JOIN accounts creator ON vlts.creatorId = creator.id
      WHERE vltkps.vaultId = @vaultId;
      ";
      List<VaultedKeep> vaultedKeeps = _db.Query<VaultedKeep, VaultKeep, Vault, Profile, VaultedKeep>(sql, (vaultedKeep, vaultedkeep, vault, profile) =>
      {
        vaultedKeep.VaultedKeepId = vaultedkeep.Id;
        vaultedKeep.Id = vault.Id;
        vaultedKeep.Creator = profile;
        return vaultedKeep;
      }, new { vaultId }).ToList();
      return vaultedKeeps;
    }

    internal void RemoveVaultKeep(int id)
    {
      string sql = @"
      DELETE FROM vaultkeeps
      WHERE id = @id;
      ";
      _db.Execute(sql, new { id });
    }
  }
}