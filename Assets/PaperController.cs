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

	private int _maxPaperCount = 4;
	private int _counter = 0;

	void Awake()
	{
		float power = 30;
		leftPanel.GetComponent<Magnet> ().magnetPower = power;
		topPanel.GetComponent<Magnet> ().magnetPower = power;
		rightPanel.GetComponent<Magnet> ().magnetPower = power;
		bottomPanel.GetComponent<Magnet> ().magnetPower = power;

		leftPanel.GetComponent<Magnet>().OnObjectMagnetized += HandleOnObjectMagnetized;
		rightPanel.GetComponent<Magnet>().OnObjectMagnetized += HandleOnObjectMagnetized;
		topPanel.GetComponent<Magnet>().OnObjectMagnetized += HandleOnObjectMagnetized;
		bottomPanel.GetComponent<Magnet>().OnObjectMagnetized += HandleOnObjectMagnetized;
	}

	void HandleOnObjectMagnetized (GameObject sender, GameObject magnetObject)
	{
		sender.GetComponent<Magnet> ().RemoveMagnetObject (magnetObject);
		paperContainerPanel.GetComponent<PaperContainer> ().RemovePaper (magnetObject);
		Destroy (magnetObject);
		CreatePaper ();
	}

	void Start()
	{
		CreatePaper ();
	}

	void CreatePaper()
	{
		while (paperContainerPanel.GetComponent<PaperContainer>().Papers.Count < _maxPaperCount) {
		
			GameObject obj = Instantiate(paperPrefab) as GameObject;
			obj.name = "paper - " + _counter.ToString("000");
			obj.GetComponent<PaperContentViewer>().OnDropAtBottomPanel += HandleOnDropAtBottomPanel;
			obj.GetComponent<PaperContentViewer>().OnDropAtLeftPanel += HandleOnDropAtLeftPanel;
			obj.GetComponent<PaperContentViewer>().OnDropAtTopPanel += HandleOnDropAtTopPanel;
			obj.GetComponent<PaperContentViewer>().OnDropAtRightPanel += HandleOnDropAtRightPanel;;
			obj.GetComponent<PaperContentViewer>().IsAccessible = true;
			paperContainerPanel.GetComponent<PaperContainer>().AddPaper(obj);
			_counter++;
		}
//		paperContainerPanel.GetComponent<PaperContainer> ().Papers [0].GetComponent<PaperContentViewer> ().IsAccessible = true;
	}


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




}
