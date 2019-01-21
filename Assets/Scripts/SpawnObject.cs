using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnObject : MonoBehaviour {

    public GameObject prefab;
    public int numberOfSpawns = 5;

    public String spawnType;

    private GameObject[] spawns;

    // Use this for initialization
    void Start () {
        spawns = GameObject.FindGameObjectsWithTag(spawnType);

        System.Random rnd = new System.Random();
        for (int i = 0; i<numberOfSpawns; i++)
        {
            int r = rnd.Next(spawns.Length);
            SpawnObjectInArea(spawns[r]);
        }
    }
	
    void SpawnObjectInArea(GameObject spawn)
    {
        var size = spawn.transform.localScale;
        Vector3 pos = spawn.transform.position + new Vector3(UnityEngine.Random.Range(-size.x / 2, size.x / 2),
                                           UnityEngine.Random.Range(-size.y / 2, size.y / 2),
                                           UnityEngine.Random.Range(-size.z / 2, size.z / 2));
        var pieceOfFood = Instantiate(prefab, pos, Quaternion.identity);
        GOContainter containter = spawn.GetComponent<GOContainter>();
        containter.objects.Add(pieceOfFood);
    }

    private void OnDrawGizmosSelected()
    {
        var spawns = GameObject.FindGameObjectsWithTag(spawnType);
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        foreach (var spawn in spawns)
        {
            Gizmos.DrawCube(spawn.transform.position, spawn.transform.localScale);
        }
    }
}
