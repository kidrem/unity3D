using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class tween {
	public string tweenType; 	// 记录tween动作类型
	public string id;	// 用于过滤
	public int loops;	// 记录循环次数
	public int currentLoop;		
 
	public Transform transform;		
	public Vector3 originalPosition;	
	public Vector3 originalRotation;	
	public Vector3 originalScale;	 
	public Vector3 target;	// 记录目标position，rotation或者scale
 
	public float time;	//记录动作限时
	public bool isPause;	//记录tween是否停止
	public bool autoKill;	//记录tween是否自动被杀死
	public Coroutine coroutine;		//记录tween对应的协程
 
	public delegate void Callback();	//回调函数的委托
	public Callback onComplete;		//完成时的回调
	public Callback onKill;		//被杀死时的回调
	public Callback onPause;	//停止时的回调
 
	//一下Set前缀的方法用于设置属性，为了能够像官方一样链式调用，就返回tween自己
	public tween(string type, Transform trans, Vector3 tar, float ti) {
		tweenType = type;
		transform = trans;
		target = tar;
		time = ti;
		//设置特殊值
		originalPosition = new Vector3 (trans.position.x, trans.position.y, trans.position.z);
		originalRotation = new Vector3 (trans.rotation.x, trans.rotation.y, trans.rotation.z);
		originalScale = new Vector3 (trans.localScale.x, trans.localScale.y, trans.localScale.z);
		//设置起始transform，在Restart等时候用
		id = type;
		loops = 1;
		currentLoop = 0;
		isPause = false;
		autoKill = true;
		coroutine = null;
		onComplete = null;
		//设置默认值
		DOTween.getInstance ().Add (this);
		//加进队列用于管理
	}
	
	//设置循环次数
	public tween SetLoops(int l) {
		loops = l;
		return this;
	}
	
	//设置过滤信息
	public tween SetId(string i) {
		id = i;
		return this;
	}
	
	//设置对应协程
	public tween SetCoroutine(Coroutine c) {
		coroutine = c;
		return this;
	}

	//设置是否被自动杀死
	public tween SetAutoKill(bool auto) {
		autoKill = auto;
		return this;
	}

	//设置完成时的回调函数
	public tween SetOnComplete(Callback c) {
		onComplete += c;
		return this;
	}

	//设置被杀死时的回调函数
	public tween SetOnKill(Callback c) {
		onKill += c;
		return this;
	}

	//设置停止时的回调函数
	public tween SetOnPause(Callback c) {
		onPause += c;
		return this;
	}

	//停止
	public void Pause() {
		isPause = true;
	}

	//播放
	public void Play() {
		isPause = false;
	}

	//重启所有
	public void Restart() {
		ResetPosition ();
		ResetRotation ();
		ResetScale();
		Play ();
	}

	//重启position的动作
	public void ResetPosition() {
		transform.position = new Vector3 (originalPosition.x, originalPosition.y, originalPosition.z);
	}

	//重启rotation的动作
	public void ResetRotation() {
		transform.rotation = Quaternion.Euler(new Vector3 (originalRotation.x, originalRotation.y, originalRotation.z));
	}

	//重启scale的动作
	public void ResetScale() {
		transform.localScale = new Vector3 (originalScale.x, originalScale.y, originalScale.z);
	}
	//立即完成tween动作
	public void Complete() {
		if (tweenType == "DoMove") {
			transform.position = target;
		} else if (tweenType == "DoRotate") {
			transform.rotation = Quaternion.Euler (target);
		} else if (tweenType == "DoScale") {
			transform.localScale = target;
		} else {
			Debug.Log ("Wrong typeName!");
		}
		OnComplete ();
	}

	//杀死tween
	public void Kill() {
		MonoBehaviour mono = transform.GetComponent<MonoBehaviour> ();
		mono.StopCoroutine (coroutine);
		DOTween.getInstance ().Remove (this);
	}

	//完成时的回调函数
	public void OnComplete() {
		if (onComplete != null) {
			onComplete ();
		}
		if (autoKill) {
			Kill ();
		}
	}

	//杀死时的回调函数
	public void OnKill() {
		if (onKill != null) {
			onKill ();
		}
	}

	//停止时的回调函数
	public void OnPause() {
		if (onPause != null) {
			onPause ();
		}
	}
}