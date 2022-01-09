using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Keepr.Models;
using Keepr.Services;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class QueryController : ControllerBase
  {
    private KeepsService _ks;

    public QueryController(KeepsService ks)
    {
      _ks = ks;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get(string search)
    {
      try
      {
        List<Keep> keeps = _ks.Search(search);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}