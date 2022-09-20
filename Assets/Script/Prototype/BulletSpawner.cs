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
        // ���� ���� , �� ȸ������ �������� �����ش޶�� ��û�� �ϴ� �� �ε� , �˾Ƽ� Ʈ���������� �ٲ�� ���� ��ġ���� ������ �Ǵµ�
        // rotation �������� ����� ������ �ȵȰ� �ƴұ�
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, this.transform.position, this.transform.rotation);
        Debug.Log(this.transform.rotation);
        //bullet.transform.LookAt(bulletDirection);
        bullet.GetComponent<Bullet>().owner = this.gameObject;
    }
}
