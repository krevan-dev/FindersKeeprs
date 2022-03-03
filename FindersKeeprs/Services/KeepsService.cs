using System;
using System.Collections.Generic;
using FindersKeeprs.Models;
using FindersKeeprs.Repositories;

namespace FindersKeeprs.Services
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

    internal Keep GetById(int id)
    {
      Keep keep = _repo.GetById(id);
      if (keep == null)
      {
        throw new Exception("Invalid Keep Id...");
      }
      return keep;
    }

    internal Keep Create(Keep newKeep)
    {
      return _repo.Create(newKeep);
    }

    internal Keep Edit(Keep keepToUpdate)
    {
      Keep original = GetById(keepToUpdate.Id);
      if (original.CreatorId != keepToUpdate.CreatorId)
      {
        throw new Exception("You are not the creator of this keep...");
      }
      original.Name = keepToUpdate.Name != null ? keepToUpdate.Name : original.Name;
      original.Description = keepToUpdate.Description != null ? keepToUpdate.Description : original.Description;
      original.Img = keepToUpdate.Img != null ? keepToUpdate.Img : original.Img;
      _repo.Edit(original);
      return original;
    }

    internal void Delete(int keepId, string userId)
    {
      Keep keep = GetById(keepId);
      if (keep.CreatorId != userId)
      {
        throw new Exception("You are not the creator of this keep...");
      }
      _repo.Delete(keepId);
    }

    internal List<Keep> GetKeepsByUserId(string id)
    {
      return _repo.GetKeepsByUserId(id);
    }
  }
}