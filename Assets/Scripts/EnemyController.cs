using Photon.Pun;
using UnityEngine;

public class EnemyController : MonoBehaviourPun
{
    [SerializeField] private float health;

    public void TakeDamage(float damage)
    {
        if (damage >= health)
        {
            Die();
        }
        else
        {
            health -= damage;
        }
    }

    private void Die()
    {
        //TODO: Fix Non-Master clients so they can kill things
        PhotonNetwork.Destroy(this.gameObject);
        //TODO: Do some cool death effect
    }

}
