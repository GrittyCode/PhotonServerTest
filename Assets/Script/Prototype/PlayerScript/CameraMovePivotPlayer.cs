using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovePivotPlayer : MonoBehaviour
{

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Vector3 playerPos = player.transform.position;
        playerPos.y = this.transform.position.y;
        this.transform.position = playerPos;
    }
}
