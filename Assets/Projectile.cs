using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;
    public Vector3 direction;
    public float speed;
    public System.Action destroyed;

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }

        if (destroyed != null)
        {
            destroyed.Invoke();
        }
        
        Destroy(gameObject);
    }
}
