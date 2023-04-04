namespace checkpoint_8_keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vaultKeepsService;

    private readonly VaultsService _vaultsService;

    private readonly Auth0Provider _auth;

    public VaultKeepsController(VaultKeepsService vaultKeepsService, VaultsService vaultsService, Auth0Provider auth)
    {
      _vaultKeepsService = vaultKeepsService;
      _vaultsService = vaultsService;
      _auth = auth;
    }

    [HttpPost]
    [Authorize]

    async public Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
    {
      try
      {
        Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
        vaultKeepData.CreatorId = userInfo.Id;
        Vault vault = _vaultsService.GetOneVault(vaultKeepData.VaultId, userInfo?.Id);
        VaultKeep vaultKeep = _vaultKeepsService.CreateVaultKeep(vaultKeepData, userInfo?.Id, vault.CreatorId);

        return Ok(vaultKeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]

    async public Task<ActionResult<string>> DeleteVaultKeep(int id)
    {
      try
      {
        Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
        string message = _vaultKeepsService.removeVaultKeep(id, userInfo?.Id);
        return Ok(message);

      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


  }

}