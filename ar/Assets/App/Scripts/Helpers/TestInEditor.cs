using Newtonsoft.Json;
using PhishAR.Core.Services;
using PhishAR.ETHTB.Models;
using PhishAR.ETHTB.QR;
using PhishAR.ETHTB.Services;
using UnityEngine;

namespace PhishAR.ETHTB.Helpers
{
    public class TestInEditor : MonoBehaviour
    {
        [SerializeField] private TextAsset _overlayJson;
        [SerializeField] private GameObject _welcomeMessage;

        [Space] [SerializeField] private string _webIpAddress;

        [ContextMenu(nameof(TestQrScannedAndContentFetched))]
        public void TestQrScannedAndContentFetched()
        {
            ServiceLocator.TryGetService(out DataContext dataContext);
            {
                var webQrPayloadModel = new WebQrPayloadModel
                {
                    Height = 52.08f,
                    Width = 29.3f
                };
                var webQr = new WebQr
                {
                    Payload = webQrPayloadModel,
                    QrSideLenght = .17f
                };
                dataContext.WebQr = webQr;
                dataContext.OverlayModel = JsonConvert.DeserializeObject<OverlayModel>(_overlayJson.text);

                _welcomeMessage.SetActive(false);
            }
        }

        [ContextMenu(nameof(TestConnectToWeb))]
        public void TestConnectToWeb()
        {
            ServiceLocator.TryGetService(out DataContext dataContext);
            {
                var webQrPayloadModel = new WebQrPayloadModel
                {
                    Height = 52.08f,
                    Width = 29.3f,
                    IpAddress = _webIpAddress
                };
                var webQr = new WebQr
                {
                    Payload = webQrPayloadModel,
                    QrSideLenght = .17f
                };
                dataContext.WebQr = webQr;
            }
        }
    }
}
