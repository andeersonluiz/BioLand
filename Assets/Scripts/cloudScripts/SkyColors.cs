using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SkyColors : MonoBehaviour {
    Enviroment ev;
    private SpriteRenderer[] childrenColorList;
    float alpha = 0f;
    float alphaChildren = 0f;
    float inx =1;
    private Parameters parameters;
    private float _valueAlpha;
    private void Start() {
        childrenColorList =gameObject.GetComponentsInChildren<SpriteRenderer>();
        SetAlpha();
        ev = FindObjectOfType<Enviroment>();
        parameters = new Parameters();
        valueAlpha= parameters.valueAlpha;
    }

    private void Update() {
            if(!ev.isPaused){
                if(alpha>=1){
                    inx = -1;
                    alpha=1;
                }
                if(alpha<=0){
                    inx = 1;
                    alpha=0;
                }
                SetAlpha();
                setAlphaChildren();
                alpha +=valueAlpha*inx;
            }
        
    }


    void SetAlpha(){
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a= alpha;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
        
    }
    void SetAlphaChild(SpriteRenderer sr){
        Color tmp = sr.color;
        tmp.a= alpha;
        sr.material.color = tmp;        
    }


     void setAlphaChildren(){
         foreach (SpriteRenderer sr in childrenColorList)
         {
             SetAlphaChild(sr);
         }
         
     }
         public float valueAlpha{get;set;}

}