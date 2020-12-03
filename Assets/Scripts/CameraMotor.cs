using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{

    // attempt at origionroot
    public GameObject OrigionRoot;


    public Transform target;

    private readonly float _distance = 3.0f;
    // private readonly float _height = 0.7f;
    private readonly float _height = 1.5f;
    private readonly float _heightOffset = 0.0f;
    private readonly float _heightDamping = 5.0f;
    private readonly float _rotationDamping = 3.0f;

    public bool IsMoving { set; get; }
    public Vector3 rotation = new Vector3(35, 0, 0);


    private void Start()
    {
        //attempt at origion root
        gameObject.transform.SetParent(OrigionRoot.transform,true);
    }

    void LateUpdate()
    {
        if (target == null) return;

        if (!IsMoving)
            return;

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

        // 
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(rotation),2.0f * Time.deltaTime);
        
    }
}
