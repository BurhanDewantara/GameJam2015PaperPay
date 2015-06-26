using UnityEngine;
using System.Collections;


public class SOColor : ScriptableObject{
	public ColorType colorType;
	public Color color;

	public string ColorToRichText(string PaperColorText)
	{
		return GameHelper.SetColorInText(color,PaperColorText);
	}
}
