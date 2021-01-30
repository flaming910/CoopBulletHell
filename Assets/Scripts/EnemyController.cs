using UnityEngine;

public class EnemyController : MonoBehaviour
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
        Destroy(this.gameObject);
        //TODO: Do some cool death effect
    }

}
