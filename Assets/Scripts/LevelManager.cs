using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { set; get; }

    public  bool SHOW_COLLIDER = true;  //AW remove before build

    // Level Spawning
    private const float DISTANCE_BEFORE_SPAWN = 100f; // /was 100
    private const int INITIAL_SEGMENTS = 5;// was 10
    private const int INITIAL_TRANSITION_SEGMENTS = 4;
    // private const int MAX_SEGMENTS_ON_SCREEN = 15;
    private const int MAX_SEGMENTS_ON_SCREEN = 20;
    private Transform cameraContainer;
    private int amountOfActiveSegments;
    private int continiousSegments;
    private int currentSpawnZ;
    private int currentLevel;
    private int y1, y2, y3;



    // List of Pieces
    public List<Piece> ramps = new List<Piece>();
    public List<Piece> longblocks = new List<Piece>();
    public List<Piece> jumps = new List<Piece>();
    public List<Piece> slides = new List<Piece>();
    [HideInInspector]
    public List<Piece> pieces = new List<Piece>();  // All the pieces in the pool


    //List of Segments
    public List<Segment> availableSegments = new List<Segment>();
    public List<Segment> availableTransitions = new List<Segment>();
    [HideInInspector]
    public List<Segment> segments = new List<Segment>();


    // GamePlay
    private bool isMoving = false;

    private void Awake()
    {
        Instance = this;
        cameraContainer = Camera.main.transform;
        currentSpawnZ = 0;
        currentLevel = 0;
                          
    }

    private void Start()
    {

        for (int i = 0; i < INITIAL_SEGMENTS; i++)
        {
            if (i < INITIAL_TRANSITION_SEGMENTS)
            {
                SpawnTransition();
            }
            else
            {
                //Generate Segments
                GenerateSegment();
            }
        }
    }

    private void Update()
    {
        //Debug.Log("MAX_SEGMENTS_ON_SCREEN" + MAX_SEGMENTS_ON_SCREEN);
        if (currentSpawnZ - cameraContainer.position.z < DISTANCE_BEFORE_SPAWN)
        {
            Debug.Log("Update ");
            Debug.Log("currentSpawnZ" + currentSpawnZ);
            Debug.Log("cameraContainer.position.z" + cameraContainer.position.z);

            Debug.Log("DISTANCE_BEFORE_SPAWN" + DISTANCE_BEFORE_SPAWN);

            Debug.Log("$$$$$$$$$$$$$$ CREATE SEGMENT");

            GenerateSegment();

        }

        if (amountOfActiveSegments >= MAX_SEGMENTS_ON_SCREEN)
        {
            Debug.Log("Shoudl despawn");
            segments[amountOfActiveSegments - 1].DeSpawn();
            amountOfActiveSegments--;

        }
    }


    public Segment GetSegment(int id, bool transition)
    {

        Segment s = null;
        s = segments.Find(x => x.SegId == id && x.transition == transition && !x.gameObject.activeSelf);

        if (s == null)
        {
            GameObject go = Instantiate((transition) ? availableTransitions[id].gameObject : availableSegments[id].gameObject) as GameObject;
            s = go.GetComponent<Segment>();

            s.SegId = id;
            s.transition = transition;

            segments.Insert(0, s);
        }
        else
        {
            segments.Remove(s);
            segments.Insert(0, s);
        }

        return s;

    }




    public Piece GetPiece(PieceType pt,  int visualIndex)
    {
        Piece p = pieces.Find(x => x.type == pt && x.visualIndex == visualIndex && !x.gameObject.activeSelf);
        
        if (p == null)
        {
            GameObject go = null;
            if (pt == PieceType.ramp)
            {
                go = ramps[visualIndex].gameObject;
            }
            else if (pt == PieceType.longblock)
            {
                go = longblocks[visualIndex].gameObject;
            }
            else if (pt == PieceType.jump)
            {
                go = jumps[visualIndex].gameObject;
            }
            else if (pt == PieceType.slide)
            {
                go = slides[visualIndex].gameObject;
            }

            go = Instantiate(go);
            p = go.GetComponent<Piece>();
            pieces.Add(p);
        }
        return p;

    }


    private void GenerateSegment()
    {
        SpawnSegment();

        if (Random.Range(0f,1f) < (continiousSegments * 0.25f))
        {
            //Spawn Transition segment
            continiousSegments = 0;
            SpawnTransition();

        }
        else
        {
            continiousSegments++;
        }
        

    }

    private void SpawnSegment()
    {
        Debug.Log("Shoudl spawn segment");
        
        List<Segment> possibleSeg = availableSegments.FindAll(x => x.beginY1 == y1 || x.BeginY2 == y2 || x.BeginY3 == y3);
        int id = Random.Range(0, possibleSeg.Count);

        Segment s = GetSegment(id, false);
        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;

        s.transform.SetParent(transform);
       
        
        s.transform.localPosition = Vector3.forward * currentSpawnZ;
        Debug.Log("s.length" + s.length);
        currentSpawnZ += s.length;
        Debug.Log("currentSpawnZ" + currentSpawnZ);
        amountOfActiveSegments ++;
        Debug.Log("%%%%%%%%%%%%%%%%%%%%%%%%%S.transform" + s.transform.localPosition);
        s.Spawn();



    }

    private void SpawnTransition()
    {
        Debug.Log("Should spawn trans");
        List<Segment> possibleTransition = availableTransitions.FindAll(x => x.beginY1 == y1 || x.BeginY2 == y2 || x.BeginY3 == y3);
        int id = Random.Range(0, possibleTransition.Count);

        Segment s = GetSegment(id, true);
        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;

        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s.length;
        Debug.Log("currentSpawnZ" + currentSpawnZ);
        amountOfActiveSegments++;
        s.Spawn();

    }

   
    public void UpdateSpawnOrigin(Vector3 originDelta)
    {
        // originDelta;
        Debug.Log("Level Manager originDelta " + originDelta);
       // currentSpawnZ = 0;
        Debug.Log("###################currentSpawnZ" + currentSpawnZ);
        


    }
   
}

