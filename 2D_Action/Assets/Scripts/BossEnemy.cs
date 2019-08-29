using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : Enemy
{
    public float attackDistance;
    public float attackTime;
    public float attackSpeed;
    public float chaseSpeed;
    public Enemy[] enemies;
    private int halfhealth;
    private GameObject[] summonPoints;
    private GameObject explosionPoint;
    private Animator anim;
    private Slider healthBar;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        summonPoints = GameObject.FindGameObjectsWithTag("summonPoint");
        explosionPoint = GameObject.FindGameObjectWithTag("BossExplosionPoint");
        anim = GetComponent<Animator>();
        halfhealth = health / 2;
        healthBar = FindObjectOfType<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public override void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthBar.value = health;
        if (health <= halfhealth)
        {
            anim.SetTrigger("Stage2");
        }
        //UpdateHealthUI(health);
        if (health <= 0)
        {
            Instantiate(deathEffect, explosionPoint.transform.position, explosionPoint.transform.rotation);
            Instantiate(splat, explosionPoint.transform.position, explosionPoint.transform.rotation);
            Destroy(gameObject);
            healthBar.gameObject.SetActive(false);
        }
        Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
        GameObject summonPoint = summonPoints[Random.Range(0, summonPoints.Length)];
        Instantiate(randomEnemy, summonPoint.transform.position, summonPoint.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            //float distancetoplayer = Vector2.Distance(transform.position, player.position);
            //Debug.Log(distancetoplayer);
            if (Vector2.Distance(transform.position, player.position) < attackDistance)
            {
                Debug.Log("Player is Close to attack");
                if (Time.time >= attackTime)
                {
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
    }

    IEnumerator Attack()
    {
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;
        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent,2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }
}
