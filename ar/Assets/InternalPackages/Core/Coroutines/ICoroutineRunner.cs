using System.Collections;
using PhishAR.Core.Services;

namespace PhishAR.Core.Coroutines
{
    public interface ICoroutineRunner : IService
    {
        public void Enqueue(IEnumerator coroutine);
    }
}
