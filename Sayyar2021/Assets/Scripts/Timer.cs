using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Firebase;
using Firebase.Database;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviourPunCallbacks
{
    private float timeRemaining;
    public static float fullDuration;
        public DatabaseReference reference;

public GameObject bar;
private void Start() {
    if(PhotonNetwork.CurrentRoom.MaxPlayers==PhotonNetwork.CurrentRoom.PlayerCount){

    switch(PhotonNetwork.CurrentRoom.PlayerCount){
       case 2: 
        timeRemaining =10 * 60;
        break;

        case 3:
        timeRemaining =9 * 60;
        break;
        
        case 4: 
        timeRemaining =8 * 60;
        break;
    }
    fullDuration = timeRemaining;
    animateBar();
   }
}

    void animateBar(){
    LeanTween.scaleY(bar,0.01f,timeRemaining).setOnComplete(finishGame);
}
    void finishGame(){
    Debug.Log("Time has run out!");
    PlanetsOnPlane.status = "Lost";
    storeData();
     SceneManager.LoadScene("HomeScene");
}

    private async void storeData(){
    FirebaseStorageAfterGame.InitializeFirebase();
    //await FirebaseStorageAfterGame.storeVirtualPlayroomData();
    //await FirebaseStorageAfterGame.storeBadgeData();
    await FirebaseStorageAfterGame.storeTimeAndStatus();
    
    }
}