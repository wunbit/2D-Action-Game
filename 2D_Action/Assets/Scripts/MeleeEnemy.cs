using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float stopDistance;
    public float attackTime;
    public float attackSpeed;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            //float distanceCheck = Vector2.Distance(transform.position, player.position);
            //Debug.Log(distanceCheck);
            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                if (Time.time >= attackTime)
                {
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
    }

//test make it so that instead of the function, it does damage on Contact.
    IEnumerator Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage);
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
