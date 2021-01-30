using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float reloadTime;
    private float timeSinceShot;
    private Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float zVelocity = Input.GetAxis("Vertical");
        float xVelocity = Input.GetAxis("Horizontal");

        rigidBody.velocity = new Vector3(xVelocity * speed, 0, zVelocity * speed);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction=target-transform.position;
            float rotation=Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg;
            transform.rotation=Quaternion.Euler(0, rotation, 0);
        }
        //Shoot
        if (timeSinceShot <= reloadTime)
        {
            timeSinceShot += Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            ShootAlt();
        }
    }

    private void Shoot()
    {
        var bulletObj = Instantiate(bullet, transform.position, transform.rotation);
        bulletObj.GetComponent<Rigidbody>().velocity = transform.forward * 20;
        bulletObj.GetComponent<Projectile>().lifetime = 1.5f;
        timeSinceShot = 0;
    }

    private void ShootAlt()
    {
        Vector3 rotation = transform.rotation.eulerAngles;

        var bulletObj = Instantiate(bullet, transform.position, Quaternion.Euler(rotation));
        bulletObj.GetComponent<Rigidbody>().velocity = bulletObj.transform.forward * 16;
        bulletObj.GetComponent<Projectile>().lifetime = 0.6f;
        bulletObj = Instantiate(bullet, transform.position, Quaternion.Euler(rotation + new Vector3(0, 12, 0)) );
        bulletObj.GetComponent<Rigidbody>().velocity = bulletObj.transform.forward * 16;
        bulletObj.GetComponent<Projectile>().lifetime = 0.6f;
        bulletObj = Instantiate(bullet, transform.position, Quaternion.Euler(rotation - new Vector3(0, 12, 0)));
        bulletObj.GetComponent<Rigidbody>().velocity = bulletObj.transform.forward * 16;
        bulletObj.GetComponent<Projectile>().lifetime = 0.6f;
        timeSinceShot = -reloadTime;
    }
}
