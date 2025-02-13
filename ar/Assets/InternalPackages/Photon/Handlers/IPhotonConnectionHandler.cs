using PhishAR.Core.Services;
using Photon.Realtime;
using System;

namespace PhishAR.Photon.Handlers
{
    public interface IPhotonConnectionHandler : IConnectionCallbacks, IService
    {
        EventHandler ConnectedToServer { get; set; }
        EventHandler<DisconnectCause> Disconnected { get; set; }
        void Connect();
    }
}
