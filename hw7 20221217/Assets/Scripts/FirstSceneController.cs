using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneController : MonoBehaviour, IUserAction, ISceneController {
    public GuardFactory guard_factory;
    public ScoreRecorder recorder;
    public GuardActionManager action_manager;
    public int playerSign = -1;
    public GameObject player;
    public int max_score=0;
    public UserGUI gui;
    private List<GameObject> guards;
    private bool game_over = false;

    
    void Awake() {
        SSDirector director = SSDirector.GetInstance();
        director.CurrentScenceController = this;
        guard_factory = Singleton<GuardFactory>.Instance;
        action_manager = gameObject.AddComponent<GuardActionManager>() as GuardActionManager;
        gui = gameObject.AddComponent<UserGUI>() as UserGUI;
        LoadResources();
        recorder = Singleton<ScoreRecorder>.Instance;
    }

    void Update() {
        if(!game_over)
        for (int i = 0; i < guards.Count; i++) {
            guards[i].gameObject.GetComponent<GuardData>().playerSign = playerSign;
        }
        else
        {
            player.GetComponent<PlayerInput>().changeStateF();
        }
    }


    public void LoadResources() {
        Instantiate(Resources.Load<GameObject>("Prefabs/Plane"));
        player = Instantiate(
            Resources.Load("Prefabs/Player"), 
            new Vector3(13, 8, -13), Quaternion.identity) as GameObject;
        guards = guard_factory.GetPatrols();

        for (int i = 0; i < guards.Count; i++) {
            action_manager.GuardPatrol(guards[i], player);
        }
    }

    public int GetScore() {
        return recorder.GetScore();
    }

    public bool GetGameover() {
        return game_over;
    }

    public void Restart() {
        SceneManager.LoadScene("Scenes/mySence");
    }

    void OnEnable() {
        GameEventManager.ScoreChange += AddScore;
        GameEventManager.GameoverChange += Gameover;
    }
    void OnDisable() {
        GameEventManager.ScoreChange -= AddScore;
        GameEventManager.GameoverChange -= Gameover;
    }

    void AddScore() {
        recorder.AddScore();
    }

    void Gameover() {
        game_over = true;
    }
    public void setScore(int s)
    {
        max_score = s;
    }
    public int getScore()
    {
        return max_score;
    }
}
