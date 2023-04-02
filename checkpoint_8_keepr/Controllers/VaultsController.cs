namespace checkpoint_8_keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vaultsService;

    private readonly VaultKeepsService _vaultKeepsService;

    private readonly Auth0Provider _auth;

    public VaultsController(VaultsService vaultsService, VaultKeepsService vaultKeepsService, Auth0Provider auth)
    {
      _vaultsService = vaultsService;
      _vaultKeepsService = vaultKeepsService;
      _auth = auth;
    }

    [HttpPost]
    [Authorize]

    async public Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
    {
      try
      {
        Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
        vaultData.CreatorId = userInfo.Id;
        Vault vault = _vaultsService.CreateVault(vaultData);
        vault.Creator = userInfo;
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]

    async public Task<ActionResult<Vault>> GetOneVault(int id)
    {
      try
      {
        Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
        Vault vault = _vaultsService.GetOneVault(id, userInfo?.Id);
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpPut("{id}")]
    [Authorize]

    async public Task<ActionResult<Vault>> EditVault(int id, [FromBody] Vault updateData)
    {
      try
      {
        Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
        updateData.Id = id;
        Vault vault = _vaultsService.EditVault(updateData, userInfo?.Id);
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpDelete("{id}")]
    [Authorize]

    async public Task<ActionResult<string>> DeleteVault(int id)
    {
      try
      {
        Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
        string vault = _vaultsService.DeleteVault(id, userInfo.Id);
        return Ok(vault);

      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // NOTE Still need to get this done
    // [HttpGet("{id}/keeps")]

    // async public Task<ActionResult<List<VaultedKeep>>> GetVaultedKeepsByVaultId(int id)
    // {
    //   try
    //   {
    //     Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
    //     List<VaultedKeep> vaultedKeeps = _vaultKeepsService.GetVaultedKeepsByVaultId(id, userInfo?.Id);
    //     return Ok(vaultedKeeps);
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }








  }
}