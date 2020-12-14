using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Parameters
{
    //taxa de aumento de alimentacao que varia de acordo com as variaveis do ambiente
    private float _taxa;
    //quantidade de score que o jogador a cada 1% do contaminante perdido;
    private int _scorePercentageLost;
    //variavel responsavel pelo tempo de efeito de um biorremediador
    private int _numDaysEffect;
    //variavel responsavel por determinar de quantos em quantos dias ocorre a diminuição do score
    private int _numberDayCicles;    
    //variavel que representa quanto tempo é um dia
    private float _timeDay ;  
    //variavel que representa quanto o tempo é cortado ( timeDay/timeSpeed)
    private float _timeSpeed ;  
    //intervalo para nascer uma nuvem
    private float _respawnCloud ;  
    //velocidade da nuvem
    private float _speedCloud ; 
    //velocidade com que o ceu escurece
    private float _valueAlpha;
    //variavel que define quanto de pontuacao perde a cada 10 dias
    private int _scoreLoss10days;
    //custo de atenuacao natural;
    private int _costAtenuation;
    //custo de  bioestimulacao;
    private int _costBioest;
    //custo de bioaumento;
    private int _costBioaum;
    //duracao do efeito da atenuacao(em dias)
    private int _durationAtenuation;
    //duracao do bioestimulacao(em dias)
    private int _durationBioest;
    //duracao do bioaumento(em dias)
    private int _durationBioAum;
    //valor de nutrientes ganhos no efeito de atenuacao;
    private int _valueAtenuation;
    //valor de nutrientes ganhos no efeito de bioestimulo;
    private int _valueBioest;
    //valor de nutrientes ganhos no efeito de bioaumento;
    private int _valueBioaum;

    private string _path;
    public Parameters(){
        taxa = 2f;
        scorePercentageLost= 20;
        numDaysEffect = 5;
        numberDayCicles = 10;
        timeDay = 1f;   
        timeSpeed=timeDay/2; 
        respawnCloud=15f;
        speedCloud=0.5f;
        valueAlpha=0.0001f;

        scoreLoss10days=-20;

        costAtenuation=-10;
        costBioest=-25;
        costBioaum=-100;
    
        valueAtenuation=-1;
        valueBioest=5;
        valueBioaum=2;
        
        

        path = Application.dataPath +"/LogEvents.txt";

    }
   

    
    public float verifyStatusEnviroment(float pH,float temp){

            if ((pH >= 6.5f && pH <= 7) && (temp >= 25 && temp <= 30)) {
                return 1f;
            }else if ((pH >= 6.5f && pH <= 7) || (temp >= 25 && temp <= 30)) {
                return 0.9f;
            }else {
                return 0.8f;
        }
        
    }
    
    public (Color,Color,Color) verifyVariablesEnviroment(VariablesEnviroment variables){
        Color perfect = new Color (57/255f, 163/255f, 64/255f, 1f);
        Color good = new Color(253/255f, 172/255f, 7/255f, 1);
        Color bad = new Color (219/255f, 77/255f, 77/255f, 1f);
        Color phColor;
        Color tempColor;
        Color moistureColor;
        if(variables.pH >= 6.5f && variables.pH <= 7){
            phColor=perfect;
        }else if(variables.pH >= 6f && variables.pH < 6.5f){
            phColor=good;
        }else{
            phColor=bad;
        }
        if(variables.temperature >= 25 && variables.temperature <= 30){
            tempColor=perfect;
        }else if(variables.temperature >= 20 && variables.temperature <25){
            tempColor=good;
        }else{
            tempColor=bad;
        }
        if(variables.moisture == Moisture.high){
            moistureColor=perfect;
        }else if(variables.moisture == Moisture.medium){
            moistureColor=good;
        }else{
            moistureColor=bad;
        }

        return (moistureColor:moistureColor,tempColor:tempColor,phColor:phColor);
    }

    public float getMoistureStatus(Moisture moisture){
        switch (moisture)
            {
            case Moisture.low:
                return 0.9f;
                break;
            case Moisture.medium:
                return 1f;
                break;
            case Moisture.high:
                return 1.1f;
                break;
            default: 
                return 1;
                break;
        }
    }

    public float taxa { get; set; }
    public int scorePercentageLost { get; set; }
    public int numDaysEffect { get; set; }
    public int numberDayCicles { get; set; }
    public float timeDay { get; set; }
    public float timeSpeed { get; set; }
    public float respawnCloud { get; set; }
    public float speedCloud { get; set; }
    public float valueAlpha { get; set; }
    public int scoreLoss10days { get; set; }

public int costAtenuation { get; set; }
public int costBioest { get; set; }
public int costBioaum { get; set; }
public int durationAtenuation { get; set; }
public int durationBioest { get; set; }
public int durationBioAum { get; set; }



public int valueAtenuation { get; set; }
public int valueBioest { get; set; }
public int valueBioaum { get; set; }

public string path { get; set; }

}



    

