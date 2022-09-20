using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class RotationPivotMouse : MonoBehaviourPun
{

    [SerializeField] private GameObject Body;
    [SerializeField] Camera cam;

    //Ray
    Ray ray;
    RaycastHit hitdata;

    private Vector3 lookVec;
    public Vector3 LookVec => lookVec;
    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitdata))
        {
            lookVec = new Vector3(hitdata.point.x, transform.position.y, hitdata.point.z);
            Body.transform.LookAt(lookVec);
        }
        

    }

}
