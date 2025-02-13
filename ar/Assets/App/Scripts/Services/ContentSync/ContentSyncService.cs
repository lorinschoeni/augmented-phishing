using System;
using Newtonsoft.Json;
using PhishAR.Core.Services;
using PhishAR.ETHTB.Models;
using PhishAR.Web;
using UnityEngine;
using UnityEngine.Networking;

namespace PhishAR.ETHTB.Services.ContentSync
{
    public class ContentSyncService : MonoBehaviour, IContentSyncService
    {
        private const float SyncFrequency = 0.5f;

        private string _contentEndpoint;
        private DataContext _dataContext;

        private float _timeSinceLastSync;
        private IWebRequestService _webRequestService;

        public string LastResponsePayload { get; set; }

        private void Start()
        {
            ServiceLocator.TryGetService(out _webRequestService);
            ServiceLocator.TryGetService(out _dataContext);
            _dataContext.WebQrUpdated += OnWebQrUpdated;
        }

        private void Update()
        {
            _timeSinceLastSync += Time.deltaTime;
            if (_timeSinceLastSync > SyncFrequency)
            {
                _timeSinceLastSync -= SyncFrequency;
                Sync();
            }
        }

        private void OnWebQrUpdated(object sender, EventArgs e)
        {
            _contentEndpoint = _dataContext.WebQr.Payload.IpAddress;
        }

        private void Sync()
        {
            if (string.IsNullOrEmpty(_contentEndpoint)) return;
            _webRequestService.SendRequest(UnityWebRequest.Get(_contentEndpoint), response =>
            {
                if (response.result == UnityWebRequest.Result.ConnectionError ||
                    response.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("Error: " + response.error);
                }
                else
                {
                    var payload = response.downloadHandler.text;
                    if (payload == LastResponsePayload) return;
                    LastResponsePayload = payload;
                    _dataContext.OverlayModel = JsonConvert.DeserializeObject<OverlayModel>(payload);
                }
            });
        }
    }
}
