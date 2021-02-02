using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageLanguagePhase : MonoBehaviour
{
    public Text textWin;
    public GameObject vizualizeResult;
    public Text raking;
    public GameObject nextPhase;

    public Text textLose;
    public GameObject tryAgain;
    public GameObject exit;

    public SpriteRenderer background;
    public Image calendar;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;

    public Sprite[] vizualizeResultPT;
    public Sprite[] nextPhasePT;
    public Sprite[] tryAgainPT;
    public Sprite[] exitPT;

    public Sprite[] button1PT;
    public Sprite[] button2PT;
    public Sprite[] button3PT;
    public Sprite[] button4PT;
    public Sprite[] button5PT;

    public Sprite backgroundPT;
    public Sprite calendarPT;

    public Text levelContaminationText;
    public Text quantityText;
    public Text objectiveGraphText;
    public Text selectActionText;
    public Text lastResultText;
    public Text scoreText;
    public Text removedText;
    public Text viewEveryText;
    public Dropdown dropdown;

    private string score = "SCORE:";
    private string noCoinsError = "You don't have enough BioCoins";
    private string applyingText = "APPLYING \n ";
    private string daysLeftText = "days left";

    private string objectiveTextPt1 = "OBJECTIVE\n";
    private string objectiveTextPt2 = " mg/Kg CONCENTRATION";

    private string moistureLow = "LOW";
    private string moistureMedium = "MEDIUM";
    private string moistureHigh = "HIGH";

    private string attenuation = "NATURAL ATTENUATION ";
    private string bioestimulation = "BIOESTIMULATION ";
    private string bioaumentation = "BIOAUMENTATION ";
    private string day = "DAY ";
    public string getScore()
    {
        return score;
    }
    public string getNoCoinsError()
    {
        return noCoinsError;
    }
    public string getApplyingText()
    {
        return applyingText;
    }

    public string getDaysLeftText()
    {
        return daysLeftText;
    }

    public string getObjectiveTextPt1()
    {
        return objectiveTextPt1;
    }

    public string getObjectiveTextPt2()
    {
        return objectiveTextPt2;
    }

    public string getMoistureLow()
    {
        return moistureLow;
    }
    public string getMoistureMedium()
    {
        return moistureMedium;
    }
    public string getMoistureHigh()
    {
        return moistureHigh;
    }
    public string getDay()
    {
        return day;
    }
    public string getAttenuation()
    {
        return attenuation;
    }
    public string getBioestimulation()
    {
        return bioestimulation;
    }
    public string getBioaumentation()
    {
        return bioaumentation;
    }
    void Awake()
    {
        SpriteState sprState;
        Dropdown.OptionData data1, data2, data3, data4;
        List<Dropdown.OptionData> datas = new List<Dropdown.OptionData>();
        if (Application.systemLanguage == SystemLanguage.Portuguese)
        {
            score = "PONTUAÇÃO:";
            applyingText = "APLICANDO \n ";
            noCoinsError = "Você não tem BioCoins suficientes";
            daysLeftText = "dias restantes";
            objectiveTextPt1 = "OBJETIVO\n";
            objectiveTextPt2 = " mg/Kg DE CONCENTRAÇÃO";
            moistureLow = "BAIXA";
            moistureMedium = "MÉDIO";
            moistureHigh = "ALTA";
            day = "DIA ";
            attenuation = "ATENUAÇÃO NATURAL ";
            bioestimulation = "BIOESTIMULAÇÃO ";
            bioaumentation = "BIOAUMENTAÇÃO ";
            background.sprite = backgroundPT;
            calendar.sprite = calendarPT;

            textWin.text = "VOCE VENCEU";
            textLose.text = "VOCE PERDEU";
            selectActionText.text = "SELECIONE UMA AÇÃO";
            lastResultText.text = "ULTIMO RESULTADO";
            scoreText.text = "Pontuação:";
            removedText.text = "Removido:";
            levelContaminationText.text = "Nivel de\nContaminação";
            quantityText.text = "Porcentagem atual";
            objectiveGraphText.text = "OBJETIVO";

            viewEveryText.text = "Ver a cada";
            dropdown.ClearOptions();
            data1 = new Dropdown.OptionData();
            data1.text = "5 dias";
            datas.Add(data1);
            data2 = new Dropdown.OptionData();
            data2.text = "20 dias";
            datas.Add(data2);
            data3 = new Dropdown.OptionData();
            data3.text = "50 dias";
            datas.Add(data3);
            data4 = new Dropdown.OptionData();
            data4.text = "100 dias";
            datas.Add(data4);

            foreach (Dropdown.OptionData item in datas)
            {
                dropdown.options.Add(item);
            }

            sprState.pressedSprite = button1PT[1];

            button1.GetComponent<Image>().sprite = button1PT[0];
            button1.GetComponent<Button>().spriteState = sprState;

            sprState.pressedSprite = button2PT[1];

            button2.GetComponent<Image>().sprite = button2PT[0];
            button2.GetComponent<Button>().spriteState = sprState;

            sprState.pressedSprite = button3PT[1];

            button3.GetComponent<Image>().sprite = button3PT[0];
            button3.GetComponent<Button>().spriteState = sprState;

            sprState.pressedSprite = button4PT[1];
            sprState.disabledSprite = button4PT[2];

            button4.GetComponent<Image>().sprite = button4PT[0];
            button4.GetComponent<Button>().spriteState = sprState;

            sprState.pressedSprite = button5PT[1];
            sprState.disabledSprite = button5PT[2];

            button5.GetComponent<Image>().sprite = button5PT[0];
            button5.GetComponent<Button>().spriteState = sprState;

            sprState.pressedSprite = vizualizeResultPT[1];

            vizualizeResult.GetComponent<Image>().sprite = vizualizeResultPT[0];
            vizualizeResult.GetComponent<Button>().spriteState = sprState;

            raking.text = "Classificação";

            sprState.pressedSprite = nextPhasePT[1];

            nextPhase.GetComponent<Image>().sprite = nextPhasePT[0];
            nextPhase.GetComponent<Button>().spriteState = sprState;

            sprState.pressedSprite = tryAgainPT[1];

            tryAgain.GetComponent<Image>().sprite = tryAgainPT[0];
            tryAgain.GetComponent<Button>().spriteState = sprState;

            sprState.pressedSprite = exitPT[1];

            exit.GetComponent<Image>().sprite = exitPT[0];
            exit.GetComponent<Button>().spriteState = sprState;
        }
    }


}
