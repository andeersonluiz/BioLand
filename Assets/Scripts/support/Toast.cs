using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Linq;


public class Toast : MonoBehaviour
{
    public Text txt;
    public Image background;
    public Button btn;
    private float countdown = 0;
    private bool clicked = true;
    private ManageLanguageMainScreen manageLanguageMainScreen;

    void Start()
    {
        manageLanguageMainScreen = FindObjectOfType<ManageLanguageMainScreen>();
        background.enabled = false;
        txt.enabled = false;
    }

    void Update()
    {
        if (!clicked)
        {
            countdown += Time.deltaTime;
        }
        if (countdown > 4f)
        {
            clicked = true;
            countdown = 0;
        }
    }

    public void showToast(string text)
    {

        if (!btn.interactable && clicked)
        {
            StartCoroutine(showToastCOR(manageLanguageMainScreen.getErrorPlay(), 2));
            clicked = false;
        }
    }

    public void showToastNotCoins(string text)
    {
        if (clicked)
        {
            StartCoroutine(showToastCOR(text, 2));
            clicked = false;
        }

    }

    private IEnumerator showToastCOR(string text,
        int duration)
    {
        Color orginalColor = txt.color;
        Color orginalColorBg = background.color;
        background.enabled = true;
        txt.enabled = true;
        txt.text = text;


        //Fade in
        yield return fadeInAndOut(txt, true, 0.5f);

        //Wait for the duration
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return null;
        }

        //Fade out
        yield return fadeInAndOut(txt, false, 0.5f);
        background.enabled = false;
        txt.enabled = false;
        txt.color = orginalColor;
    }

    IEnumerator fadeInAndOut(Text targetText, bool fadeIn, float duration)
    {
        //Set Values depending on if fadeIn or fadeOut
        float a, b;
        if (fadeIn)
        {
            a = 0f;
            b = 1f;
        }
        else
        {
            a = 1f;
            b = 0f;
        }

        Color currentColor = Color.clear;
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);
            targetText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            background.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
    }
}
