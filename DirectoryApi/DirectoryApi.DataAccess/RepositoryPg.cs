using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DirectoryApi.Shared;

namespace DirectoryApi.DataAccess
{
    public class RepositoryPg : IRepository
    {
        private readonly DirectoryContext _context;

        public RepositoryPg(DirectoryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Person> GetAsync(int? id)
        {
            var entity = default(Person);

            if (id.HasValue)
            {
                var entityPg = await _context.People.FirstOrDefaultAsync(x => x.Id == id)
                                                    .ConfigureAwait(false);

                if (entityPg != null)
                    entity = DataAccessMappingPg.Mapper.Map<PersonPg, Person>(entityPg);
            }

            return entity;
        }

        public async Task<List<Person>> GetAllAsync()
        {
            var entities = default(List<Person>);
            var entitiesPg = await _context.People.ToListAsync()
                                                  .ConfigureAwait(false);

            if (entitiesPg != null)
                entities = DataAccessMappingPg.Mapper.Map<IEnumerable<PersonPg>, List<Person>>(entitiesPg);

            if (entities == null)
                entities = new List<Person>();

            return entities;
        }
    }
}
