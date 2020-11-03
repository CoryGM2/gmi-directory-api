using System;

using AutoMapper;

using DirectoryApi.Shared;

namespace DirectoryApi.DataAccess
{
    public static class DataAccessMappingPg
    {
        private static IMapper _mapper;

        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                    _mapper = InitializeMapper();

                return _mapper;
            }
        }

        public static IMapper InitializeMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Person, PersonPg>();
                cfg.CreateMap<PersonPg, Person>();
            });

            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();

            return mapper;
        }
    }
}
