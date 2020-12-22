using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{

    public float fireTime = 0.5f;
      public GameObject bullet;

    void OnEnable()                         // called from track piece - select new piece on enable
    {


        BulletDestroy.OnFireExited += OnFireExited;         // select next pool item to use
    }

    private void OnDisable()                // called from track piece - select next track piece to use
    {


        BulletDestroy.OnFireExited += OnFireExited;     // select next pool item to use
    }
  
    public int pooledAmount = 40;
    List<GameObject> bullets;

    public Vector3 spawnOrigin;             // Vector3 of origin point for floting origion

    private Vector3 spawnPosition;             // Vector3 of where to spawn track pieces

    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(bullet);
            obj.SetActive(false);
            bullets.Add(obj);

        }
        //InvokeRepeating("Fire", fireTime, fireTime);
        for (int i = 0; i < 10; i++)
        {
            Fire();
        }

    }

   public  void Fire()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                Debug.Log("Bullets found " + i);
                //   bullets[i].transform.position = transform.position;
                //  bullets[i].transform.rotation = transform.rotation;

                spawnOrigin = spawnOrigin + new Vector3(0, 0, 20f);
                bullets[i].transform.position = spawnPosition + spawnOrigin;


                bullets[i].SetActive(true);
                break;
            }
        }
    }

    public void UpdateSpawnOrigin(Vector3 originDelta)          // function to rest the origin point to 0,0,0 
    {
        spawnOrigin = spawnOrigin + originDelta;
    }

    void OnFireExited()
    {
        Debug.Log("FIRE");
        Fire();
    }
}
