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

    internal string DeleteVault(int id, string userId)
    {
      Vault vault = this.GetOneVault(id, userId);
      if (vault.CreatorId != userId) throw new Exception("This is not your Vault to delete goofy");
      bool result = _repo.Remove(id);
      if (!result) throw new Exception($"Could not delete Vault with id: {vault.Id}");
      return $"Deleted {vault.Name}";

    }

    internal Vault EditVault(Vault updateData, string userId)
    {
      Vault original = this.GetOneVault(updateData.Id, updateData.CreatorId);
      if (original.CreatorId != userId) throw new Exception("This is not your Vault to Edit");

      original.Name = updateData.Name != null ? updateData.Name : original.Name;
      original.IsPrivate = updateData.IsPrivate != null ? updateData.IsPrivate : original.IsPrivate;
      int rowsAffected = _repo.Update(original);

      if (rowsAffected == 0) throw new Exception($"Could not modify {updateData.Name} @ id {updateData.Id}");

      if (rowsAffected > 1) throw new Exception($"You just updated {rowsAffected} of recipes into {updateData.Name}");
      return original;

    }

    internal List<Vault> GetAllVaultsByAccountId(string id)
    {
      {
        List<Vault> vaults = _repo.GetAllByProfileId(id);

        return vaults;
      }
    }

    internal List<Vault> GetAllVaultsByProfileId(string profileId, string userId)
    {
      List<Vault> vaults = _repo.GetAllByProfileId(profileId);
      List<Vault> filteredVaults = vaults.FindAll(v => v.CreatorId == userId || v.IsPrivate == false);
      return filteredVaults;
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