using System.Collections.Generic;
using UnityEngine;

public class SpawningModule : ModuleBase
{
    public GameObject prefabForSpawn;

    private List<GameObject> spawnedObjects = new List<GameObject>();
      
    public override void OnTick()
    {
        base.OnTick();
        GameObject obj = Instantiate(prefabForSpawn);
        spawnedObjects.Add(obj);
    }
}