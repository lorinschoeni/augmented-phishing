using System;
using PhishAR.Core.Services;
using PhishAR.ETHTB.Models;
using PhishAR.ETHTB.QR;

namespace PhishAR.ETHTB.Services
{
    public class DataContext : IService
    {
        public EventHandler OverlayModelUpdated { get; set; }
        public EventHandler WebQrUpdated { get; set; }

        private OverlayModel _overlayModel;
        public OverlayModel OverlayModel
        {
            get => _overlayModel;
            set
            {
                _overlayModel = value;
                OverlayModelUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        private WebQr _webQr;
        public WebQr WebQr
        {
            get => _webQr;
            set
            {
                _webQr = value;
                WebQrUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        public float ScreenWidth => WebQr.QrSideLenght / WebQr.Payload.QrRelativeWidth;
        public float ScreenHeight => WebQr.QrSideLenght / WebQr.Payload.QrRelativeHeight;
    }
}
