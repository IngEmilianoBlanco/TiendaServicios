﻿using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Libro.Tests.AsyncInterfaces
{
    public class AsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider query;

        public AsyncQueryProvider(IQueryProvider query)
        {
            this.query = query;
        }
        public IQueryable CreateQuery(Expression expression)
        {
            return new AsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new AsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return this.query.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return this.query.Execute<TResult>(expression);
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
        {
            var tipoResultado = typeof(TResult).GetGenericArguments()[0];
            var resultadoEjecucion = typeof(IQueryProvider)
                                        .GetMethod(
                                            name: nameof(IQueryProvider.Execute),
                                            genericParameterCount: 1,
                                            types: new[] { typeof(Expression) }
                                        ).MakeGenericMethod(tipoResultado)
                                        .Invoke(this, new[] { expression });

            return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))?
                .MakeGenericMethod(tipoResultado).Invoke(null, new[] { resultadoEjecucion });
        }
    }
}
