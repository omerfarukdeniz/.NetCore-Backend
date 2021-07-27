using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Helpers
{
    public class AwaitableLock
    {
        private readonly SemaphoreSlim _toLock;

        public AwaitableLock()
        {
            _toLock = new SemaphoreSlim(1, 1);
        }

        public async Task<LockReleaser> Lock(TimeSpan timeOut)
        {
            if (await _toLock.WaitAsync(timeOut))
            {
                return new LockReleaser(_toLock);
            }
            throw new TimeoutException();
        }

        public struct LockReleaser : IDisposable
        {
            private readonly SemaphoreSlim _toRelease;

            public LockReleaser(SemaphoreSlim toRelease)
            {
                _toRelease = toRelease;
            }
            public void Dispose()
            {
                _toRelease.Release();
            }
        }
    }
}
