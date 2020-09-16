using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    /*  public Transform lookAt;
      public Vector3 Offset = new Vector3(0, 5.0f, -10.0f);



      private void LateUpdate()
      {
          Vector3 desiredPosition = lookAt.position + Offset;
          desiredPosition.x = 0;
          transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);
      }

      */

    public Transform target;

    private readonly float _distance = 3.0f;
    // private readonly float _height = 0.7f;
    private readonly float _height = 1.0f;
    private readonly float _heightOffset = 0.0f;
    private readonly float _heightDamping = 5.0f;
    private readonly float _rotationDamping = 3.0f;

    void LateUpdate()
    {
        if (target == null) return;

        var position = transform.position;
        var targetPosition = target.position;

        //   if (!PlayerController.Dead)
        //  {
        var wantedRotationAngle = target.eulerAngles.y;
        var wantedHeight = targetPosition.y + _height;

        var currentRotationAngle = transform.eulerAngles.y;
        var currentHeight = position.y;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, _rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightDamping * Time.deltaTime);

        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        position = targetPosition;

        var distance = Vector3.forward * _distance;
        position -= currentRotation * distance;

        transform.position = new Vector3(position.x, currentHeight + _heightOffset, position.z);
        //   }

        //  transform.LookAt(target);
    }
}
