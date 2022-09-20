using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;

using UnityEngine;
using UnityEngine.UI;

using ExitGames.Client.Photon;

public class NetworkManager : MonoBehaviourPunCallbacks , IOnEventCallback
{
    [Header("DisconnectPanel")]
    public GameObject DisconnectPanel;
    public InputField nickNameInput;

    [Header("RoomPanel")]
    public GameObject RoomPanel;


    public void Connect()
    {
        PhotonNetwork.LocalPlayer.NickName = nickNameInput.text;
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnEvent(EventData photonEvent)
    {
        if(0 == photonEvent.Code)
        {
            object[] data = (object[])photonEvent.CustomData;
            for(int i = 0; i<data.Length;++i)
            {
                print(data[i]);
            }
        }
    }
    void Update()
    {
        PlayerScript Player = FindPlayer();
    }
    PlayerScript FindPlayer()
    {
        foreach(GameObject Player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (Player.GetPhotonView().IsMine)
                return Player.GetComponent<PlayerScript>();
        }
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Screen.SetResolution(540, 960, false);
        
    }

    // 마스터 서버나 로비에 접속을 하면 참가를 할 수 있도록
    public override void OnConnectedToMaster()
    {
        // 방에 참가 또는 생성을 하게 된다.
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 5 }, null);
    }
    // 참가시 callback 함수
    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        ShowPanel(RoomPanel);
    }

    void ShowPanel(GameObject CurPanel)
    {
        DisconnectPanel.SetActive(false);
        RoomPanel.SetActive(false);

        CurPanel.SetActive(true);
    }

}
