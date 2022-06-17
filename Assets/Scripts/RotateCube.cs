using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    public float speed = 50f;
    bool drag = false;
    float x = 0;
    float y = 0;
    // Start is called before the first frame update

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
        if(Input.GetKeyDown("r"))
        {
            this.transform.rotation = Quaternion.Euler(0, 45, 0);
        }
    }

    void FixedUpdate()
    {
        if(drag)
        {
            x = Input.GetAxis("Mouse X") * speed * Time.fixedDeltaTime;
            y = Input.GetAxis("Mouse Y") * speed * Time.fixedDeltaTime;

            this.transform.Rotate(y, -x, 0, Space.World);
        }
    }
}
