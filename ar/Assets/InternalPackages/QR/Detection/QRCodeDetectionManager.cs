using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.QR;
using PhishAR.QR.Detection.Events;
using PhishAR.QR.Detection.Models;
using UnityEngine;

namespace PhishAR.QR.Detection
{
    public class QRCodeDetectionManager : MonoBehaviour
    {
        private const float QRTimeOutMilliseconds = 500f;
        private const float QRTimeOutSeconds = 0.5f;
        [SerializeField] private QRCodeWatcherController _qrCodeWatcherController;

        private readonly Dictionary<Guid, QRCodeModel> _detectedQRCodes = new Dictionary<Guid, QRCodeModel>();

        private readonly List<Guid> _ignoredQRCodes = new List<Guid>();

        private readonly Queue<QRCodeWatcherEventArgs> _qrCodeEventQueue = new Queue<QRCodeWatcherEventArgs>();

        private Dictionary<QRCodeDetectionEventType, Action<QRCode>> _qrCodeDetectionHandlers;

        private void Start()
        {
            _qrCodeWatcherController.QRCodeWatcherDetected += OnQRCodeWatcherDetected;

            _qrCodeDetectionHandlers =
                new Dictionary<QRCodeDetectionEventType, Action<QRCode>>
                {
                    {QRCodeDetectionEventType.Added, OnQRCodeAdded},
                    {QRCodeDetectionEventType.Updated, OnQRCodeUpdated},
                    {QRCodeDetectionEventType.Removed, OnQRCodeRemoved}
                };
        }

        private void Update()
        {
            lock (_qrCodeEventQueue)
            {
                if (_qrCodeEventQueue.Count > 0) HandleQRCodeEvents();
            }
        }

        public event EventHandler QRDetectionProcessStopped;

        public event EventHandler<QRCodeModelEventArgs> QRCodeModelAdded;
        public event EventHandler<QRCodeModelEventArgs> QRCodeModelUpdated;
        public event EventHandler<QRCodeModelEventArgs> QRCodeModelRemoved;

        public void StartQRCodeDetection()
        {
            _qrCodeWatcherController.StartQRCodeDetection();
        }

        public void StopQRCodeDetection()
        {
            _qrCodeWatcherController.StopQRCodeDetection();
            ResetQRCodeDetectionHandler();

            QRDetectionProcessStopped?.Invoke(this, EventArgs.Empty);
        }

        public void RemoveQRCodeModel(QRCodeModel qrCodeModel)
        {
            _ignoredQRCodes.Add(qrCodeModel.Id);
            _detectedQRCodes.Remove(qrCodeModel.Id);

            StartCoroutine(TemporarilyIgnoreQRCode(qrCodeModel.Id));
        }

        private IEnumerator TemporarilyIgnoreQRCode(Guid qrCodeId)
        {
            yield return new WaitForSeconds(QRTimeOutSeconds);
            _ignoredQRCodes.Remove(qrCodeId);
        }

        private void OnQRCodeWatcherDetected(object sender, QRCodeWatcherEventArgs e)
        {
            if (_ignoredQRCodes.Contains(e.QRCode.Id)) return;

            lock (_qrCodeEventQueue)
            {
                _qrCodeEventQueue.Enqueue(e);
            }
        }

        private void ResetQRCodeDetectionHandler()
        {
            lock (_qrCodeEventQueue)
            {
                _qrCodeEventQueue.Clear();
            }

            lock (_detectedQRCodes)
            {
                foreach (var element in _detectedQRCodes)
                    QRCodeModelRemoved?.Invoke(this, new QRCodeModelEventArgs(element.Value));

                _detectedQRCodes.Clear();
            }

            lock (_ignoredQRCodes)
            {
                _ignoredQRCodes.Clear();
            }
        }

        private void HandleQRCodeEvents()
        {
            lock (_qrCodeEventQueue)
            {
                var currentEventArguments = _qrCodeEventQueue.Dequeue();
                if (_ignoredQRCodes.Contains(currentEventArguments.QRCode.Id)) return;

                _qrCodeDetectionHandlers[currentEventArguments.EventType].Invoke(currentEventArguments.QRCode);
            }
        }

        private void OnQRCodeRemoved(QRCode qrCode)
        {
            var id = qrCode.Id;
            if (_ignoredQRCodes.Contains(id) || !_detectedQRCodes.ContainsKey(id)) return;

            lock (_detectedQRCodes)
            {
                QRCodeModelRemoved?.Invoke(this, new QRCodeModelEventArgs(_detectedQRCodes[id]));
                _detectedQRCodes.Remove(id);
            }
        }

        private void OnQRCodeAdded(QRCode qrCode)
        {
            var id = qrCode.Id;
            if (_ignoredQRCodes.Contains(id) || ShouldIgnoreQRCodeRecognizedOutsideApp(qrCode) ||
                _detectedQRCodes.ContainsKey(id)) return;

            var qrCodeModel = new QRCodeModel(qrCode);
            _detectedQRCodes.Add(id, qrCodeModel);
            QRCodeModelAdded?.Invoke(this, new QRCodeModelEventArgs(qrCodeModel));
        }

        private void OnQRCodeUpdated(QRCode qrCode)
        {
            var id = qrCode.Id;
            if (_ignoredQRCodes.Contains(id)) return;

            var qrCodeModel = new QRCodeModel(qrCode);
            if (ShouldAddQRCodeRecognizedOutsideApp(qrCode))
            {
                _detectedQRCodes.Add(id, qrCodeModel);
                QRCodeModelAdded?.Invoke(this, new QRCodeModelEventArgs(qrCodeModel));
            }
            else if (_detectedQRCodes.ContainsKey(id))
            {
                _detectedQRCodes[id] = qrCodeModel;
                QRCodeModelUpdated?.Invoke(this, new QRCodeModelEventArgs(qrCodeModel));
            }
        }

        private bool ShouldIgnoreQRCodeRecognizedOutsideApp(QRCode qrCode)
        {
            return GetElapsedTimeFromLastDetectionInMilliseconds(qrCode) > QRTimeOutMilliseconds &&
                   !_detectedQRCodes.ContainsKey(qrCode.Id);
        }

        private bool ShouldAddQRCodeRecognizedOutsideApp(QRCode qrCode)
        {
            return GetElapsedTimeFromLastDetectionInMilliseconds(qrCode) <= QRTimeOutMilliseconds &&
                   !_detectedQRCodes.ContainsKey(qrCode.Id);
        }

        private double GetElapsedTimeFromLastDetectionInMilliseconds(QRCode qrCode)
        {
            return Math.Abs((qrCode.LastDetectedTime.UtcDateTime - DateTimeOffset.UtcNow).TotalMilliseconds);
        }
    }
}
