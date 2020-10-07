using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    public static GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        player = this.gameObject;
    }

    void StopJump()
    {
        anim.SetBool("Jump", false);
    }

  //  void StopMagic()
  //  {
  //      anim.SetBool("isMagic", false);
  //  }

    // Update is called once per frame
    void Update()
    {
          if (Input.GetKeyDown(KeyCode.Space) && anim.GetBool("isMagic") == false)
           {
                anim.SetBool("Jump", true);
            }
        //  else if (Input.GetKeyDown(KeyCode.M) && anim.GetBool("Jump") == false)
      //  if (Input.GetKeyDown(KeyCode.M) && anim.GetBool("Jump") == false)
      //  {
      //      anim.SetBool("isMagic", true);
      //  }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.transform.Rotate(Vector3.up * 90);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.transform.Rotate(Vector3.up * -90);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            this.transform.Translate(-0.1f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            this.transform.Translate(0.1f, 0, 0);
        }
    }
}
