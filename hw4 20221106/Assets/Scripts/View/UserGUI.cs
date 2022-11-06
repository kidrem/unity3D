using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyNamespace {
    public class UserGUI : MonoBehaviour {
        private string guide =  "\n";
        private IUserAction action;
        private GUIStyle textStyle;
        private GUIStyle hintStyle;
        private GUIStyle btnStyle;
        public CharacterController characterCtrl;
        public int status;

	    // Use this for initialization
	    void Start () {
            status = 0;
            action = Director.GetInstance().CurrentSecnController as IUserAction;
        }
	
	    // Update is called once per frame
	    void OnGUI () {
            textStyle = new GUIStyle {
                fontSize = 40,
                alignment = TextAnchor.MiddleCenter
            };
            hintStyle = new GUIStyle {
                fontSize = 15,
                fontStyle = FontStyle.Normal
            };
            btnStyle = new GUIStyle("button") {
                fontSize = 30
            };
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 195, 100, 50), 
                "Priest-And-Devil", textStyle);
            GUI.Label(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 125, 100, 50), 
                "Cube: Periest\n Syphere: Devil\n\n" + guide, hintStyle);
            if (status == 1) {
                // GameOver
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "GameOver!", textStyle);
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", btnStyle)) {
                    status = 0;
                    action.Restart();
                }
            } else if (status == 2) {
                // Win
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "You Win!", textStyle);
                if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", btnStyle)) {
                    status = 0;
                    action.Restart();
                }
            }
	    }

        public void SetCharacterCtrl(CharacterController _characterCtrl) {
            characterCtrl = _characterCtrl;
        }

        void OnMouseDown() {
            if (gameObject.name == "boat") {
                action.MoveBoat();
            } else {
                action.CharacterClicked(characterCtrl);
            }
        }
    }
}