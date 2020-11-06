using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GraphAbundancePopUp : MonoBehaviour {
    
    public GameObject prefrabBacteria;
    public GameObject contentListGraph;
    public GameObject empty;


    public GameObject contentListDay;
    public GameObject text;

    private float heightQuadGraph;
    private float minQuadGraph;
    private List<float[]> listGraph;

    private List<Color> colorList;
    private int colorPos;
    private GameObject goResultInstantiateEmpty;
    private bool _isLoaded;
    private static int filterValue =1;


    private void Awake() {
        print("awakening...");
        listGraph= GraphAbundanceScript.listGraph;
        GraphAbundanceScript graph = FindObjectOfType<GraphAbundanceScript> ();
        graph.setText();
        colorPos =4;
        isLoaded=false;
        colorList = new List<Color> () {
            new Color (219/255f, 77/255f, 77/255f, 1f),
            new Color (57/255f, 163/255f, 64/255f, 1f),
            new Color (222/255f, 180/255f, 67/255f, 1f),
            new Color (106/255f, 73/255f, 214/255f, 1f),
            new Color (56/255f, 145/255f, 209/255f, 1f),
        };
        setColors();

        GameObject temp = text;
        temp.transform.localScale = new Vector3(1f/32.08213f,1f/47.31878f,1f);
        text = temp;
    }
    public void setColors(){
        int i = 0;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag ("quadradoLegendaPoupUp").OrderByDescending (y => y.transform.position.y)) {
            go.GetComponent<SpriteRenderer> ().color = colorList[i];
            i++;
        }
    }
    public void createGraph(){

        for(int i=0;i<listGraph.Count;i++){
        goResultInstantiateEmpty = Instantiate(empty);
        goResultInstantiateEmpty.transform.localScale = new Vector3(1f,0.605f/47.3957f,1f);
        goResultInstantiateEmpty.transform.parent = contentListGraph.transform; 
            for(int j=4;j>=0;j--){
                
                createBar(listGraph[i][j]);
            }
            createDay(i);
        }   
    }
    
    private void createDay(float index){
        if(index==0){
            GameObject textDay = Instantiate (text); 
            textDay.GetComponent<Text>().text = "DAY "+1;
            textDay.transform.parent = contentListDay.transform;
        }else{
            GameObject textDay = Instantiate (text); 
            textDay.GetComponent<Text>().text = "DAY "+index*5;
            textDay.transform.parent = contentListDay.transform;
        }


    }
    private void createBar (float percentage) {
        if (colorPos <0) {
            colorPos = 4;
        }
        GameObject goBacteria = Instantiate (prefrabBacteria); 
        goBacteria.GetComponent<RectTransform>().sizeDelta = new Vector2 (1f,percentage*500f);
        goBacteria.transform.localScale = new Vector3 (1f,1f/78.33561296f,1f);
        goBacteria.GetComponent<Image> ().color = colorList[colorPos];
        goBacteria.transform.parent = goResultInstantiateEmpty.transform;

        colorPos--;

    }
        private float modulo (float i) {
        if (i < 0) {
            return i * -1;
        }
        return i;
    }

    //0 4 8
    //010
    public void filterGraph(int value){
        
        removeAllGraphs();
        switch (value)
        {
            case 0:
                createGraphConditional(1);
                filterValue =1;
            break;
            case 1:
                createGraphConditional(4);
                filterValue =4;
            break;
             case 2:
                createGraphConditional(10);
                filterValue =10;
            break;
            case 3:
                createGraphConditional(20);
                filterValue =20;
                break;
            }


        }
        
    private void createGraphConditional(int condition){
        for(int i=0;i<listGraph.Count;i++){
            if(i%condition==0){
                goResultInstantiateEmpty = Instantiate(empty);
                goResultInstantiateEmpty.transform.localScale = new Vector3(1f,0.605f/47.3957f,1f);
                goResultInstantiateEmpty.transform.parent = contentListGraph.transform;
                    for(int j=4;j>=0;j--){
        
                    createBar(listGraph[i][j]);
            }
            createDay(i);
            }
    }
    }
    public void removeAllGraphs(){
        int childs = contentListGraph.transform.childCount;
        for(int i=0;i<childs;i++){
            GameObject.Destroy(contentListGraph.transform.GetChild(i).gameObject);
            GameObject.Destroy(contentListDay.transform.GetChild(i).gameObject);

        }
    }

    void makeGraph(){
        if((listGraph.Count-1)%filterValue==0){
            goResultInstantiateEmpty = Instantiate(empty);
            goResultInstantiateEmpty.transform.localScale = new Vector3(1f,0.605f/47.3957f,1f);
            goResultInstantiateEmpty.transform.parent = contentListGraph.transform; 
            for(int j=4;j>=0;j--){
                
                createBar(listGraph[listGraph.Count-1][j]);
            }
            createDay(listGraph.Count-1);
        }
    }
        
    
     void Update() {
         if(GraphAbundanceScript.changed && isLoaded){
              listGraph= GraphAbundanceScript.listGraph;
              makeGraph();
              isLoaded=true;
              GraphAbundanceScript.changed=false;
         }

    }
     public bool isLoaded { get; set; }

}