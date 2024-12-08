using System.Collections;
using UnityEngine;

public class BS_Player_Controller : MonoBehaviour
{
    public Rigidbody rb;
    public float speedMove = 5f;
    public Animator animator;
    private bool gameOver;
    private Coroutine powerUpCountdown;

    PowerUpType currentPowerUp = PowerUpType.None;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        PlayerInput();
        GameOverCheck();
    }

    public bool IsGameOver
    {
        get { return gameOver; }
        set { gameOver = value; }
    }

    void PlayerInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, 0, vertical).normalized;

        if (move.magnitude >= 0.05f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speedMove);
            rb.MovePosition(transform.position + move * speedMove * Time.deltaTime);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void GameOverCheck()
    {
        CheckPlayerPosition();
        if (!gameOver)
        {
            // PowerUpIndicatorPosition();
            FireRockets();
        }
        else
        {
          //  GameObject.Find("UIManager").GetComponent<UIManager>().GameOver();
        }
    }

    void CheckPlayerPosition()
    {
        Die();
        if (transform.position.y < -5)
        {
            gameOver = true;
            Destroy(gameObject);
        }
    }
    public void Die()
    {
        if (DestroythePartical.instance != null)
        {

            if (DestroythePartical.instance.playerDie == true)
            {
                gameOver = true;
                animator.SetBool("Death", true);
            }
        }
      
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            currentPowerUp = other.gameObject.GetComponent<PowerUps>().powerUpType;
            // powerUpIndicator.gameObject.SetActive(true);
            // powerUpIndicator.gameObject.GetComponent<Renderer>().material.color = PowerUpColor(currentPowerUp);

            Destroy(other.gameObject);
            if (powerUpCountdown != null)
            {
                StopCoroutine(powerUpCountdown);
            }
            powerUpCountdown = StartCoroutine(PowerUpCountdownRoutine());
        }
    }

    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        currentPowerUp = PowerUpType.None;
        // powerUpIndicator.SetActive(false);
    }

    void FireRockets()
    {
        if (currentPowerUp == PowerUpType.RocketBarrage && Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets();
        }
    }

    void LaunchRockets()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            // GameObject tmpRocket = Instantiate(rocketPrefab, transform.position, Quaternion.identity);
            // tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
        }
    }

    Color PowerUpColor(string currentOne)
    {
        if (currentOne == "ZapCannon")
        {
            return Color.yellow;
        }
        else if (currentOne == "RocketBarrage")
        {
            return Color.red;
        }
        else
        {
            return Color.gray;
        }
    }
}
