﻿using CleanArchiteture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiteture.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> Get(Guid id, CancellationToken cancellationToken);
        Task<List<T>> GetAll(CancellationToken cancellationToken);
    }
}
