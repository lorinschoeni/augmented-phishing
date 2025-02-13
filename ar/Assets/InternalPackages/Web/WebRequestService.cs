using System.Collections;
using PhishAR.Core.Coroutines;
using UnityEngine.Networking;
using static PhishAR.Web.IWebRequestService;

namespace PhishAR.Web
{
    public class WebRequestService : IWebRequestService
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public WebRequestService(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void SendRequest(UnityWebRequest webRequest, WebRequestDoneCallback callback)
        {
            _coroutineRunner.Enqueue(RequestCoroutine(webRequest, callback));
        }

        private IEnumerator RequestCoroutine(UnityWebRequest webRequest, WebRequestDoneCallback callback)
        {
            yield return webRequest.SendWebRequest();
            callback?.Invoke(webRequest);

            // WebRequest must be disposed after usage in order to prevent memory leak.
            webRequest.Dispose();
        }
    }
}
