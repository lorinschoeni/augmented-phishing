using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections.Generic;

namespace PhishAR.Photon.Events
{
    public class PhotonEventReceiver : IPhotonEventReceiver
    {
        private readonly Dictionary<byte, HashSet<PhotonEventListenerBase>> _eventCodeListeners =
            new Dictionary<byte,
                HashSet<PhotonEventListenerBase>>();

        public PhotonEventReceiver()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        public void AddListener(byte code, PhotonEventListenerBase listener)
        {
            if (_eventCodeListeners.TryGetValue(code, out var eventListeners)) eventListeners.Add(listener);
            else _eventCodeListeners.Add(code, new HashSet<PhotonEventListenerBase> { listener });
        }

        public void RemoveListener(byte code, PhotonEventListenerBase listener)
        {
            if (_eventCodeListeners.TryGetValue(code, out var eventListeners)) eventListeners.Remove(listener);
        }

        public void OnEvent(EventData photonEvent)
        {
            var hasListener = _eventCodeListeners.TryGetValue(photonEvent.Code, out var listeners);
            if (!hasListener || listeners == null) return;
            foreach (var listener in listeners)
                listener.OnEventReceived(photonEvent.CustomData);
        }
    }
}
