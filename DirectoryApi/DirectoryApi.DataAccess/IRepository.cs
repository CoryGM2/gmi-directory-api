using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DirectoryApi.Shared;

namespace DirectoryApi.DataAccess
{
    public interface IRepository
    {
        Task<List<Person>> GetAllAsync();
        Task<Person> GetAsync(int? id);
    }
}