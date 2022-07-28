using AutoMapper;
using EGM.GQL.DataAccess.Primitives.Entities;
using EGM.GQL.Primitives.Models;

namespace EGM.GQL.Mappers.Profiles
{
    public class DbProfile : Profile
    {
        public DbProfile()
        {
            CreateMap<DbPerson, Person>();
            CreateMap<Person, DbPerson>();
            CreateMap<DbSex, Sex>();
            CreateMap<Sex, DbSex>();
        }
    }
}