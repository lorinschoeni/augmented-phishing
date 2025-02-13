using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

namespace PhishAR.Photon.Events
{
    public class PhotonEventSender : IPhotonEventSender
    {
        public void SendEvent(byte code, object content)
        {
            SendEvent(code, content, new RaiseEventOptions(), SendOptions.SendReliable);
        }

        public void SendEvent(byte code, object content, SendOptions sendOptions)
        {
            SendEvent(code, content, new RaiseEventOptions(), sendOptions);
        }

        public void SendEvent(byte code, object content, RaiseEventOptions eventOptions, SendOptions sendOptions)
        {
            PhotonNetwork.RaiseEvent(code, content, eventOptions, sendOptions);
        }
    }
}
