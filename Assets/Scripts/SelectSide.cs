using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSide : MonoBehaviour
{
    CubeState cubeState;
    ReadCube readCube;
    int layerMask = 1 << 8;
    // Start is called before the first frame update
    void Start()
    {
        cubeState = GetComponent<CubeState>();
        readCube = GetComponent<ReadCube>();
    }

    // Update is called once per frame
    void Update()
    {
        //Group cublets to rotate the face
        if(Input.GetMouseButtonDown(0) && !CubeState.autoRotating)
        {
            readCube.ReadState();

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                GameObject face = hit.collider.gameObject;

                List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    cubeState.up,
                    cubeState.down,
                    cubeState.front,
                    cubeState.back,
                    cubeState.right,
                    cubeState.left,
                };

                foreach(List<GameObject> cubeSide in cubeSides)
                {
                    if(cubeSide.Contains(face))
                    {
                        cubeState.PickUp(cubeSide);
                        cubeSide[4].transform.parent.GetComponent<PivotRotation>().Rotate(cubeSide);
                    }
                }
            }
        }
    }
}
