using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float laneSpeed;
    public float jumpLength;
    public float jumpHeight;
    public float slideLength;
    public int maxLife = 3;
    public float minSpeed = 10f;
    public float maxSpeed = 20f;
    public float invincibleTime;
    public GameObject model;

    private Animator anim;
    private Rigidbody rb;
    private BoxCollider boxCollider;

    private int currentLane = 1;
    private Vector3 verticalTargetPosition;
    private bool jumping = false;
    private float jumpStart;
    private bool sliding = false;
    private float slideStart;
    private Vector3 boxColliderSize;
    private Vector3 boxColliderCenter;
    private bool canMove;
    private int currentLife;
    private bool invincible = false;
    private UIManager uIManager;
    [HideInInspector]
    public int coins;
    [HideInInspector]
    public float score;
    [HideInInspector]
    public float distance;
    private float previousSpeed;
    private int scoreMultiplier = 1;
    private bool isGameOver = false;
    private bool isGameStart = false;

    private bool isSwipping = false;
    private Vector2 startingTouch;

    //Power Up Time
    [HideInInspector]
    public float remainingPowerTimeMagnet = 0f;
    [HideInInspector]
    public float remainingPowerTimeInvincibility = 0f;
    [HideInInspector]
    public float remainingPowerTimeScoreMultiplier = 0f;
    [HideInInspector]
    public float remainingPowerTimeHighJump = 0f;

    //Sounds
    public AudioSource jumpSound;
    public AudioSource slideSound;
    public AudioSource powerUpCollectedSound;
    public AudioSource coinCollectedSound;
    public AudioSource hitSound;
    public AudioSource deathSound;

    private string lastLC_Action = "";
    private string lastJD_Action = "";

    // Start is called before the first frame update
    void Start()
    {
        isGameStart = true;
        canMove = false;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        boxColliderSize = boxCollider.size;
        boxColliderCenter = boxCollider.center;
        anim.Play("Idle");
        currentLife = maxLife;
        speed = 0;
        uIManager = FindObjectOfType<UIManager>();
        GameManager.gameManager.StartMissions();

        // Chờ 3 giây trước khi bắt đầu chạy
        StartCoroutine(SendStartPacket());
        StartCoroutine(StartRunning());
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            score += Time.deltaTime * speed * scoreMultiplier;
            uIManager.UpdateScoreText((int)score);

            distance += Time.deltaTime * speed;
            uIManager.UpdateDistanceText((int)distance);
        }

        if (GameManager.gameManager.controlByCamera)
        {
            if (ConfirmAction.LR_Action != lastLC_Action)
            {
                lastLC_Action = ConfirmAction.LR_Action;
                if (ConfirmAction.LR_Action == "Left")
                {
                    print("Sang trai 1 lan");
                    ChangeLane(-1);
                }
                else if (ConfirmAction.LR_Action == "Right")
                {
                    ChangeLane(1);
                }
            }

            if (ConfirmAction.JD_Action != lastJD_Action)
            {
                lastJD_Action = ConfirmAction.JD_Action;
                if (ConfirmAction.JD_Action == "Jump")
                {
                    Jump();
                }
                else if (ConfirmAction.JD_Action == "Down")
                {
                    Slide();
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeLane(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeLane(1);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Slide();
            }
        }

        if (Input.touchCount == 1)
        {
            if (isSwipping)
            {
                Vector2 diff = Input.GetTouch(0).position - startingTouch;
                diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);
                if (diff.magnitude > 0.01f)
                {
                    if (Mathf.Abs(diff.y) > Mathf.Abs(diff.x))
                    {
                        if (diff.y < 0)
                        {
                            Slide();
                        }
                        else
                        {
                            Jump();
                        }
                    }
                    else
                    {
                        if (diff.x < 0)
                        {
                            ChangeLane(-1);
                        }
                        else
                        {
                            ChangeLane(1);
                        }
                    }

                    isSwipping = false;
                }
            }

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startingTouch = Input.GetTouch(0).position;
                isSwipping = true;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                isSwipping = false;
            }

        }


        if (jumping)
        {
            float ratio = (transform.position.z - jumpStart) / jumpLength;
            if (ratio >= 1f)
            {
                jumping = false;
                anim.SetBool("Jump", false);
            }
            else
            {
                verticalTargetPosition.y = Mathf.Sin(ratio * Mathf.PI) * jumpHeight;
            }
        }
        else
        {
            verticalTargetPosition.y = Mathf.MoveTowards(verticalTargetPosition.y, 0, 5 * Time.deltaTime);
        }

        if (sliding)
        {
            float ratio = (transform.position.z - slideStart) / slideLength;
            if (ratio >= 1f)
            {
                sliding = false;
                anim.SetBool("Slide", false);
                boxCollider.size = boxColliderSize;
                boxCollider.center = boxColliderCenter;
            }
        }

        Vector3 targetPosition = new Vector3(verticalTargetPosition.x, verticalTargetPosition.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = Vector3.forward * speed;
        }
    }

    IEnumerator SendStartPacket()
    {
        // Gửi gói tin "START" mỗi 0.1 giây trong 3 giây
        for (int i = 0; i < 30; i++)
        {
            Communication.communication.SendData("START");
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator StartRunning()
    {
        yield return new WaitForSeconds(3f);
        canMove = true; // Cho phép di chuyển sau 3 giây
        speed = minSpeed;
        anim.Play("Running");
    }

    void ChangeLane(int direction)
    {
        if (!canMove) return;
        int targetLane = currentLane + direction;
        if (targetLane < 0 || targetLane > 2)
            return;
        currentLane = targetLane;
        verticalTargetPosition = new Vector3((currentLane - 1), 0, 0);
    }

    void Jump()
    {
        if (!canMove) return;
        if (!jumping && !sliding)
        {
            jumpStart = transform.position.z;
            anim.SetFloat("JumpSpeed", speed / jumpLength);
            anim.SetBool("Jump", true);
            jumping = true;

            jumpSound.Play();
        }
    }

    void Slide()
    {
        if (!canMove) return;
        if (!jumping && !sliding)
        {
            slideStart = transform.position.z;
            anim.SetFloat("JumpSpeed", speed / slideLength);
            anim.SetBool("Slide", true);
            Vector3 newCenter = boxCollider.center;
            newCenter.y = newCenter.y / 3;
            boxCollider.center = newCenter;
            Vector3 newSize = boxCollider.size;
            newSize.y = newSize.y / 3;
            boxCollider.size = newSize;
            sliding = true;

            slideSound.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Powerup"))
        {
            powerUpCollectedSound.Play();
        }

        if (other.CompareTag("Coin"))
        {
            coins++;
            uIManager.UpdateCoinText(coins);
            other.transform.parent.gameObject.SetActive(false);

            coinCollectedSound.Play();
        }

        if (invincible)
            return;

        if (other.CompareTag("Obstacles"))
        {
            canMove = false;
            previousSpeed = speed;
            speed = 0;
            currentLife--;
            uIManager.UpdateLives(currentLife);
            anim.SetTrigger("Hit");
            if (currentLife <= 0)
            {
                deathSound.Play();

                GameManager.gameManager.coins += coins;
                if (GameManager.gameManager.highScore < (int)score)
                {
                    GameManager.gameManager.highScore = (int)score;
                }
                GameManager.gameManager.Save();
                isGameStart = false;
                isGameOver = true;
                speed = 0;
                anim.SetBool("Dead", true);
                uIManager.gameOverPanel.SetActive(true);
                Invoke("CallStart", 3f);
            }
            else
            {
                hitSound.Play();

                StartCoroutine(Blinking(invincibleTime));
            }
        }

    }
    IEnumerator Blinking(float time)
    {
        invincible = true;
        float timer = 0;
        float blinkPeriod = 0.1f;
        bool enabled = false;
        yield return new WaitForSeconds(1f);
        canMove = true;
        speed = minSpeed;
        while (timer < time)
        {
            yield return new WaitForSeconds(blinkPeriod);
            timer += blinkPeriod;
            enabled = !enabled;
            model.SetActive(enabled);
        }
        model.SetActive(true);
        invincible = false;
        StartCoroutine(IncreaseSpeedOverTime(previousSpeed)); // Gọi coroutine để tăng tốc độ từ từ
    }

    IEnumerator IncreaseSpeedOverTime(float targetSpeed)
    {
        float startTime = Time.time;
        float elapsedTime = 0f;
        float duration = 10f; // Thời gian để tốc độ tăng từ minSpeed lên tốc độ trước khi va chạm

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            speed = Mathf.Lerp(minSpeed, targetSpeed, t); // Tăng tốc độ từ minSpeed đến targetSpeed theo thời gian
            elapsedTime = Time.time - startTime;
            yield return null;
        }

        speed = targetSpeed; // Đảm bảo tốc độ đạt đúng giá trị cuối cùng
    }

    void CallStart()
    {
        GameManager.gameManager.EndRun();
    }

    public void IncreaseSpeed()
    {
        speed *= 1.1f;
        if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        }
    }

    public int GetCurrentLife()
    {
        return currentLife;
    }

    public void SetCurrentLife(int index)
    {
        currentLife = index;
    }

    public void SetInvincibility(bool value)
    {
        invincible = value;
    }

    public void SetScoreMultiplier(int value)
    {
        scoreMultiplier = value;
    }

    public void SetJumpHeight(int value)
    {
        jumpLength = jumpLength * value / jumpHeight;
        jumpHeight = value;
    }

    public bool GetGameStart()
    {
        return isGameStart;
    }

    public bool GetGameOver()
    {
        return isGameOver;
    }

    public void SetGameOver()
    {
        isGameOver = true;
    }

    public bool CheckCanMove()
    {
        return canMove;
    }
}
