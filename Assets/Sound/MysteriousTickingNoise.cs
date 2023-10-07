using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MysteriousTickingNoise : MonoBehaviour
{
    public Sound tickingSound;

    [MinMaxSlider(0, 1)] public Vector2 volumeRangeOverLivetime;

    private void Awake()
    {
        tickingSound.mySource = gameObject.AddComponent<AudioSource>();
        tickingSound.mySource.clip = tickingSound.myClip;
        tickingSound.mySource.volume = tickingSound.myVolume;
        tickingSound.mySource.pitch = tickingSound.myPitch;
        tickingSound.mySource.loop = tickingSound.MyLoop;
    }

    private void Start()
    {
        tickingSound.mySource.Play();
        UpdateTickingVolume(0);
    }

    /// <summary>
    /// roundProcess beschreibt den Prozentanteil des Runden Progresses (0 = runde hat grade erst angefangen, 1 = Runde ist vorbei)
    /// </summary>
    /// <param name="roundProcess"></param>
    public void UpdateTickingVolume(float roundProcess)
    {
        if (roundProcess > 1)
        { Debug.LogWarning("Der roundProcess ist größer als 1 ihr Deppen! Er ist " + roundProcess, gameObject); }
        else if (roundProcess < 0)
        { Debug.LogWarning("Der roundProcess ist kleiner als 0 ihr Deppen! Er ist " + roundProcess, gameObject); }

        // Stellen Sie sicher, dass roundProcess zwischen 0 und 1 liegt.
        roundProcess = Mathf.Clamp(roundProcess, 0f, 1f);

        float minVolume = volumeRangeOverLivetime.x;
        float maxVolume = volumeRangeOverLivetime.y;

        // Lerp (Interpolation) zwischen minVolume und maxVolume basierend auf roundProcess.
        float newVolume = Mathf.Lerp(minVolume, maxVolume, roundProcess / 1f);

        // Setzen Sie die Lautstärke der AudioSource auf den neuen Wert.
        tickingSound.mySource.volume = newVolume;
    }


    // DEBUG
    public float debugRoundProcess;

    [Button]
    void DebugUpdateTickVolume()
    {
        UpdateTickingVolume(debugRoundProcess);
    }


}
