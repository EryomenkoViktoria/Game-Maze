using UnityEngine;

public class Bonus : MonoBehaviour
{
    PlayerControls playerControls;

    private void Start()
    {
        playerControls = FindObjectOfType<PlayerControls>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerControls.HunterTime();
            Destroy(gameObject);
        }
    }
}
