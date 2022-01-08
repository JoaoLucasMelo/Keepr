using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;
    private readonly KeepsService _ks;

    public VaultsService(VaultsRepository repo, KeepsService ks)
    {
      _repo = repo;
      _ks = ks;
    }

    internal Vault GetById(int id, string userId)
    {
      Vault found = _repo.FindById(id);
      if (found == null)
      {
        throw new Exception("Invalid ID");
      }
      if (found.CreatorId == userId)
      {
        return found;
      }
      if (found.IsPrivate == true)
      {
        throw new Exception("This Vault is private");
      }
      return _repo.GetById(id);
    }
    internal Vault FindById(int id)
    {
      Vault found = _repo.FindById(id);
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

    internal Vault Edit(Vault update, string id)
    {
      Vault oldVault = FindById(update.Id);
      if (oldVault.CreatorId != id)
      {
        throw new Exception("You dont have permission");
      }
      oldVault.Name = update.Name != null && update.Name.Trim().Length > 1 ? update.Name : oldVault.Name;
      oldVault.Description = update.Description != null && update.Description.Trim().Length > 1 ? update.Description : oldVault.Description;
      oldVault.IsPrivate = update.IsPrivate != null ? update.IsPrivate : oldVault.IsPrivate;
      _repo.Edit(oldVault);
      return oldVault;
    }

    internal List<Vault> GetVaultsByProfileId(string id)
    {
      return _repo.GetByProfileId(id);
    }

    internal void Delete(int id, string creatorId)
    {
      Vault delete = FindById(id);
      if (delete.CreatorId != creatorId)
      {
        throw new Exception("You don't have permission to delete");
      }
      _repo.Delete(id);
    }

    internal List<Vault> GetVaultsByAccount(string id)
    {
      return _repo.GetVaultsByAccount(id);
    }

    internal Vault GetKeepsByVaultId(int id, string userId)
    {
      Vault found = FindById(id);
      if (found.CreatorId != userId && found.IsPrivate == true)
      {
        throw new Exception("You don't have permission");
      }
      return found;
    }
  }
}
