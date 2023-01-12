using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ExtensionMethods  {}
 
namespace MyExtensionMethods {
	public static class MyExtensions {
		public static IEnumerator DoMove(this MonoBehaviour mono, tween myTween) {
			//外层循环决定tween的循环次数
			for (; myTween.currentLoop < myTween.loops; myTween.currentLoop++) {
				Debug.Log ("i = " + myTween.currentLoop);
				Vector3 distance = (myTween.target - myTween.transform.position) / myTween.time;
				//计算每帧移动的距离
				for (float f = myTween.time; f >= 0.0f; f -= Time.deltaTime) {
					//just like call update()
					Debug.Log ("Move");
					myTween.transform.Translate (distance * Time.deltaTime);
					//移动
					yield return null;
					while (myTween.isPause == true) {
						Debug.Log ("Move Pause");
						yield return null;
					}
					//如果停止，将停在while中，下一帧不移动
 
				}
				if (myTween.currentLoop < myTween.loops - 1) {
					myTween.ResetPosition ();
				}
			}
			myTween.OnComplete ();
			//完成后执行回调函数。
		}
 
		public static tween DoMove(this Transform transform, Vector3 target, float time)
		{
			MonoBehaviour mono = transform.GetComponents<MonoBehaviour> () [0];
			tween myTween = new tween ("DoMove", transform, target, time);
			Coroutine coroutine =  mono.StartCoroutine (mono.DoMove(myTween));
			myTween.SetCoroutine (coroutine);
			return myTween;
		}
 
		public static IEnumerator DoRotate(this MonoBehaviour mono, tween myTween) {
			for (; myTween.currentLoop < myTween.loops; myTween.currentLoop++) {
				Vector3 angle = (myTween.target - myTween.transform.rotation.eulerAngles) / myTween.time;
				//Debug.Log ("angle = " + angle);
				for (float f = myTween.time; f >= 0.0f; f -= Time.deltaTime) {
					//just like call update()
					Debug.Log ("Rotate");
					myTween.transform.Rotate (angle * Time.deltaTime);
					yield return null;
					while (myTween.isPause == true) {
						Debug.Log ("Rotate Pause");
						yield return null;
					}
 
				}
				if (myTween.currentLoop < myTween.loops - 1) {
					myTween.ResetRotation ();
				}
			}
			myTween.OnComplete ();
		}
 
		public static tween DoRotate(this Transform transform, Vector3 target, float time)
		{
			MonoBehaviour mono = transform.GetComponents<MonoBehaviour> () [0];
			tween myTween = new tween ("DoRotate", transform, target, time);
			Coroutine coroutine =  mono.StartCoroutine (mono.DoRotate(myTween));
			myTween.SetCoroutine (coroutine);
			return myTween;
		}
 
		public static IEnumerator DoScale(this MonoBehaviour mono, tween myTween) {
			for (; myTween.currentLoop < myTween.loops; myTween.currentLoop++) {
				Vector3 scale = (myTween.target - myTween.transform.localScale) / myTween.time;
				for (float f = myTween.time; f >= 0.0f; f -= Time.deltaTime) {
					//just like call update()
					Debug.Log ("Scale");
					myTween.transform.localScale += scale * Time.deltaTime;
					yield return null;
					while (myTween.isPause == true) {
						Debug.Log ("Scale Pause");
						yield return null;
					}
 
				}
				if (myTween.currentLoop < myTween.loops - 1) {
					myTween.ResetScale ();
				}
			}
			myTween.OnComplete ();
		}
 
		public static tween DoScale(this Transform transform, Vector3 target, float time)
		{
			MonoBehaviour mono = transform.GetComponents<MonoBehaviour> () [0];
			tween myTween = new tween ("DoScale", transform, target, time);
			Coroutine coroutine =  mono.StartCoroutine (mono.DoScale(myTween));
			myTween.SetCoroutine (coroutine);
			return myTween;
		}
 
	}
}