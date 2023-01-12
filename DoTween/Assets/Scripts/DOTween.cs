using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class DOTween  {
	private static DOTween _instance; //DOTween类的单实例
	private static List<tween> tweenList = new List<tween>(); //管理tween对象的链表
	//初始化一个DOTween的基本属性
	public static void Init() {
		_instance = new DOTween ();
	}
	//获得DOTween的单实例
	public static DOTween getInstance() {
		if (_instance == null) {
			Init ();
		}
		return _instance;
	}
	//为tween的链表加入新的tween
	public void Add(tween newTween) {
		tweenList.Add (newTween);
	}
	//从链表中删除kill的tween
	public void Remove(tween oldTween) {
		tweenList.Remove (oldTween);
	}
 
	/*public static int getTweenSize() {
		return tweenList.Count;
	}*/
	//停止所有的tween
	public static void PauseAll() {
		foreach (tween t in tweenList) {
			t.Pause ();
		}
	}
	//通过filter过滤停止指定的tween
	public static void Pause(string filter) {
		foreach (tween t in tweenList) {
			if (t.id == filter) {
				t.Pause ();
			}
		}
	}
	//通过transform停止指定位置的tween
	public static void Pause(Transform trans) {
		foreach (tween t in tweenList) {
			if (t.transform == trans) {
				t.Pause ();
			}
		}
	}
	//杀死所有tween
	public static void KillAll() {
		foreach (tween t in tweenList) {
			t.Kill ();
		}
	}
	//通过filter过滤杀死指定的tween
	public static void Kill(string filter) {
		foreach (tween t in tweenList) {
			if (t.id == filter) {
				t.Kill ();
			}
		}
	}
	//通过transform停止指定位置的tween
	public static void Kill(Transform trans) {
		foreach(tween t in tweenList) {
			if (t.transform == trans) {
				t.Kill ();
			}
		}
	}
	//播放所有tween
	public static void PlayAll() {
		foreach (tween t in tweenList) {
			t.Play ();
		}
	}
	//通过filter过滤播放制定的tween
	public static void Play(string filter) {
		foreach (tween t in tweenList) {
			if (t.id == filter) {
				t.Play ();
			}
		}
	}
	//通过transform播放指定位置的tween
	public static void Play(Transform trans) {
		foreach (tween t in tweenList) {
			if (t.transform == trans) {
				t.Play ();
			}
		}
	}
	//反转所有tween的isPause属性
	public static void TogglePauseAll() {
		foreach (tween t in tweenList) {
			if (t.isPause) {
				t.Play ();
			} else {
				t.Pause ();
			}
		}
	}
	//通过filter过滤反转指定的tween的isPause
	public static void TogglePause(string filter) {
		foreach (tween t in tweenList) {
			if (t.id == filter) {
				if (t.isPause) {
					t.Play ();
				} else {
					t.Pause ();
				}
			}
		}
	}
	//通过transform反转指定位置的tween属性
	public static void TogglePause(Transform trans) {
		foreach (tween t in tweenList) {
			if (t.transform == trans) {
				if (t.isPause) {
					t.Play ();
				} else {
					t.Pause ();
				}
			}
		}
	}
	//重启所有tween
	public static void RestartAll() {
		foreach (tween t in tweenList) {
			t.currentLoop = 0;
			t.Restart ();
		}
	}
	//通过filter过滤重启指定的tween
	public static void Restart(string filter) {
		foreach (tween t in tweenList) {
			if (t.id == filter) {
				t.currentLoop = 0;
				t.Restart ();
			}
		}
	}
	//通过transform重启指定位置的tween
	public static void Restart(Transform trans) {
		foreach (tween t in tweenList) {
			if (t.transform == trans) {
				t.currentLoop = 0;
				t.Restart ();
			}
		}
	}
	//立即完成所有tween
	public static void CompleteAll() {
		foreach (tween t in tweenList) {
			t.Complete ();
		}
	}
	//通过filter过滤完成指定的tween
	public static void Complete(string filter) {
		foreach (tween t in tweenList) {
			if (t.id == filter) {
				t.Complete ();
			}
		}
	}
	//通过transform完成指定位置的tween
	public static void Complete(Transform trans) {
		foreach (tween t in tweenList) {
			if (t.transform == trans) {
				t.Complete ();
			}
		}
	}
}