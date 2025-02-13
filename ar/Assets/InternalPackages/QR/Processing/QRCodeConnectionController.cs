using PhishAR.Core.Services;
using PhishAR.Photon.Handlers;
using PhishAR.QR.Detection;
using PhishAR.QR.Detection.Events;
using PhishAR.QR.Detection.Models;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PhishAR.QR.Processing
{
    public class QRCodeConnectionController : MonoBehaviour
    {
        [SerializeField] private QRCodeDetectionManager _qrCodeDetectionManager;

        private readonly Dictionary<Guid, Pose> _ignoredInitialStartingPoses = new Dictionary<Guid, Pose>();

        private readonly Dictionary<string, QRCodeModel> _usedQRCodes = new Dictionary<string, QRCodeModel>();

#if !UNITY_EDITOR //This is UWP specific so it is not neccessary in the editor, also throws KeyNotFounException in editor
        private void Start()
        {
            _qrCodeDetectionManager.QRCodeModelAdded += OnQRCodeModelAdded;
            _qrCodeDetectionManager.QRCodeModelUpdated += OnQRCodeModelUpdated;
            if (!ServiceLocator.TryGetService<IPhotonMatchmakingHandler>(out var matchmakingHandler)) return;

            matchmakingHandler.JoinedRoom += (sender, args) =>
                ProcessRoomJoinAttemptFinished(_usedQRCodes[args], RoomJoinAttemptSuccess.Joined);

            matchmakingHandler.JoinRoomFailed += (sender, args) =>
                ProcessRoomJoinAttemptFinished(_usedQRCodes[args], RoomJoinAttemptSuccess.NotJoined);
        }
#endif

        public event EventHandler<QRCodeConnectionEventArgs> RoomJoinAttemptFinished;

        private void OnQRCodeModelAdded(object sender, QRCodeModelEventArgs args)
        {
            _usedQRCodes.Add(args.QRCode.Payload, args.QRCode);
            _ignoredInitialStartingPoses.Add(args.QRCode.Id, args.QRCode.TopLeftPose);
        }

        private void OnQRCodeModelUpdated(object sender, QRCodeModelEventArgs args)
        {
            _usedQRCodes[args.QRCode.Payload] = args.QRCode;

            if (!ShouldJoinRoom(args)) return;

            if (!ServiceLocator.TryGetService<IPhotonMatchmakingHandler>(out var matchmakingHandler)) return;
            matchmakingHandler.JoinOrCreateRoom(args.QRCode.Payload);
        }

        private bool ShouldJoinRoom(QRCodeModelEventArgs args)
        {
            return !_ignoredInitialStartingPoses[args.QRCode.Id].position.Equals(args.QRCode.TopLeftPose.position) &&
                   !_ignoredInitialStartingPoses[args.QRCode.Id].rotation.Equals(args.QRCode.TopLeftPose.rotation);
        }

        private void ProcessRoomJoinAttemptFinished(QRCodeModel qrCodeModel,
            RoomJoinAttemptSuccess roomJoinAttemptSuccess)
        {
            RoomJoinAttemptFinished?.Invoke(this, new QRCodeConnectionEventArgs(qrCodeModel, roomJoinAttemptSuccess));

            _usedQRCodes.Remove(qrCodeModel.Payload);
            _ignoredInitialStartingPoses.Remove(qrCodeModel.Id);
            _qrCodeDetectionManager.RemoveQRCodeModel(qrCodeModel);
        }
    }
}
