using System;
using Newtonsoft.Json;
using PhishAR.Core.Services;
using PhishAR.ETHTB.Services;
using PhishAR.QR.Detection;
using PhishAR.QR.Detection.Events;
using UnityEngine;

namespace PhishAR.ETHTB.QR
{
    public class WebQrDetection : MonoBehaviour
    {
        [SerializeField] private QRCodeDetectionManager _qrCodeDetectionManager;
        [SerializeField] private GameObject _welcomeMessage;

        private DataContext _dataContext;

        private void Start()
        {
            _qrCodeDetectionManager.QRCodeModelAdded += OnQRCodeModelAddedOrUpdated;
            _qrCodeDetectionManager.QRCodeModelUpdated += OnQRCodeModelAddedOrUpdated;
            ServiceLocator.TryGetService(out _dataContext);
        }

        private void OnQRCodeModelAddedOrUpdated(object sender, QRCodeModelEventArgs args)
        {
            try
            {
                _dataContext.WebQr = new WebQr()
                {
                    Payload = JsonConvert.DeserializeObject<WebQrPayloadModel>(args.QRCode.Payload),
                    QrSideLenght = args.QRCode.PhysicalLength
                };
            }
            catch (Exception e)
            {
                Debug.Log("Error occured while trying to deserialize QR payload");
                Debug.Log(e.Message);
                Debug.Log(args.QRCode.Payload);
                throw;
            }

            if (_dataContext.WebQr.Payload == null)
            {
                Debug.LogError("QR payload not valid");
                Debug.Log(args.QRCode.Payload);
                return;
            }

            _welcomeMessage.SetActive(false);
        }
    }
}
