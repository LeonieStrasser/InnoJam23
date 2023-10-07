using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class TypeSoundGenerator : MonoBehaviour
{
    [Range(0, 1)] public float soundDelay;
    [Tooltip("Hier muss der Soundname rein, den der type SOund im AudioManager hat!")] public string typeSoundName;

    float typeSoundTimer;
    bool timerOn;


    //Immer wenn ein keyboard button gedrückt wird wird die SetTypeSound Methode gecallt
    public void SetTypeSound()
    {
        // Wenn der Timer noch nicht läuft, wird er gestartet und timerOn auf true gesetzt
        if (!timerOn)
        {
            timerOn = true;
            typeSoundTimer = 0f;

            PlayTypeSound();
        }
        // Wenn der Timer schon läuft, wird er zurückgesetzt auf 0
        else
        {
            typeSoundTimer = 0f;
        }
    }

    private void Update()
    {
        // Hier kommt ein Timer hin, der, wenn timerOn == true ist, jeden Frame hochgezählt wird.
        if (timerOn)
        {
            typeSoundTimer += Time.deltaTime;

            // Wenn der Timer größer als soundDelay ist, wird er zurückgesetzt auf 0 und timerOn auf false gesetzt.
            if (typeSoundTimer >= soundDelay)
            {
                timerOn = false;
                typeSoundTimer = 0f;

                // Führen Sie hier den Code aus, um den Sound abzuspielen.
                StopTypeSound();
            }
        }
    }

    private void PlayTypeSound()
    {
        AudioManager.instance.Play(typeSoundName);
    }

    private void StopTypeSound()
    {
        AudioManager.instance.Stop(typeSoundName);
    }


    [Button]
    void DebugKeyboardKlick()
    {
        SetTypeSound();
    }
}
