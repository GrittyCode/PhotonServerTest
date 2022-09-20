using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 4.0f;
    public float Speed => speed;

    [SerializeField]
    public GameObject owner;


    private Rigidbody bulletRigidbody;
    private float lifeTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        bulletRigidbody.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (0.0f >= lifeTime)
            Destroy(this.gameObject);
    }
}
