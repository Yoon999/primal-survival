using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 20f;
    public bool isHoming = false;

    private Transform target;

    public void Initialize(Transform targetTransform)
    {
        target = targetTransform;
    }

    private void Update()
    {
        if (!target)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;

        if (isHoming)
        {
            transform.forward = Vector3.Lerp(transform.forward, direction, Time.deltaTime * 10f);
        }

        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform != target)
        {
            return;
        }
        
        IDamageable damageable = other.GetComponent<IDamageable>();
        damageable?.TakeDamage(damage);

        Destroy(gameObject);
    }
}