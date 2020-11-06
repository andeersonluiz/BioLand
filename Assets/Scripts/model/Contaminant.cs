using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type{Nothing=0, Diesel=1, Biodiesel=2};
public class Contaminant
{
    string _name;
    Type _type;
    float _qtdMax;
    float _qtd;
    float _percentage;
    int i=1;

    public Contaminant(string name,float qtdMax,float qtd,Type type){ 
        this.name = name;
        this.qtdMax = qtdMax;
        this.qtd= qtd;
        this.type = type;
        this.percentage = (float)qtd/qtdMax;
    }

  
    public string name { get; set; }
    public float qtd { 
        get => _qtd;
        set{
           // Debug.Log("day "+ i);
            _qtd=value;
            //Debug.Log("qtd" + _qtd);

            percentage = (float)_qtd/qtdMax;
            //Debug.Log("percentage" + percentage);
            i++;
        } 
    }
    public float qtdMax { get; set; }
    public float percentage { get; set; }
    public Type type{get; set;}

    public Contaminant getClone(){
        return (Contaminant)this.MemberwiseClone();
    }
    public override string ToString(){
        return "Name: "+name+" Percentage contamined: "+(percentage*100)+"%";
    }

}
