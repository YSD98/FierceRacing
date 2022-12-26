using UnityEngine;
using System.Collections.Generic;

public class Ghost : MonoBehaviour
{
    public List<GhostData> ghostsList = new List<GhostData>();
    public GhostData lastData;
    public bool isRec;
}
