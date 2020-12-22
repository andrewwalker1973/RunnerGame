using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelLayoutGenerator : MonoBehaviour
{
    public LevelChunkData[] levelChunkData;  // Array of track tiles
    public LevelChunkData firstChunk;       // Which track piece to use first

    private LevelChunkData previousChunk;   // which track piece used last

    public Vector3 spawnOrigin;             // Vector3 of origin point for floting origion

    private Vector3 spawnPosition;             // Vector3 of where to spawn track pieces
    public int chunksToSpawn = 10;              // How many trackpieces to create when starting

    //adding in for pool
    public int PooledAmount = 20;           // Total number of trackpieces in pool
    List<GameObject> trackPieces;           // list of all Track Pieces
 
    void OnEnable()                         // called from track piece - select new piece on enable
    {
        
        
     //   TriggerExit.OnChunkExited += PickAndSpawnChunkPool;         // select next pool item to use
    }
   
    private void OnDisable()                // called from track piece - select next track piece to use
    {
      
        
    //    TriggerExit.OnChunkExited += PickAndSpawnChunkPool;     // select next pool item to use
    }


    void Start()
    {
        // pooling stuff
        trackPieces = new List<GameObject>();                   // initilize the list
        previousChunk = firstChunk;                             // indicator of previous track piece used



        for (int i = 0; i < PooledAmount; i++)              // create 20 track pieces in the list
        {


            LevelChunkData chunkToSpawn = PickNextChunk_nopos();

            GameObject objectFromChunk = chunkToSpawn.levelChunks[Random.Range(0, chunkToSpawn.levelChunks.Length)];
            previousChunk = chunkToSpawn;

         
            GameObject obj = (GameObject)Instantiate(objectFromChunk);
            obj.SetActive(false);                                   // set as false as only want small amount to start
            trackPieces.Add(obj);                                   // add track pieces to List


        }

         for (int i = 0; i < chunksToSpawn; i++)                    // Create 10 initial section of the track and mark active
        {
            GetPoolobject();
        }
  

    }


    LevelChunkData PickNextChunk_nopos()                            // Function to determine which trackpiece to used based on entry direction
    {
        List<LevelChunkData> allowedChunkList = new List<LevelChunkData>();
        LevelChunkData nextChunk = null;

        LevelChunkData.Direction nextRequiredDirection = LevelChunkData.Direction.North;

        switch (previousChunk.exitDirection)
        {
            case LevelChunkData.Direction.North:
                nextRequiredDirection = LevelChunkData.Direction.South;
               break;
            case LevelChunkData.Direction.East:
                nextRequiredDirection = LevelChunkData.Direction.West;
               break;
            case LevelChunkData.Direction.South:
                nextRequiredDirection = LevelChunkData.Direction.North;
             break;
            case LevelChunkData.Direction.West:
                nextRequiredDirection = LevelChunkData.Direction.East;
              break;
            default:
                break;
        }

        for (int i = 0; i < levelChunkData.Length; i++)
        {
            if (levelChunkData[i].entryDirection == nextRequiredDirection)
            {
                allowedChunkList.Add(levelChunkData[i]);
            }
        }

        nextChunk = allowedChunkList[Random.Range(0, allowedChunkList.Count)];

        return nextChunk;

    }
  
    
   

    public void UpdateSpawnOrigin(Vector3 originDelta)          // function to rest the origin point to 0,0,0 
    {
        spawnOrigin = spawnOrigin + originDelta;
    }


    void PickAndSpawnChunkPool()                                // function to select next avail track piece
    {
            GetPoolobject();
    }


    public GameObject GetPoolobject()                       // function to select next avail track piece
    {

        
        for (int i = 0; i < trackPieces.Count; i++)            // for all the items in the List  look for inactive track pieces       
        {
            
            Debug.Log("Looking for Track piece at " + i);
            if (!trackPieces[i].activeInHierarchy)                  // If not active then pick one  move to correct location and mark ative and return piece knowledge 


                //  PROBLEM ISSUE - this for loop only seems to work once for the count of , the trackpieces not active are enabled and used as needed once and then become inactive but never used again.
            {
                Debug.Log("######### Found Track Piece at " + i);
                spawnOrigin = spawnOrigin + new Vector3(0, 0, 20f);
                trackPieces[i].transform.position = spawnPosition + spawnOrigin; 
                trackPieces[i].SetActive(true);
                return trackPieces[i];
            }
        }
        
       return null;
        
    }
    
}


/*
public class LevelLayoutGenerator : MonoBehaviour
{
    public LevelChunkData[] levelChunkData;
    public LevelChunkData firstChunk;

    private LevelChunkData previousChunk;

    public Vector3 spawnOrigin;

    private Vector3 spawnPosition;
    public int chunksToSpawn = 10;

    //adding in for pool
    public int PooledAmount = 30;
    List<GameObject> trackPieces;


  //  void OnEnable()
    {
        TriggerExit.OnChunkExited += PickAndSpawnChunk;
    }

    private void OnDisable()
    {
        TriggerExit.OnChunkExited -= PickAndSpawnChunk;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PickAndSpawnChunk();
        }
    }

    void Start()
    {

        // pooling stuff
        trackPieces = new List<GameObject>();
        for (int i = 0; i < PooledAmount; i++)
        {

        }

        previousChunk = firstChunk;

        for (int i = 0; i < chunksToSpawn; i++)
        {
            PickAndSpawnChunk();
        }
    }

    LevelChunkData PickNextChunk()
    {
        List<LevelChunkData> allowedChunkList = new List<LevelChunkData>();
        LevelChunkData nextChunk = null;

        LevelChunkData.Direction nextRequiredDirection = LevelChunkData.Direction.North;

        switch (previousChunk.exitDirection)
        {
            case LevelChunkData.Direction.North:
                nextRequiredDirection = LevelChunkData.Direction.South;
                spawnPosition = spawnPosition + new Vector3(0f, 0, previousChunk.chunkSize.y);

                break;
            case LevelChunkData.Direction.East:
                nextRequiredDirection = LevelChunkData.Direction.West;
                spawnPosition = spawnPosition + new Vector3(previousChunk.chunkSize.x, 0, 0);
                break;
            case LevelChunkData.Direction.South:
                nextRequiredDirection = LevelChunkData.Direction.North;
                spawnPosition = spawnPosition + new Vector3(0, 0, -previousChunk.chunkSize.y);
                break;
            case LevelChunkData.Direction.West:
                nextRequiredDirection = LevelChunkData.Direction.East;
                spawnPosition = spawnPosition + new Vector3(-previousChunk.chunkSize.x, 0, 0);

                break;
            default:
                break;
        }

        for (int i = 0; i < levelChunkData.Length; i++)
        {
            if (levelChunkData[i].entryDirection == nextRequiredDirection)
            {
                allowedChunkList.Add(levelChunkData[i]);
            }
        }

        nextChunk = allowedChunkList[Random.Range(0, allowedChunkList.Count)];

        return nextChunk;

    }

    void PickAndSpawnChunk()
    {
        LevelChunkData chunkToSpawn = PickNextChunk();

        GameObject objectFromChunk = chunkToSpawn.levelChunks[Random.Range(0, chunkToSpawn.levelChunks.Length)];
        previousChunk = chunkToSpawn;
        Instantiate(objectFromChunk, spawnPosition + spawnOrigin, Quaternion.identity);

    }

    public void UpdateSpawnOrigin(Vector3 originDelta)
    {
        spawnOrigin = spawnOrigin + originDelta;
    }

}
*/