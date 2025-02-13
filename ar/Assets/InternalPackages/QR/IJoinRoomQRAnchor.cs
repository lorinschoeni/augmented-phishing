using PhishAR.Core.Services;
using UnityEngine;

namespace PhishAR.QR
{
    public interface IJoinRoomQRAnchor : IService
    {
        public Transform Transform { get; }
    }
}
