using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhishAR.Core.Coroutines
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        private readonly Queue<IEnumerator> _coroutinesToRun = new Queue<IEnumerator>();
        private IEnumerator _currentCoroutine;
        private int _currentCoroutineCount;

        private void Update()
        {
            lock (_coroutinesToRun) _currentCoroutineCount = _coroutinesToRun.Count;
            for (var i = 0; i < _currentCoroutineCount; i++)
            {
                lock (_coroutinesToRun) _currentCoroutine = _coroutinesToRun.Dequeue();
                StartCoroutine(_currentCoroutine);
            }
        }

        public void Enqueue(IEnumerator coroutine)
        {
            if (coroutine is null) return;
            lock (_coroutinesToRun) _coroutinesToRun.Enqueue(coroutine);
        }
    }
}
