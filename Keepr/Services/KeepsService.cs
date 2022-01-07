using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }

    internal List<Keep> GetAll()
    {
      return _repo.GetAll();
    }

    internal Keep Create(Keep newKeep)
    {
      return _repo.Create(newKeep);
    }

    internal Keep GetById(int id)
    {
      Keep found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid ID");
      }
      return found;
    }

    internal Keep Edit(Keep update)
    {
      Keep oldKeep = GetById(update.Id);
      oldKeep.Name = update.Name != null && update.Name.Trim().Length > 1 ? update.Name : oldKeep.Name;
      oldKeep.Description = update.Description != null && update.Description.Trim().Length > 1 ? update.Description : oldKeep.Description;
      oldKeep.Img = update.Img != null && update.Img.Trim().Length > 1 ? update.Img : oldKeep.Img;
      _repo.Edit(oldKeep);
      return oldKeep;
    }

    internal void Delete(int id, string creatorID)
    {
      Keep delete = GetById(id);
      if (delete.CreatorId != creatorID)
      {
        throw new Exception("You don't have permissiont to delete");
      }
      _repo.Delete(id);
    }
  }
}