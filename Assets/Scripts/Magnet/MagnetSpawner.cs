using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSpawner : MonoBehaviour
{
    public int maxmagnet = 5;
    public float chanceToSpawn = 0.5f;
    public bool forceSpawnAll = false;

    private GameObject[] magnet;

    private void Awake()
    {

        magnet = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            magnet[i] = transform.GetChild(i).gameObject;
        }

        OnDisable();
    }


    private void OnEnable()
    {
        if (Random.Range(0.0f, 1.0f) > chanceToSpawn)
        {
            return;
        }

        if (forceSpawnAll)
            for (int i = 0; i < maxmagnet; i++)
            {
                magnet[i].SetActive(true);
            }

        else
        {
            int r = Random.Range(0, maxmagnet);
            for (int i = 0; i < r; i++)
            {
                magnet[i].SetActive(true);
            }
        }

    }

    private void OnDisable()
    {
        foreach (GameObject go in magnet)
            go.SetActive(false);
    }
}
