using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using System.Threading.Tasks;

[RequireComponent(typeof(ARRaycastManager))]
public class PlanetsOnPlane : MonoBehaviour
{


     [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public DatabaseReference reference;
    private FirebaseUser user;
 private ARRaycastManager raycastManager;
 private GameObject spawnedObject;

 [SerializeField]
 private ARSessionOrigin session ;
 private ARPlaneManager AR_Plane_Manager;

[SerializeField] 
private GameObject placablePrefab;

[SerializeField]
private GameObject[] planets;
[SerializeField]
private Material[] planetsMaterial;

[SerializeField]
private Camera ARCamera;

private PlacementObject selectedObject;

[SerializeField] 
private ARPlane plane;

private Vector3 planeSize;

private LineRenderer lineRenderer;
private Vector3 solarSystemSize;

private bool[] isPlanetInserted;

private bool isGameFinished = true;
static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
private void Awake() {
    raycastManager = GetComponent<ARRaycastManager>();
    AR_Plane_Manager = GetComponent<ARPlaneManager>();
    lineRenderer = planets[7].GetComponent<LineRenderer>();
    AR_Plane_Manager.enabled = true;
    isPlanetInserted = new bool[8];
    for(int i=0; i<planets.Length; i++){
        isPlanetInserted[i] = false;
    }
}

public void disablePlane(){
      setPosition();
   AR_Plane_Manager.enabled = false;
}
   public void onClickExitGameButton(){
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("HomeScene");
        }
  private void Update() {
    Vector2 touchPosition = default;
    if(AR_Plane_Manager.enabled){
    if(raycastManager.Raycast(touchPosition,s_Hits,TrackableType.PlaneWithinBounds)){
        var hitPose = s_Hits[0].pose;
        if(spawnedObject==null){
            spawnedObject = Instantiate(placablePrefab,hitPose.position,Quaternion.identity);
            Debug.Log("IF");
            solarSystemSize = new Vector2 (lineRenderer.bounds.size.x, lineRenderer.bounds.size.z);
            Debug.Log("SOLAR SYSTEM SIZE: " + solarSystemSize);
            disablePlane();
        }
        else{
            spawnedObject.transform.position = hitPose.position;
            spawnedObject.transform.rotation = hitPose.rotation;
            Debug.Log("ELSE");
        } 
        }
   
    }
        if(Input.touchCount>0){
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
            if(touch.phase == TouchPhase.Began){
               Debug.Log("touch begin");
                //selectedObject.transform.localScale = new Vector3 (0.125f,0.125f,0.125f);
                Ray ray = ARCamera.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit)){
                     selectedObject = hit.transform.GetComponent<PlacementObject>();
                     if(selectedObject != null){
                         PlacementObject[] otherObj = (PlacementObject[]) FindObjectsOfType(typeof(PlacementObject));
                         foreach(PlacementObject obj in otherObj){
                             obj.Selected = selectedObject == obj;
                         }
                     }
                }
            }
         if(raycastManager.Raycast(touchPosition, s_Hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinInfinity))
        {
            Debug.Log("during touch");
            Pose hitPose = s_Hits[0].pose;
                if(selectedObject.Selected)
                {
                    selectedObject.transform.position = hitPose.position;
                }
            }
            
            }
            checkPlanets();
        }       
            private void checkPlanets(){
                    Debug.Log("selected name" + selectedObject.gameObject.name);
            switch(selectedObject.gameObject.name){
                case "Mercury(Clone)":
                if(Vector3.Distance(planets[0].transform.position, selectedObject.transform.position)<=0.4){
                 Debug.Log(Vector3.Distance(planets[0].transform.position,selectedObject.transform.position));

                    Destroy(selectedObject);
                    planets[0].GetComponent<Renderer>().material = planetsMaterial[0];
                    isPlanetInserted[0] = true;
                    Debug.Log("planet added!");
                }
                else Debug.Log("incorrect planet!");
                break;
                   case "Venus(Clone)":
                if(Vector3.Distance(planets[1].transform.position, selectedObject.transform.position)<=0.4){
                                    Debug.Log(Vector3.Distance(planets[1].transform.position,selectedObject.transform.position));

                    Destroy(selectedObject);
                    planets[1].GetComponent<Renderer>().material = planetsMaterial[1];
                       isPlanetInserted[1] = true;
                                        Debug.Log("planet added!");

                }
                else Debug.Log("incorrect planet!");
                break;
                   case "NormalEarth(Clone)":
                if(Vector3.Distance(planets[2].transform.position, selectedObject.transform.position)<=0.4){
                                    Debug.Log(Vector3.Distance(planets[2].transform.position,selectedObject.transform.position));

                    Destroy(selectedObject);
                    planets[2].GetComponent<Renderer>().material = planetsMaterial[2];
                 isPlanetInserted[2] = true;
                                        Debug.Log("planet added!");

                }
                else Debug.Log("incorrect planet!");
                break;
                   case "Mars(Clone)":
                if(Vector3.Distance(planets[3].transform.position, selectedObject.transform.position)<=0.23){
                                    Debug.Log(Vector3.Distance(planets[3].transform.position,selectedObject.transform.position));

                    Destroy(selectedObject);
                    planets[3].GetComponent<Renderer>().material = planetsMaterial[3];
                    isPlanetInserted[3] = true;
                                        Debug.Log("planet added!");

                }
                else Debug.Log("incorrect planet!");
                break;
                   case "Jupiter(Clone)":
                if(Vector3.Distance(planets[4].transform.position, selectedObject.transform.position)<=0.23){
                                    Debug.Log(Vector3.Distance(planets[4].transform.position,selectedObject.transform.position));

                    Destroy(selectedObject);
                    planets[4].GetComponent<Renderer>().material = planetsMaterial[4];
                    isPlanetInserted[4] = true;

                                        Debug.Log("planet added!");

                }
                else Debug.Log("incorrect planet!");
                break;
                   case "Saturn(Clone)":
                if(Vector3.Distance(planets[5].transform.position, selectedObject.transform.position)<=0.23){
                                    Debug.Log(Vector3.Distance(planets[5].transform.position,selectedObject.transform.position));

                    Destroy(selectedObject);
                    planets[5].GetComponent<Renderer>().material = planetsMaterial[5];
                    isPlanetInserted[5] = true;

                                        Debug.Log("planet added!");

                }
                else Debug.Log("incorrect planet!");
                break;
                   case "Uranus(Clone)":
                if(Vector3.Distance(planets[6].transform.position, selectedObject.transform.position)<=0.23){
                                    Debug.Log(Vector3.Distance(planets[6].transform.position,selectedObject.transform.position));

                    Destroy(selectedObject);
                    planets[6].GetComponent<Renderer>().material = planetsMaterial[6];
                    isPlanetInserted[6] = true;

                                        Debug.Log("planet added!");

                }
                else Debug.Log("incorrect planet!");
                break;
                   case "Neptune(Clone)":
                if(Vector3.Distance(planets[7].transform.position, selectedObject.transform.position)<=0.23){
                                    Debug.Log(Vector3.Distance(planets[7].transform.position,selectedObject.transform.position));

                    Destroy(selectedObject);
                    planets[7].GetComponent<Renderer>().material = planetsMaterial[7];

                    isPlanetInserted[7] = true;
                                        Debug.Log("planet added!");
                }
                else Debug.Log("incorrect planet!");
                break;
                default: break;
            }
             for(int i=0; i<planets.Length; i++){
        if(!isPlanetInserted[i]){
            isGameFinished = false;
        }
            }
            if(isGameFinished){
                //InitializeFirebase();
                //finishGame();
            }
        }

 //async void InitializeFirebase(){
        //reference = FirebaseDatabase.GetInstance("https://sayyar-2021-default-rtdb.firebaseio.com/").RootReference;
        // await saveBadgeData();
          //}
        // public async Task saveBadgeData(){
        //     int badgeID = Random.Range(0,12);
        //     var path = await Task.Run(() => reference.Child("Badges").Child(""+badgeID).Child("BadgePath").GetValueAsync().Result.Value);
        //     var result = await Task.Run(() => reference.Child("playerinfo").Child(user.UserId).Child("BadgeIDs").Child(""+badgeID).GetValueAsync().Result);
        //     if(result.Exists){
        //       int badgeCount = (int) await Task.Run(() => reference.Child("playerinfo").Child(user.UserId).Child("BadgeIDs").Child(""+badgeID).Child("BadgeCount").GetValueAsync().Result.Value); 
        //       badgeCount++;
        //       await Task.Run(() => reference.Child("playerinfo").Child(user.UserId).Child("BadgeIDs").Child(""+badgeID).Child("BadgeCount").SetValueAsync(badgeCount)); 
        //     }
        //     else{
        //     await Task.Run(() => reference.Child("playerinfo").Child(user.UserId).Child("BadgeIDs").Child(""+badgeID).Child("BadgeID").SetValueAsync(badgeID));        
        //     await Task.Run(() => reference.Child("playerinfo").Child(user.UserId).Child("BadgeIDs").Child(""+badgeID).Child("BadgeCount").SetValueAsync(1));        
        //     await Task.Run(() => reference.Child("playerinfo").Child(user.UserId).Child("BadgeIDs").Child(""+badgeID).Child("BadgePath").SetValueAsync(path));        
        //         }

        //     //await Task.Run(() => reference.Child("Game").Child(gameID).Child("Badge").Child(""+badgeID).SetValueAsync(badgeID));        
        // }
//   private void finishGame(){
//      SceneManager.LoadScene("HomeScene");
//   }
    public void setPosition(){
        for(int i=0; i<planets.Length;i++){
    float randomX = Random.Range(-3, 3);
    float randomY = Random.Range(-3, 3);
     float randomZ = Random.Range(-3, 3);
    Vector3 randomPosition = new Vector3 (randomX, randomY, randomZ);    
    Debug.Log("RandomPosition" + randomPosition);
    Debug.Log("Plane local scale " + plane.transform.localScale);
     Instantiate(planets[i].transform,randomPosition,Quaternion.identity);
        }
}
    }

