using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {
    private IUserAction action;
    private GUIStyle score_style = new GUIStyle();
    private GUIStyle title_style = new GUIStyle();
    private GUIStyle over_style = new GUIStyle();
    private GUIStyle text_style = new GUIStyle();
    public int max_score = 0;
    public bool windows = false;
    void Start () {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
        title_style.normal.textColor = new Color(0,0,0);
        title_style.fontSize = 60;
        score_style.normal.textColor = new Color(1,0,0);
        score_style.fontSize = 40;
        over_style.normal.textColor = new Color(1, 0, 0);
        over_style.fontSize = 50;
        text_style.normal.textColor = new Color(1, 0, 0);
        text_style.fontSize = 20;
        max_score = action.getScore();
    }

    [Obsolete]
    private void OnGUI() {
        GUI.Label(new Rect(Screen.width/2-140, 10, 200, 50), "智能巡逻兵", title_style);
        GUI.Label(new Rect(20, 40, 200, 50), "分数:", score_style);
        GUI.Label(new Rect(120, 40, 200, 50), action.GetScore().ToString(), score_style);
        GUI.Label(new Rect(20, 80, 200, 50), "最高分:", score_style);
        GUI.Label(new Rect(150, 80, 200, 50), (max_score> action.GetScore()?max_score: action.GetScore()).ToString(), score_style);
        if (windows&&Input.GetKeyDown(KeyCode.Escape))
        {
            windows = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape)||windows)
        {
            windows = true;
            GUI.Window(0, new Rect(365, 160, 600, 400), funcwin, "菜单");
        }
        if (action.GetGameover()) {
            GUI.Label(new Rect(Screen.width / 2 - 90, Screen.height / 2 - 100, 100, 100), "游戏结束", over_style);
            if (max_score < action.GetScore()) {
                max_score = action.GetScore();
                action.setScore(max_score);
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 20, 100, 50), "重新开始")) {
                action.Restart();
                return;
            }
        }
    }

    [Obsolete]
    private void funcwin(int id)
    {
        GUI.Label(new Rect(150,100,200,300), "游戏玩法：\n使用W、A、S、D移动\n使用↑、↓、←、→来控制视角\n使用左shift来加速\n使用Space跳跃或翻滚", text_style);
        if (GUI.Button(new Rect(100,300,120,60), "返回"))
        {
            windows = false;
        }
        if (GUI.Button(new Rect(400, 300, 120, 60), "退出"))
        {
            Application.Quit();
        }
    }

}
