using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour
{
    public Transform target;

    public float x = 0;
    public float y = 0;

    //方向靈敏度
    public float sensitivityX = 10f;
    public float sensitivityY = 10f;

    public float distZ;

    public Quaternion rotationEuler;
    public Vector3 camPos;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody)
        {
            rigidbody.freezeRotation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        x += Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        y -= Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

        if (x > 360)
        {
            x -= 360;
        }
        else if (x < 0)
        {
            x += 360;
        }
        //print(y);
        if (y > 10)
        {
            y = 10;
        }
        else if (y < -4)
        {
            y = -4;
        }

        rotationEuler = Quaternion.Euler(y, x, 0);
        camPos = rotationEuler * new Vector3(0, 0, -distZ) + target.position;
        camPos.y = camPos.y + 2.5f;

        transform.rotation = rotationEuler;
        transform.position = camPos;
    }
}
