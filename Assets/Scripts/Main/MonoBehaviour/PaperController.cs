using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PaperController : MonoBehaviour {

	public GameObject leftPanel;
	public GameObject topPanel;
	public GameObject rightPanel;
	public GameObject bottomPanel;

	public GameObject paperPrefab;
	public GameObject paperContainerPanel;

	public SOFish fishData;

	private List<SOColor> _paperList;
	private int _maxPaperCount = 10;
	private int _counter = 0;

	void Awake()
	{
		_paperList = PaperGameManager.shared ().paperInGame;

		float power = 10;/* #### value from upgradeable*/
		leftPanel.GetComponent<Magnet> ().magnetPower = power;
		topPanel.GetComponent<Magnet> ().magnetPower = power;
		rightPanel.GetComponent<Magnet> ().magnetPower = power;
		bottomPanel.GetComponent<Magnet> ().magnetPower = power;

		leftPanel.GetComponent<Magnet>().OnObjectMagnetized += HandleOnObjectMagnetized;
		rightPanel.GetComponent<Magnet>().OnObjectMagnetized += HandleOnObjectMagnetized;
		topPanel.GetComponent<Magnet>().OnObjectMagnetized += HandleOnObjectMagnetized;
		bottomPanel.GetComponent<Magnet>().OnObjectMagnetized += HandleOnObjectMagnetized;

		leftPanel.GetComponent<PaperDropPanel> ().AddColorTarget(_paperList [0]);
		rightPanel.GetComponent<PaperDropPanel> ().AddColorTarget(_paperList [1]);
		topPanel.GetComponent<PaperDropPanel> ().AddColorTarget(_paperList [2]);

		List<SOColor> restColor = new List<SOColor> ();
		restColor.AddRange (_paperList);
		restColor.Remove (_paperList [0]);
		restColor.Remove (_paperList [1]);
		restColor.Remove (_paperList [2]);

		bottomPanel.GetComponent<PaperDropPanel> ().AddColorRangeTarget(restColor);   
		bottomPanel.GetComponent<PaperDropPanel> ().isTrashPanel = true;
	}

	void Start()
	{
		CreatePaper ();
	}

	void CreatePaper()
	{
		while (paperContainerPanel.GetComponent<PaperContainer>().Papers.Count < _maxPaperCount) {
		
			GameObject obj = GeneratePaperObject(paperPrefab);
			paperContainerPanel.GetComponent<PaperContainer>().AddPaper(obj);
			obj.GetComponent<PaperContent>().SetItem (GeneratePaperItem());
			_counter++;
		}
//		paperContainerPanel.GetComponent<PaperContainer> ().Papers [0].GetComponent<PaperContentViewer> ().IsAccessible = true;
	}

	GameObject GeneratePaperObject(GameObject paperPrefab)
	{
		GameObject obj = Instantiate(paperPrefab) as GameObject;
		obj.name = "paper - " + _counter.ToString("000");
		obj.transform.Rotate (new Vector3 (0,0,Random.Range(-10,10)));
		obj.GetComponent<PaperContentViewer>().OnDropAtBottomPanel += HandleOnDropAtBottomPanel;
		obj.GetComponent<PaperContentViewer>().OnDropAtLeftPanel += HandleOnDropAtLeftPanel;
		obj.GetComponent<PaperContentViewer>().OnDropAtTopPanel += HandleOnDropAtTopPanel;
		obj.GetComponent<PaperContentViewer>().OnDropAtRightPanel += HandleOnDropAtRightPanel;;
		obj.GetComponent<PaperContentViewer>().IsAccessible = true;

		return obj;
	}

	PaperItem GeneratePaperItem()
	{
		SOColor randomColorTint 	= _paperList [Random.Range (0, _paperList.Count)];
		SOColor randomColorText 	= _paperList [Random.Range (0, _paperList.Count)];
		string randomName 			= fishData.GetRandomName ();
		Sprite randomImage 			= fishData.GetRandomImage ();

		PaperItem pItem = new PaperItem (randomColorTint, randomColorText, randomImage, 1/* #### value from upgradeable*/, randomName);
		return pItem;
	}


	#region Paper Handler
	void HandleOnDropAtTopPanel (GameObject sender)
	{
		sender.GetComponent<PaperContentViewer> ().IsAccessible = false;	
		topPanel.GetComponent<Magnet> ().AddMagnetObject (sender);
	}

	void HandleOnDropAtRightPanel (GameObject sender)
	{
		sender.GetComponent<PaperContentViewer> ().IsAccessible = false;	
		rightPanel.GetComponent<Magnet> ().AddMagnetObject (sender);
	}

	void HandleOnDropAtLeftPanel (GameObject sender)
	{
		sender.GetComponent<PaperContentViewer> ().IsAccessible = false;	
		leftPanel.GetComponent<Magnet> ().AddMagnetObject (sender);
	}

	void HandleOnDropAtBottomPanel (GameObject sender)
	{
		sender.GetComponent<PaperContentViewer> ().IsAccessible = false;	
		bottomPanel.GetComponent<Magnet> ().AddMagnetObject (sender);
	}


	/// <summary>
	/// Handles the on object magnetized.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="magnetObject">Magnet object.</param>
	void HandleOnObjectMagnetized (GameObject sender, GameObject magnetObject)
	{
		//Calculate
		CalculateScore (sender, magnetObject);


		//REMOVE
		sender.GetComponent<Magnet> ().RemoveMagnetObject (magnetObject);
		paperContainerPanel.GetComponent<PaperContainer> ().RemovePaper (magnetObject);
		Destroy (magnetObject);

		CreatePaper ();
	}


	void CalculateScore(GameObject panel, GameObject pObject)
	{
		PaperItem paperObject = pObject.GetComponent<PaperContent> ().paper;
		SOColor colorObject = paperObject.colorTint;

		if (PaperGameManager.shared().playMode == GamePlayModeType.Say_The_Color) 
		{
			colorObject = paperObject.colorTint;
		} 
		else if ( PaperGameManager.shared().playMode == GamePlayModeType.Say_The_Text) 
		{
			colorObject = paperObject.colorText;
		}
		
		
		if (panel.GetComponent<PaperDropPanel> ().IsColorExist (colorObject)) {
			PaperGameManager.shared().DoCorrect();
		}
		else{
			PaperGameManager.shared().DoMistake();
		}
	}
	#endregion



}
