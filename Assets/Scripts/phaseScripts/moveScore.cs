using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class moveScore : MonoBehaviour
{
    private float velocityMove = 0.5f;
    private bool up;
    // Start is called before the first frame update
    void Start()
    {
        if (System.Convert.ToInt32(gameObject.GetComponent<Text>().text) > 0)
        {
            up = true;
        }
        else
        {
            up = false;
        }
    }

    public static int IntParseFast(string value)
    {
        int result = 0;
        for (int i = 0; i < value.Length; i++)
        {
            char letter = value[i];
            result = 10 * result + (letter - 48);
        }
        return result;
    }
    void Update()
    {
        if (up)
        {
            Color colorTmp = gameObject.GetComponent<Text>().color;
            colorTmp.a = colorTmp.a - 0.01f;
            gameObject.GetComponent<Text>().color = colorTmp;
            gameObject.transform.Translate(Vector3.up * velocityMove * Time.deltaTime);
            if (gameObject.transform.position.y > -6.3)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Color colorTmp = gameObject.GetComponent<Text>().color;
            colorTmp.a = colorTmp.a - 0.01f;
            gameObject.GetComponent<Text>().color = colorTmp;
            gameObject.transform.Translate(Vector3.down * velocityMove * Time.deltaTime);
            if (gameObject.transform.position.y < -7.6)
            {
                Destroy(gameObject);
            }
        }
    }
}
