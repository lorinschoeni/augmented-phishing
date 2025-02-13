using PhishAR.QR.Detection;
using PhishAR.QR.Detection.Events;
using UnityEngine;

namespace PhishAR.ETHTB.QR
{
    public class QrAnchor : MonoBehaviour
    {
        [SerializeField] private QRCodeDetectionManager _qrCodeDetectionManager;

        private void Start()
        {
            _qrCodeDetectionManager.QRCodeModelAdded += OnQRCodeModelAddedOrUpdated;
            _qrCodeDetectionManager.QRCodeModelUpdated += OnQRCodeModelAddedOrUpdated;
        }

        private void OnQRCodeModelAddedOrUpdated(object sender, QRCodeModelEventArgs args)
        {
            transform.SetPositionAndRotation(args.QRCode.CenterPose.position, args.QRCode.CenterPose.rotation);
        }
    }
}
