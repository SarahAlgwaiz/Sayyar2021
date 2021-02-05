using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace com.cactusteam.Sayyar{
    public class PlayerConnectionScript: MonoBehaviourPunCallbacks{
        [Tooltip("Maximum number of players in each room. When a room is full, new players can't join in. Therefore a new room will be created for the new player.")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;
    
    [SerializeField]
private Button joinButton;
 [SerializeField]
private Button createButton;
[SerializeField]
private GameObject connectingText;
[SerializeField]
private GameObject joinRoomView;
[SerializeField]
private GameObject createRoomView;
[SerializeField]
private TMPro.TMP_Text roomCodeCreateField;
[SerializeField]
private Button roomCodeConfirmButton;
[SerializeField]
private TMPro.TMP_InputField roomNumField;
 
 [SerializeField]
private GameObject GameView;
 
 [SerializeField]
private Button ExitGameButton;
 
private int roomNumber;

string gameVersion = "1";


    public void OnClickExitButton(){
        GameView.SetActive(false);
        createRoomView.SetActive(false);
        createButton.gameObject.SetActive(true);
        joinButton.gameObject.SetActive(true);
        PhotonNetwork.LeaveRoom();
        Debug.Log("exited game");
    }
    public void OnClickJoinButton(){
        joinRoomView.SetActive(true);
        joinButton.gameObject.SetActive(false);
        createButton.gameObject.SetActive(false);
    }
    public void OnClickCreateButton(){
        createRoomView.SetActive(true);
        joinButton.gameObject.SetActive(false);
        createButton.gameObject.SetActive(false);
        createRoom();
    }
    public void OnClickRoomCodeConfirmButton(){
        connectingText.SetActive(true);
        joinRoomView.SetActive(false);
        JoinRoom();
    }
        public override void OnConnectedToMaster(){
            Debug.Log("connected to master");
            createButton.gameObject.SetActive(true);
            joinButton.gameObject.SetActive(true);
            connectingText.SetActive(false);
        }
        public void createRoom(){
            while(!PhotonNetwork.IsConnected){
                Connect();
            }
            Debug.Log("Connected create room");
             roomNumber=  UnityEngine.Random.Range(0, 100000);
            PhotonNetwork.CreateRoom(roomNumber.ToString("00000"), new RoomOptions{IsVisible = false, IsOpen = true, MaxPlayers = maxPlayersPerRoom});
            Debug.Log("Room Created");
        }
        public override void OnCreatedRoom(){
            roomCodeCreateField.text = "Room Number: "+roomNumber;
        }
        
        public void JoinRoom(){
            while(!PhotonNetwork.IsConnected){
                Connect();
            }         
               string roomCode = roomNumField.text;
               if(roomCode.Length!=5){
                   Debug.Log("Room Code length should be 5");
               }
          
            PhotonNetwork.JoinRoom(roomCode);
            
        }
        public override void OnDisconnected(DisconnectCause cause){
            createButton.gameObject.SetActive(true);
            joinButton.gameObject.SetActive(true);
            connectingText.SetActive(false);
            Debug.Log("disconnected" + cause);
        }       
          void Awake() {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        public override void OnJoinRoomFailed(short returnCode, string message){
            Debug.Log("join room failed" + message);
            connectingText.SetActive(false);
            createButton.gameObject.SetActive(true);
            joinButton.gameObject.SetActive(true);
        }
        public override void OnJoinedRoom(){
            Debug.Log("Success! joined room");
            GameView.gameObject.SetActive(true);
            joinRoomView.SetActive(false);
        }
         void Start() {
               createButton.gameObject.SetActive(false);
               joinButton.gameObject.SetActive(false);
               connectingText.SetActive(true);
            Connect();

        }
       public override void OnCreateRoomFailed(short returnCode, string message){
           Debug.Log("failed to create room");
           createRoom();
            
        }
        public void Connect(){
           
            connectingText.SetActive(true);
            if(PhotonNetwork.IsConnected){
                Debug.Log("Success");
            }
            else{
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
                Debug.Log("Success");

            }
    
        }
    }
}