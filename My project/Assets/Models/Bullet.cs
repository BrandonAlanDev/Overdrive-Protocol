using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 5f;
    public int damage = 5;

    private Vector3 direction;

    public void Init(Vector3 dir)
    {
        direction = dir.normalized;
        direction.y = 0f;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>()?.TakeDamage(damage);
            Debug.Log("Enemigo disparado");
            Destroy(gameObject);
        }
    }
}
