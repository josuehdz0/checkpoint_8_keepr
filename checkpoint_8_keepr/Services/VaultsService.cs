namespace checkpoint_8_keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }

    internal Vault CreateVault(Vault vaultData)
    {
      Vault vault = _repo.CreateVault(vaultData);
      return vault;
    }

    internal Vault GetOneVault(int id, string userId)
    {
      Vault vault = _repo.GetOneVault(id);
      if (vault == null) throw new Exception($"There is no Vault with id: {id}");
      if (vault.CreatorId != userId && vault.IsPrivate == true) throw new Exception("You cannot see this private Vault. It is not yours");
      return vault;


    }
  }
}