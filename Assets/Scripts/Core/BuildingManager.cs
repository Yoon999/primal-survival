using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    [FormerlySerializedAs("buildingPrefab")] public GameObject buildingPrefabTest;

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
        bps.buildingPrefab = buildingPrefabTest;
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
    
    public bool TryBuild(GameObject buildingPrefab, Vector3 position, ResourceManager.ResourceType requiredResource, int resourceCost)
    {
        if (!ResourceManager.Instance.ConsumeResource(requiredResource, resourceCost))
        {
            Debug.LogWarning("자원이 부족하여 건설할 수 없습니다!");
            return false;
        }
        
        GameObject newBuilding = Instantiate(buildingPrefab, position, Quaternion.identity);
        RegisterBuilding(newBuilding);
        Debug.Log("건물이 성공적으로 배치되었습니다!");
        
        return true;
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