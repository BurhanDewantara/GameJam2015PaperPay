using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Artoncode.Core;

public class TimerController : SingletonMonoBehaviour<TimerController> {

	public delegate void TimerControllerDelegate(GameObject sender);
	public event TimerControllerDelegate OnTimesUp;

	public GameObject timerHandle;

	private bool isTimeRunning;
	private Slider timeSlider;
	private float tick;

	void Awake()
	{
		timeSlider = this.GetComponent<Slider> ();
		tick = 0.0f;
	}

	public void SetTime(float time)
	{
		this.timeSlider.minValue = 0;
		this.timeSlider.value = 0;
		this.timeSlider.maxValue = time;
	}

	void FixedUpdate()
	{
		if (!isTimeRunning)
			return;

		timeSlider.value += Time.deltaTime;

		Tick ();

		if (timeSlider.value >= timeSlider.maxValue) {
			isTimeRunning = false;
			if(OnTimesUp!=null)
				OnTimesUp(this.gameObject);
		}

	}

	void Tick()
	{
		tick += Time.deltaTime;
		if (tick >= 1.0f) {
			tick %=1.0f;
			timerHandle.GetComponent<TimerHandler>().Ticking();
		}
	}


	public void StartTime()
	{
		isTimeRunning = true;
		timeSlider.value = 0;
	}

	public void ResumeTime()
	{
		isTimeRunning = true;
	}

	public void StopTime()
	{
		isTimeRunning = false;
	}
}
