using PhishAR.Core.Services;
using UnityEngine.Networking;

namespace PhishAR.Web
{
    public interface IWebRequestService : IService
    {
        delegate void WebRequestDoneCallback(UnityWebRequest webRequest);

        void SendRequest(UnityWebRequest webRequest, WebRequestDoneCallback callback);
    }
}
