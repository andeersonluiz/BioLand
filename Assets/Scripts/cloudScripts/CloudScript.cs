



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudScript : MonoBehaviour {
    private List<GameObject> listCoulds;
    public GameObject could1;
    public GameObject could2;
    public GameObject could3;
    public GameObject could4;
    public GameObject could5;
    public GameObject could6;
    public GameObject could7;
    public GameObject could8;
    private float time =0;
    private Parameters parameters;
    private float _respawnCloud;
    private Enviroment ev;
    private float _speedCloud ;

    //-5.5f -7.1
    private void Start() {
        parameters = new Parameters();
       ev = FindObjectOfType<Enviroment>();

        respawnCloud = parameters.respawnCloud;
        speedCloud = parameters.speedCloud;
        listCoulds =  new List<GameObject>{could1,could2,could3,could4,could5,could6,could7,could8};
    }
    private void Update() {
        //print("respawn: "+respawnCloud);
        time += Time.deltaTime;
        if(time>respawnCloud && !ev.isPaused){
            Vector3 position = new Vector3(gameObject.transform.position.x, Random.Range(-6f,-7.1f),-0.13f);
            Instantiate(listCoulds[Random.Range(0,7)],position,Quaternion.identity);
            time=0;
        }
    }
        public float respawnCloud { get; set; }
        public float speedCloud { get; set; }



}