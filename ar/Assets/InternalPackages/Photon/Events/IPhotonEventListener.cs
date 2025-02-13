using System;

namespace PhishAR.Photon.Events
{
    public interface IPhotonEventListener<T>
    {
        public EventHandler<T> EventReceived { get; set; }
    }
}
