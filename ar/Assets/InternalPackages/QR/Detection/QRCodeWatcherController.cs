using System;
using Microsoft.MixedReality.QR;
using PhishAR.QR.Detection.Events;
using UnityEngine;

namespace PhishAR.QR.Detection
{
    public class QRCodeWatcherController : MonoBehaviour
    {
        private bool _accessInitialized;
        private QRCodeWatcherAccessStatus _accessStatus;
        private bool _activeDetection;

        private bool _isSupported;

        private QRCodeWatcher _qrCodeWatcher;
        private bool _userInformed;
        public EventHandler<QRCodeWatcherEventArgs> QRCodeWatcherDetected;

        private async void Start()
        {
            _isSupported = QRCodeWatcher.IsSupported();

            _accessStatus = await QRCodeWatcher.RequestAccessAsync();
            _accessInitialized = true;
        }

        private void Update()
        {
            if (IsQRCodeWatcherNotInitialized())
            {
                if (_accessStatus == QRCodeWatcherAccessStatus.Allowed)
                {
                    SetupQRCodeDetection();
                    StartQRCodeDetection();
                }
                else if (!_userInformed)
                {
                    _userInformed = true;
                    Debug.Log(
                        $"Cannot set up the QR code detection process due to the QR code watcher status: {_accessStatus}");
                }
            }
        }

        private bool IsQRCodeWatcherNotInitialized()
        {
            return _qrCodeWatcher == null && _isSupported && _accessInitialized && !_userInformed;
        }

        private void SetupQRCodeDetection()
        {
            _qrCodeWatcher = new QRCodeWatcher();

            _qrCodeWatcher.Added += OnQRCodeAdded;
            _qrCodeWatcher.Updated += OnQRCodeUpdated;
            _qrCodeWatcher.Removed += OnQRCodeRemoved;
        }

        public void StartQRCodeDetection()
        {
            if (_qrCodeWatcher == null || _activeDetection) return;

            _qrCodeWatcher.Start();

            _activeDetection = true;
        }

        public void StopQRCodeDetection()
        {
            if (_qrCodeWatcher == null || !_activeDetection) return;

            _qrCodeWatcher.Stop();

            _activeDetection = false;
        }

        private void OnQRCodeAdded(object sender, QRCodeAddedEventArgs args)
        {
            QRCodeWatcherDetected?.Invoke(this, new QRCodeWatcherEventArgs(args.Code, QRCodeDetectionEventType.Added));
        }

        private void OnQRCodeUpdated(object sender, QRCodeUpdatedEventArgs args)
        {
            QRCodeWatcherDetected?.Invoke(this,
                new QRCodeWatcherEventArgs(args.Code, QRCodeDetectionEventType.Updated));
        }

        private void OnQRCodeRemoved(object sender, QRCodeRemovedEventArgs args)
        {
            QRCodeWatcherDetected?.Invoke(this,
                new QRCodeWatcherEventArgs(args.Code, QRCodeDetectionEventType.Removed));
        }
    }
}
