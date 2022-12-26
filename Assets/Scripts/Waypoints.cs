using UnityEngine;
using UnityEngine.UI;

public class Waypoints : MonoBehaviour
{
    Vector3 currentWaypoint;
    Quaternion  currentWaypointRotation;
    Car CarScript;
    GamePlayManager gamePlay;

    private void Update()
    {
        if (gamePlay.Gmm.ghostGameObject[0].isRec == true)
        {
            gamePlay.Gmm.Recordd(0);
        }
    }

    private void Start()
    {
        CarScript = GetComponent <Car>();
        gamePlay = GetComponent<GamePlayManager>();
        CarScript.resetCar.onClick.AddListener(CarResetPos);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "StartWaypoint")
        {
           gamePlay.GameStarted = true;
           gamePlay.Gmm.ghostGameObject[0].isRec = true;
            
            currentWaypoint = other.transform.position;
            currentWaypointRotation = other.transform.localRotation;
        }
        if (other.CompareTag("Waypoint"))
        {
            currentWaypoint = other.transform.position;
            currentWaypointRotation = other.transform.localRotation ;
           
        }
    }
  
    public void CarResetPos()
    {
      transform.SetPositionAndRotation(currentWaypoint, currentWaypointRotation);
    }
}

