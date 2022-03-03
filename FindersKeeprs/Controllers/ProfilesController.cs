using FindersKeeprs.Models;
using FindersKeeprs.Services;
using Microsoft.AspNetCore.Mvc;

namespace FindersKeeprs.Controllers
{
  [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly AccountService _accountService;

    public ProfilesController(AccountService accountService)
    {
      _accountService = accountService;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> Get(string id)
    {
        Profile profile = _accountService.GetProfileById(id);
        return Ok (profile);
    }
  }
}