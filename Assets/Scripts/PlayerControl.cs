using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private GameObject bullet;
    [SerializeField] private float reloadTime;
    [SerializeField] private float dashCooldown;
    private float timeSinceShot;
    private float timeSinceDash;
    private Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        timeSinceShot = reloadTime;
        timeSinceDash = dashCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        float zVelocity = Input.GetAxis("Vertical");
        float xVelocity = Input.GetAxis("Horizontal");
        var rbVelocity = rigidBody.velocity;
        var targetVelocity = new Vector3(xVelocity, 0, zVelocity);
        targetVelocity = Vector3.ClampMagnitude(targetVelocity, 1);
        targetVelocity *= speed;

        var velocityChange = (targetVelocity - rbVelocity);

        rigidBody.AddForce(velocityChange, ForceMode.Force);

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

        //Dash
        if (timeSinceDash <= dashCooldown)
        {
            timeSinceDash += Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
    }

    private void Shoot()
    {
        var bulletObj = Instantiate(bullet, transform.position, transform.rotation);
        bulletObj.GetComponent<Rigidbody>().velocity = transform.forward * 20;
        bulletObj.GetComponent<Projectile>().lifetime = 1.5f;
        bulletObj.GetComponent<Projectile>().damage = 1f;
        timeSinceShot = 0;
    }

    private void ShootAlt()
    {
        Vector3 rotation = transform.rotation.eulerAngles;

        var bulletObj = Instantiate(bullet, transform.position, Quaternion.Euler(rotation));
        bulletObj.GetComponent<Rigidbody>().velocity = bulletObj.transform.forward * 16;
        bulletObj.GetComponent<Projectile>().lifetime = 0.6f;
        bulletObj.GetComponent<Projectile>().damage = 1f;
        bulletObj = Instantiate(bullet, transform.position, Quaternion.Euler(rotation + new Vector3(0, 12, 0)) );
        bulletObj.GetComponent<Rigidbody>().velocity = bulletObj.transform.forward * 16;
        bulletObj.GetComponent<Projectile>().lifetime = 0.6f;
        bulletObj.GetComponent<Projectile>().damage = 1f;
        bulletObj = Instantiate(bullet, transform.position, Quaternion.Euler(rotation - new Vector3(0, 12, 0)));
        bulletObj.GetComponent<Rigidbody>().velocity = bulletObj.transform.forward * 16;
        bulletObj.GetComponent<Projectile>().lifetime = 0.6f;
        bulletObj.GetComponent<Projectile>().damage = 1f;
        timeSinceShot = -reloadTime;
    }

    private void Dash()
    {
        rigidBody.AddForce(transform.forward * 25, ForceMode.Impulse);
    }
}
