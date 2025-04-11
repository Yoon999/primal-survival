using UnityEngine;

public class BuildingPlacementSystem : MonoBehaviour
{
    public GameObject buildingPrefab; // 배치할 건물 프리팹
    public LayerMask groundLayer; // 건물을 놓을 수 있는 땅 레이어
    public Material validPlacementMaterial; // 배치 가능 시 색상
    public Material invalidPlacementMaterial; // 배치 불가 시 색상

    private GameObject previewObject; // 미리보기 오브젝트
    private Renderer previewRenderer; // 미리보기 오브젝트의 렌더러
    private bool canPlace = false; // 현재 배치 가능 여부

    private void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
        CreatePreviewObject();
    }

    private void Update()
    {
        MovePreview();
        HandlePlacement();
    }

    // 미리보기 오브젝트 생성
    private void CreatePreviewObject()
    {
        previewObject = Instantiate(buildingPrefab);
        previewRenderer = previewObject.GetComponentInChildren<Renderer>();
        var coll = previewObject.GetComponent<Collider>();
        if (coll)
        {
            coll.enabled = false; // 충돌 방지
        }
    }

    // 마우스를 따라 미리보기 이동
    private void MovePreview()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            previewObject.transform.position = hit.point;
            canPlace = CheckPlacement(hit.point);
            previewRenderer.material = canPlace ? validPlacementMaterial : invalidPlacementMaterial;
        }
    }

    // 건물 배치
    private void HandlePlacement()
    {
        if (!Input.GetMouseButtonDown(0) || !canPlace)
        {
            return;
        }
        
        Vector3 placementPosition = previewObject.transform.position;
        BuildingManager.Instance.TryBuild(buildingPrefab, placementPosition, ResourceManager.ResourceType.Materials, 50);
    }

    // 배치 가능 여부 체크
    private bool CheckPlacement(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 1.5f); // 반경 1.5m 체크
        foreach (Collider col in colliders)
        {
            if (col.gameObject.CompareTag("Building"))
                return false;
        }
        return true;
    }
}
