using PhishAR.Core.Services;
using Photon.Realtime;
using System;

namespace PhishAR.Photon.Handlers
{
    public interface IPhotonMatchmakingHandler : IMatchmakingCallbacks, IService
    {
        EventHandler<string> JoinedRoom { get; set; }
        EventHandler<string> JoinRoomFailed { get; set; }
        void JoinOrCreateRoom(string roomName);
    }
}
