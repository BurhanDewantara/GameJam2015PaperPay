using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CannedFoodContentViewer : MonoBehaviour {

	public delegate void CanContentViewerDelegate(GameObject sender);
	public event CanContentViewerDelegate OnCanClicked;
	public event CanContentViewerDelegate OnCanDestroyed;

	private Animator anim;

	void Awake()
	{
		anim = this.GetComponent<Animator> ();
		this.GetComponent<Button> ().onClick.AddListener (OnClick);
	}

	public void OnClick()
	{
		if (OnCanClicked != null)
			OnCanClicked (this.gameObject);
	}

	public void Slide()
	{
		anim.SetTrigger ("Slide");

	}
	public void Drop()
	{
		if(OnCanDestroyed!=null) OnCanDestroyed(this.gameObject);	
	}




}
