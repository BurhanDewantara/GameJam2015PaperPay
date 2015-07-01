using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(AudioItem))]
public class AudioItemPropertyDrawer : PropertyDrawer {

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty (position, label, property);
		
		
		position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);
		EditorGUI.indentLevel = 0;
		
		Rect _1Rect = new Rect (position.x, position.y, 50, position.height);
		Rect _2Rect = new Rect (position.x+65, position.y, position.width - 65, position.height);

		
		//		string emotionName = property.FindPropertyRelative ("emotion").enumDisplayNames[property.FindPropertyRelative ("emotion").enumValueIndex];
		EditorGUI.PropertyField (_1Rect, property.FindPropertyRelative ("key"), GUIContent.none);
		EditorGUI.PropertyField (_2Rect, property.FindPropertyRelative ("audio"), GUIContent.none);
		
		EditorGUI.EndProperty ();
	}}
