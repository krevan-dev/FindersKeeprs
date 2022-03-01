using System;
using FindersKeeprs.Models;
using FindersKeeprs.Repositories;

namespace FindersKeeprs.Services
{
  public class VaultKeepsService
  {
      private readonly VaultKeepsRepository _repo;
      private readonly VaultsService _vaultsService;

    public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService)
    {
      _repo = repo;
      _vaultsService = vaultsService;
    }

    internal VaultKeep Create(VaultKeep newVaultKeep, Account userInfo)
    {
      Vault vault = _vaultsService.GetById(newVaultKeep.VaultId, userInfo);
      if (vault.CreatorId != userInfo.Id)
      {
        throw new Exception("No can do bud...");
      }
      return _repo.Create(newVaultKeep);
    }

    internal VaultKeep GetById(int id)
    {
      VaultKeep vk = _repo.GetById(id);
      if (vk == null)
      {
        throw new Exception("Invalid Relationship Id");
      }
      return vk;
    }

    internal void Delete(int id)
    {
      _repo.Delete(id);
    }
  }
}