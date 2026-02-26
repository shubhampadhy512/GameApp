using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    Animator animator;

    // Event to notify other scripts when hit
    public UnityEvent<int, Vector2> damageableHit;

    [Header("UI Reference")]
    public HealthBar healthBar;

    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get { return _maxHealth; }
        set
        {
            _maxHealth = value;
            if (healthBar != null) healthBar.SetMaxHealth(_maxHealth);
        }
    }

    [SerializeField]
    private int _health = 100;
    public int Health
    {
        get { return _health; }
        set
        {
            _health = Mathf.Clamp(value, 0, MaxHealth);

            if (healthBar != null)
            {
                healthBar.SetHealth(_health);
            }

            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isFacingRight = true;

    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            if (animator != null)
            {
                animator.SetBool("IsAlive", value);

                if (!value)
                {
                    animator.SetTrigger("die");
                    Flip();
                }
            }

            Debug.Log("IsAlive set to " + value);
        }
    }

    public bool LockVelocity
    {
        get { return animator != null && animator.GetBool("lockVelocity"); }
        set { if (animator != null) animator.SetBool("lockVelocity", value); }
    }

    [SerializeField]
    private bool isInvincible = false;

    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("Animator not found on Damageable object!");
        }
    }

    private void Start()
    {
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(MaxHealth);
            healthBar.SetHealth(Health);
        }
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;

            isInvincible = true;
            timeSinceHit = 0;

            if (animator != null)
            {
                animator.SetTrigger("hit");
                LockVelocity = true;
            }

            damageableHit?.Invoke(damage, knockback);

            Debug.Log("Current Health: " + Health);

            return true;
        }

        return false;
    }
}
