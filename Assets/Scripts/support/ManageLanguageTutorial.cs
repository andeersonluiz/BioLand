using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageLanguageTutorial : MonoBehaviour
{
    string[] listText = new string[]{
        "Welcome to the BioLand tutorial, your goal in the game is to decontaminate the environment in the most sustainable way possible.",
        "Each player will start with 300 BioCoins. They represent their sustainability points in the environment.",
        "Every 1% decontaminated you earn 20 BioCoins and every 10 days after that you lose 20 BioCoins.",
        "Decontamination is important, but doing it in the most efficient way is even more important.",
        "Now we will explain each component of the game.",
        "Your BioCoins number is displayed here. The more BioCoins you have, the better the sustainability of the environment.",
        "Here the days of the game are shown. Every 30 days you will have to choose a technique.",
        "There are 3 techniques available today, each will use a certain amount of BioCoins.",
        "The values ​​were calculated based on the impact of the techniques on the environment.",
        "The \"Latest results\" table displays the results of your last 30 days, the score obtained and the percentage decontaminated.",
        "Each level will have a goal, you will win if you reach it.",
        "Here it informs the percentage of contamination and the amount of the contaminant.",
        "The environment variables show the factors that affect the decontamination process.",
        "This area shows the name of the contaminant.",
        "Moisture affects the ability of bacteria to break down.",
        "Temperature and pH affect the rate of absorption of bacteria", // 14
        "If the values ​​are green it means they are very good, if yellow it is average and red means bad.",
        "Pay attention to them when using a more expensive technique.",
        "The bacteria chart displays the relative chart that represents the amount of bacteria in percent.",
        "It will help you to see how a bacterium behaves over time. In the legend you can see the quantity and name of each bacterium.",
        "You can see the history from the first day by clicking on the graph. You can also filter on intervals of certain days.",
        "In the contamination graph, you can see the contamination of the environment over time.",
        "Well, the tutorial is over. Have fun playing BioLand !!",
        };
    public Text spikTutorial;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public SpriteRenderer background;
    public Image calendar;
    public Text levelContaminationText;
    public Text viewEveryText;
    public Text objectiveGraphText;

    public Dropdown dropdown;
    public Text objectiveText;
    public Text moistureText;
    public Sprite[] button1PT;
    public Sprite[] button2PT;
    public Sprite[] button3PT;
    public Sprite[] button4PT;
    public Sprite[] button5PT;

    public Sprite backgroundPT;
    public Sprite calendarPT;

    public Text day;
    public Text lastResultText;
    public Text scoreText;
    public Text removedText;
    public string[] getListTutorial()
    {
        return listText;
    }
    void Awake()
    {
        SpriteState sprState;
        Dropdown.OptionData data1, data2, data3, data4;
        List<Dropdown.OptionData> datas = new List<Dropdown.OptionData>();
        if (Application.systemLanguage == SystemLanguage.Portuguese)
        {
            spikTutorial.text = "Pular Tutorial";
            listText = new string[]{
            "Bem-vindo ao tutorial de BioLand, seu objetivo no jogo é descontaminar o ambiente do modo mais sustentável possível.",
            "Cada jogador começará com 300 BioCoins. Elas representam seus pontos de sustentabilidade no ambiente.",
            "A cada 1% descontaminado você ganha 20 BioCoins e a cada 10 dias passados você perde 20 BioCoins.",
            "Descontaminar é importante, mas fazer ela do modo mais eficiente é mais importante ainda.",
            "Agora vamos explicar cada componente do jogo.",
            "Aqui é exibido seu número de BioCoins. Quanto mais BioCoins tiver, melhor a sustentabilidade do ambiente.",
            "Aqui é mostrado os dias do jogo. A cada 30 dias você terá que escolher uma técnica.",
            "Existem 3 técnicas disponíveis atualmente, cada uma utilizará uma determinada quantidade de BioCoins.",
            "Os valores foram calculados com base no impacto das técnicas no meio ambiente.",
            "No quadro \"Últimos resultados\" é exibido o resultado dos seus últimos 30 dias, pontuação obtida e porcentagem descontaminada.",
            "Cada fase terá um objetivo, você vencerá se alcançá-lo.",
            "Aqui informa a porcentagem de contaminação e a quantidade do contaminante.",
            "Nas variáveis de ambiente são exibidos os fatores que afetam o processo de descontaminação.",
            "Esta área mostra qual nome do contaminante.",
            "A umidade afeta a capacidade de degradação das bactérias.",
            "A temperatura e o pH afetam a taxa de absorção das bactérias",//14       
            "Se os valores estiverem verdes significam que são muito bons, se for amarelo é mediano e vermelho significa ruim.",
            "Atenção para eles na hora de utilizar uma técnica mais custosa.",
            "O gráfico de bactérias exibe o gráfico relativo que representa a quantidade de bactérias em porcentagem.",
            "Ele o auxiliará para ver como uma bactéria se comporta durante o tempo. Na legenda é possível ver a quantidade e o nome de cada bactéria.",
            "É possível ver o histórico desde o primeiro dia clicando no gráfico. Também é possível filtrar em intervalos de dias determinados.",
            "No gráfico de contaminação é possível ver a contaminação do ambiente com o passar do tempo.",
            "Bom, o tutorial acabou. Divirta-se jogando BioLand!!",
      };
            objectiveText.text = objectiveText.text.Replace("OBJECTIVE", "OBJETIVO");
            objectiveText.text = objectiveText.text.Replace("CONCENTRATION", "DE CONCENTRAÇÃO");
            moistureText.text = "MÉDIO";
            background.sprite = backgroundPT;
            calendar.sprite = calendarPT;
            day.text = "DIA 1";
            lastResultText.text = "ULTIMO RESULTADO";
            scoreText.text = "Pontuação:";
            removedText.text = "Removido:";
            levelContaminationText.text = "Nivel de\nContaminação";
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
        }
    }

    // Update is called once per frame

}
