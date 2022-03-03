    using System;
using System.Collections.Generic;
using FindersKeeprs.Models;
using FindersKeeprs.Repositories;

namespace FindersKeeprs.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
        _repo = repo;
    }

    internal Vault GetById(int vaultId, Account userInfo)
    {
        Vault vault = _repo.GetById(vaultId);
        if (vault == null)
        {
            throw new Exception("Invalid Id");
        }
        if (vault.CreatorId != userInfo?.Id && vault.isPrivate == true)
        {
            throw new Exception("You cannot do that...");
        }
        return vault;
    }

    internal Vault Create(Vault newVault)
    {
        return _repo.Create(newVault);
    }

    internal Vault Edit(Vault updatedVault, Account userInfo)
    {
        Vault original = GetById(updatedVault.Id, userInfo);
        if (original.CreatorId != updatedVault.CreatorId)
        {
        throw new Exception("You are not the creator of this vault...");
        }
        original.Name = updatedVault.Name != null ? updatedVault.Name : original.Name;
        original.Description = updatedVault.Description != null ? updatedVault.Description : original.Description;
        original.isPrivate = updatedVault.isPrivate != null ? updatedVault.isPrivate : original.isPrivate;
        _repo.Edit(original);
        return original;
        }

    internal List<Vault> GetVaultsByAccount(Account userInfo)
    {
      List<Vault> vault = _repo.GetVaultsByAccount(userInfo.Id);
      return vault;
    }

    internal void Delete(int vaultId)
    {
      _repo.Delete(vaultId);
    }

    internal List<VaultKeepViewModel> GetKeepsByVaultId(int id, Account userInfo)
    {
        Vault vault = GetById(id, userInfo);
        return _repo.GetKeepsByVaultId(id);
    }
  }
}