using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private AudioSource source;
    public float speed;
    private Rigidbody2D rb2d;
    public int health;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Transform weaponPoint;
    public GameObject lifeUpEffect;
    private Vector2 moveAmount;
    public Animator hurtFlash;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void ChangeWeapon(Weapon weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, weaponPoint.position, weaponPoint.rotation, weaponPoint);
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
        source.Play();
        hurtFlash.SetTrigger("HurtPanelFlash");
        UpdateHealthUI(health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void HealPlayer(int healAmount)
    {
        Instantiate(lifeUpEffect, transform.position, transform.rotation);
        if ((health + healAmount) > 5)
        {
            health = 5;
        }
        else
        {
            health += healAmount;
        }
        UpdateHealthUI(health);
    }

    void FixedUpdate() 
    {
        rb2d.MovePosition(rb2d.position + moveAmount * Time.fixedDeltaTime);
    }
}
