using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public Vector2 targetPosition;
    public float lifeTime;
    public int damage = 1;
    public Player playerScript;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPosition = playerScript.transform.position;
        Invoke("DestroyProjectile", lifeTime);
    }
    void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            playerScript.TakeDamage(damage);
            DestroyProjectile();
        }
    }
}
