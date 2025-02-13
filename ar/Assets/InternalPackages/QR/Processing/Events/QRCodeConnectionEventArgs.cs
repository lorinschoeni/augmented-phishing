using PhishAR.QR.Detection.Models;
using System;

namespace PhishAR.QR.Detection.Events
{
    public class QRCodeConnectionEventArgs : EventArgs
    {
        public QRCodeModel UsedQRCodeModel { get; }
        public RoomJoinAttemptSuccess JoinAttemptSuccess { get; }

        public QRCodeConnectionEventArgs(QRCodeModel qrCodeModel, RoomJoinAttemptSuccess joinAttemptSuccess)
        {
            JoinAttemptSuccess = joinAttemptSuccess;
            UsedQRCodeModel = qrCodeModel;
        }
    }
}
