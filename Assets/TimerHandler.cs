using UnityEngine;
using System.Collections;

public class TimerHandler : MonoBehaviour
{
	private Animator anim;

	bool tick;

	void Awake ()
	{
		tick = true;
		anim = this.GetComponent<Animator> ();
	}


	public void PlayAnimation()
	{
		anim.speed = 1;
	}
	public void StopAnimation()
	{
		anim.speed = 0;
	}



	//DEPRECATED;
	private void Ticking ()
	{
		if (tick) {
			this.GetComponent<RectTransform> ().rotation = Quaternion.Euler (new Vector3 (0, 0, 15));
			tick = false;
		} else {
			this.GetComponent<RectTransform> ().rotation = Quaternion.Euler (new Vector3 (0, 0, -15));
			tick = true;
		}

	}
}
