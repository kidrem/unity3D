using UnityEngine;
using System.Collections;
using MyExtensionMethods;
 
public class dotweenTest : MonoBehaviour {
	private tween myTween;
	// Use this for initialization
	void Start () {
		DOTween.Init ();
		myTween = transform.DoMove (new Vector3 (15.0f, 0f, 0f), 3.0f).SetLoops(3);
		transform.DoRotate (new Vector3(150f, 0f, 0f), 3.0f).SetLoops(1);
		transform.DoScale (new Vector3 (3f, 3f, 3f), 3.0f).SetLoops(1);
	}
 
	// Update is called once per frame
	void Update () {
		//Debug.Log ("tween Size = " + DOTween.getTweenSize ());
		if (Input.GetKeyDown ("f")) {
			Debug.Log ("f down PauseAll");
			DOTween.PauseAll ();
		}
		if (Input.GetKeyDown ("g")) {
			Debug.Log ("g down PlayAll");
			DOTween.PlayAll ();
		}
		if (Input.GetKeyDown ("h")) {
			Debug.Log ("h down RestartAll");
			DOTween.RestartAll ();
		}
		if (Input.GetKeyDown ("j")) {
			Debug.Log ("j down CompleteAll");
			DOTween.CompleteAll ();
		}
		if (Input.GetKeyDown ("a")) {
			Debug.Log ("a down Pause DoScale");
			DOTween.Pause("DoScale");
		}
	}
}