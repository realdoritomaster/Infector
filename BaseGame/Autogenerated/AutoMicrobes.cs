using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;
using System;
using System.Linq.Expressions;


public class SpawnObjects  : MonoBehaviour, IGameMode
{

    public List<GameObject> objects;

    public SpawnObjects() { }

    public SpawnObjects(List<GameObject> objects)
    {
        this.objects = objects;
    }

    public GameModes gamemode { get { return (GameModes)PlayerPrefs.GetInt("GameMode"); } }

    public void Spawn(Vector2 bounds, Quaternion rotation, Transform parent)
    {
        foreach (GameObject obj in objects)
        {
            switch (gamemode)
            {
                case GameModes.Adventure:
                    if (obj.tag != "Enemy")
                    {
                        GameObject A = Instantiate(obj, bounds, rotation);
                        A.transform.parent = parent;
                        A.SetActive(true);
                    }
                    break;

                case GameModes.Time_Challenge:
                    
                    GameObject T = Instantiate(obj, bounds, rotation);
                    T.transform.parent = parent;
                    T.SetActive(true);
                    break;
            }          
        }       
    }
}

public class AutoMicrobes : MonoBehaviour
{
    //public List<GameObject> ChunkBounds = new List<GameObject>();
    //public Tuple<GameObject, Vector2, bool> ChunkBoundsTuple;

    //public Dictionary<Vector2, bool> ChunkBounds = new Dictionary<Vector2, bool>();
    public List<Tuple<GameObject, Vector2, bool>> ChunkBounds = new List<Tuple<GameObject, Vector2, bool>>();

    public GameObject ChunkBound;
    public int ChunkSize;
    public int MicrobePerChunk;
    public int ViewDistance;
    public int MarginLimit;

    public List<GameObject> ObjsToInstantiate = new List<GameObject>();
    public GameObject Player;
    public GameObject BoundsParent;

    private Coroutine CO;
    private Coroutine DltBnds;
    private bool OnUpdate;
    private int time;
    private Vector2 Key;
    private Tuple<GameObject, Vector2, bool> SavedTuple;

    void Update()
    {       
        if (OnUpdate && time >= 1)
        {
            time = 0;
            
            DltBnds = StartCoroutine(DeleteMicrobes());
            CO = StartCoroutine(CreateBounds());
            SpawnMicrobes();
        }
    }
    
    public void Start()
    {

        Debug.Log("Adventure Started");
        OnUpdate = true;
        CO = StartCoroutine(CreateBounds());
        
    }

    IEnumerator DeleteMicrobes()
    {
        foreach (Tuple<GameObject, Vector2, bool> bound in ChunkBounds)
        {
            if (Vector2.Distance(Player.transform.position, bound.Item1.transform.position) > ViewDistance)
            {                             
                //ChunkBounds.Remove(bound.Item1.transform.position);
                ChunkBounds.Remove(bound);
                Destroy(bound.Item1);
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    /// Spawn Microbes within bounds
    void SpawnMicrobes()
    {

        for (int i = 0; i < ChunkBounds.Count; i++)
        {            

            if (ChunkBounds[i].Item3 != true) //if bound hasnt spawned microbes yet.
            {
                Key = ChunkBounds[i].Item2;
                
                for (int e = 0; e < MicrobePerChunk; e++) 
                {
                    Vector2 rand = new Vector2(Random.Range(-22.5f, 22.5f) + Key.x, Random.Range(-22.5f, 22.5f) + Key.y); //identify random Vector2 within bound
                    SpawnObjects _spawn = new SpawnObjects(ObjsToInstantiate);

                    _spawn.Spawn(rand, new Quaternion(0, 0, 0, 0), ChunkBounds[i].Item1.transform);           
                }
                SavedTuple = ChunkBounds[i];

                ChunkBounds.Remove(SavedTuple);
                ChunkBounds.Add(new Tuple<GameObject, Vector2, bool>(SavedTuple.Item1, SavedTuple.Item2, true));
            }
        }    
    }

    /// Dynamically generate bounds relative to player location
    IEnumerator CreateBounds()
    {
        for (int x = (ChunkSize - 1) * -45; x < ChunkSize;)
        {
            for (int y = (ChunkSize - 1) * -45; y < ChunkSize;)
            {
                int xvec = (x + 45) + (Mathf.RoundToInt(Player.transform.position.x / 45) * 45); //X-axis of grid
                int yvec = (y + 45) + (Mathf.RoundToInt(Player.transform.position.y / 45) * 45); //Y-axis of grid
                
                if (!ChunkBounds.Any(m => m.Item2 == new Vector2(xvec, yvec)))
                {
                    GameObject obj = Instantiate(ChunkBound, ChunkBound.transform.position = new Vector3(
                        xvec,
                        yvec, 
                        0),
                        ChunkBound.transform.rotation);

                    obj.transform.parent = BoundsParent.transform;

                    ChunkBounds.Add(new Tuple<GameObject, Vector2, bool>(obj, obj.transform.position, false));
                    
                }
                
                y += 45;
                
            }

            x += 45;
        }

        ///Original code
        {
            /*for (int x = (ChunkSize - 1) * -45; x < ChunkSize;)
            {
                for (int y = (ChunkSize - 1) * -45; y < ChunkSize;)
                {
                    int xvec = (x + 45) + (Mathf.RoundToInt(Player.transform.position.x / 45) * 45); //X-axis of grid
                    int yvec = (y + 45) + (Mathf.RoundToInt(Player.transform.position.y / 45) * 45); //Y-axis of grid

                    if (!ChunkBounds.ContainsValue(new Vector2(xvec, yvec)))
                    {
                        GameObject obj = Instantiate(ChunkBound, ChunkBound.transform.position = new Vector3(
                            xvec,
                            yvec,
                            0),
                            ChunkBound.transform.rotation);

                        ChunkBounds.Add(obj, obj.transform.position);

                    }

                    y += 45;

                }

                x += 45;
            }*/
        } 

        yield return new WaitForSeconds(.5f);
        
        time++;           
    }
}
