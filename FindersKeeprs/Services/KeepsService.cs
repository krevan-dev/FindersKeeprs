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

    internal Keep Create(Keep newKeep)
    {
      return _repo.Create(newKeep);
    }

  }
}