using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateGifs : MonoBehaviour
{
    public Sprite[] spritesImages;
    public Image imageObj;
    private bool continueBool = true;
    int index = 0;
    private bool isFirst = true;
    private float time;
    void Update()
    {
        time += Time.deltaTime;

        if (continueBool)
        {
            if ((int)(time % 1) == 0 && time >= 1)
            {
                imageObj.sprite = spritesImages[index];
                index++;
                time = 0;

            }
            if (index == spritesImages.Length)
            {
                continueBool = false;
            }
        }


    }

}
