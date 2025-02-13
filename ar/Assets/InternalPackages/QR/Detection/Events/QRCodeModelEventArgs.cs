using PhishAR.QR.Detection.Models;
using System;

namespace PhishAR.QR.Detection.Events
{
    public class QRCodeModelEventArgs : EventArgs
    {
        public QRCodeModel QRCode { get; }

        public QRCodeModelEventArgs(QRCodeModel qrCode)
        {
            QRCode = qrCode;
        }
    }
}
