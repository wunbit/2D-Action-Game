using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerEnemy : Enemy
{
    public float timeBetweenSummons;
    public float summonTime;
    public float distancetoEscape;
    public Enemy minion;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    private bool runningAway = false;
    private Vector2 targetPosition;
    public GameObject[] summonPoints;
    private Animator anim;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        GetRandomPosition();
        anim = GetComponent<Animator>();
        summonPoints = GameObject.FindGameObjectsWithTag("SummonerSummonPoint");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player != null)
        {
            float distancetoplayer = Vector2.Distance(transform.position, player.position);
            if (distancetoplayer > distancetoEscape || runningAway)
            {
                if (Vector2.Distance(transform.position, targetPosition) > 0.5f)
                {
                    //Debug.Log("distance is higher");
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                    anim.SetBool("isRunning", true);
                }
                else
                {
                    runningAway = false;
                    anim.SetBool("isRunning", false);
                    if (Time.time >= summonTime)
                    {
                        summonTime = Time.time + timeBetweenSummons;
                        anim.SetTrigger("Summon");
                    }
                }
            }
            if (distancetoplayer <= distancetoEscape && !runningAway && !anim.GetCurrentAnimatorStateInfo(0).IsName("Summon"))
            {
                GetRandomPosition();
                runningAway = true;
            }
        }
    }

    public void Summon()
    {
        if (player != null && summonPoints != null)
        {
            GameObject summonPoint = summonPoints[Random.Range(0, summonPoints.Length)];
            Instantiate(minion, summonPoint.transform.position, summonPoint.transform.rotation);
        }
    }

    public void GetRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);
    }
}
