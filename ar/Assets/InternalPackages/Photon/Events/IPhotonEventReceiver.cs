using PhishAR.Core.Services;
using Photon.Realtime;

namespace PhishAR.Photon.Events
{
    public interface IPhotonEventReceiver : IOnEventCallback, IService
    {
        public void AddListener(byte code, PhotonEventListenerBase listener);
        public void RemoveListener(byte code, PhotonEventListenerBase listener);
    }
}
