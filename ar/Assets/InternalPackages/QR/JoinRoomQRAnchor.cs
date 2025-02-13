using PhishAR.QR.Detection.Events;
using PhishAR.QR.Processing;
using UnityEngine;

namespace PhishAR.QR
{
    public class JoinRoomQRAnchor : MonoBehaviour, IJoinRoomQRAnchor
    {
        [SerializeField] private QRCodeConnectionController _QRCodeConnectionController;

        private void Start()
        {
            _QRCodeConnectionController.RoomJoinAttemptFinished += OnJoinRoomAttemptFinished;
        }

        public Transform Transform => transform;

        private void OnJoinRoomAttemptFinished(object sender, QRCodeConnectionEventArgs e)
        {
            if (e.JoinAttemptSuccess == RoomJoinAttemptSuccess.NotJoined) return;

            transform.SetPositionAndRotation(
                e.UsedQRCodeModel.CenterPose.position,
                e.UsedQRCodeModel.CenterPose.rotation
            );
        }
    }
}
