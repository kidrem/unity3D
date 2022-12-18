using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour {
    public GameObject model;
    public PlayerInput pi;
    public float walkSpeed = 1.5f;
    public float runMultiplier = 3f;
    public float jumpVelocity = 4f;
    public float rollVelocity = 1f;

    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec; // 平面移动向量
    private Vector3 thrustVec; // 跳跃冲量
    private bool death = true;
    private bool lockPlanar = false; // 跳跃时锁死平面移动向量

    void Awake() {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    //刷新每秒60次
    void Update() {
        /*2.使用Lerp加权平均解决从走路到跑步没有过渡*/
        float targetRunMulti = pi.run ? 2.0f : 1.0f;
        anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), targetRunMulti, 0.3f));
        //播放翻滚动画
        if (rigid.velocity.magnitude > 1.0f) {
            anim.SetTrigger("roll");
        }
        //播放跳跃动画
        if (pi.jump) {
            anim.SetTrigger("jump");
        }
        
        //转向
        if(pi.Dmag > 0.01f) {
            /*1.旋转太快没有补帧*/
            /*2.使用Slerp内插值解决*/
            Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.2f);
            model.transform.forward = targetForward;
        }
        if(!lockPlanar) {
            //保存供物理引擎使用
            planarVec = pi.Dmag * model.transform.forward * walkSpeed * (pi.run ? runMultiplier : 1.0f);
        }
        if (!pi.getState()&&death)
        {
            death = false;
            anim.SetBool("live",false);
        }else if(pi.getState()){
            death = true;
            anim.SetBool("live", true);
        }
        
    }

    //物理引擎每秒50次
    private void FixedUpdate() {
        //1.修改位置
        //2.修改速度
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        //一帧
        thrustVec = Vector3.zero;
    }

    public void OnJumpEnter() {
        pi.inputEnabled = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, jumpVelocity, 0);
    }

    public void OnRollEnter() {
        pi.inputEnabled = false;
        lockPlanar = true;
    }

    public void OnRollUpdate() {
        thrustVec = model.transform.forward * anim.GetFloat("rollVelocity") * 1.0f;
    }

    public void OnGround() {
        anim.SetBool("OnGround", true);
    }

    public void NotOnGround() {
        anim.SetBool("OnGround", false);
    }

    public void OnGroundEnter() {
        pi.inputEnabled = true;
        lockPlanar = false;
    }

    public void OnFallEnter() {
        pi.inputEnabled = false;
        lockPlanar = true;
    }

    public void OnJabEnter() {
        pi.inputEnabled = false;
        lockPlanar = true;
    }

    public void OnJabUpdate() {
        thrustVec = model.transform.forward * anim.GetFloat("jabVelocity")*1.4f;
    }
}
