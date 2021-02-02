
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class LogEvent
{

    string log;
    public LogEvent()
    {
        log = "[";
    }


    public void registerLog(int day, Bacteria[] bacterias, Contaminant contaminant, VariablesEnviroment variables, Action actionEffect, int score)
    {
        Events ev = new Events(day, bacterias, contaminant, variables, actionEffect, score);
        string json = JsonConvert.SerializeObject(ev, Formatting.Indented);
        log += json + ",";
    }

    public void registerLogAction(Action actionSelected, int score, float polluantRemoved)
    {
        var polluantRemovedTemp = (System.Math.Round(polluantRemoved, 3) * 100).ToString().Replace(',', '.');
        log += "{ \" Action Selected \" : \" " + actionSelected + " \" ,\" Last Result(score) \" : " + score + " ,\" Last Result(% removed) \" :  " + polluantRemovedTemp + " },";
    }

    public void finalizeLog(int valuePhase, string status)
    {
        log += "{ \" status \" : \"" + status + "\" },";
        log = log.Substring(0, log.Length - 1) + "]";
        PlayerManager.Instance.saveLog(log, valuePhase);

    }

}

