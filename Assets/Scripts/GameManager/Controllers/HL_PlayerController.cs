using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HL_PlayerController : MonoBehaviour
{
    [Header("UI:")]
    public UnityEngine.UI.Image HealthImage;
    public UnityEngine.UI.Image ShieldImage;
    public UnityEngine.UI.Text ScoreText;

    [Header("Input settings:")]
    public float speedMultiplier = 5.0f;

    [Space]
    [Header("Character Stats:")]
    public Vector2 movementDirection;
    public float movementSpeed;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator animator;

    public bool DisableMovement = false;
    public bool ControllerActive = false;

    public SpriteRenderer LocalPlayerSprite;

    int iShield = 0;
    int iHealth = 50;
    int Score = 0;
    public int CurrentTaskIndex = 0;
    void Start()
    {
        UpdateHealthUI();
        ControllerActive = true;
    }

    bool bDone = false;
    void Update()
    {
        if (!bDone)
        {
            bDone = true;
        }

        if (DisableMovement || !ControllerActive)
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical",0);
            animator.SetFloat("Speed", 0);
            return;
        }

        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();
        Move();
        Animate();

        if (Input.GetAxis("Horizontal") >= 0.1f || Input.GetAxis("Horizontal") <= -0.1f || Input.GetAxis("Vertical") >= 0.1f || Input.GetAxis("Vertical") <= -0.1f)
        {
            animator.SetFloat("LastMoveX", Input.GetAxis("Horizontal"));
            animator.SetFloat("LastMoveY", Input.GetAxis("Vertical"));
        }


    }

    public void GainScore(int Gain)
    {
        Score += Gain;
        ScoreText.text = "Score : " + Score;

    }
    public bool Heal(int iAmountOfHeal)
    {
        bool bReturn = iHealth < 100;

        iHealth = (Mathf.Clamp(iHealth + iAmountOfHeal, 0, 100));
        UpdateHealthUI();
        return bReturn;
    }

    public bool GainShield(int iAmountOfShieldGain)
    {
        bool bReturn = iShield < 100;

        iShield = (Mathf.Clamp(iShield + iAmountOfShieldGain, 0, 100));
        UpdateHealthUI();
        return bReturn;
    }

    public void UpdateHealthUI()
    {
        HealthImage.fillAmount = (float)iHealth / 100f;
        ShieldImage.fillAmount = (float)iShield / 100f;
    }
    public bool Hurt(int iDamageDealt,bool bHitShield = true)
    {
        if (iDamageDealt <= 0)
            return false;

        int iNewHealth = iHealth;

        if (iShield > 0 && bHitShield)
        {
            int iNewShield = iShield - iDamageDealt;

            if (iNewShield < 0)
            {
                iNewHealth += iNewShield;
                iNewShield = 0;
            }

            iShield = iNewShield;
        }
        else
        {
            iNewHealth = iHealth - iDamageDealt;
        }

        if (iNewHealth < 0)
            iNewHealth = 0;

        iHealth = iNewHealth;

        UpdateHealthUI();

        if (iNewHealth == 0)
        {
            OnDeath();
            return true;
        }

        return false;
    }

    void OnDeath()
    {

    }
    public void SetLocalPlayerState(bool bActive)
    {
        ControllerActive = bActive;
        LocalPlayerSprite.enabled = bActive;
    }
    void Move()
    {
        rb.velocity = movementDirection * movementSpeed * speedMultiplier;
    }

    void Animate()
    {
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
        animator.SetFloat("Speed", movementSpeed);
    }

}
