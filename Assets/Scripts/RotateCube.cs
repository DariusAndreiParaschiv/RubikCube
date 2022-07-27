using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    public float speed = 1000f;
    public float lateralRotationSpeed = 3;
    public float lateralX = 3;
    public float lateralY = 1;
    public float lateralZ = -3;
    private bool rotate = false;
    private bool reposition = false;
    Vector3 angleVector;
    bool drag = false;
    float x = 0;
    float y = 0;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(1))
        {
            drag = false;
        }
        if(Input.GetMouseButtonDown(1))
        {
            drag = true;
        }
        //reset cube rotation to default
        if (Input.GetKeyDown("r"))
        {
            rotate = true;
               
        }
        //position the cube upright
        if (Input.GetKeyDown("t"))
        {
            CubeRepositionAngles();
            reposition = true; 
        }
        //signal to rotate
        if (rotate)
        {
            RotateTo(Quaternion.Euler(0, 0, 0));
            if (transform.localRotation == Quaternion.Euler(0, 0, 0))
            {
                rotate = false;
            }
        }
        if (reposition)
        {
            RotateTo(Quaternion.Euler(angleVector.x, angleVector.y, angleVector.z));
            if (transform.localRotation == Quaternion.Euler(angleVector.x, angleVector.y, angleVector.z))
            {
                reposition = false;
            }
        }
    }

    private void RotateTo(Quaternion targetRotation)
    {
        var step = 300f * Time.deltaTime;
        if (transform.localRotation != targetRotation)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, step);
        }
    }

    /// <summary>
    /// Calculate right angles to reposition the cube upright
    /// </summary>
    /// 
    private void CubeRepositionAngles()
    {
        angleVector = transform.localEulerAngles;
        //round angleVector to 90 degrees
        angleVector.x = Mathf.Round(angleVector.x / 90) * 90;
        angleVector.y = Mathf.Round(angleVector.y / 90) * 90;
        angleVector.z = Mathf.Round(angleVector.z / 90) * 90;
    }

    void FixedUpdate()
    {
        if(drag)
        {
            x = Input.GetAxis("Mouse X") * speed * Time.fixedDeltaTime;
            y = Input.GetAxis("Mouse Y") * speed * Time.fixedDeltaTime;

            this.transform.Rotate(y, -x, y, Space.World);
        }
        //lateral rotation, not yet to be used
        //this.transform.Rotate(Input.mouseScrollDelta.y * lateralX * lateralRotationSpeed, Input.mouseScrollDelta.y * lateralY * lateralRotationSpeed, Input.mouseScrollDelta.y * lateralZ * lateralRotationSpeed, Space.World);
    }
}
