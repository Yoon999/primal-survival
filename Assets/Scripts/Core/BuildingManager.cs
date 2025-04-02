using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    public GameObject buildingPrefab;

    private List<GameObject> buildings = new List<GameObject>(); // 배치된 건물 리스트

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        BuildingPlacementSystem bps = gameObject.AddComponent<BuildingPlacementSystem>();
        bps.buildingPrefab = buildingPrefab;
    }

    private void Update()
    {
        foreach (GameObject building in buildings)
        {
            if (building.TryGetComponent(out IUpdatableBuilding updatableBuilding))
            {
                updatableBuilding.UpdateBuilding();
            }
        }
    }
    
    // 건물 추가
    public void RegisterBuilding(GameObject building)
    {
        buildings.Add(building);
    }

    // 건물 제거
    public void RemoveBuilding(GameObject building)
    {
        if (buildings.Contains(building))
        {
            buildings.Remove(building);
            Destroy(building);
        }
    }
}