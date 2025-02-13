using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;

namespace PhishAR.Photon.Handlers
{
    public class PhotonMatchmakingHandler : IPhotonMatchmakingHandler
    {
        public EventHandler<string> JoinedRoom { get; set; }
        public EventHandler<string> JoinRoomFailed { get; set; }

        private string _currentlyConnectedRoom;
        private string _lastRoomName;

        public PhotonMatchmakingHandler()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        public void JoinOrCreateRoom(string roomName)
        {
            if (!PhotonNetwork.IsConnectedAndReady) return;
            
            _lastRoomName = roomName;
            
            if (string.IsNullOrEmpty(_currentlyConnectedRoom)) PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions() { IsOpen = true, IsVisible = true }, null);
            else JoinRoomFailed?.Invoke(this, roomName);
        }

        public void OnJoinedRoom()
        {
            _currentlyConnectedRoom = PhotonNetwork.CurrentRoom.Name;
            JoinedRoom?.Invoke(this, _lastRoomName);
        }

        public void OnJoinRoomFailed(short returnCode, string message)
        {
            JoinRoomFailed?.Invoke(this, _lastRoomName);
        }
        
        #region Not implemented
        public void OnFriendListUpdate(List<FriendInfo> friendList)
        {
        }

        public void OnJoinRandomFailed(short returnCode, string message)
        {
        }

        public void OnLeftRoom()
        {
        }
        
        public void OnCreatedRoom()
        {
        }

        public void OnCreateRoomFailed(short returnCode, string message)
        {
        }
        #endregion
    }
}
