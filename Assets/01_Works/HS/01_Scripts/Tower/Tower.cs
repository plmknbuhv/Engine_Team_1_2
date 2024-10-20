using UnityEngine;

public class Tower : MonoBehaviour
{
    public float detectRadius;
    public ContactFilter2D contactFilter;
    
    public Collider2D[] _colliders;

    private void Awake()
    {
        _colliders = new Collider2D[20];
    }

    private void Update()
    {
        SearchTarget();
    }

    public Collider2D SearchTarget()
    {
        int count = Physics2D.OverlapCircle(transform.position, detectRadius, contactFilter, _colliders);

        if (count < 1) return null;

        float shortDistance = Vector3.Distance(transform.position, _colliders[0].transform.position);
        Collider2D nearTarget = _colliders[0];

        for (int i = 1; i < count; i++)
        {
            var distance = Vector3.Distance(transform.position, _colliders[i].transform.position);
            if (shortDistance > distance)
            {
                shortDistance = distance;
                nearTarget = _colliders[i];
            }  
        }

        return nearTarget;
    }
    
#if UNITY_EDITOR
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
        Gizmos.color = Color.white;
    }
#endif
}
