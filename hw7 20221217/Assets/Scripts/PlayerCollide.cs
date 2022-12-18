using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollide : MonoBehaviour {
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Guard") {
            Singleton<GameEventManager>.Instance.PlayerGameover();
        }
    }
}
