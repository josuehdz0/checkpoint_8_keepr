namespace checkpoint_8_keepr.Models
{
  public class Vault
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Img { get; set; }
    public bool? IsPrivate { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
  }


  public class VaultedKeep : Vault
  {
    public int VaultedKeepId { get; set; }
  }
}