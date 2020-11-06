
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using Newtonsoft.Json;

public class LogEvent{

    string path = Application.dataPath +"/LogEvents.txt";
    string log;
    public LogEvent(){
        log="[";
        if(!File.Exists(path)){
            File.WriteAllText(path,"{");

        }
    }


    public void registerLog(int day, Bacteria[] bacterias, Contaminant contaminant,VariablesEnviroment variables,Action actionEffect,int score){
        Events ev =  new Events(day,bacterias,contaminant,variables,actionEffect,score);
        string json = JsonConvert.SerializeObject(ev,Formatting.Indented);
        log+=json+",";
    }

    public void registerLogAction(Action actionSelected,int score, float polluantRemoved ){
        log+="{ \" Action Selected \" : \" "+actionSelected+" \" ,\" Last Result(score) \" : "+score+" ,\" Last Result(% removed) \" :  "+System.Math.Round(polluantRemoved,3)+" },";
    }
    
    public void finalizeLog(int valuePhase){
        log=log.Substring(0, log.Length - 1)+"]";
        PlayerManager.Instance.saveLog(log,valuePhase);
       
    }

}

