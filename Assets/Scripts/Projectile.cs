using Photon.Pun;
using UnityEngine;

public class Projectile : MonoBehaviourPun
{
    public float lifetime;
    public float damage;

    // Update is called once per frame
    void Update()
    {
        if (lifetime > 0)
        {
            lifetime -= Time.deltaTime;
        }
        else
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().TakeDamage(damage);
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

}
