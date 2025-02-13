using PhishAR.Core.Services;
using PhishAR.Photon.Handlers;
using UnityEngine;

namespace PhishAR.Photon.Utils
{
    public class JoinRoomFromEditor : MonoBehaviour
    {
        [SerializeField] private string _roomName;

        [ContextMenu(nameof(JoinRoom))]
        public void JoinRoom()
        {
            if (!ServiceLocator.TryGetService<IPhotonMatchmakingHandler>(out var matchmakingHandler)) return;
            matchmakingHandler.JoinOrCreateRoom(_roomName);
        }
    }
}
