using PhishAR.Photon.Utils;
using System;

namespace PhishAR.Photon.Events
{
    public abstract class PhotonEventListenerBase
    {
        protected PhotonEventListenerBase(IPhotonEventReceiver eventReceiver, byte eventCode)
        {
            EventCode = eventCode;
            eventReceiver.AddListener(EventCode, this);
        }

        public byte EventCode { get; set; }

        public abstract void OnEventReceived(object data);
    }

    public class PhotonEventListener<T> : PhotonEventListenerBase, IPhotonEventListener<T>
    {
        private readonly byte _eventCode;

        public PhotonEventListener(IPhotonEventReceiver eventReceiver, byte eventCode) : base
            (eventReceiver, eventCode)
        {
        }

        public EventHandler<T> EventReceived { get; set; }

        public override void OnEventReceived(object data)
        {
            if (PhotonPayloadConvert.TryDeserializeObject(data, out T eventModel))
                EventReceived?.Invoke(this, eventModel);
        }
    }
}
