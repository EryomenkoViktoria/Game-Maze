using UnityEngine;

public class EnemyAI : EnemyControler
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && playerHunter)
            TakeDamage(PlayerControls.attack);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Destroy(gameObject);
    }
}
