using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;

namespace PhishAR.Photon.Handlers
{
    public class PhotonConnectionHandler : IPhotonConnectionHandler
    {
        public EventHandler ConnectedToServer { get; set; }
        public EventHandler<DisconnectCause> Disconnected { get; set; }

        public PhotonConnectionHandler()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        public void Connect()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public void OnConnectedToMaster()
        {
            ConnectedToServer?.Invoke(this, EventArgs.Empty);
        }

        public void OnDisconnected(DisconnectCause cause)
        {
            Disconnected?.Invoke(this, cause);
        }

        #region Not implemented
        public void OnConnected()
        {
            // Use connected to master for joining rooms etc.
            // This only indicates connection is established.
        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {
        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {
        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
        }
        #endregion
    }
}
