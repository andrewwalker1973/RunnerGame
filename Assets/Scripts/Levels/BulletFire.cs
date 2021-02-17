using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{

   // public float fireTime = 0.5f;
   //  public GameObject bullet;
    private int randomInt;
    private const int INITIAL_SEGMENTS = 20;// was 10

    public GameObject[] segmentArr;

    void OnEnable()                         // called from track piece - select new piece on enable
    {


        BulletDestroy.OnFireExited += OnFireExited;         // select next pool item to use
    }

    private void OnDisable()                // called from track piece - select next track piece to use
    {


        BulletDestroy.OnFireExited += OnFireExited;     // select next pool item to use
    }
  
    public int pooledAmount = 25;
    List<GameObject> bullets;

    //try add transitions
    List<GameObject> Transition;
    private const int INITIAL_TRANSITION_SEGMENTS = 4;
    public GameObject[] TransArr;
    private int randomIntTrans;
    public int TranspooledAmount = 6;
    public int TrackPieceCount = 0;

    public Vector3 spawnOrigin;             // Vector3 of origin point for floting origion

    private Vector3 spawnPosition;             // Vector3 of where to spawn track pieces

    // Start is called before the first frame update
    void Start()
    {

       
        bullets = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {

                //Generate Segments
                randomInt = Random.Range(0, segmentArr.Length);
                
             
                GameObject obj = Instantiate(segmentArr[randomInt]) as GameObject;


               
                obj.SetActive(false);
                bullets.Add(obj);
  
            

        }

        Transition = new List<GameObject>();
        for (int i = 0; i < TranspooledAmount; i++)
        {
            randomIntTrans = Random.Range(0, TransArr.Length);
                  GameObject Transobj = Instantiate(TransArr[randomIntTrans]) as GameObject;
                   Transobj.SetActive(false);
                   Transition.Add(Transobj);
        }


        for (int i = 0; i < INITIAL_SEGMENTS; i++)
        {
            if (i < INITIAL_TRANSITION_SEGMENTS)
            {
                CreateTransition();
            }
            else
            {
                //Generate Segments
                CreateSegment();
            }
        }
        TrackPieceCount = 0;
    }

   public  void CreateSegment()
    {
        TrackPieceCount++;
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                spawnOrigin = spawnOrigin + new Vector3(0, 0, 20f);
                bullets[i].transform.position = spawnPosition + spawnOrigin;


                bullets[i].SetActive(true);
                break;
            }
        }
    }


    public void CreateTransition()
    {
        TrackPieceCount++;
        for (int i = 0; i < Transition.Count; i++)
        {
            if (!Transition[i].activeInHierarchy)
            {
                spawnOrigin = spawnOrigin + new Vector3(0, 0, 20f);
                Transition[i].transform.position = spawnPosition + spawnOrigin;


                Transition[i].SetActive(true);
                break;
            }
        }
    }



   public void UpdateSpawnOrigin(Vector3 originDelta)          // function to rest the origin point to 0,0,0 
    {
        Debug.Log("originDelta " + originDelta);
        Debug.Log("spawnOrigin " + spawnOrigin);

        spawnOrigin = spawnOrigin + originDelta;
        
    }
  

    void OnFireExited()
    {
       // Debug.Log("TrackPieceCount " + TrackPieceCount);

        if (TrackPieceCount == 10)
        {
            CreateTransition();
            TrackPieceCount = 0;
        }
        else
        {
            CreateSegment();
        }
    }
}
