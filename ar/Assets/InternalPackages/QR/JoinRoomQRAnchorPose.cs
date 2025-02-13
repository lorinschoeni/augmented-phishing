using PhishAR.Core.Services;
using UnityEngine;

namespace PhishAR.QR
{
    public class JoinRoomQRAnchorPose : MonoBehaviour
    {
        private IJoinRoomQRAnchor _joinRoomQrAnchor;

        private void Start()
        {
            ServiceLocator.TryGetService(out _joinRoomQrAnchor);
        }

        private void Update()
        {
            transform.SetPositionAndRotation(
                _joinRoomQrAnchor.Transform.position,
                _joinRoomQrAnchor.Transform.rotation
            );
        }
    }
}
