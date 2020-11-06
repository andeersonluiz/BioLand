using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GraphLineScript : MonoBehaviour {
    public GameObject prefrabPoint;
    public SpriteRenderer quadGraph;
    public Transform progressBar;
    //altura do quadro
    private float heightQuadGraph;
    //posicao em X do primeiro ponto
    private float positionInitialX;
    //limite inferior do quadro onde e gerado o grafico
    private float minQuadGraph;

    private float percentageContaminant;
    //velocidade quando o grafico é redimensinado
    private float velocityMove;

    private GameObject ContaminantPrevious;

    //controla a barra de porcentagem de contaminacao
    private bool progressing;
    //representa as variaveis de dia no grafico
    private int[] listDaysTable;
    //valor da escala, inicialmente tem 0.5 e depois é cortado pela mentade
    private float valueScale;
    //valor que é subtraido quando o grafico é redimensionado, representa a distancia em x quando o grafico é redimensionado
    private float xPositionScale;
    //valor que é subtraido quando o grafico é redimensionado, representa a distancia em y quando o grafico é redimensionado
    private float yPositionScaling;
    //posicao X do gameObject que gera os pontos
    private float xPosition;
    //posicao y do gameObject que gera os pontos
    private float yPosition;
    // diferença entre cada ponto no eixo X
    private float sumInnerX;

    // booleando que verifica se o grafico esta sendo redimensionado
    private bool isScaling=false;

    //variavel de controle para o tamanho dos pontos
    private int countScale=1;

    //valor, que quando alcancado, o grafico é redimensionado
    private int nextValueScale=5;
    //numero de vezes que um ponto no grafico é gerado
    int countPoint=0;
    //limitador para realizar acoes em um determinado tempo
    private float countDownAction=0;
    // dicionario com todos os pontos e o index deles ordenados de acordo com a insercao
    Dictionary<int,GameObject> dictPoints;

    private Parameters parameters;
    private bool isFirst =true;
    private GameObject pointInstance;
    private float countTemp=0;

    private float originalScalePoint;
    public void init (Contaminant c) {
        progressBar.GetComponent<Image> ().fillAmount = (float) c.qtd / c.qtdMax;
        percentageContaminant = (float) c.qtd / c.qtdMax;
        positionInitialX= 1.744f;

        valueScale=0.5f;
        xPositionScale=0.17f*5;
        sumInnerX=1.282f;
        xPosition = transform.position.x;
        countPoint=0;
        progressing = false;
        listDaysTable = new int[]{5,10,15,20};
        dictPoints= new Dictionary<int,GameObject>();

        velocityMove=2f;
        originalScalePoint = prefrabPoint.transform.localScale.x;
        parameters= new Parameters();

        updateRadialProgressBar (percentageContaminant,c);
        heightQuadGraph = modulo ((quadGraph.bounds.max.y) - (quadGraph.bounds.min.y));
        minQuadGraph = (quadGraph.bounds.min.y);
        insertPoint (c);
    }

    public void insertPoint (Contaminant c) {
        percentageContaminant = (float) c.qtd / c.qtdMax;
        updateRadialProgressBar (percentageContaminant,c);
        float posY = minQuadGraph + (heightQuadGraph * percentageContaminant);
        GameObject pointInstance=new GameObject();
        if(countPoint == nextValueScale){               
            updateValuesScale();
        }
        if(countPoint>=5){
            pointInstance=instantiatePoint(posY,true);
        }else{
            pointInstance=instantiatePoint(posY,false);
        }
        dictPoints.Add(countPoint,pointInstance);

        if (countPoint != 0) {
            DrawLine (ContaminantPrevious.transform.position, pointInstance.transform.position);
            progressing = true;
        }

        countPoint++;

        pointInstance.transform.parent = this.transform;
        ContaminantPrevious = pointInstance;
        

    }

    private GameObject instantiatePoint(float posY,bool biggerThenFive){
        if(biggerThenFive){
            float positionX = transform.GetChild(transform.childCount - 1).localPosition.x;
                Vector3 positionPoint = transform.TransformPoint(positionX+(sumInnerX), 0, 0);
                pointInstance = Instantiate (prefrabPoint,new Vector3 ( positionPoint.x, posY, prefrabPoint.transform.position.z), Quaternion.identity);
                if(Array.IndexOf<int>(listDaysTable,(countPoint*5))!=-1){
                    updateKeyPoint();
                }else{
                    updateOthersPoint();
                }
        }else{
            pointInstance = Instantiate (prefrabPoint, new Vector3 (positionInitialX+(sumInnerX*countPoint), posY, prefrabPoint.transform.position.z), Quaternion.identity);
        }
        return pointInstance;
    }

    private GameObject updateKeyPoint(){
        if(countScale>2){
            pointInstance.transform.localScale = new Vector3(originalScalePoint/2,originalScalePoint/2,0);
        }else{
            pointInstance.transform.localScale = new Vector3(originalScalePoint/countScale,originalScalePoint/countScale,0);
        }
        return pointInstance;
        
    }

    private GameObject updateOthersPoint(){
        
        if(countScale>2){
            pointInstance.transform.localScale = new Vector3((originalScalePoint*countTemp)/(countScale+1),(originalScalePoint*countTemp/(countScale+1)),0);
        }else{
            pointInstance.transform.localScale = new Vector3(originalScalePoint/countScale,originalScalePoint/countScale,0);

        }
        return pointInstance;
        
    }
    private void updateValuesScale(){
        nextValueScale=(nextValueScale*2) - (Convert.ToInt32(nextValueScale*0.15f));
        countScale*=2;
        valueScale=valueScale/2;
        xPositionScale=0.17f*(valueScale*10);
        xPosition = transform.position.x;
        countTemp+=0.5f;
    }

    float modulo (float i) {
        if (i < 0) {
            return i * -1;
        }
        return i;
    }

    void DrawLine (Vector3 start, Vector3 end) {
        GameObject myLine = new GameObject ();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer> ();
        LineRenderer lr = myLine.GetComponent<LineRenderer> ();
        lr.gameObject.tag = "bacteriaPercentual";
        lr.useWorldSpace = false;
        lr.sortingOrder = 1;
        lr.material = new Material (Shader.Find ("Sprites/Default"));
        lr.SetColors (Color.black, Color.black);
        lr.SetWidth (0.03f, 0.03f);
        lr.transform.parent = this.transform;
        lr.SetPosition (0, myLine.transform.InverseTransformPoint ((start)));
        lr.SetPosition (1, myLine.transform.InverseTransformPoint ((end)));
    }

    void resizeGraph () {
        this.transform.position =  new Vector3( Mathf.MoveTowards(transform.position.x, xPosition-xPositionScale, Time.deltaTime * (velocityMove/1.2f)),transform.position.y,0);
        this.transform.localScale = new Vector3( Mathf.MoveTowards(this.transform.localScale.x, valueScale, Time.deltaTime * (velocityMove/2.1f)),this.transform.localScale.y,0);
        isScaling=false;
    }

    void DestroyBar () {
        for (int i = 0; i < 2; i++) {
            Destroy (this.gameObject.transform.GetChild (i).gameObject);
        }
    }

    public void incrementDayBar () {
        GameObject[] days = GameObject.FindGameObjectsWithTag ("day");
        int i=0;
        foreach (GameObject go in days) {
            listDaysTable[i]=listDaysTable[i]*2;
            go.GetComponent<Text> ().text = (listDaysTable[i]).ToString ();
            i++;
        }
    }
    private void updateScalePoints(){        
        foreach (int index in dictPoints.Keys)
        {
        if(!(Array.IndexOf<int>(listDaysTable,(index*5))!=-1) && index!=0){
            if(countScale>=2){
                dictPoints[index].transform.localScale = new Vector3( (2*originalScalePoint*(countScale-1))/countScale , (originalScalePoint*(countScale-1))/(countScale*countScale) ,0);
            }else{
                dictPoints[index].transform.localScale = new Vector3( originalScalePoint/countScale , originalScalePoint/(2*countScale*countScale) ,0);


            }
        }else{
            dictPoints[index].transform.localScale = new Vector3(originalScalePoint*countScale,originalScalePoint/2,0);
            }
        }

    }


    void updateRadialProgressBar (float per,Contaminant contaminant) {
        GameObject[] percentual = GameObject.FindGameObjectsWithTag ("percentualContamination");
        GameObject[] contaminatQtd = GameObject.FindGameObjectsWithTag ("qtdContamination");
        foreach (GameObject go in percentual) {
            if (percentageContaminant < 0f) {
                go.GetComponent<Text> ().text = "0%";
            } else {
                go.GetComponent<Text> ().text = System.Math.Round ((per * 100), 1).ToString () + "%";
            }
        }
        foreach (GameObject go in contaminatQtd) {
            if (percentageContaminant < 0f) {
                go.GetComponent<Text> ().text = "0";
            } else {
                go.GetComponent<Text> ().text = ((int)(contaminant.qtd)).ToString ();
            }
        }

    }
    public void setObjectiveValue(float value){
        int heightCanvasQuad = 363;
        int widthCanvasQuad = 193;
        float heightObjective = heightCanvasQuad * value;

        GameObject[] goList = GameObject.FindGameObjectsWithTag("objective");
        foreach(GameObject go in goList){
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(widthCanvasQuad,heightObjective);
        }
        setTextObjective(value);

    }

    private void setTextObjective(float value ){
        GameObject go = GameObject.FindGameObjectWithTag("objectiveText");
        go.GetComponent<Text>().text ="OBJECTIVE\n"+(value*100).ToString()+"% LEVEL CONTAMINATION";
    }
    void Start () { }
    void Update () {
        
        if(countPoint == nextValueScale){
            isScaling=true;
        }

        if (isScaling) {
            if(isFirst){
                incrementDayBar();
                updateScalePoints();
                isFirst=false;
            }
            resizeGraph();
        }else{
            isFirst=true;
        }   
        updatePercentageContaminant();
    
    }

    private void updatePercentageContaminant(){

        if (progressBar.GetComponent<Image> ().fillAmount <= percentageContaminant || modulo ((progressBar.GetComponent<Image> ().fillAmount - percentageContaminant)) < 0.001) {
            progressBar.GetComponent<Image> ().fillAmount = percentageContaminant;
        } else {
            progressBar.GetComponent<Image> ().fillAmount -= 0.001f;
        }

    }
}