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
        // ㅋㅋ 우진이 왔다감
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
