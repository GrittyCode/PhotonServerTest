using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    int startPlayerCount = 4;
    public int connectCount = 0;
    [SerializeField]
    TextMeshProUGUI nameText;
    [SerializeField]
    TextMeshProUGUI connectText;


    int roomCount = 0;
    string defaultRoomName = "Room";


    private void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
    }
    public void Connect()
    {
        Debug.Log("Connect");
        PhotonNetwork.LocalPlayer.NickName = nameText.text;
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        
        PhotonNetwork.JoinOrCreateRoom("Room"+ roomCount.ToString(), new RoomOptions { MaxPlayers = 2 },null);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("접속 실패");
        ++roomCount;
        PhotonNetwork.JoinOrCreateRoom("Room" + roomCount.ToString(), new RoomOptions { MaxPlayers = 2 }, null);

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        connectCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if (startPlayerCount == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            GetComponent<PhotonView>().RPC("LoadingInGame", RpcTarget.All);
        }
        else if (PhotonNetwork.IsConnected)
        {
            connectCount = PhotonNetwork.CurrentRoom.PlayerCount;
            connectText.GetComponent<ConnectCountText>().RefreshServerText(connectCount);
        }
    }

    [PunRPC]
    private void LoadingInGame()
    {
        PhotonNetwork.LoadLevel("Map");

    }
}
