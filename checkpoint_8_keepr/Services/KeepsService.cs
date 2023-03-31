namespace checkpoint_8_keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }

    internal Keep CreateKeep(Keep keepData)
    {
      Keep keep = _repo.CreateKeep(keepData);
      return keep;
    }

    internal Keep EditKeep(Keep updateData)
    {
      Keep original = this.GetOneKeep(updateData.Id);
      original.Name = updateData.Name != null ? updateData.Name : original.Name;
      original.Description = updateData.Description != null ? updateData.Description : original.Description;
      int rowsAffected = _repo.Update(original);
      if (rowsAffected == 0) throw new Exception($"Could not modify {updateData.Name} @ id {updateData.Id}");
      if (rowsAffected > 1) throw new Exception($"You just updated {rowsAffected} of recipes into {updateData.Name}");
      return original;
    }

    internal List<Keep> GetAllKeeps(string id)
    {
      List<Keep> keeps = _repo.GetAll();
      return keeps;
    }

    internal Keep GetOneKeep(int id)
    {
      Keep keep = _repo.GetOne(id);
      if (keep == null) throw new Exception("This Keep ain't it");
      return keep;
    }
  }
}