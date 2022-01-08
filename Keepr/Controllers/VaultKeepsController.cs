using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;
    private readonly KeepsService _ks;

    public VaultKeepsController(VaultKeepsService vks, KeepsService ks)
    {
      _vks = vks;
      _ks = ks;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVK)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newVK.CreatorId = userInfo.Id;
        _ks.AddKeep(newVK.KeepId);
        VaultKeep vk = _vks.Create(newVK);
        return Ok(vk);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<String>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        VaultKeep found = _vks.GetById(id);
        _ks.RemoveKeep(found.KeepId);
        _vks.Delete(id, userInfo.Id);
        return Ok("VaultKeep Deleted");
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}