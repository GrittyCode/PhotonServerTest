using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Photon.Chat;
using UnityEngine;


using ExitGames.Client.Photon;


public class PlayerScript : MonoBehaviourPun
{
    PhotonView PV;
    // Start is called before the first frame update


    // 동적 오브젝트 이벤트 받기 코드
    void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += EventReceive;
    }
    void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= EventReceive;
    }
    void EventReceive(EventData photonEvent)
    {
        if(0 == photonEvent.Code)
        {
            object[] data = (object[])photonEvent.CustomData;
            for(int i = 0; i < data.Length; ++i)
            {
                print(data[i]);
            }
        }
    }
    ////////////////////////////////////////////////////////////////////






    void Start()
    {
        PV = photonView;

        // 만약 이게 Is Mince 경우 RPC 를 하고 매개변수를 넘겨주는 것 이다.
        if (PV.IsMine) PV.RPC("TestRPC", RpcTarget.All, "A", "B");

    }

    [PunRPC]
    void TestRPC(string value1, string value2)
    {
        print("RPC 실행");
    }

    void SendEvent()
    {
        object[] content = new object[] { new Vector3(10.0f, 2.0f, 5.0f), 1, 2, 5, 10 };
        PhotonNetwork.RaiseEvent(0, content, RaiseEventOptions.Default , SendOptions.SendReliable);
    }
    


    // Update is called once per frame
    void Update()
    {
        
    }
}
