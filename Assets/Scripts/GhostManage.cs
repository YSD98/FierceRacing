using UnityEngine;
using System.Collections;

public struct GhostData
{
    public Vector3 pos;
    public Quaternion rot;

    public GhostData(Transform transform)
    {
        pos = transform.position;
        rot = transform.rotation;
    }
}
public class GhostManage : MonoBehaviour
{
    public Transform playerCar;
    public Ghost[] ghostGameObject;
    //public int ghostNum;

    
    public void Recordd(int ghostNum)
    {
        if (playerCar.position != ghostGameObject[ghostNum].lastData.pos || playerCar.rotation != ghostGameObject[ghostNum].lastData.rot)
        {
            var newGhostData = new GhostData(playerCar);
            ghostGameObject[ghostNum].ghostsList.Add(newGhostData);

            ghostGameObject[ghostNum].lastData = newGhostData;
        }
    }
    public void Play(int ghostNum)
    {
        ghostGameObject[ghostNum].transform.gameObject.SetActive(true);
        StartCoroutine(StartGhost(ghostNum));
    }

    IEnumerator StartGhost(int ghostNum)
    {
        for (int i = 0; i < ghostGameObject[ghostNum].ghostsList.Count; i++)
        {
            ghostGameObject[ghostNum].transform.position = ghostGameObject[ghostNum].ghostsList[i].pos;
            ghostGameObject[ghostNum].transform.rotation = ghostGameObject[ghostNum].ghostsList[i].rot;
            yield return new  WaitForFixedUpdate();
        }
    }
}
