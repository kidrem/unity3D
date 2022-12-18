using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardData : MonoBehaviour {
    public GameObject model;
    public float walkSpeed = 1.2f;
    public float runSpeed = 2.5f;
    public int sign;
    public bool isFollow = false;
    public int playerSign = -1;
    public Vector3 start_position;

    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;

    void Awake() {
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    public void OnGround() {
        anim.SetBool("OnGround", true);
    }
    public void OnGroundEnter() {
        
    }
}
