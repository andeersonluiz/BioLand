using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GraphAbundanceScript : MonoBehaviour
{
    public GameObject prefrabBacteria;
    public RectTransform quadGraph;
    public GameObject contentListGraph;
    public GameObject empty;
    public GameObject scrollbar;
    public GameObject[] daysList;
    private GameObject goResultInstantiateEmpty;

    private float heightQuadGraph;
    private float minQuadGraph;
    private float[] positionsX;

    private int limitDays;

    private bool canDoAction;

    private Bacteria[] listbacterias;
    private List<Color> colorList;
    private int colorPos;

    private List<float[]> _listGraph;
    private bool control = false;
    private bool _changed = false;
    private int[] listDaysTable;

    private bool move = false;
    private float time;
    private Enviroment ev;
    private float velocityMove;
    private float value;
    private int numberFrames;
    private int index = 0;

    private void setVariables()
    {
        ev = FindObjectOfType<Enviroment>();
        positionsX = new float[] { -10.55f, -9.1f, -7.6f, -6f, -4.5f };
        canDoAction = true;
        colorPos = 4;
        time = 0f;
        velocityMove = 0.5f;
        listDaysTable = new int[] { 0, 5, 10, 15, 20 };
        numberFrames = 25;
        colorList = new List<Color>() {
            new Color (219/255f, 77/255f, 77/255f, 1f),
            new Color (57/255f, 163/255f, 64/255f, 1f),
            new Color (222/255f, 180/255f, 67/255f, 1f),
            new Color (106/255f, 73/255f, 214/255f, 1f),
            new Color (56/255f, 145/255f, 209/255f, 1f),
        };
        listGraph = new List<float[]>();
        setColors();
    }
    public void generateGraphAbundance(Bacteria[] Listbacterias)
    {
        goResultInstantiateEmpty = Instantiate(empty);
        goResultInstantiateEmpty.transform.localScale = new Vector3(1f, 0.013f, 1f);
        goResultInstantiateEmpty.GetComponent<RectTransform>().sizeDelta = new Vector2(1f, 446);
        goResultInstantiateEmpty.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.02f);
        goResultInstantiateEmpty.transform.parent = contentListGraph.transform;

        listbacterias = Listbacterias;
        float[] qtd = new float[5];
        for (int i = 0; i < 5; i++)
        {
            qtd[i] = listbacterias[i].qtd;
        }
        if (!control)
        {
            setVariables();
            control = true;
        }
        generateBar(qtd);
        Canvas.ForceUpdateCanvases();
        value = (1 - this.GetComponent<ScrollRect>().horizontalNormalizedPosition) / numberFrames;
        if (limitDays > 5)
        {
            move = true;
            incrementDay();
        }
    }

    private void generateBar(float[] qtd)
    {
        float sumPercentage = 0;
        float sumValues = qtd[0] + qtd[1] + qtd[2] + qtd[3] + qtd[4];
        for (int i = 4; i >= 0; i--)
        {
            float percentageValue = (float)qtd[i] / sumValues;
            createBar(percentageValue);
            setText();
            sumPercentage += percentageValue;

        }
        listGraph.Add(new float[] { (float)qtd[0] / sumValues, (float)qtd[1] / sumValues, (float)qtd[2] / sumValues, (float)qtd[3] / sumValues, (float)qtd[4] / sumValues });
        changed = true;
        limitDays++;
    }

    private void createBar(float percentage)
    {
        if (colorPos < 0)
        {
            colorPos = 4;
        }
        GameObject goBacteria = Instantiate(prefrabBacteria);
        goBacteria.GetComponent<RectTransform>().sizeDelta = new Vector2(1f, goResultInstantiateEmpty.GetComponent<RectTransform>().sizeDelta.y * percentage);
        goBacteria.transform.localScale = new Vector3(1f, 1f / 76.92309f, 1f);
        goBacteria.GetComponent<Image>().color = colorList[colorPos];
        goBacteria.transform.parent = goResultInstantiateEmpty.transform;
        colorPos--;

    }
    private void setColors()
    {
        int i = 0;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("quadradoLegenda").OrderByDescending(y => y.transform.position.y))
        {
            go.GetComponent<SpriteRenderer>().color = colorList[i];
            i++;
        }

    }

    public void setText()
    {
        int i = 0;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("textLegenda").OrderByDescending(y => y.transform.position.y))
        {
            if (listbacterias[i].qtd < 10)
            {
                go.GetComponent<Text>().text = listbacterias[i].name + " (10)";
            }
            else
            {
                go.GetComponent<Text>().text = listbacterias[i].name + " (" + (int)(listbacterias[i].qtd) + ")";

            }
            i++;

        }
        i = 0;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("textLegendaPoupUp").OrderByDescending(y => y.transform.position.y))
        {
            if (listbacterias[i].qtd < 10)
            {
                go.GetComponent<Text>().text = listbacterias[i].name + " (10)";
            }
            else
            {
                go.GetComponent<Text>().text = listbacterias[i].name + " (" + (int)(listbacterias[i].qtd) + ")";
            }
            i++;
        }

    }
    public void incrementDay()
    {
        int i = 0;
        foreach (GameObject go in daysList)
        {
            listDaysTable[i] = listDaysTable[i] + 5;
            go.GetComponent<Text>().text = (listDaysTable[i]).ToString();
            i++;
        }
    }

    private float modulo(float i)
    {
        if (i < 0)
        {
            return i * -1;
        }
        return i;
    }

    void moveGraph()
    {
        ScrollRect sRect = this.GetComponent<ScrollRect>();
        if (sRect.horizontalNormalizedPosition < 1 && index <= numberFrames)
        {
            sRect.horizontalNormalizedPosition += value;
            index++;
        }
        else
        {
            Canvas.ForceUpdateCanvases();
            move = false;
            value = 0;
            index = 0;
        }



    }

    private void DestroyBar()
    {
        for (int i = 0; i < 10; i++)
        {
            Destroy(this.gameObject.transform.GetChild(i).gameObject);
        }
    }

    public List<GameObject> SortByDistance(List<GameObject> objects, Vector3 mesureFrom)
    {
        return objects.OrderBy(x => Vector3.Distance(x.transform.position, mesureFrom)).ToList();
    }


    void Start()
    {

    }

    void Update()
    {

        if (move)
        {
            moveGraph();
        }

    }


    public static List<float[]> listGraph { get; set; }
    public static bool changed { get; set; }

}