using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "AudioEvent", menuName = "Audio/Audio Event", order = 1)]
public class AudioEvent : ScriptableObject{
	public string audioEventName = "null";
	public AudioClip audioClip;

	[Range(0.0f,1.0f)]
	public float volume = 1.0f;
	[Range(-3.0f,3.0f)]
	public float pitch = 1.0f;
	[Range(0,256)]
	public int priority = 128;
	public bool Loop;

    [Range(0f, 1f)]
    public float spatialSlend = 0;

    [Range(0f, 500f)]
    public float minDistance = 0;
    [Range(100f, 1000f)]
    public float maxDistance = 100;
}
