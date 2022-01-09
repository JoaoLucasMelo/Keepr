using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly VaultsService _vs;

    public AccountController(AccountService accountService, VaultsService vs)
    {
      _accountService = accountService;
      _vs = vs;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpGet("vaults")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Vault>>> GetVaultsByAccount()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<Vault> vaults = _vs.GetVaultsByAccount(userInfo.Id);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<Account>> Edit([FromBody] Account editData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Account newAccount = _accountService.Edit(editData, userInfo.Email);
        return Ok(newAccount);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }


}