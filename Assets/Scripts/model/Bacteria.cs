using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Pollutant { Nothing = 0, Diesel = 1, Biodiesel = 2 };
public enum Degradant { Nothing = 0, Diesel = 1, Biodiesel = 2 };
public class Bacteria
{
    private int _id;
    private string _name;
    private float _qtd;
    //taxa de absorcao da bacteria
    //taxa de degradacao da bacteria
    private float _degRate;
    private Pollutant[] _toxic;
    private Degradant[] degradant;

    public Bacteria(int id, string name, float qtd, Pollutant[] toxicList, Degradant[] degList, float degRate)
    {
        this.id = id;
        this.name = name;
        this.qtd = qtd;
        this.degRate = degRate;
        this.toxicList = toxicList;
        this.degList = degList;
    }

    public int id { get; set; }
    public string name { get; set; }
    public float qtd { get; set; }
    public Pollutant[] toxicList { get; set; }
    public Degradant[] degList { get; set; }
    public float degRate { get; set; }

    public static Bacteria[] getClone(Bacteria[] bacs)
    {
        Bacteria[] bacsTemp = new Bacteria[5];
        for (int i = 0; i < 5; i++)
        {
            bacsTemp[i] = (Bacteria)bacs[i].MemberwiseClone();
        }
        return bacsTemp;
    }
    public override string ToString()
    {
        return name + ": " + qtd + " ";
    }
}
