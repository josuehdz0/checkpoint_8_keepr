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

    internal List<Keep> GetAllKeeps(string id)
    {
      List<Keep> keeps = _repo.GetAll();
      return keeps;
    }
  }
}