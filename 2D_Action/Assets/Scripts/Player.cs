using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed;
    public GameManager manager;
    private Rigidbody2D rb2d;
    public float health;
    private Vector2 moveAmount;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speed = manager.playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;
        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate() 
    {
        rb2d.MovePosition(rb2d.position + moveAmount * Time.fixedDeltaTime);
    }
}
