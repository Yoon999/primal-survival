/*
 * 1. 자동 소비 (`AutoConsume`)
 * - `consumeInterval` (초)마다 `consumeAmount`만큼 자원 차감.
 * - 자원이 부족하면 소비 중지.
 *
 * 2. 일회성 소비 (`ConsumeOnce`)
 * - 건설, 연구 같은 이벤트에서 즉시 소비 가능.
 *
 * 3. 소비 중지 (`StopConsuming`)
 * - 기계가 꺼지거나 자원이 부족할 때 소비 중단.
 */

using System.Collections;
using UnityEngine;

public class ResourceConsumer : MonoBehaviour
{
    public ResourceManager.ResourceType resourceType;
    public int consumeAmount = 10;
    public float consumeInterval = 5.0f;

    private bool isConsuming;

    private void Start()
    {
        StartCoroutine(AutoConsume());
    }

    // 자동 자원 소비 코루틴
    private IEnumerator AutoConsume()
    {
        isConsuming = true;
        while (isConsuming)
        {
            yield return new WaitForSeconds(consumeInterval);

            // 자원 소비 시도
            var success = ResourceManager.Instance.ConsumeResource(resourceType, consumeAmount);

            // 자원이 부족하면 소비 중지
            if (success)
            {
                continue;
            }

            Debug.LogWarning($"{resourceType}이 부족하여 소비를 중지합니다.");
            StopConsume();
        }
    }

    // 즉시 소비 (건설 등 일회성 소비)
    public bool ConsumeOnce()
    {
        return ResourceManager.Instance.ConsumeResource(resourceType, consumeAmount);
    }

    // 소비 중지
    public void StopConsume()
    {
        isConsuming = false;
    }
}