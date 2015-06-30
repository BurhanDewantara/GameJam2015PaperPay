using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(UpgradeData))]
public class UpgradeDataPropertyDrawer : PropertyDrawer {

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty (position, label, property);
		
		position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);
		EditorGUI.indentLevel = 0;
		
		Rect _1Rect = new Rect (position.x, position.y, 20, position.height);
		Rect _1Rects = new Rect (position.x+20, position.y, 25, position.height);
		
		Rect _2Rect = new Rect (position.x+55, position.y, 40, position.height);
		Rect _2Rects = new Rect (position.x+95, position.y, 20, position.height);

		Rect _3Rect  = new Rect (position.x+115, position.y, 30, position.height);
		Rect _3Rects  = new Rect (position.x+140, position.y, 20, position.height);

		
		//		string emotionName = property.FindPropertyRelative ("emotion").enumDisplayNames[property.FindPropertyRelative ("emotion").enumValueIndex];

		EditorGUI.LabelField(_1Rect,"Lv:");
		EditorGUI.PropertyField (_1Rects, property.FindPropertyRelative ("level"), GUIContent.none);

		EditorGUI.PropertyField (_2Rect, property.FindPropertyRelative ("value"), GUIContent.none);
		EditorGUI.LabelField(_2Rects,"Pts");

		EditorGUI.LabelField(_3Rect,"- P:");
		EditorGUI.PropertyField (_3Rects, property.FindPropertyRelative ("price"), GUIContent.none);



		
		EditorGUI.EndProperty ();
	}
}
