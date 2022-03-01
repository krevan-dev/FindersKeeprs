using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using FindersKeeprs.Models;
using FindersKeeprs.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindersKeeprs.Controllers
{
  [ApiController]
    [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vks;

    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVaultKeep)
    {
        try
        {
            Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
            newVaultKeep.CreatorId = userInfo.Id;
            VaultKeep newVK = _vks.Create(newVaultKeep, userInfo);
            return Ok(newVK);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Delete(int id)
    {
        try
        {
            Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
            VaultKeep vk = _vks.GetById(id);
            if (userInfo?.Id != vk.CreatorId)
            {
                throw new Exception("You are not authorized to do this.");
            }
            _vks.Delete(id);
            return Ok("Successfully deleted vault");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
  }
}