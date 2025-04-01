/*
 * 1. 자동 채집 (`AutoCollect`)
 * - `gatherInterval` (채집 간격)마다 `gatherAmount`만큼 자동으로 자원 추가.
 * - 예) 3초마다 식량 5개 증가.
 *
 * 2. 수동 채집 (`CollectManually`)
 * - 버튼 클릭 같은 인터랙션을 통해 즉시 자원 획득 가능.
 *
 * 3. 채집 중지 (`StopCollecting`)
 * - 건물이 파괴되거나 일시 정지가 필요할 때 사용 가능.
 */

using System.Collections;
using UnityEngine;

public class ResourceCollector : MonoBehaviour
{
    public ResourceManager.ResourceType resourceType;
    public int gatherAmount = 5;
    public float gatherInterval = 3.0f;

    private bool isCollecting = false;

    private void Start()
    {
        StartCoroutine(AutoCollect());
    }

    // 자동 채집 코루틴
    private IEnumerator AutoCollect()
    {
        isCollecting = true;
        while (isCollecting)
        {
            yield return new WaitForSeconds(gatherInterval);
            ResourceManager.Instance.AddResource(resourceType, gatherAmount);
        }
    }

    // 수동 채집
    public void CollectManually()
    {
        ResourceManager.Instance.AddResource(resourceType, gatherAmount);
    }

    // 채집 중지
    public void StopCollecting()
    {
        isCollecting = false;
    }
}