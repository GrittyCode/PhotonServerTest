using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine;
using TMPro;
public class PlayerName : MonoBehaviourPun
{
    [SerializeField]
    TextMeshProUGUI nameField;
    // Start is called before the first frame update
    void Start()
    {
        nameField.text = photonView.Owner.NickName;
    }

}
