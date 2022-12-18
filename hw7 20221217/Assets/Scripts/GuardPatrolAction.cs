﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPatrolAction : SSAction {
    private enum Dirction { EAST, NORTH, WEST, SOUTH };
    private GameObject player;
    private float pos_x, pos_z;
    private float move_length;
    private bool move_sign = true;
    private Dirction dirction = Dirction.EAST;
    public bool inputEnabled = true;
    private GuardData data;
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private GuardPatrolAction() { }
    public override void Start() {
        data = gameobject.GetComponent<GuardData>();
        anim = gameobject.GetComponent<Animator>();
        rigid = gameobject.GetComponent<Rigidbody>();
        anim.SetFloat("forward", 1.0f);
    }
    public static GuardPatrolAction GetSSAction(Vector3 location, GameObject player) {
        GuardPatrolAction action = CreateInstance<GuardPatrolAction>();
        action.pos_x = location.x;
        action.pos_z = location.z;
        action.player = player;
        action.move_length = Random.Range(5, 6);
        return action;
    }

    public override void Update() {
        //保留供物理引擎调用
        planarVec = gameobject.transform.forward * data.walkSpeed;
    }

    public override void FixedUpdate() {
        Gopatrol();
        if (data.playerSign == data.sign&& player.GetComponent<PlayerInput>().getState()) {
            this.destroy = true;
            this.callback.SSActionEvent(this, SSActionEventType.Competeted, 0, this.gameobject);
        }
    }

    void Gopatrol() {
        if (move_sign) {
            switch (dirction) {
                case Dirction.EAST:
                    pos_x -= move_length;
                    break;
                case Dirction.NORTH:
                    pos_z += move_length;
                    break;
                case Dirction.WEST:
                    pos_x += move_length;
                    break;
                case Dirction.SOUTH:
                    pos_z -= move_length;
                    break;
            }
            move_sign = false;
        }
        this.transform.LookAt(new Vector3(pos_x, 0, pos_z));
        float distance = Vector3.Distance(transform.position, new Vector3(pos_x, 0, pos_z));

        if (distance > 0.9) {
            rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z);
        } else {
            dirction = dirction + 1;
            if(dirction > Dirction.SOUTH) {
                dirction = Dirction.EAST;
            }
            move_sign = true;
        }
    }
}
