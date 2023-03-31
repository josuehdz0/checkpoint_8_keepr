namespace checkpoint_8_keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _keepsService;

    private readonly Auth0Provider _auth;

    public KeepsController(KeepsService keepsService, Auth0Provider auth)
    {
      _keepsService = keepsService;
      _auth = auth;
    }


    [HttpPost]
    [Authorize]

    async public Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
    {
      try
      {
        Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
        keepData.CreatorId = userInfo.Id;
        Keep keep = _keepsService.CreateKeep(keepData);
        keep.Creator = userInfo;
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpGet]

    async public Task<ActionResult<List<Keep>>> GetAllKeeps()
    {
      try
      {
        Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
        List<Keep> keeps = _keepsService.GetAllKeeps(userInfo?.Id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpGet("{id}")]

    public ActionResult<Keep> FindOneKeep(int id)
    {
      try
      {
        Keep keep = _keepsService.GetOneKeep(id);
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpPut("{id}")]
    [Authorize]

    async public Task<ActionResult<Keep>> EditKeep(int id, [FromBody] Keep updateData)
    {
      try
      {
        Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
        updateData.Id = id;
        Keep keep = _keepsService.EditKeep(updateData, userInfo?.Id);
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpDelete("{id}")]
    [Authorize]

    async public Task<ActionResult<string>> DeleteKeep(int id)
    {
      try
      {
        Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
        string keep = _keepsService.DeleteKeep(id, userInfo.Id);
        return Ok(keep);

      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }






  }
}