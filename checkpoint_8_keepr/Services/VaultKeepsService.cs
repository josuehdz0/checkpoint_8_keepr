namespace checkpoint_8_keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    private readonly VaultsService _vaultService;

    private readonly KeepsRepository _keeprepo;

    public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultService, KeepsRepository keeprepo)
    {
      _repo = repo;
      _vaultService = vaultService;
      _keeprepo = keeprepo;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId, Vault vault, Keep keep)
    {
      if (vault.CreatorId != userId) throw new UnauthorizedAccessException("This is not your vaultkeep to create");
      VaultKeep vaultKeep = _repo.CreateVaultKeep(vaultKeepData);
      keep.Kept++;
      _keeprepo.Update(keep);

      return vaultKeep;
    }

    internal List<VaultedKeep> GetVaultedKeepsByVaultId(int vaultId, string userId)
    {

      Vault vault = _vaultService.GetOneVault(vaultId, userId);
      List<VaultedKeep> vaultedKeeps = _repo.GetVaultedKeeps(vaultId);
      return vaultedKeeps;
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