using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;

public class TutorialScript : MonoBehaviour
{

    public GameObject light;
    public Text textQuad;
    public GameObject popUp;
    public Sprite quad;
    public Sprite circle;
    private List<Vector2> listPositions;
    private List<Vector2> listScales;
    private string[] listText;
    private Tuple<int, int, int>[] mapActions;
    private int index = 1;

    private bool clicked;
    private void Start()
    {
        clicked = true;
        mapActions = new Tuple<int, int, int>[]{
            Tuple.Create(0,0,0),
            Tuple.Create(1,0,0),
            Tuple.Create(2,0,0),
            Tuple.Create(3,0,0),
            Tuple.Create(4,0,0),

            Tuple.Create(5,1,0),

            Tuple.Create(6,2,0),
//ok
            Tuple.Create(7,3,0),
            Tuple.Create(8,3,0),
//ok
            Tuple.Create(9,4,0),
//ok
            Tuple.Create(10,5,0),
//ok            
            Tuple.Create(11,6,0),
//ok
            Tuple.Create(12,7,1),
//ok
            Tuple.Create(13,8,1),
//ok
            Tuple.Create(14,9,1),
//ok
            Tuple.Create(15,10,1),
//ok
            Tuple.Create(16,7,1),
            Tuple.Create(17,7,1),
            Tuple.Create(18,11,1),
            Tuple.Create(19,11,1),
            Tuple.Create(20,12,1),
            Tuple.Create(21,13,1),
            Tuple.Create(22,0,0),
        };
        ManageLanguageTutorial manageLanguageTutorial = FindObjectOfType<ManageLanguageTutorial>();
        listText = manageLanguageTutorial.getListTutorial();
        listPositions = new List<Vector2>(){
            new Vector2(0f,0f),//nada(0)
            new Vector2(-0.4271f,0.87f),//score(1)
            new Vector2(0.44986f,0.92441f),//dias(2)
            new Vector2(0.0088f,0.6049f),//tecnicas(3)
            new Vector2(-0.231f,0.87f) ,//ultimos resultados(4)
            new Vector2(0.3425f,0.7851f),//objetivo(5)
            new Vector2(0.404f,0.129f),//porcentagem(6)
            new Vector2(0.3954f,0.2535f),//variaveis de ambiente(7)
            new Vector2(0.3964f,0.4051f),//nomeContaminante(8)
            new Vector2(0.39581f,0.35529f),//moisture(9)
            new Vector2(0.3959f,0.2641f),//temp e ph(10)
            new Vector2(-0.2452f,0.0068f),//grafico de abundancia(11)
            new Vector2(-0.022f,0.1827f),//grafico de abundancia(popUp)(12)
            new Vector2(0.1531f,0.0068f),//grafico de contaminação(13)
        };
        listScales = new List<Vector2>(){
            new Vector2(0f,0f),//nada
            new Vector2(0.12318f,0.1310426f),//score
            new Vector2(0.09665149f,0.1635101f),//dias
            new Vector2(10.04853f,0.2065122f),//tecnicas
            new Vector2(0.1965818f,0.3346073f),//ultimos resultados
            new Vector2(0.1804836f,0.1478486f),//objetivo
            new Vector2(0.1505937f,0.2547668f),//porcentagem
            new Vector2(0.1867574f,0.2008869f),//variaveis de ambiente
            new Vector2(0.1805442f,0.03856178f),//nomeContaminante
            new Vector2(0.1805442f,0.04431838f),//moisture
            new Vector2(0.1805442f,0.09133092f),//temp e ph
            new Vector2(0.4922864f,0.4950128f),//grafico de abundancia
             new Vector2(0.6789691f,0.7221825f),//grafico de abundancia(popUp)
            new Vector2(0.3114302f,0.4950128f),//grafico de contaminação
        };

        mountStepTutorial(0);
    }
    void mountStepTutorial(int i)
    {
        Tuple<int, int, int> tupleTemp = mapActions[i];
        if (tupleTemp.Item2 == 12)
        {
            popUp.SetActive(true);
        }
        else
        {
            popUp.SetActive(false);

        }
        light.GetComponent<Transform>().localPosition = listPositions[tupleTemp.Item2];
        light.GetComponent<Transform>().localScale = listScales[tupleTemp.Item2];
        textQuad.text = listText[tupleTemp.Item1];

        if (tupleTemp.Item3 == 0)
        {
            light.GetComponent<SpriteMask>().sprite = circle;
        }
        else
        {
            light.GetComponent<SpriteMask>().sprite = quad;

        }
    }

    public void clickNext()
    {
        if (index == mapActions.Length && clicked)
        {

            ScenesManager.Instance.changeSceneAfterTutorial("scneDog");
            clicked = false;
        }
        else
        {
            mountStepTutorial(index);
            index++;
        }

    }
}