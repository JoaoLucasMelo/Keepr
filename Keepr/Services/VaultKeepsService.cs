using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    private readonly KeepsService _ks;
    private readonly VaultsService _vs;

    public VaultKeepsService(VaultKeepsRepository repo, KeepsService ks, VaultsService vs)
    {
      _repo = repo;
      _ks = ks;
      _vs = vs;
    }

    internal VaultKeep Create(VaultKeep newVK)
    {
      Vault vault = _vs.FindById(newVK.VaultId);
      if (vault.CreatorId != newVK.CreatorId)
      {
        throw new Exception("Not Authorized");
      }
      return _repo.Create(newVK);
    }
    internal VaultKeep GetById(int id)
    {
      VaultKeep found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid ID");
      }
      return found;
    }
    internal void Delete(int id, string creatorId)
    {
      VaultKeep delete = GetById(id);
      if (delete.CreatorId != creatorId)
      {
        throw new Exception("You don't have permission to delete");
      }
      _repo.Delete(id);
    }

  }
}