using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotation : MonoBehaviour
{
    private List<GameObject> activeSide;
    private bool drag = false;
    private bool rotating = false;
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

    void LateUpdate()
    {
        if (drag && !rotating)
        {
            RotateSide(activeSide);
            if (Input.GetMouseButtonUp(0))
            {
                drag = false;
                RightRotateAngle();
            }
        }
        if (rotating)
        {
            RotateToPosition();
        }
    }

    private void RotateSide(List<GameObject> side)
    {
        rotation = Vector3.zero;

        if (side[0] == cubeState.front[0])
        {
            rotation.x = (Input.GetAxis("Mouse X") * speed * Time.fixedDeltaTime) * -sensitivity;
        }
        if (side[0] == cubeState.back[0])
        {
            rotation.x = (Input.GetAxis("Mouse X") * speed * Time.fixedDeltaTime) * sensitivity;
        }
        if (side[0] == cubeState.up[0])
        {
            rotation.y = (Input.GetAxis("Mouse X") * speed * Time.fixedDeltaTime) * sensitivity;
        }
        if (side[0] == cubeState.down[0])
        {
            rotation.y = (Input.GetAxis("Mouse X") * speed * Time.fixedDeltaTime) * -sensitivity;
        }
        if (side[0] == cubeState.left[0])
        {
            rotation.z = (Input.GetAxis("Mouse X") * speed * Time.fixedDeltaTime) * sensitivity;
        }
        if (side[0] == cubeState.right[0])
        {
            rotation.z = (Input.GetAxis("Mouse X") * speed * Time.fixedDeltaTime) * -sensitivity;
        }

        transform.Rotate(rotation, Space.Self);
    }

    public void Rotate(List<GameObject> side)
    {
        activeSide = side;
        drag = true;
    }

    public void StartAutoRotate(List<GameObject> side, float angle)
    {
        cubeState.PickUp(side);
        Vector3 localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
        targetQuaternion = Quaternion.AngleAxis(angle, localForward) * transform.localRotation;
        activeSide = side;
        rotating = true;
        drag = false;
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
        rotating = true;
    }
    /// <summary>
    /// Snap the rotating face into the nearest position
    /// </summary>
    private void RotateToPosition()
    {
        drag = false;
        var step = 3 * rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);

        if (Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1)
        {
            transform.localRotation = targetQuaternion;
            cubeState.PutDown(activeSide, transform.parent);
            readCube.ReadState();
            rotating = false;
            CubeState.autoRotating = false;
        }
    }
}