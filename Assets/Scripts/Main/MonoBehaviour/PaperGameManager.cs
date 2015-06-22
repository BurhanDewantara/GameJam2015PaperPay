using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Artoncode.Core;


public class PaperGameManager : SingletonMonoBehaviour< PaperGameManager >
{
	public bool isDebug;
	public GamePlayModeType playMode;
	public List<SOColor> paperInGame;
	public List<int> comboLimit;

	public float uTime;
	public float uChanceBonus;
	public float uChanceGem;
	public float uExtraTotalBonus;
	public float uSlideSpeed;
	public float uMistakeCost;


	private int _mistakes = 0;
	private int _comboCounter = 0;
	private int _maxComboCounter = 0;
	private List<int> _cannedFish = new List<int> (6);

	void Awake ()
	{
		playMode = GamePlayModeType.Say_The_Color;
		for (int i = 0; i < comboLimit.Count; i++) 
			_cannedFish.Add (0);
	
	
	
	}





	void OnGUI ()
	{
		if (isDebug) {
			GUILayout.BeginVertical ("box");
				GUILayout.Label ("PlayMode : " + playMode);
			GUILayout.EndVertical ();
			GUILayout.BeginVertical ("box");
				for (int i = 0; i < comboLimit.Count; i++) {
					GUILayout.Label ("Canned x" + (1 << i) + " : " + _cannedFish [i]);
				}	
				GUILayout.Label ("Mistakes : " + _mistakes);
				GUILayout.Label ("Combo : " + _comboCounter);
				GUILayout.Label ("Max Combo : " + _maxComboCounter);
			GUILayout.EndVertical ();


			if(GUI.Button(new Rect(Screen.width -200,0,200,40),"Create"))
			{
				CannedFoodMachineController.shared().Create();
			}
			if(GUI.Button(new Rect(Screen.width -200,50,200,40),"Change"))
			{
				TriggerPlayMode();
			}
		}
	}

	public void TriggerPlayMode ()
	{
		switch (playMode) {
		case GamePlayModeType.Say_The_Color:
			playMode = GamePlayModeType.Say_The_Text;
			break;
		case GamePlayModeType.Say_The_Text:
			playMode = GamePlayModeType.Say_The_Color;
			break;
		} 
	}

	public void DoCorrect ()
	{
		int i = comboLimit.Count - 1;
		for (i = 0; i < comboLimit.Count-1; i++) {
			if (_comboCounter < comboLimit [i])
				break;
		}

		int idx = Mathf.Clamp (i, 0, comboLimit.Count - 1);

		_cannedFish [idx]++;
		_comboCounter++;
		 
		if (_comboCounter > _maxComboCounter) {
			_maxComboCounter = _comboCounter;
		}

	}

	public void DoMistake ()
	{
		_mistakes++;
		_comboCounter = 0;
	}




}
