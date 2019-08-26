using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int damage = 1;
    public GameObject explosion;
    public GameObject fireballSound;
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, lifeTime);
        Invoke("DestroyProjectile", lifeTime);
        Instantiate(fireballSound, transform.position, transform.rotation);
    }

    void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy" || col.tag == "Boss")
        {
            col.GetComponent<Enemy>().TakeDamage(damage);
            DestroyProjectile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
