using PhishAR.Core.Coroutines;
using PhishAR.Core.Services;
using PhishAR.ETHTB.Services.ContentSync;
using PhishAR.Web;
using UnityEngine;

namespace PhishAR.ETHTB.Services
{
    public class ServiceLocatorSetUp : MonoBehaviour
    {
        private void Awake()
        {
            var coroutineRunner = GetComponent<ICoroutineRunner>();
            ServiceLocator.RegisterService(coroutineRunner);

            IWebRequestService webRequestService = new WebRequestService(coroutineRunner);
            ServiceLocator.RegisterService(webRequestService);

            var contentService = GetComponent<IContentSyncService>();
            ServiceLocator.RegisterService(contentService);

            var dataContext = new DataContext();
            ServiceLocator.RegisterService(dataContext);
        }
    }
}
