/*
 * 1. 자원 확인 (`GetResourceAmount`)
 * - `int foodAmount = ResourceManager.Instance.GetResourceAmount(ResourceManager.ResourceType.Food);`
 * - 현재 보유 중인 자원 개수 확인.
 *
 * 2. 이벤트 시스템 (`OnResourceChanged`)
 * - 콜백이 필요할 때 사용 가능.
 * - `ResourceManager.OnResourceChanged += (type, amount) => { 콜백 코드 };`
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public enum ResourceType
    {
        Food,
        Fuel,
        Materials,
        Artifact
    }

    private readonly Dictionary<ResourceType, int> resources = new();
    public static ResourceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // 자원 초기값 설정
        resources[ResourceType.Food] = 100;
        resources[ResourceType.Fuel] = 50;
        resources[ResourceType.Materials] = 50;
        resources[ResourceType.Artifact] = 0;
    }

    public event Action<ResourceType, int> OnResourceChanged;

    // 자원 획득
    public void AddResource(ResourceType type, int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        resources[type] += amount;
        OnResourceChanged?.Invoke(type, resources[type]);
    }

    // 자원 소모
    public bool ConsumeResource(ResourceType type, int amount)
    {
        if (amount < 0 || resources[type] < amount)
        {
            return false;
        }

        resources[type] -= amount;
        OnResourceChanged?.Invoke(type, resources[type]);

        return true;
    }

    // 현재 자원 수량 반환
    public int GetResourceAmount(ResourceType type)
    {
        return resources.GetValueOrDefault(type, 0);
    }
}