using System;
using System.Collections.Generic;
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
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;
    private readonly VaultsService _vs;
    private readonly KeepsService _ks;

    public ProfilesController(ProfilesService ps, VaultsService vs, KeepsService ks)
    {
      _ps = ps;
      _vs = vs;
      _ks = ks;
    }
    [HttpGet("{id}/keeps")]
    public ActionResult<IEnumerable<Keep>> GetKeepsById(string id)
    {
      try
      {
        List<Keep> keeps = _ks.GetByProfileId(id);
        return Ok(keeps);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/vaults")]
    public ActionResult<IEnumerable<Vault>> GetVaultsById(string id)
    {
      try
      {
        List<Vault> vaults = _vs.GetVaultsByProfileId(id);
        return Ok(vaults);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<Profile> GetById(string id)
    {
      try
      {
        Profile profile = _ps.GetById(id);
        return Ok(profile);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<Profile>> Edit([FromBody] Profile editData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Profile newAccount = _ps.Edit(editData, userInfo.Id);
        return Ok(newAccount);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


  }

}
