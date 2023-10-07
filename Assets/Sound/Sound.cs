using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip myClip;

    [Range(0f, 1f)]
    public float myVolume;
    [Range(.1f, 3f)]
    public float myPitch;
    public bool MyLoop;

    // Pich Randomize
    public bool randomizePitch;
    public Vector2 minMaxPitch;

    [HideInInspector]
    public AudioSource mySource;

}