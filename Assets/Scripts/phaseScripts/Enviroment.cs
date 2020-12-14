using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public enum Action{Atenuation=0, Bioestimulation=1, Bioaumentation=2,None=3};

public class Enviroment : MonoBehaviour {
    //referencia para scripts para geracao de graficos
    private GraphAbundanceScript abundanceScript;
    private GraphLineScript lineScript;
    
    //gameObjects para avisar qual biorremediador esta sendo realizado e quanto tempo falta para terminar
    
    
    //variavel para armazenar o tempo decorrido
    private float time = 0f;

    //variveis que serao exibidas no jogo, bacterias, contaminantes e variaveis de ambiente(temp,ph e umidade), respectivamente
    private Bacteria[] listBacterias;
    private Contaminant contaminant;
    private VariablesEnviroment variables;
    private float objectiveValue;
    //taxa de aumento de alimentacao que varia de acordo com as variaveis do ambiente
    private float taxa;

    //variavel de dia do jogo
    protected static int day;
    //referencia do gameObject da variavel dia;
    private GameObject dayGo;
    //quantidade de score que o jogador a cada 1% do contaminante perdido;
    private int scorePercentageLost;
    
    //score inicial do jogador;
    private int score;
    //referencia do gameObject da variavel score;
    private static GameObject scoreGo;
    //contador que guarda o numero de dias passados, quando chega no numberDayCicle, é perdido score
    private int countDay = 1;
    //gameobject responsavel pelo efeito de perda ou ganho
    public GameObject scoreChanged;

    //contador para controle do tempo de efeito do biorremediador
    private int countDownActionDays = 0;
    //variavel responsavel pelo tempo de efeito de um biorremediador
    private int numDaysEffect;
    //biorremediador que esta em efeito 
    private Action actionEffect;
    //variavel de controle para limitar o uso de acoes repetidas
    private bool usedAction = false;

    //variavel responsavel por determinar de quantos em quantos dias ocorre a diminuição do score
    private int numberDayCicles;

    //referencia dos dias da tabela

    //referencia da class responsavel pela regulação dos parametros
    private Parameters parameters;

    //classe responsavel por gerar o log
    private LogEvent log;

    //valor que representa quanto tempo é um dia
    private float _timeDay;

    private Status statusButton;
    private bool _isPaused=false;
    
    private int globalEnviroment;

    public bool isPaused { get; set; }
    public float timeDay { get; set; }

    private int scoreLoss10days;

    private int costAtenuation;
    private int costBioest;
    private int costBioaum;

    private int durationAtenuation;
    private int durationBioest;
    private int durationBioAum;

    private int valueAtenuation;
    private int valueBioest;
    private int valueBioaum;

    private bool isGameOver = false;
    public  GameObject bioaugmentationPoupUp;

    public GameObject quadMessage;
    public GameObject nameEffect;

    public GameObject gameWinPanel;
    public GameObject scoreWin;
    public GameObject[] starsGif;
    public GameObject gameLosePanel;
    
    public GameObject moistureContainer;
    public GameObject temperatureContainer;
    public GameObject phContainer;
    public GameObject goTec;
    private GameObject moistureText;
    private GameObject temperatureText;
    private GameObject phText;
    private GameObject contaminantText;

    private float[] degRate;
    private int previousScore;
    private float absRateNutrient;
    private static PlayerManager playerManager;

    public GameObject contentListRanking;
    public GameObject rowInstance;
    public GameObject div;

    public Text[] previousValues = new Text[3];
    private int scorePrevious;
    private float contaminantPrevious;
    int scoreTemp;
    float contaminantTemp;
	
	List<float> dataDegradeted;
	
	private float valuePrevious;
	private Toast toast;
    void Awake () {
        
        listBacterias = Bacteria.getClone( ScenesManager.bacterias);
        contaminant = ScenesManager.contaminant.getClone();
        variables = ScenesManager.variables.getClone();
        objectiveValue= ScenesManager.objectiveValue;

        parameters = new Parameters();
        log = new LogEvent();

        taxa = parameters.taxa;
        scorePercentageLost=parameters.scorePercentageLost;
        numDaysEffect=parameters.numDaysEffect;
        numberDayCicles = parameters.numberDayCicles;
        timeDay = parameters.timeDay;

        scoreLoss10days = parameters.scoreLoss10days;
        costAtenuation=parameters.costAtenuation;
        costBioest=parameters.costBioest;
        costBioaum=parameters.costBioaum;


        valueAtenuation=parameters.valueAtenuation;
        valueBioest=parameters.valueBioest;
        valueBioaum=parameters.valueBioaum;
        
        day = 1;
        score =300;
        previousScore = score;
        actionEffect = Action.None;
        absRateNutrient=1;
        globalEnviroment = -2;

        scorePrevious=score;
        contaminantPrevious=contaminant.percentage;
		valuePrevious = contaminant.qtd;
        degRate = new float[5];
        dataDegradeted = new List<float>();

        for(int i=0;i<5;i++){
            degRate[i]=listBacterias[i].degRate;
        }

       
        setVariables();


    }

    //inicializa os gameObjects e valores de dia, score, graficos,variaveis de ambientes e aviso do efeito  
    void setVariables(){
        dayGo =GameObject.FindGameObjectsWithTag ("dayGeneral")[0];
        scoreGo = GameObject.FindGameObjectsWithTag ("score")[0];
        dayGo.GetComponent<Text> ().text = day.ToString ();
        scoreGo.GetComponent<Text> ().text = score.ToString ();
        
        abundanceScript = FindObjectOfType<GraphAbundanceScript> ();
        abundanceScript.generateGraphAbundance (listBacterias);
        lineScript = FindObjectOfType<GraphLineScript> ();
        
        lineScript.init (contaminant);
        lineScript.setObjectiveValue(objectiveValue,contaminant);
        

        updateStatusVariablesEnviroment();

        changeDegradationRate(parameters.getMoistureStatus(variables.moisture));
        absRateNutrient = parameters.verifyStatusEnviroment(variables.pH,variables.temperature);
		toast = FindObjectOfType<Toast>();
        openSelectAction();
    
    }

    //metodo que alimenta as bacterias
    void alimentar (int qtdNutri) {
        float sumBac = sumBacterias(); 
        switch (actionEffect) {
            case Action.Atenuation:
                foreach (Bacteria b in listBacterias) {
                    int random = UnityEngine.Random.Range(0,2);
                    float value=calculateEatNegative(b,sumBac,qtdNutri,random);
                        b.qtd -=  value;   
                }
                break;

            case Action.Bioestimulation:
             foreach (Bacteria b in listBacterias) {
                 int random = UnityEngine.Random.Range(0,2);
                float value=calculateEatPositive(b,sumBac,qtdNutri,random);
                    if((Array.Exists(b.degList, element => (int) element== (int)contaminant.type))){
						                        Debug.Log("Alimentei2:"+ value+" y " + globalEnviroment);

                        b.qtd +=  value;   
                    }else{
                        b.qtd +=  (value/1.5f);   
                    }
             }
                break; 
            case Action.Bioaumentation:
                foreach (Bacteria b in listBacterias) {
                    int random = UnityEngine.Random.Range(0,2);
                float value=calculateEatPositive(b,sumBac,qtdNutri,random);
                    if(bacteriaEffectBiaum.name == b.name){
						                        Debug.Log("Alimentei3:"+ value+" x "+bacteriaEffectBiaum.name + "y" + globalEnviroment);

                        b.qtd +=  value;   
                    }else{
                        b.qtd -=  (value);   
                    }                  
                    }
                break;
            default:
                break;
        }
        
        

    }
    // metodo para degradar as bacterias e contaminante
    void degradar () {
		
        int antes = roundPercentualContaminant(contaminant.percentage);
		Debug.Log("antes" + antes);
        float sum = sumBacterias();
        foreach (Bacteria b in listBacterias) {
            if (b.qtd > 10 && Array.Exists(b.degList, element => (int) element== (int)contaminant.type)) {
                float degradationValueTemp;
                degradationValueTemp = ( contaminant.qtdMax *(b.qtd*(b.degRate))*(1+(b.qtd/sum)));
                contaminant.qtd -=degradationValueTemp;
                //b.qtd += ((degradationValueTemp*absRateNutrient));
				b.qtd += ((degradationValueTemp/contaminant.qtdMax)+0.0015f)*b.qtd*absRateNutrient;
				dataDegradeted.Add(degradationValueTemp*absRateNutrient);
            }else if(b.qtd > 10 && Array.Exists(b.toxicList, element => (int) element== (int)contaminant.type)){
                float degradationValueTemp = ( contaminant.qtdMax *(b.qtd*(b.degRate) )) ;
                b.qtd -= ((degradationValueTemp/contaminant.qtdMax)+0.0015f)*b.qtd*absRateNutrient;
            }
        }
		        int depois = roundPercentualContaminant(contaminant.percentage);

		Debug.Log("depois" + depois);
        setPercentualContaminationDifference(antes,depois);
    }
    private float calculateEatNegative(Bacteria b,float sum,int qtdNutri,int random){
            if(b.qtd<10)
                return 1;
            return (float)((Math.Abs(qtdNutri-random)*absRateNutrient));
    }
    private float calculateEatPositive(Bacteria b,float sum,int qtdNutri,int random){
        return (float)((Math.Abs(qtdNutri+random)*absRateNutrient));
    }
    private float sumBacterias(){
        float sum=0;
        foreach (Bacteria b in listBacterias) {
                sum+=b.qtd;
            }
        return sum;
    }
    // metodo para obter o valor do percentual de contaminacao
    private int roundPercentualContaminant(float percentage){
        return (int) (System.Math.Round ((System.Math.Round ((percentage) * 100, 3))));
    }

    private void setPercentualContaminationDifference(int antes, int depois){
        //print("ANTES E DPS"+ antes + ","+ depois);
        if (antes != depois) {
            int dif = (antes- depois);
            updateScoreScript(scorePercentageLost * dif);            
        }      
    }



    void Update () {
        time += Time.deltaTime;
		if(Input.GetKeyDown(KeyCode.K)){
            //gameOver(true);
			//changeVariablesEnviroment();

        }
        if (time > timeDay && !isPaused) {
            updateDay();
            updateCountDown();
            countDay++;
            alimentar(globalEnviroment);
            degradar();
            time = 0;
            
            if (countDay == numberDayCicles){
                 updateScoreScript(scoreLoss10days);
                 countDay=0;
            }

            if(day%5==0 && day!=0){
                log.registerLog(day, listBacterias,contaminant,variables,actionEffect,score);
                if(objectiveValue>contaminant.percentage){
                    gameOver(true);
                }
                if(score<=0){
                    gameOver(false);
                }
				if(day%90==0){
					updateValueTec();

				}
                if(day%100==0){
                    changeVariablesEnviroment();
                }
                insertGraphs();
                updateScoreGame();
            }

            
            
        }

        

    }
	
	public void updateValueTec(){
		float mean = meanDegradeted();
		valueAtenuation= -1*(int)mean;
		Debug.Log("VALUE ATENUATION"+valueAtenuation);
		valueBioest=(int)(mean*0.4f);
				Debug.Log("VALUE valueBioest"+valueBioest);

		valueBioaum=(int)(mean*0.2f);
				Debug.Log("VALUE valueBioaum"+valueBioaum);

	}
    public void openSelectAction(){
        setLastResult();
        if(day!=1){
            ScenesManager.Instance.closeAllPopUps();

        }
        goTec.SetActive(true);
        ButtonVelocityManager.Instance.setStatus(0);


    }

    public void setLastResult(){
        scoreTemp = score - scorePrevious;
        contaminantTemp= contaminantPrevious-contaminant.percentage;
		float valueRemoved = valuePrevious - contaminant.qtd;
		Debug.Log(valuePrevious+" - "+contaminant.qtd);
        if(scoreTemp<0){
            previousValues[0].color=Color.red;
        }else if(scoreTemp>0){
            previousValues[0].color=Color.green;
        }
        previousValues[0].text = scoreTemp.ToString();
        previousValues[1].text = System.Math.Round((valueRemoved),1).ToString()+" mg/Kg";
        scorePrevious=score;
        contaminantPrevious = contaminant.percentage;
		valuePrevious= contaminant.qtd;
    }
    public void closeSelectAction(){
        ButtonVelocityManager.Instance.setStatus(ButtonVelocityManager.Instance.lastButtonUsedIndex);
        goTec.SetActive(false);


    }
    public void restart(){
        SceneManager.LoadScene("scneDog"); 
    }
    private void gameOver(bool win){
        isPaused=true;
        if(win){
            gameWinPanel.SetActive(true);
            scoreWin.GetComponent<Text>().text= "SCORE:"+score;
            GameObject goStar = Instantiate(starsGif[getStar()]);
            goStar.transform.parent = gameWinPanel.transform;
            goStar.GetComponent<RectTransform>().localPosition = new Vector2(-3f,65f);
            goStar.GetComponent<RectTransform>().localScale = new Vector3(1f,1f,1f);
            PlayerManager.Instance.setScore(ScenesManager.valuePhase,score);
			log.finalizeLog(ScenesManager.valuePhase,"win");
        }else{
            gameLosePanel.SetActive(true);
			log.finalizeLog(ScenesManager.valuePhase,"lose");

        }
        disableActionButtons();
        disableDayButtons();
    }

	public void exitPhase(){
		log.finalizeLog(ScenesManager.valuePhase,"quit");

	}
    private int getStar(){
        if(score<300){
            return 0;
        }else if(score<600){
            return 1;
        }
        return 2;
    }
    private void updateDay(){
        day++;
        dayGo.GetComponent<Text> ().text = day.ToString ();
    }

    private void updateScoreScript(int valueScore){
        print("updating... "+valueScore);
        score+=valueScore;
    }

    private void updateScoreGame(){
        int scoreDifference = score - previousScore;
        effectScore(scoreDifference);
        
    }

    private void insertGraphs(){
        abundanceScript.generateGraphAbundance (listBacterias);
        lineScript.insertPoint (contaminant);
    }

    private void updateCountDown(){
            if (countDownActionDays > 0) {
                updateTextEffect(countDownActionDays);
                countDownActionDays--;
            }
            if (countDownActionDays == 0) {
                usedAction = false;
                quadMessage.SetActive(false);
                nameEffect.SetActive(false);
                openSelectAction();
            }
    }
    public void effectScore(int value){
        if(value==0){
            return;
        }
        GameObject goTmp = scoreChanged;
        if(value<0){
            goTmp.GetComponent<Text>().text = value.ToString();
            goTmp.GetComponent<Text>().color=Color.red;

        }else{
            goTmp.GetComponent<Text>().text = "+"+value.ToString();
            goTmp.GetComponent<Text>().color=Color.green;

        }
        Instantiate(goTmp,goTmp.transform.position,Quaternion.identity);
        scoreGo.GetComponent<Text> ().text = (score).ToString ();
        previousScore = score;

    }
    //metodo que seleciona o efeito do biorremediador
    public void setAction(int act){
        if(countDownActionDays==0 && day>0){
        int globalValueTemp=0;  
		
        switch(act){
            case 0:
                globalValueTemp =(int)(valueAtenuation);
                applyAction(Action.Atenuation,globalValueTemp,new Vector3(7.66f,1,1),costAtenuation);
                closeSelectAction();
                break;
            case 1:
				if(score>(costBioest*-1)){
					globalValueTemp =(int)(valueBioest);
					applyAction(Action.Bioestimulation,globalValueTemp,new Vector3(6.66f,1,1),costBioest);
					closeSelectAction();
			
				}else{
					toast.showToastNotCoins("You don't have enough BioCoins");
				}
                break;

            case 2:
				Debug.Log("score:"+ score);
				if(score>(costBioaum*-1)){
					disableActionButtons();
					showPoupUpBioaugmentation();
					setNameButtonsPoupUp();
				}else{
					toast.showToastNotCoins("You don't have enough BioCoins");

				}
                 
                
				break;
        }
        }
    }

    private void applyAction(Action action,int valueGlobalEnviroment,Vector3 sizeQuadMessage, int cost){
        if(score>cost){
            quadMessage.SetActive(true);
            nameEffect.SetActive(true);
            actionEffect= action;
            globalEnviroment = valueGlobalEnviroment;
            log.registerLogAction(action,scoreTemp,contaminantTemp);
            countDownActionDays =30; 
            nameEffect.GetComponent<Text>().text = "APPLYING \n "+(Enum.GetName(typeof(Action), (int)action)).ToUpper() +"("+countDownActionDays+" days left)";
            quadMessage.transform.localScale = sizeQuadMessage;
            usedAction = true;
            updateScoreScript(cost);
            effectScore(cost);   
        }
    }

    private void showPoupUpBioaugmentation(){
        bioaugmentationPoupUp.SetActive(true);
    }

	private Bacteria bacteriaEffectBiaum;
    public void executeBioaugmentation(int pos){
        bioaugmentationPoupUp.SetActive(false); 
        listBacterias[pos].qtd += (int)(500*absRateNutrient);
		bacteriaEffectBiaum= listBacterias[pos];
        applyAction(Action.Bioaumentation,(int)(valueBioaum),new Vector3(6.66f,1,1),costBioaum);
        closeSelectAction();
        enableAllButtons();
    }

    //metodo que altera o texto do quadro de efeito 
    private void updateTextEffect(int d){
        nameEffect.GetComponent<Text>().text = nameEffect.GetComponent<Text>().text.Replace("("+d+" days left)","("+(d-1)+" days left)");
    }
    //desabilita todos os botos de tecnicas
    private void disableActionButtons(){
        GameObject[] listButtons = GameObject.FindGameObjectsWithTag("buttonAction");
        foreach(GameObject go in listButtons){
            go.GetComponent<Button>().interactable = false;
        }
    }
    private void disableDayButtons(){
       GameObject[] listButtons = GameObject.FindGameObjectsWithTag("buttonVel");
        foreach(GameObject go in listButtons){
            go.GetComponent<Button>().interactable = false;
        }
    }
    //coloca os nome das bacterias nos botos do poupUp de bioaumento
    private void setNameButtonsPoupUp(){
        GameObject[] listButtons = GameObject.FindGameObjectsWithTag("buttonActionPoupUp");
        int i=0;
        foreach(GameObject go in listButtons){
            go.GetComponentInChildren<Text>().text = listBacterias[i].name;
            i++;
        }
    }
    //habilita todos os botos de tecnicas
    public void enableAllButtons(){
        GameObject[] listButtons = GameObject.FindGameObjectsWithTag("buttonAction");
        foreach(GameObject go in listButtons){
            go.GetComponent<Button>().interactable = true;
        }
    }

    private void changeVariablesEnviroment(){
        variables.pH = (float)System.Math.Round(UnityEngine.Random.Range(6f, 7.6f),1);
        variables.temperature = UnityEngine.Random.Range(20,36);
        variables.moisture = (Moisture)(UnityEngine.Random.Range(0,3));
        changeDegradationRate(parameters.getMoistureStatus(variables.moisture));
        absRateNutrient = parameters.verifyStatusEnviroment(variables.pH,variables.temperature);

        updateStatusVariablesEnviroment();
    }

	private float meanDegradeted(){
		float sum=0;
		foreach(float f in dataDegradeted){
			sum+=f;
		}
		float mean = sum/dataDegradeted.Count;
		dataDegradeted.Clear();
		return mean;
	}
    private void changeDegradationRate(float valueDeg){
        int i=0;
        foreach(Bacteria b in listBacterias){
            b.degRate = degRate[i]*valueDeg;
            i++;
        }
    }


    private void updateStatusVariablesEnviroment(){
        moistureText = moistureContainer.transform.GetChild(0).gameObject;
        temperatureText= temperatureContainer.transform.GetChild(0).gameObject;
        phText =phContainer.transform.GetChild(0).gameObject;
        contaminantText =GameObject.FindGameObjectWithTag ("contaminant");
        contaminantText.GetComponent<Text>().text = contaminant.name;
        (Color moistureColor, Color temperatureColor, Color phColor) = parameters.verifyVariablesEnviroment(variables);       
        moistureText.GetComponent<Text> ().text = variables.moisture.ToString().ToUpper();
        temperatureText.GetComponent<Text> ().text = variables.temperature.ToString()+"°C";
        phText.GetComponent<Text> ().text = variables.pH.ToString();
        moistureContainer.GetComponent<SpriteRenderer>().color = moistureColor;
        temperatureContainer.GetComponent<SpriteRenderer>().color =temperatureColor;
        phContainer.GetComponent<SpriteRenderer>().color =phColor;
    }

    public void mountRanking(Dictionary<string,int> dict){
        destroyAllChildrens(contentListRanking);
        foreach(var item in dict)
        {
            createRowNameScore(item.Key, item.Value);
            GameObject divInstance = Instantiate(div);
             divInstance.transform.SetParent( contentListRanking.transform,false);
        }
    }
    public void destroyAllChildrens(GameObject go){
    int childs = go.transform.childCount-1;
    for (int i = 0;i<childs ;i++)
        {
            GameObject.Destroy(go.transform.GetChild(i).gameObject);
        }
    }
    public void createRowNameScore(string name,int score){
        GameObject goResult = Instantiate(rowInstance);
        goResult.transform.GetChild(0).GetComponent<Text>().text = name;
        goResult.transform.GetChild(1).GetComponent<Text>().text = score.ToString();
        goResult.transform.SetParent(contentListRanking.transform,false);
    }
    
}