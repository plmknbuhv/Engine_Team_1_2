using UnityEngine;

public enum AudioType
{
    SFX, 
    Music
}

[CreateAssetMenu(menuName = "SO/SoundClip")]
public class SoundSO : ScriptableObject
{
    public AudioType audioType;
    public AudioClip clip;
    public bool loop = false;
    public bool randomizePitch = false;

    [Range(0, 1f)]
    public float randomPitchModifier = 0.1f;
    [Range(0.1f, 2f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float basePitch = 1f;
}