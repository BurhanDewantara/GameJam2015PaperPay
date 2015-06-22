using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(Currency))]
public class CurrencyPropertyDrawer : PropertyDrawer 
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty (position, label, property);


		position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);
		EditorGUI.indentLevel = 0;

		Rect _1Rect = new Rect (position.x, position.y, 50, position.height);
		Rect _1Rects = new Rect (position.x+50, position.y, 20, position.height);

		Rect _2Rect = new Rect (position.x+65, position.y, 50, position.height);
		Rect _2Rects = new Rect (position.x+115, position.y, 20, position.height);

//		string emotionName = property.FindPropertyRelative ("emotion").enumDisplayNames[property.FindPropertyRelative ("emotion").enumValueIndex];
		EditorGUI.PropertyField (_1Rect, property.FindPropertyRelative ("coin"), GUIContent.none);
		EditorGUI.PropertyField (_2Rect, property.FindPropertyRelative ("gem"), GUIContent.none);

		EditorGUI.LabelField(_1Rects,"C");
		EditorGUI.LabelField(_2Rects,"G");



		EditorGUI.EndProperty ();
	}


}
