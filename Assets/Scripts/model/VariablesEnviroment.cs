
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Moisture { low = 0, medium = 1, high = 2 };
public class VariablesEnviroment
{

    private float _pH;
    private float _temperature;
    private Moisture _moisture;
    public VariablesEnviroment(float pH, float temperature, Moisture moisture)
    {
        this.pH = pH;
        this.temperature = temperature;
        this.moisture = moisture;
    }



    public float pH { get; set; }
    public float temperature { get; set; }
    public Moisture moisture { get; set; }

    public VariablesEnviroment getClone()
    {
        return (VariablesEnviroment)this.MemberwiseClone();
    }
    public override string ToString()
    {
        return "pH: " + pH + " Temperature: " + temperature + " Moisture: " + moisture;
    }
}