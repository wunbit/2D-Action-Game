using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    public float attackDistance;
    public float attackTime;
    public float attackSpeed;
    public float chaseSpeed;
    public Enemy[] enemies;
    public int halfhealth;
    public GameObject[] summonPoints;
    private Animator anim;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        summonPoints = GameObject.FindGameObjectsWithTag("summonPoint");
        anim = GetComponent<Animator>();
        halfhealth = health / 2;
    }

    public override void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= halfhealth)
        {
            anim.SetTrigger("Stage2");
        }
        //UpdateHealthUI(health);
        if (health <= 0)
        {
            Destroy(gameObject);
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
            float distancetoplayer = Vector2.Distance(transform.position, player.position);
            //Debug.Log(distancetoplayer);
            if (Vector2.Distance(transform.position, player.position) < attackDistance)
            {
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
