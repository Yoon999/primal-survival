using System.Collections;
using UnityEngine;

public class BuildingBase : MonoBehaviour, IDamageable
{
    [Header("Building Stats")]
    public float maxHealth = 100f;
    protected float currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name}이(가) {amount} 데미지를 입었습니다. 남은 체력: {currentHealth}");

        if (currentHealth <= 0)
        {
            OnDestroyed();
        }
    }

    protected virtual void OnDestroyed()
    {
        Debug.Log($"{gameObject.name}이(가) 파괴되었습니다.");
        BuildingManager.Instance.RemoveBuilding(gameObject);
        Destroy(gameObject);
    }

    public float GetHealthPercent()
    {
        return currentHealth / maxHealth;
    }
}