using Microsoft.MixedReality.QR;
using System;

namespace PhishAR.QR.Detection.Events
{
    public class QRCodeWatcherEventArgs : EventArgs
    {
        public QRCode QRCode { get; }
        public QRCodeDetectionEventType EventType { get; }

        public QRCodeWatcherEventArgs(QRCode qrCode, QRCodeDetectionEventType eventType)
        {
            QRCode = qrCode;
            EventType = eventType;
        }
    }
}
