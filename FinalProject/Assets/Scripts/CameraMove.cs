using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform Tank;
    public float MoveSpeed;

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Camera Move
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(Tank.position.x, Tank.position.y, transform.position.z), Time.deltaTime * MoveSpeed);
        //Camera Limit
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4, 4), Mathf.Clamp(transform.position.y, -3, 3), transform.position.z);

    }
}
