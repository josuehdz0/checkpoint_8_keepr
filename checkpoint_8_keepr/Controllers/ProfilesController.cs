namespace checkpoint_8_keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly AccountService _accountService;

    private readonly Auth0Provider _auth;

    private readonly KeepsService _keepsService;

    private readonly VaultsService _vaultsSrevice;

    public ProfilesController(AccountService accountService, Auth0Provider auth, KeepsService keepsService, VaultsService vaultsSrevice)
    {
      _accountService = accountService;
      _auth = auth;
      _keepsService = keepsService;
      _vaultsSrevice = vaultsSrevice;
    }

    [HttpGet("{id}")]

    public ActionResult<Profile> GetProfile(string id)
    {
      try
      {
        // Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
        Profile profile = _accountService.GetProfile(id);
        return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]

    public ActionResult<List<Keep>> GetKeepsByProfileId(string id)
    {
      try
      {
        // Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
        List<Keep> keeps = _keepsService.GetAllKeepsByProfileId(id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/vaults")]

    public async Task<ActionResult<List<Vault>>> GetVaultsByProfileId(string id)
    {
      try
      {
        Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
        List<Vault> vaults = _vaultsSrevice.GetAllVaultsByProfileId(id, userInfo?.Id);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }


    }
  }
}