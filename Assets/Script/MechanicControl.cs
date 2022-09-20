using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class MechanicControl : MonoBehaviourPun , IPunObservable
{

    NetworkManager NM;
    PhotonView PV;


    public float runSpeed = 4.0f;
    public float rotationSpeed = 1000.0f;

    CharacterController pcController;
    Vector3 direction;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        PV = photonView;
        NM = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();

        pcController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.gameObject.transform.position);

            stream.SendNext(this.gameObject.transform.rotation);
        }
        else
        {
            
        }
    }
    private void FixedUpdate()
    {
        // 포톤 뷰에서 로컬이 내가 아닐때 움직이지 마라.
        if (!photonView.IsMine)
            return;
        animator.SetFloat("Speed", pcController.velocity.magnitude);
        CharacterControl_Slerp();
        Punching();
        SetRootMotion();
    }
    void SetRootMotion()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Punching1") == true
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Punching2") == true)
        {
            animator.applyRootMotion = true;
        }
        else
            animator.applyRootMotion = false;

    }

    void Punching()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Punching1"))
            {
                animator.SetTrigger("Punching2");
            }
            else
            {
                animator.SetTrigger("Punching1");
            }
        }

    }

    void CharacterControl_Slerp()
    {
        // 펀칭을 했을 때 캐릭터가 움직이지 않도록 설정
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Punching1")
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Punching2"))
        {
            direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (direction.sqrMagnitude > 0.01f)
            {
                Vector3 forward = Vector3.Slerp(transform.forward
                                                , direction
                                                , rotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction));
                transform.LookAt(transform.position + forward);
            }
            else
            {

            }
            pcController.Move(direction * runSpeed * Time.deltaTime);
        }
    }


}
