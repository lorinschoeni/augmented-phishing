using PhishAR.QR.Detection.Models;
using System;
using UnityEngine;

namespace PhishAR.QR.Detection.UI
{
    public class QRCodeOutlineGO : MonoBehaviour
    {
        [SerializeField] private GameObject _qrCodeDataGameObject;
        [SerializeField] private GameObject _qrCodePhysicalSizeGameObject;
        [SerializeField] private GameObject _qrCodePositionGameObject;

        private QRCodeModel _qrCodeModel;

        private TextMesh _qrCodeData;
        private TextMesh _qrCodePhysicalLength;
        private TextMesh _qrCodePosition;

        private DateTimeOffset _lastUpdateNeededTime;

        private void Awake()
        {
            _qrCodeData = _qrCodeDataGameObject.GetComponent<TextMesh>();
            _qrCodePhysicalLength = _qrCodePhysicalSizeGameObject.GetComponent<TextMesh>();
            _qrCodePosition = _qrCodePositionGameObject.GetComponent<TextMesh>();
        }

        private void Update()
        {
            if (_lastUpdateNeededTime == _qrCodeModel.LastDetectedTime) return;

            _lastUpdateNeededTime = _qrCodeModel.LastDetectedTime;
            UpdatePose();
        }

        public void ProcessQRCode(QRCodeModel qrCodeModel)
        {
            _qrCodeModel = qrCodeModel;
            UpdatePose();
        }

        private void UpdatePose()
        {
            Pose pose = _qrCodeModel.TopLeftPose;
            if (pose == Pose.identity) return;

            UpdateOutline(pose);
            UpdateQRCodeTextualInfo(pose);
        }

        private void UpdateOutline(Pose pose)
        {
            Transform outlineTransform = gameObject.transform;
            outlineTransform.SetPositionAndRotation(pose.position, pose.rotation);
            outlineTransform.localScale = Vector3.one * _qrCodeModel.PhysicalLength;
        }

        private void UpdateQRCodeTextualInfo(Pose pose)
        {
            _qrCodeData.text = $"Data: {_qrCodeModel.Payload}";
            _qrCodePhysicalLength.text = $"Size: {_qrCodeModel.PhysicalLength.ToString("F04")}m";
            _qrCodePosition.text = $"Position: {pose.position.ToString()}";
        }
    }
}
