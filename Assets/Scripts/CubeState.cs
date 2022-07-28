using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeState : MonoBehaviour
{
    public List<GameObject> front = new List<GameObject>();
    public List<GameObject> back = new List<GameObject>();
    public List<GameObject> up = new List<GameObject>();
    public List<GameObject> down = new List<GameObject>();
    public List<GameObject> left = new List<GameObject>();
    public List<GameObject> right = new List<GameObject>();

    public static bool shuffle = false;
    public static bool started = false;

    public void PickUp(List<GameObject> cubeSide)
    {
        foreach(GameObject face in cubeSide)
        {
            //Attach to the center cubelet the adjecent cubelets of the same side
            if(face != cubeSide[4])
            {
                face.transform.parent.transform.parent = cubeSide[4].transform.parent;
            }
        }
    }

    public void PutDown(List<GameObject> cubeSide, Transform pivot)
    {
        foreach(GameObject cublet in cubeSide)
        {
            if(cublet != cubeSide[4])
            {
                cublet.transform.parent.transform.parent = pivot;
            }
        }
    }
}
