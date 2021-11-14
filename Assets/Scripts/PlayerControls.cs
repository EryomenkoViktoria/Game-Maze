using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    Text textHealts;

    [SerializeField]
    private float speed = 8f;

    [SerializeField]
    private float health = 100f;

    internal static int attack = 30;

    private new Rigidbody rigidbody;

    internal static Action<bool> OnPlayerHunter, OnGameOver;

    internal bool iHunter;

    private bool pauseNow;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        GameManager.OnPauseNow += CheckPause;
    }

    private void OnDestroy()
    {
        GameManager.OnPauseNow -= CheckPause;
    }

    private void CheckPause(bool pause)
    {
        if (pause)
            pauseNow = true;
        else
            pauseNow = false;
    }

    private void Update()
    {
        if (!pauseNow)
        {
            rigidbody.velocity = Vector2.zero;

            if (Input.GetKey(KeyCode.A))
                rigidbody.velocity += Vector3.left * speed;
            if (Input.GetKey(KeyCode.D))
                rigidbody.velocity += Vector3.right * speed;
            if (Input.GetKey(KeyCode.W))
                rigidbody.velocity += Vector3.forward * speed;
            if (Input.GetKey(KeyCode.S))
                rigidbody.velocity += Vector3.back * speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
            TakeDamage(EnemyAI.damage);
    }

    public void TakeDamage(int damage)
    {
        if (!iHunter)
        {
            health -= damage;
            if (health <= 0)
            {
                textHealts.text = " ";
                OnGameOver?.Invoke(true);
            }
            textHealts.text = health.ToString();
        }
    }

    internal async void HunterTime()
    {
        iHunter = true;
        OnPlayerHunter?.Invoke(true);

        await Task.Delay(15000);

        iHunter = false;
        OnPlayerHunter?.Invoke(false);
    }
}