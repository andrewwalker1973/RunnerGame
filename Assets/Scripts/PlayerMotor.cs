using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    private const float LANE_DISTANCE = 2.5f; //set the lane width
    private const float TURN_SPEED = 0.05f;


    //  Animation
    private Animator anim;
    // Movement
    private CharacterController controller;
    private float jumpForce = 4.0f; // WAS 4
    private float gravity = 12f; // WAS 12
    private float verticalVelocity;
    private float speed = 7.0f;
   
    private int desiredLane = 1; // 0=left 1=middle 2=right

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // gather the inputs on which lane we should be in
        if (MobileInput.Instance.SwipeLeft)
        {
            MoveLane(false);
        }
        if (MobileInput.Instance.SwipeRight)
        {
            MoveLane(true);
        }

        /*  if (Input.GetKeyDown(KeyCode.LeftArrow))
          { 
              // move left
              MoveLane(false);
          }
          if (Input.GetKeyDown(KeyCode.RightArrow))
          {
              // move right
              MoveLane(true);
          }
  */
        // Calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * LANE_DISTANCE;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * LANE_DISTANCE;
        }


        // Calcuate move vector
        Vector3 moveVector = Vector3.zero;
       //  moveVector.x = (targetPosition - transform.position).normalized.x * speed; // character was shakaing 

        moveVector.x = (targetPosition - transform.position).x * speed;

        

        // Calc Y
        if (controller.isGrounded) //if grounded
        {
            //bool isGrounded = true;
            anim.SetBool("Grounded",true);

            verticalVelocity = -0.1f;
           

         //   if (Input.GetKeyDown(KeyCode.Space))
         if (MobileInput.Instance.SwipeUp)
            {
                //Jump
                anim.SetTrigger("Jump");
                verticalVelocity = jumpForce;
              

            }
        }
        else
        {
           // bool isGrounded = false;
            //  Debug.Log("NOTGrounded "); 
            verticalVelocity -= (gravity * Time.deltaTime); // slowly fall to ground level
          //  Debug.Log("Floating");
            // Fast Falling area
           // if (Input.GetKeyDown(KeyCode.M))
           if (MobileInput.Instance.SwipeDown)
            {
                verticalVelocity = -jumpForce;  //drop immediatly to ground
            }
        }
        
        
        moveVector.y = verticalVelocity;
        moveVector.z = speed;


        // move player
        //Debug.Log("Move Vecto" + moveVector);
        controller.Move(moveVector * Time.deltaTime);

        //Rotate charatcter in direction of travel
        Vector3 dir = controller.velocity;
        if (dir != Vector3.zero)
       {
            dir.y = 0;
             transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);
            
        }
        
    }

    private void MoveLane(bool goingRight)
    {
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);

    }

  //  private bool IsGrounded()
  //  {
        //Ray groundRay = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f, controller.bounds.center.z),Vector3.down);
  //      Ray groundRay = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y), controller.bounds.center.z), Vector3.down);
  //      Debug.DrawRay(groundRay.origin, groundRay.direction, Color.cyan, 1.0f);


   //      return Physics.Raycast(groundRay, 0.001f + 0.1f);
        
 //   }
    
}
