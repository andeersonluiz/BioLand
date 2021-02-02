using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageLanguagePhasesScene : MonoBehaviour
{

    public Text[] scoreText;
    void Start()
    {
        if (Application.systemLanguage == SystemLanguage.Portuguese)
        {
            foreach (Text item in scoreText)
            {
                item.text = "MELHOR PONTUAÇÃO";
            }
        }
    }


}
