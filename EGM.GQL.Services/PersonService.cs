﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EGM.GQL.Abstractions.Services;
using EGM.GQL.DataAccess.Abstractions;
using EGM.GQL.DataAccess.Abstractions.Repositories;
using EGM.GQL.DataAccess.Primitives.Entities;
using EGM.GQL.Primitives.Models;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore.Query;

namespace EGM.GQL.Services
{
    internal sealed class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonRepository<DbPerson> _repository;
        private readonly IMapper _mapper;

        public PersonService(IUnitOfWork unitOfWork, IPersonRepository<DbPerson> repository, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task<Result<IList<Person>>> GetAllPeopleAsync(
            Func<IQueryable<DbPerson>, IOrderedQueryable<DbPerson>> orderBy = null,
            Func<IQueryable<DbPerson>, IIncludableQueryable<DbPerson, object>> include = null,
            bool disableTracking = true, CancellationToken cancellationToken = default)
        {
            var entities = await _repository.GetAllAsync(orderBy, include, disableTracking,
                cancellationToken);

            var people = _mapper.Map<IList<DbPerson>, IList<Person>>(entities);

            return new Result<IList<Person>>(people);
        }

        public async Task<Result<Person>> GetPersonByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.FindAsync(cancellationToken, id);

            if (entity is null)
            {
                var exception = new Exception($"Person with Id {id} not found.");
                return new Result<Person>(exception);
            }

            var person = _mapper.Map<DbPerson, Person>(entity);
            return new Result<Person>(person);
        }
    }
}