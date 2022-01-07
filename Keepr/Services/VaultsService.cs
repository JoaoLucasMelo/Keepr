using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }

    internal Vault GetById(int id)
    {
      Vault found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid ID");
      }
      return found;
    }

    internal Vault Create(Vault newVault)
    {
      return _repo.Create(newVault);
    }

    internal Vault Edit(Vault update)
    {
      Vault oldVault = GetById(update.Id);
      oldVault.Name = update.Name != null && update.Name.Trim().Length > 1 ? update.Name : oldVault.Name;
      oldVault.Description = update.Description != null && update.Description.Trim().Length > 1 ? update.Description : oldVault.Description;
      oldVault.IsPrivate = update.IsPrivate != null ? update.IsPrivate : oldVault.IsPrivate;
      _repo.Edit(oldVault);
      return oldVault;
    }

    internal void Delete(int id, string creatorId)
    {
      Vault delete = GetById(id);
      if (delete.CreatorId != creatorId)
      {
        throw new Exception("You don't have permission to delete");
      }
      _repo.Delete(id);
    }
  }
}
