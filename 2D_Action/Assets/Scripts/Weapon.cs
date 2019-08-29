using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    CameraController cam;
    public GameObject projectile;
    public Transform shotPoint;
    Vector2 direction;
    public float timeBetweenShots;
    private float shotTime;
    public float shakeMag = 1.5f;
    public float shakeLenght = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

        if (Input.GetMouseButton(0))
        {
            if (Time.time >= shotTime)
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                shotTime = Time.time + timeBetweenShots;
                direction.x = transform.position.x - 0.5f;
                direction.y = transform.position.y - 0.5f;
                cam.Shake((transform.position - shotPoint.position).normalized, shakeMag, shakeLenght);
            }
        }
    }
}
