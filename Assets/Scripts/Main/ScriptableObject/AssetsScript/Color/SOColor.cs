using UnityEngine;
using System.Collections;


public class SOColor : ScriptableObject{
	public ColorType colorType;
	public Color color;

	public string ColorToRichText(string PaperColorText)
	{
		var rgbString = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", 
		                              (int)(color.r * 255), 
		                              (int)(color.g * 255), 
		                              (int)(color.b * 255),
		                              (int)(color.a * 255));

		string retVal = "<color="+rgbString+">";
		retVal += PaperColorText;
		return retVal += "</color>";
	}
}
