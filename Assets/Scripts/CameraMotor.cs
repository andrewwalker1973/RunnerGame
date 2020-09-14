using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;
    public Vector3 Offset = new Vector3(0, 5.0f, -10.0f);

   

    private void LateUpdate()
    {
        Vector3 desiredPosition = lookAt.position + Offset;
        desiredPosition.x = 0;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);
    }
}
