namespace checkpoint_8_keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;

    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
      VaultKeep vaultKeep = _repo.CreateVaultKeep(vaultKeepData);
      return vaultKeep;
    }

    internal string removeVaultKeep(int id, string userId)
    {
      VaultKeep vaultkeep = _repo.GetOne(id);
      if (vaultkeep == null) throw new Exception($"Not a VaultKeep at id {id}");
      if (vaultkeep.CreatorId != userId) throw new UnauthorizedAccessException("This is not your vaultkeep to remove");
      _repo.RemoveVaultKeep(id);
      return $"This keep has been removed from the vault";
    }
  }
}