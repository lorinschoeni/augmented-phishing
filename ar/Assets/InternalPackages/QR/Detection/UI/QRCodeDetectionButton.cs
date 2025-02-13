using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

namespace PhishAR.QR.Detection.UI
{
    public class QRCodeDetectionButton : MonoBehaviour
    {
        [SerializeField] private QRCodeDetectionManager _qrCodeDetectionManager;

        private Interactable _qrCodeDetectionButton;

        private void Start()
        {
            _qrCodeDetectionButton = gameObject.GetComponent<Interactable>();
            _qrCodeDetectionButton.IsToggled = true;
            _qrCodeDetectionButton.CanSelect = true;
            _qrCodeDetectionButton.CanDeselect = true;

            var toggleReceiver = _qrCodeDetectionButton.GetReceiver<InteractableOnToggleReceiver>();
            toggleReceiver.OnSelect.AddListener(_qrCodeDetectionManager.StartQRCodeDetection);
            toggleReceiver.OnDeselect.AddListener(_qrCodeDetectionManager.StopQRCodeDetection);

            _qrCodeDetectionManager.QRDetectionProcessStopped +=
                (sender, args) => _qrCodeDetectionButton.IsToggled = false;
        }
    }
}
