namespace TiendaServicios.Api.Libro.Tests.AsyncInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> enumerator;

        public AsyncEnumerator(IEnumerator<T> enumerator) =>  this.enumerator = enumerator ?? throw new ArgumentNullException();

        public T Current => this.enumerator.Current;


        public async ValueTask DisposeAsync()
        {
            await Task.CompletedTask;
        }

        public async ValueTask<bool> MoveNextAsync()
        {
            return await Task.FromResult(this.enumerator.MoveNext());
        }
    }
}
