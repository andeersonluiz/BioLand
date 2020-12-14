using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Linq;

public class PlayerManager  : MonoBehaviour{
    private static DatabasePlayer databasePlayer;
    private static PlayerPreferences playerPrefs;
    public GameObject popUpLogin;
    public GameObject popUpRegister;
    public GameObject LoginSucess;

    public GameObject inputLoginUsername;
    public GameObject inputRegisterUsername;
    
    public GameObject messageLogin;
    private Text messageTextLogin;
    public GameObject messageRegister;
    private Text messageTextRegister;
    public GameObject progressBar;

    public GameObject loginButton;
    public GameObject logoutButton;
    public GameObject playButton;

    private string username;
    private string displayName;
    private static string playFabId;
    public static Dictionary<string,int> phaseList;
    public static Dictionary<string,int> rankingList;
    int sizePhase;
    public static bool getData =false;
    private static PlayerManager pm_instance;
    public static PlayerManager Instance{get{return pm_instance;}}
    public static bool isLogged;

    void Start(){
        pm_instance= this;
        if(databasePlayer==null){
            databasePlayer = new DatabasePlayer();
        } 
        phaseList= new Dictionary<string,int>();
        playFabId="";
        displayName="";
        sizePhase=4;
        messageTextLogin= messageLogin.GetComponent<Text>();
        messageTextRegister = messageRegister.GetComponent<Text>();
        isLogged=false;
        playButton.GetComponent<Button>().interactable=false;

        playerPrefs= new PlayerPreferences();
        if(playerPrefs.getUsername() !=null) {
            
            username=playerPrefs.getUsername();
            messageTextLogin.text="";
            progressBar.SetActive(true);
            loginButton.GetComponent<Button>().interactable=false;
            databasePlayer.loginWithUsername(username,onSucessLogin,onFailureLogin);
        }

    
    }

    public void createAccount(){
        progressBar.SetActive(true);
        messageTextRegister.text="";
        username = inputRegisterUsername.GetComponent<InputField>().text;
        databasePlayer.createAccount(username,onSucessCreateAccount,onFailureCreateAccount);

    }

    public void onSucessCreateAccount(RegisterPlayFabUserResult result){
        progressBar.SetActive(false);
        popUpRegister.SetActive(false);
        popUpLogin.SetActive(true);
        messageTextLogin.text = "Registration successfully completed!";

    }


    public void onFailureCreateAccount(PlayFabError result){
        progressBar.SetActive(false);
        switch(result.Error){
            case PlayFabErrorCode.InvalidParams:
                messageTextRegister.text = "Invalid username";
                break;
             case PlayFabErrorCode.NameNotAvailable:
                messageTextRegister.text = "Username already registered";
                break;    
            case PlayFabErrorCode.ServiceUnavailable:
                messageTextRegister.text = "Network connection error, check your connection and try again";
                break;
            default:
                    messageTextRegister.text = "Unable to register you at the moment. Try again later";
                    Debug.LogError(result.Error);
                    Debug.LogError(result.ErrorMessage);
                    break;
        }
    }


    public void loginWithUsername(){
        messageTextLogin.text="";
        progressBar.SetActive(true);
        loginButton.GetComponent<Button>().interactable=false;
        playButton.GetComponent<Button>().interactable=false;
        username = inputLoginUsername.GetComponent<InputField>().text;
        databasePlayer.loginWithUsername(username,onSucessLogin,onFailureLogin);

    }

    public void onFailureLogin(PlayFabError result){
        Debug.LogError(result.Error);
        Debug.LogError(result.ErrorMessage);
        progressBar.SetActive(false);
        switch(result.Error){
            case PlayFabErrorCode.InvalidParams:
                messageTextLogin.text="Invalid username";
                break;
            case PlayFabErrorCode.AccountNotFound:
                messageTextLogin.text="Username not registered";
                break;
            default:
                messageTextLogin.text="Unable to login you at the moment. Try again later";
                Debug.LogError(result.Error);
                Debug.LogError(result.ErrorMessage);
                break;
        }
    }
    
    public void onSucessLogin(LoginResult result){
        playFabId = result.PlayFabId;
        databasePlayer.getPlayerProfile(playFabId,onSucessGetPlayerProfile,onFailureGetPlayerProfile);
        playerPrefs.setUsername(username);
        playButton.GetComponent<Button>().interactable=true;
    }

    public void onSucessGetPlayerProfile(GetPlayerProfileResult result){
        LoginSucess.SetActive(true);
        displayName =result.PlayerProfile.DisplayName;
        LoginSucess.GetComponentInChildren<Text>().text = "Hello "+displayName+"!!";
        isLogged=true;
        popUpLogin.SetActive(false);
        loginButton.GetComponent<Button>().interactable=true;
        playButton.GetComponent<Button>().interactable=true;
        loginButton.SetActive(false);
        logoutButton.SetActive(true);
        progressBar.SetActive(false);

    }
    
    public void  onFailureGetPlayerProfile(PlayFabError result){
        LoginSucess.SetActive(false);
        LoginSucess.GetComponentInChildren<Text>().text = "";
        Debug.LogError(result.Error);
        Debug.LogError(result.ErrorMessage);
    }

    public void logout(){
        databasePlayer.logout();
        playButton.GetComponent<Button>().interactable=false;
        loginButton.SetActive(true);
        logoutButton.SetActive(false);
        LoginSucess.SetActive(false);
        playerPrefs.deleteAll();
     }

    public void updatephaseList(){
        databasePlayer.getLeaderboardAroundPlayerCloudScript(sizePhase,playFabId,onSucessGetLeaderboard,onFailureGetLeaderboard);
       
    }

    public Dictionary<string,int> getphaseList(){
        return phaseList;
    }

    private void onSucessGetLeaderboard(ExecuteCloudScriptResult result){
        print("oioi "+ result.FunctionResult);
        object resultTemp;
        var serializer = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer);
        var jsonResult =  serializer.DeserializeObject<Dictionary<string,object>>(result.FunctionResult.ToString());
        resultTemp=jsonResult["phasesAndScore"];
        var jsonResult2 =  serializer.DeserializeObject<Dictionary<string,int>>(resultTemp.ToString());
        phaseList=jsonResult2;
        getData=true;
    }
    
    private void onFailureGetLeaderboard(PlayFabError result){
        Debug.Log("error ");
    }

    public void setScore(int phaseIndex, int valueScore){
        databasePlayer.setScore(playFabId,phaseIndex,valueScore,onSucessSetScore,onFailureSetScore);
    }

    private void onSucessSetScore(ExecuteCloudScriptResult result){
        print(result.FunctionResult.ToString());
        print("sucess set Score");   
    }
    private void onFailureSetScore(PlayFabError result){
        Debug.Log("error ");
    }

    public void getRankingList(int phaseIndex){
        databasePlayer.getRankingList(phaseIndex,onSucessGetRanking,onFailureGetRanking);
    }

    private void onSucessGetRanking(ExecuteCloudScriptResult result){
        print("oioi "+ result.FunctionResult);
        object resultTemp;
        var serializer = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer);
        var jsonResult =  serializer.DeserializeObject<Dictionary<string,object>>(result.FunctionResult.ToString());
        resultTemp=jsonResult["listRanking"];
        var jsonResult2 =  serializer.DeserializeObject<Dictionary<string,int>>(resultTemp.ToString());
        rankingList=jsonResult2;
        getData=true;
        print("getData "+ rankingList.Count);
    }
     private void onFailureGetRanking(PlayFabError result){
        Debug.Log("error get Ranking");
    }

    public void saveLog(string log,int phase){
        databasePlayer.saveLog(log,phase);
    }
    public void openLogin(){
        popUpRegister.SetActive(false);
        popUpLogin.SetActive(true);

    }
    public void openRegister(){
        popUpLogin.SetActive(false);
        popUpRegister.SetActive(true);
    }
    public void closepopUp(int id){
        switch(id){
            case 0:
                popUpRegister.SetActive(false);
                break;
            case 1:
                popUpLogin.SetActive(false);
                break;
        }
    }
}