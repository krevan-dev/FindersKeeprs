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
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vs;

    public VaultsController(VaultsService vs)
    {
        _vs = vs;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> GetById(int id)
    {
        try
        {
            // TODO try putting userinfo check here instead of service...
            Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
            Vault vault = _vs.GetById(id);
            if (userInfo.Id != vault.CreatorId && vault.isPrivate == true)
            {
                throw new Exception("Unable to access that vault");
            }
            return Ok(vault);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
    {
        try
        {
            Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
            newVault.CreatorId = userInfo.Id;
            Vault createdVault = _vs.Create(newVault);
            createdVault.Creator = userInfo;
            return createdVault;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Edit(int id, [FromBody] Vault updatedVault)
    {
        try
        {   
            Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
            updatedVault.Id = id;
            updatedVault.CreatorId = userInfo.Id;
            Vault vault = _vs.Edit(updatedVault);
            return Ok(vault);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Delete (int id)
    {
        try
        {
            Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
            Vault vault = _vs.GetById(id);
            if (userInfo.Id != vault.CreatorId)
            {
                throw new Exception("You are not authorized to do this.");
            }
            _vs.Delete(id);
            return Ok("Vault deleted successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    }
}