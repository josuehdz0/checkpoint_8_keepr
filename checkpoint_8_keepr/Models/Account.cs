namespace checkpoint_8_keepr.Models;

public class Profile
{
  public string Name { get; set; }
  public string Id { get; set; }
  public string Picture { get; set; }

  public string CoverImg { get; set; }
}

public class Account : Profile
{
  public string Email { get; set; }
}
