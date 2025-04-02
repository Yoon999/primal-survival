using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public GameObject buildingPrefab; // 배치할 건물 프리팹
    public ResourceManager.ResourceType requiredResource; // 건설에 필요한 자원 종류
    public int resourceCost = 50; // 건설 비용

    public bool TryBuild(Vector3 position)
    {
        if (!ResourceManager.Instance.ConsumeResource(requiredResource, resourceCost))
        {
            Debug.LogWarning("자원이 부족하여 건설할 수 없습니다!");
            return false;
        }

        Instantiate(buildingPrefab, position, Quaternion.identity);
        Debug.Log("건물이 성공적으로 배치되었습니다!");
        return true;
    }
}