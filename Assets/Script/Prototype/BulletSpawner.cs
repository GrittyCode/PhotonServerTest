using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
public class BulletSpawner : MonoBehaviourPun
{
    public GameObject bulletPrefab;
    

    // Update is called once per frame
    void Update()
    {

        if (!photonView.IsMine)
            return;
        //Vector3 mousePos;
        //mousePos = Input.mousePosition;
        //mousePos = cam.ScreenToWorldPoint(mousePos);

        //mousePos = new Vector3( mousePos.x - this.transform.position.x
        //                        , mousePos.y - this.transform.position.y
        //                        , mousePos.z - this.transform.position.z );
        //bulletDirection = mousePos.normalized;

        if (Input.GetKey(KeyCode.Space))
        {
            photonView.RPC("ShotProcessOnServer", RpcTarget.MasterClient);
        }

    }

    [PunRPC]
    private void ShotProcessOnServer()
    {
        Fire();
    }

    void Fire()
    {
        //Vector3 bulletDirection = GetComponent<RotationPivotMouse>().LookVec;
        //Debug.Log(bulletDirection);
        // 나의 방향 , 및 회전값을 기준으로 생성해달라고 요청을 하는 것 인데 , 알아서 트랜스폼값이 바뀐걸 보면 위치값은 갱신이 되는데
        // rotation 정보값이 제대로 전달이 안된게 아닐까
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, this.transform.position, this.transform.rotation);
        Debug.Log(this.transform.rotation);
        //bullet.transform.LookAt(bulletDirection);
        bullet.GetComponent<Bullet>().owner = this.gameObject;
    }
}
