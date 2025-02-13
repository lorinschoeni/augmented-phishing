using ExitGames.Client.Photon;
using PhishAR.Core.Services;
using Photon.Realtime;

namespace PhishAR.Photon.Events
{
    public interface IPhotonEventSender : IService
    {
        public void SendEvent(byte code, object content);

        public void SendEvent(byte code, object content, SendOptions sendOptions);

        public void SendEvent(byte code, object content, RaiseEventOptions eventOptions, SendOptions sendOptions);
    }
}
