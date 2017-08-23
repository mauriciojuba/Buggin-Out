using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditor : Editor {

	public override void OnInspectorGUI(){
		AudioManager myTarget = (AudioManager)target;
		DrawDefaultInspector();
		EditorGUILayout.LabelField("Volume " + myTarget.getVolume(myTarget.eventName));
		EditorGUILayout.LabelField("Pitch " + myTarget.getPitch(myTarget.eventName));
        

        if (GUILayout.Button("Play Audio")){
			myTarget.playSound(myTarget.eventName , null);
		}
		if(GUILayout.Button("Stop Audio")){
			myTarget.stopSound();
		}
	}

}
