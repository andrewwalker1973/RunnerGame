using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class LevelLayoutGenerator : MonoBehaviour
{
    public LevelChunkData[] levelChunkData;
    public LevelChunkData firstChunk;

    private LevelChunkData previousChunk;

    public Vector3 spawnOrigin;

    private Vector3 spawnPosition;
    public int chunksToSpawn = 30;
    // private int countchunks = 0;

    //adding in for pool
    public int PooledAmount = 30;
    List<GameObject> trackPieces;
    public bool willGrow = true;
    private int chunksToSpawn_count = 0;


    void OnEnable()
    {
        // Debug.Log("On Enable");
        // TriggerExit.OnChunkExited += PickAndSpawnChunk;
        
        TriggerExit.OnChunkExited += PickAndSpawnChunkPool;
    }

    private void OnDisable()
    {
        // Debug.Log("On Disable");
        // TriggerExit.OnChunkExited -= PickAndSpawnChunk;
        
        TriggerExit.OnChunkExited += PickAndSpawnChunkPool;
    }


    void Start()
    {
        // pooling stuff
        trackPieces = new List<GameObject>();
        previousChunk = firstChunk;

        //  countchunks = 0;
        for (int i = 0; i < PooledAmount; i++) // was chunksToSpawn
        {
            //  Debug.Log("spawniug");
         //   PickAndSpawnChunk();

            //  countchunks++;

            LevelChunkData chunkToSpawn = PickNextChunk();

            GameObject objectFromChunk = chunkToSpawn.levelChunks[Random.Range(0, chunkToSpawn.levelChunks.Length)];
            previousChunk = chunkToSpawn;
            // was orig  Instantiate(objectFromChunk, spawnPosition + spawnOrigin, Quaternion.identity);
            // pooling
           // GameObject obj = (GameObject)Instantiate(objectFromChunk, spawnPosition + spawnOrigin, Quaternion.identity);

            if (chunksToSpawn_count < chunksToSpawn)
            {
                GameObject obj = (GameObject)Instantiate(objectFromChunk, spawnPosition + spawnOrigin, Quaternion.identity);
                obj.SetActive(true);
                trackPieces.Add(obj);
            }
            else
            {
                GameObject obj = (GameObject)Instantiate(objectFromChunk);
                obj.SetActive(false);
                trackPieces.Add(obj);
            }

          //  trackPieces.Add(obj);
            chunksToSpawn_count++;
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
        Debug.Log("PickAndSpawnChunk");

        LevelChunkData chunkToSpawn = PickNextChunk();

        GameObject objectFromChunk = chunkToSpawn.levelChunks[Random.Range(0, chunkToSpawn.levelChunks.Length)];
        previousChunk = chunkToSpawn;
        // was orig  Instantiate(objectFromChunk, spawnPosition + spawnOrigin, Quaternion.identity);
        // pooling
        GameObject obj = (GameObject)Instantiate(objectFromChunk, spawnPosition + spawnOrigin, Quaternion.identity);
        obj.SetActive(true);

        trackPieces.Add(obj);
    
        // if (countchunks < chunksToSpawn)
        //   {
        //      obj.SetActive(true);
        //      trackPieces.Add(obj);
        // }
        //  else
        //   {
        //       obj.SetActive(false);
        //      trackPieces.Add(obj);
        //   }
    }

    public void UpdateSpawnOrigin(Vector3 originDelta)
    {
        spawnOrigin = spawnOrigin + originDelta;
    }


    void PickAndSpawnChunkPool()
    {

     //   LevelChunkData chunkToSpawn = PickNextChunk();

    //    GameObject objectFromChunk = chunkToSpawn.levelChunks[Random.Range(0, chunkToSpawn.levelChunks.Length)];
    //    previousChunk = chunkToSpawn;

      for (int i = 0; i < trackPieces.Count; i++)
        {
           // Debug.Log("PickAndSpawnChunkPool I couint" + i);
            //   for (int i = 0; i < trackPieces.Count; i++)
            //   {
               Debug.Log("#################VALUE OF I  PickAndSpawnChunkPool" + i);
            //     if (!trackPieces[i].activeInHierarchy)
            //    {
            //        Debug.Log("FOUND SOMETHING");
            GetPoolobject();
            Debug.Log("end trackPieces[i].transform.position" + trackPieces[i].transform.position);
            //     trackPieces[i].transform.position = spawnPosition + spawnOrigin;
             Debug.Log("2nd trackPieces[i].transform.position" + trackPieces[i].transform.position);
             Debug.Log("2nd Piece Span pos" + spawnPosition);
               Debug.Log("2nd Piece Span spawnOrigin" + spawnOrigin);
            trackPieces[i].SetActive(true);
            
            break;
        }
         //       break;
         //   }
          /*  if (trackPieces[i].activeInHierarchy)
            {
                Debug.Log("did not find anythig");
              

                //  PickAndSpawnChunk_false();
                //  PickAndSpawnChunkPool();
                //break;
                GameObject obj = (GameObject)Instantiate(objectFromChunk, spawnPosition + spawnOrigin, Quaternion.identity);
                trackPieces[i].transform.position = spawnPosition + spawnOrigin;
                obj.SetActive(true);
                trackPieces.Add(obj);
                break;
            }
          
            //break;
      //  }
    }

    /*  void PickAndSpawnChunk_false()
      {
          Debug.Log("PickAndSpawnChunk_false");

         LevelChunkData chunkToSpawn = PickNextChunk();

          GameObject objectFromChunk = chunkToSpawn.levelChunks[Random.Range(0, chunkToSpawn.levelChunks.Length)];
          previousChunk = chunkToSpawn;
          // was orig  Instantiate(objectFromChunk, spawnPosition + spawnOrigin, Quaternion.identity);
          // pooling
          GameObject obj = (GameObject)Instantiate(objectFromChunk, spawnPosition + spawnOrigin, Quaternion.identity);
          obj.SetActive(false);
          trackPieces.Add(obj);

      }
    

    public GameObject GetPoolobject()
    {
       
        for (int i = 0; i < trackPieces.Count; i++)
        {
           Debug.Log("trackPieces.Count" + trackPieces.Count);
            Debug.Log("GetPoolobject I Begin" + i);
            if (!trackPieces[i].activeInHierarchy)
            {
                Debug.Log("Value of i   GetPoolobject" + i);
                spawnOrigin = spawnOrigin + new Vector3(0, 0, 20f);

                Debug.Log("#################New Found something");
             //   Debug.Log("Before 1nd trackPieces[i].transform.position" + trackPieces[i].transform.position);
              //  Debug.Log("Before Piece Span pos" + spawnPosition);
             //   Debug.Log("Before Piece Span spawnOrigin" + spawnOrigin);

                trackPieces[i].transform.position = spawnPosition + spawnOrigin; 
              //  Debug.Log("After trackPieces[i].transform.position" + trackPieces[i].transform.position);
             //   Debug.Log("After Piece Span pos" + spawnPosition);
             //   Debug.Log("1nd Piece Span spawnOrigin" + spawnOrigin);
                trackPieces[i].SetActive(true);
                
               return trackPieces[i];
                
            }

        }
        if (willGrow)
        {
            Debug.Log("Growing the list");
            LevelChunkData chunkToSpawn = PickNextChunk();

            GameObject objectFromChunk = chunkToSpawn.levelChunks[Random.Range(0, chunkToSpawn.levelChunks.Length)];
            previousChunk = chunkToSpawn;
            GameObject obj = (GameObject)Instantiate(objectFromChunk, spawnPosition + spawnOrigin, Quaternion.identity);
            
          //  Debug.Log("Piece Span pos" + spawnPosition);
           // Debug.Log("Piece Span spawnOrigin" + spawnOrigin);
            trackPieces.Add(obj);
            return obj;

        }
        return null;
        
    }
    
}

*/

public class LevelLayoutGenerator : MonoBehaviour
{
    public LevelChunkData[] levelChunkData;
    public LevelChunkData firstChunk;

    private LevelChunkData previousChunk;

    public Vector3 spawnOrigin;

    private Vector3 spawnPosition;
    public int chunksToSpawn = 10;

    void OnEnable()
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
