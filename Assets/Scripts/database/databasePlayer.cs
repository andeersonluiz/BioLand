using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
public class DatabasePlayer{


    public DatabasePlayer(){
        if(string.IsNullOrEmpty(PlayFabSettings.TitleId)){
            PlayFabSettings.TitleId="8BA6A";
        }
    }

    public void createAccount(string username,Action<RegisterPlayFabUserResult> onSucess,Action<PlayFabError> onFailure){
        Debug.Log("o user e "+username.Length);
        var request = new RegisterPlayFabUserRequest {DisplayName = username, Username = username ,Password = "YSnUVui5cX",RequireBothUsernameAndEmail =false};
        PlayFabClientAPI.RegisterPlayFabUser(request,onSucess,onFailure);
    }
    
    public void loginWithUsername(string username, Action<LoginResult> onSucess,Action<PlayFabError> onFailure){
        var request = new LoginWithPlayFabRequest {Username = username,Password = "YSnUVui5cX"};
        PlayFabClientAPI.LoginWithPlayFab(request,onSucess,onFailure);
    }

    public void getPlayerProfile(string playFabId, Action<GetPlayerProfileResult> onSucess,Action<PlayFabError> onFailure){
        var request = new GetPlayerProfileRequest {PlayFabId = playFabId};
        PlayFabClientAPI.GetPlayerProfile(request,onSucess,onFailure);
    }
    public void logout(){
        PlayFabClientAPI.ForgetAllCredentials();
    }
    

    public void getLeaderboardAroundPlayerCloudScript(int sizePhase,string playFabId,Action<ExecuteCloudScriptResult> onSucess,Action<PlayFabError> onFailure ){
    PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
    {
        FunctionName = "getLastPhase", // Arbitrary function name (must exist in your uploaded cloud.js file)
        FunctionParameter = new { sizePhase = sizePhase,playFabId=playFabId}, // The parameter provided to your function
        GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
    }, onSucess, onFailure);
}
  

    public void setScore(string playFabId,int phaseIndex, int valueScore,Action<ExecuteCloudScriptResult> onSucess,Action<PlayFabError> onFailure ){
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "setScore", // Arbitrary function name (must exist in your uploaded cloud.js file)
            FunctionParameter = new { playFabId = playFabId,valueScore=valueScore,phase=phaseIndex}, // The parameter provided to your function
            GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
        }, onSucess, onFailure);
    }

    public void getRankingList(int phaseIndex,Action<ExecuteCloudScriptResult> onSucess,Action<PlayFabError> onFailure){
        Debug.Log("odsadsi");
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "getRanking", // Arbitrary function name (must exist in your uploaded cloud.js file)
            FunctionParameter = new { phase=phaseIndex}, // The parameter provided to your function
            GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
        }, onSucess, onFailure);
    }

    public void saveLog(string data,int phaseIndex) {
    PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest() {
        Data = new Dictionary<string, string>() {
            {"Phase"+phaseIndex+":"+ System.DateTime.Now.ToString("MM/dd/yyyy HH:mm"), data}
        }
    },
    result => Debug.Log("Successfully updated user data"),
    error => {
        Debug.Log("Got error setting user data Ancestor to Arthur");
        Debug.Log(error.GenerateErrorReport());
    });
}
    public static string alfanumericoAleatorio(int tamanho)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new System.Random();
        var result = new string(
            Enumerable.Repeat(chars, tamanho)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());
        return result;
    }
    public string name { get; set; }
}