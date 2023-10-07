using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class TextEnterSoundGenerator : MonoBehaviour
{
    public void TextEnterInputSound(bool correctWord)
    {
        AudioManager.instance.PlayStackable("EnterInput");

        if(correctWord)
        {
            AudioManager.instance.PlayStackable("RightInput");
        }
        else
        {
            AudioManager.instance.PlayStackable("FalseInput");
        }
    }

    public bool debugCorrectWord;

    [Button]
    void DebugTextEnter()
    {
        TextEnterInputSound(debugCorrectWord);
    }

}
