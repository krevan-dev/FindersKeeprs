using System;
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


    // REVIEW get vault by id, if vault is private & you are creator, access allowed
    internal Vault GetById(int vaultId)
    {
        Vault vault = _repo.GetById(vaultId);
        if (vault == null)
        {
            throw new Exception("Invalid Id");
        }
        // FIXME If you are not the creator and the vault is private, then this error should fire
        // currently working with valid-auth, no-auth public is failing
        // if (userId != vault.CreatorId && vault.isPrivate == true)
        // {
        //     throw new Exception("You are not authorized to view this vault");
        // }
        return vault;
    }

    internal Vault Create(Vault newVault)
    {
        return _repo.Create(newVault);
    }

    internal Vault Edit(Vault updatedVault)
    {
        Vault original = GetById(updatedVault.Id);
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

    internal void Delete(int vaultId)
    {
      _repo.Delete(vaultId);
    }
  }
}