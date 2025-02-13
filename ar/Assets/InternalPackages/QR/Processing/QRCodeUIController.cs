using System;
using System.Collections;
using System.Collections.Generic;
using PhishAR.QR.Detection;
using PhishAR.QR.Detection.Events;
using PhishAR.QR.Detection.Models;
using PhishAR.QR.Detection.UI;
using UnityEngine;

namespace PhishAR.QR.Processing
{
    public class QRCodeUIController : MonoBehaviour
    {
        [SerializeField] private QRCodeConnectionController _qrCodeConnectionController;
        [SerializeField] private QRCodeDetectionManager _qrCodeDetectionManager;
        [SerializeField] private GameObject _qrCodePrefab;
        [SerializeField] private GameObject _notificationPrefab;

        private readonly Dictionary<Guid, GameObject> _instantiatedQRCodeRepresentations =
            new Dictionary<Guid, GameObject>();

        private void Start()
        {
            _qrCodeDetectionManager.QRCodeModelAdded += OnQRCodeModelAdded;
            _qrCodeDetectionManager.QRCodeModelUpdated += OnQRCodeModelUpdated;
            _qrCodeDetectionManager.QRCodeModelRemoved += OnQRCodeModelRemoved;

            _qrCodeConnectionController.RoomJoinAttemptFinished += OnRoomJoinAttemptFinished;
        }

        private void OnQRCodeModelAdded(object sender, QRCodeModelEventArgs args)
        {
            var qrCodePose = args.QRCode.TopLeftPose;
            var qrCodeGO = Instantiate(_qrCodePrefab, qrCodePose.position, qrCodePose.rotation);
            qrCodeGO.GetComponent<QRCodeOutlineGO>().ProcessQRCode(args.QRCode);
            _instantiatedQRCodeRepresentations.Add(args.QRCode.Id, qrCodeGO);
        }

        private void OnQRCodeModelUpdated(object sender, QRCodeModelEventArgs args)
        {
            GameObject qrCodeGO;
            if (_instantiatedQRCodeRepresentations.ContainsKey(args.QRCode.Id))
            {
                qrCodeGO = _instantiatedQRCodeRepresentations[args.QRCode.Id];
            }
            else
            {
                var qrCodePose = args.QRCode.TopLeftPose;
                qrCodeGO = Instantiate(_qrCodePrefab, qrCodePose.position, qrCodePose.rotation);
                _instantiatedQRCodeRepresentations.Add(args.QRCode.Id, qrCodeGO);
            }

            qrCodeGO.GetComponent<QRCodeOutlineGO>().ProcessQRCode(args.QRCode);
            _instantiatedQRCodeRepresentations[args.QRCode.Id] = qrCodeGO;
        }

        private void OnQRCodeModelRemoved(object sender, QRCodeModelEventArgs args)
        {
            RemoveQRCodeModel(args.QRCode);
        }

        private void RemoveQRCodeModel(QRCodeModel qrCodeModel)
        {
            var qrCodeId = qrCodeModel.Id;

            Destroy(_instantiatedQRCodeRepresentations[qrCodeId].gameObject);
            _instantiatedQRCodeRepresentations.Remove(qrCodeId);
        }

        private void OnRoomJoinAttemptFinished(object sender, QRCodeConnectionEventArgs args)
        {
            if (args.JoinAttemptSuccess == RoomJoinAttemptSuccess.Joined)
                Instantiate(_notificationPrefab).GetComponent<Notify>().Show("Successfully connected.");
            StartCoroutine(KeepQRCodeRepresentationVisibleAndDeleteCoroutine(args.UsedQRCodeModel));
        }

        private IEnumerator KeepQRCodeRepresentationVisibleAndDeleteCoroutine(QRCodeModel qrCodeModel)
        {
            yield return null;
            RemoveQRCodeModel(qrCodeModel);
        }
    }
}
