using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public float timeBetweenAttacks;
    public int damage;
    public int pickupChance;
    public int healthChance;
    public GameObject[] pickups;
    public GameObject[] healthPickup;
    public GameObject deathEffect;
    public GameObject splat;

    [HideInInspector]
    public Transform player;
    
    public virtual void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            int randomNumber = Random.Range(0,101);
            int randomHealthNumber = Random.Range(0, 101);
            int randomNumberdecider = Random.Range(0,101)+ pickupChance;
            int randomHealthNumberdecider = Random.Range(0, 101) + healthChance;
            //Debug.Log(randomNumber);
            //Debug.Log(randomHealthNumber);
            //Debug.Log(randomNumberdecider);
            //Debug.Log(randomHealthNumberdecider);
            if (randomNumberdecider >= randomHealthNumberdecider)
            {
                //Debug.Log("pickup is bigger than health");
                if (randomNumber < pickupChance)
                {
                    //Debug.Log("it should drop an item");
                    GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
                    Instantiate(randomPickup, transform.position, transform.rotation);
                }
            }
            if (randomNumberdecider < randomHealthNumberdecider)
            {
                //Debug.Log("pickup is smaller than health");
                if (randomHealthNumber < healthChance)
                {
                    //Debug.Log("health should drop");
                    GameObject randomHealth = healthPickup[Random.Range(0, pickups.Length)];
                    Instantiate(randomHealth, transform.position, transform.rotation);
                }
            }
            Instantiate(splat, transform.position, transform.rotation);
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().TakeDamage(damage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
