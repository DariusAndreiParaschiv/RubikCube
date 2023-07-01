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
    private bool reposition = false;
    private bool canTouch = true;
    private Vector3 angleVector;
    private Quaternion targetQuaternion;
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
        if(Input.GetMouseButtonDown(1) && canTouch)
        {
            drag = true;
        }
        //create smooth animation of the cube rotating to the upright position
        if (reposition)
        {
            var step = 300f * Time.deltaTime;
            if(Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1)
            {
                reposition = false;
                canTouch = true;
            }
            else
            {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);
            }     
        }
    }
    /// <summary>
    /// position the cube upright
    /// </summary>
    public void CubeReposition()
    {
        CubeRepositionAngles();
        canTouch = false;
        reposition = true;
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
        targetQuaternion.eulerAngles = angleVector;
    }

    void FixedUpdate()
    {
        if(drag)
        {
            x = Input.GetAxis("Mouse X") * speed * Time.fixedDeltaTime;
            y = Input.GetAxis("Mouse Y") * speed * Time.fixedDeltaTime;

            this.transform.Rotate(y, -x, y, Space.World);
        }
    }
}
