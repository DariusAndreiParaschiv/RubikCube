using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotation : MonoBehaviour
{
    private List<GameObject> activeSide;
    //private Vector3 localForward;
    //private Vector3 mouseRef;
    private bool drag = false;
    private bool autoRotation = false;
    private float sensitivity = 0.4f;
    private float rotationSpeed = 300f;
    private Vector3 rotation;

    private float speed = 300f;

    private Quaternion targetQuaternion;

    private ReadCube readCube;
    private CubeState cubeState;
    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if(drag)
        {
            RotateSide(activeSide);
            if(Input.GetMouseButtonUp(0))
            {
                drag = false;
                RightRotateAngle();
            }
        }
        if(autoRotation)
        {
            RotateToPosition();
        }
    }

    private void RotateSide(List<GameObject> side)
    {
        rotation = Vector3.zero;

        //Vector3 mouseOffSet = (Input.mousePosition - mouseRef);

        if(side[0] == cubeState.front[0])
        {
            //rotation.x = (mouseOffSet.x + mouseOffSet.y) * -sensitivity;
            rotation.x = (Input.GetAxis("Mouse X") * speed * Time.fixedDeltaTime + Input.GetAxis("Mouse Y") * speed * Time.fixedDeltaTime) * -sensitivity;
        }
        if (side[0] == cubeState.back[0])
        {
            //rotation.x = (mouseOffSet.x + mouseOffSet.y) * -sensitivity;
            rotation.x = (Input.GetAxis("Mouse X") * speed * Time.fixedDeltaTime + Input.GetAxis("Mouse Y") * speed * Time.fixedDeltaTime) * sensitivity;
        }
        if (side[0] == cubeState.up[0])
        {
            //rotation.x = (mouseOffSet.x + mouseOffSet.y) * -sensitivity;
            rotation.y = (Input.GetAxis("Mouse X") * speed * Time.fixedDeltaTime + Input.GetAxis("Mouse Y") * speed * Time.fixedDeltaTime) * sensitivity;
        }
        if (side[0] == cubeState.down[0])
        {
            //rotation.x = (mouseOffSet.x + mouseOffSet.y) * -sensitivity;
            rotation.y = (Input.GetAxis("Mouse X") * speed * Time.fixedDeltaTime + Input.GetAxis("Mouse Y") * speed * Time.fixedDeltaTime) * -sensitivity;
        }
        if (side[0] == cubeState.left[0])
        {
            //rotation.x = (mouseOffSet.x + mouseOffSet.y) * -sensitivity;
            rotation.z = (Input.GetAxis("Mouse X") * speed * Time.fixedDeltaTime + Input.GetAxis("Mouse Y") * speed * Time.fixedDeltaTime) * sensitivity;
        }
        if (side[0] == cubeState.right[0])
        {
            //rotation.x = (mouseOffSet.x + mouseOffSet.y) * -sensitivity;
            rotation.z = (Input.GetAxis("Mouse X") * speed * Time.fixedDeltaTime + Input.GetAxis("Mouse Y") * speed * Time.fixedDeltaTime) * -sensitivity;
        }

        transform.Rotate(rotation, Space.Self);

        //mouseRef = Input.mousePosition;
    }

    public void Rotate(List<GameObject> side)
    {
        activeSide = side;
        //mouseRef = Input.mousePosition;
        drag = true;
        //localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
    }
    /// <summary>
    /// Calculate values for the right angles when snapping rotating face into position
    /// </summary>
    public void RightRotateAngle()
    {
        Vector3 angleVector = transform.localEulerAngles;
        //round angleVector to 90 degrees
        angleVector.x = Mathf.Round(angleVector.x / 90) * 90;
        angleVector.y = Mathf.Round(angleVector.y / 90) * 90;
        angleVector.z = Mathf.Round(angleVector.z / 90) * 90;

        targetQuaternion.eulerAngles = angleVector;
        autoRotation = true;
    }
    /// <summary>
    /// Snap the rotating face into the nearest position
    /// </summary>
    private void RotateToPosition()
    {
        drag = false;
        var step = rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);

        if(Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1)
        {
            transform.localRotation = targetQuaternion;
            cubeState.PutDown(activeSide, transform.parent);
            readCube.ReadState();
            autoRotation = false;
        }
    }
}
