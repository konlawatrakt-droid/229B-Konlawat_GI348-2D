using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Shoot(Vector3 direction)
    {
        GetComponent<Rigidbody>().linearVelocity = direction * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
    
        if (collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }


        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("โดนผู้เล่น!");
        }

        Destroy(gameObject);
    }
}