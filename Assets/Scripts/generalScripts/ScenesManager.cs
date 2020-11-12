 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.SceneManagement;
 using UnityEngine.UI;

 using UnityEngine.EventSystems;

public class ScenesManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    private static Bacteria[] _bacterias;
    private static Contaminant _contaminant;
    private static VariablesEnviroment _variables;
    public GameObject popUp;
    public GameObject progressBar;
    public GameObject contentList;
    private GraphAbundancePopUp graphAbundancepopUp ;
    public Dropdown dropdown;
    public GameObject gameWinPanel;

    public GameObject bioaugmentationpopUp;
    private float _objectiveValue;
    private static PhasesManager phasesManager;
    public Scene scene;
    public float minLoadingTime =2;
    public static int valuePhase;

    public GameObject popUpRanking;
    public GameObject progressBarRanking;

    Bacteria[] listBacterias; 
    

    private static bool isTutorialCompleted = false;
    private static ScenesManager sm_instance;
    public static ScenesManager Instance{get{return sm_instance;}}
    // Start is called before the first frame update
    void Start()
    {

        listBacterias = new Bacteria[]{
            new Bacteria (0, "Agromyces sp.", 500f,new Pollutant[] {Pollutant.Nothing},new Degradant[] {Degradant.Diesel},0.000001f/2.5f), 
            new Bacteria (1, "Arthrobacter sp.", 500f,new Pollutant[] {Pollutant.Nothing},new Degradant[] {Degradant.Diesel},0.000002f/2.5f), 
            new Bacteria (2, "Bacillus sp.", 500f,new Pollutant[] {Pollutant.Nothing},new Degradant[] {Degradant.Biodiesel},0.0000024f/2.5f), 
            new Bacteria (3, "Burkholderia sp.", 500f,new Pollutant[] {Pollutant.Nothing},new Degradant[] {Degradant.Biodiesel},0.0000024f/2.5f), 
            new Bacteria (4, "Cupriavidus sp.", 500f,new Pollutant[] {Pollutant.Nothing},new Degradant[] {Degradant.Diesel},0.000001f/2.5f), 
            new Bacteria (5, "Lysobacter sp.", 500f,new Pollutant[] {Pollutant.Nothing},new Degradant[] {Degradant.Diesel},0.000001f/2.5f), 
            new Bacteria (6, "Micrococcus sp.", 500f,new Pollutant[] {Pollutant.Nothing},new Degradant[] {Degradant.Nothing},0.000002f/2.5f), 
            new Bacteria (7, "Sinomonas sp.", 500f,new Pollutant[] {Pollutant.Nothing},new Degradant[] {Degradant.Diesel},0.000001f/2.5f), 
            new Bacteria (8, "Staphylococcus sp.", 500f,new Pollutant[] {Pollutant.Nothing},new Degradant[] {Degradant.Biodiesel},0.0000024f/2.5f)
        };
        phasesManager = FindObjectOfType<PhasesManager> ();
        sm_instance = this;
    }
    private void Awake() {
    }

    public static Bacteria[]  bacterias { get; set; }
    public static Contaminant contaminant { get; set; }
    public static VariablesEnviroment variables { get; set; }
    public static float objectiveValue { get; set; }
    public static Camera camera { get; set; }

    public void changeScene(string sceneName){
        StartCoroutine(changeSceneAsync(sceneName));
    }


    IEnumerator changeSceneAsync (string sceneName){
        
        GameObject go =EventSystem.current.currentSelectedGameObject;
        if(go!=null && PlayerManager.isLogged){
            print("changing scene...1 "+sceneName );
            string textBtn = go.GetComponentInChildren<Text>().text;
            if(textBtn=="1"){
                sceneName="tutorial";
            }
            AsyncOperation operation = SceneManager.LoadSceneAsync (sceneName);   
            operation.allowSceneActivation = false;
            loadingScreen.SetActive(true);
            if(sceneName=="phasesScene")
                 PlayerManager.Instance.updatephaseList();

            if(textBtn!=""){
                setPhase(textBtn);                
            }
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress/.9f);
                slider.value = progress;
                if(sceneName=="phasesScene"){
                    while(!PlayerManager.getData){
                        yield return null;
                        }
                    }
                

                if(progress ==1){
                    break;
                }
                
                yield return null;
            }
            
            operation.allowSceneActivation = true;
            PlayerManager.getData=false;
            
        }else{
            print("error");
        }
        
 
    }


    IEnumerator wait(){
        yield return new WaitForSeconds(10f);
       
        
    }
	bool clicked=true;
    public void changeSceneAfterTutorial(string name){
		if(clicked){
			StartCoroutine(changeSceneAfterTutorialAsync(name));
			clicked=false;
		}
    }
    IEnumerator changeSceneAfterTutorialAsync(string sceneName){
                    print("changing scene...2 "+sceneName );

        if(PlayerManager.isLogged){
            string textBtn = "1";
            AsyncOperation operation = SceneManager.LoadSceneAsync (sceneName);   
            operation.allowSceneActivation = false;
            loadingScreen.SetActive(true);
            setPhase(textBtn);                

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress/.9f);
                slider.value = progress;
                if(progress ==1){
                    break;
                }
                
                yield return null;
            }
            
            operation.allowSceneActivation = true;
            PlayerManager.getData=false;
            
        }else{
            print("error");
        }
        
    }
    public void setPhase(string text){
        valuePhase = int.Parse(text);
        switch(text){
            case "1":

                bacterias=new Bacteria[] { 
                    listBacterias[6],
                    listBacterias[1],
                    listBacterias[2],
                    listBacterias[3],
                    listBacterias[8] };
                contaminant = new Contaminant ("Diesel", 10000f, 8000f, Type.Diesel);
                //variables =new VariablesEnviroment(6.6f,25f,Moisture.high);
                //variables =new VariablesEnviroment(1f,25f,Moisture.high);
                //variables =new VariablesEnviroment(1f,1f,Moisture.high);
                //variables =new VariablesEnviroment(6.6f,25f,Moisture.medium);
                variables =new VariablesEnviroment(1f,25f,Moisture.medium);
                //variables =new VariablesEnviroment(1f,1f,Moisture.medium);
                //variables =new VariablesEnviroment(6.6f,25f,Moisture.low);
                //variables =new VariablesEnviroment(1f,25f,Moisture.low);
                //variables =new VariablesEnviroment(1f,1f,Moisture.low);
                objectiveValue=0.1f;
                break;
            case "2":
                bacterias=new Bacteria[] { 
                    listBacterias[0],
                    listBacterias[1],
                    listBacterias[2],
                    listBacterias[3],
                    listBacterias[4] };
                contaminant = new Contaminant ("Biodiesel", 10000f, 8000f, Type.Biodiesel);
                //variables =new VariablesEnviroment(6.6f,25f,Moisture.high);
                //variables =new VariablesEnviroment(1f,25f,Moisture.high);
                //variables =new VariablesEnviroment(1f,1f,Moisture.high);
                //variables =new VariablesEnviroment(6.6f,25f,Moisture.medium);
                //variables =new VariablesEnviroment(1f,25f,Moisture.medium);
                //variables =new VariablesEnviroment(1f,1f,Moisture.medium);
                //variables =new VariablesEnviroment(6.6f,25f,Moisture.low);
                //variables =new VariablesEnviroment(1f,25f,Moisture.low);
                variables =new VariablesEnviroment(1f,1f,Moisture.low);
                objectiveValue=0.2f;
                break;  
            case "3":
                bacterias=new Bacteria[] { 
                    listBacterias[0],
                    listBacterias[1],
                    listBacterias[2],
                    listBacterias[3],
                    listBacterias[4] };
                contaminant = new Contaminant ("Diesel", 10000f, 8000f, Type.Diesel);
                //variables =new VariablesEnviroment(6.6f,25f,Moisture.high);
                //variables =new VariablesEnviroment(1f,25f,Moisture.high);
                //variables =new VariablesEnviroment(1f,1f,Moisture.high);
                //variables =new VariablesEnviroment(6.6f,25f,Moisture.medium);
                variables =new VariablesEnviroment(1f,25f,Moisture.medium);
                //variables =new VariablesEnviroment(1f,1f,Moisture.medium);
                //variables =new VariablesEnviroment(6.6f,25f,Moisture.low);
                //variables =new VariablesEnviroment(1f,25f,Moisture.low);
                //variables =new VariablesEnviroment(1f,1f,Moisture.low);
                objectiveValue=0.1f;
                break;  
            case "4":
                bacterias=new Bacteria[] { 
                    listBacterias[0],
                    listBacterias[1],
                    listBacterias[2],
                    listBacterias[4],
                    listBacterias[5] };
                contaminant = new Contaminant ("Diesel", 10000f, 8000f, Type.Biodiesel);
                //variables =new VariablesEnviroment(6.6f,25f,Moisture.high);
                //variables =new VariablesEnviroment(1f,25f,Moisture.high);
                //variables =new VariablesEnviroment(1f,1f,Moisture.high);
                //variables =new VariablesEnviroment(6.6f,25f,Moisture.medium);
                variables =new VariablesEnviroment(1f,25f,Moisture.medium);
                //variables =new VariablesEnviroment(1f,1f,Moisture.medium);
                //variables =new VariablesEnviroment(6.6f,25f,Moisture.low);
                //variables =new VariablesEnviroment(1f,25f,Moisture.low);
                //variables =new VariablesEnviroment(1f,1f,Moisture.low);
                objectiveValue=0.1f;
                break;      
        }
    }

    public void showpopUp(){
        if(!popUp.active){
        popUp.SetActive(true);
        graphAbundancepopUp = FindObjectOfType<GraphAbundancePopUp> ();
        StartCoroutine(createGraph());
        }
    }

    void OnMouseEnter()
    {
        print("oioioioi");
    }

    IEnumerator createGraph(){
        progressBar.SetActive(true);
        
        yield return new WaitForSeconds(0.5f);
        progressBar.SetActive(false);
        graphAbundancepopUp.createGraph();
        GraphAbundanceScript.changed=false;
        graphAbundancepopUp.isLoaded=true;
        
    }

    public void setOption(){
        graphAbundancepopUp.filterGraph(dropdown.GetComponent<Dropdown>().value);
        
    }
    public void closepopUp(int id){
        switch(id){
            case 0:
                graphAbundancepopUp.isLoaded=false;
                dropdown.GetComponent<Dropdown>().value=0;
                graphAbundancepopUp.removeAllGraphs();
                popUp.SetActive(false);
                break;
            case 1:
                bioaugmentationpopUp.SetActive(false);
                Enviroment enviroment = FindObjectOfType<Enviroment> ();
                enviroment.enableAllButtons();
                break;
            case 2:
                gameWinPanel.SetActive(false);
                break;
            case 3:
                progressBarRanking.SetActive(true);
                popUpRanking.SetActive(false);
                break;
            case 4:
                Application.Quit();
                break;
        }
        
    }
    public void closeAllPopUps(){
        
        if(popUp.activeSelf){
            graphAbundancepopUp.isLoaded=false;
            dropdown.GetComponent<Dropdown>().value=0;
            graphAbundancepopUp.removeAllGraphs();
            popUp.SetActive(false);
        }
        if(bioaugmentationpopUp.activeSelf){
             bioaugmentationpopUp.SetActive(false);
            Enviroment enviroment = FindObjectOfType<Enviroment> ();
            enviroment.enableAllButtons();
        }
    }

    public void showRanking(){
        popUpRanking.SetActive(true);
        StartCoroutine(showRankingIEnumerator());
    }

    IEnumerator showRankingIEnumerator(){
        print("oix");
        PlayerManager.Instance.getRankingList(valuePhase);
        print("oi2");
        while(!PlayerManager.getData){
            yield return null;
        }
        print("oi3");
        Enviroment enviroment = FindObjectOfType<Enviroment> ();
        enviroment.mountRanking(PlayerManager.rankingList);
        progressBarRanking.SetActive(false);
        PlayerManager.getData=false;
        yield return true;
    }
    public void setOptionBioaugmentation(int position){
        Enviroment enviroment = FindObjectOfType<Enviroment> ();
        enviroment.executeBioaugmentation(position);
    }

}
