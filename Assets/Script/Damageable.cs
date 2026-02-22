using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    Animator animator;

    // Event to notify other scripts when hit
    public UnityEvent<int, Vector2> damageableHit;

    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    [SerializeField]
    private int _health = 100;
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;

            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    // 👇 Facing direction tracker
    [SerializeField]
    private bool isFacingRight = true;

    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            animator.SetBool("IsAlive", value);

            // 👇 Only flip when character dies
            if (!value)
            {
                Flip();
            }

            Debug.Log("IsAlive set to " + value);
        }
    }

    // Controls movement locking during hit
    public bool LockVelocity
    {
        get { return animator.GetBool("lockVelocity"); }
        set { animator.SetBool("lockVelocity", value); }
    }

    [SerializeField]
    private bool isInvincible = false;

    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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

    // 👇 Flip function (only used on death)
    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Hit function
    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;

            isInvincible = true;
            timeSinceHit = 0;

            animator.SetTrigger("hit");
            LockVelocity = true;

            damageableHit?.Invoke(damage, knockback);

            Debug.Log("Current Health: " + Health);

            return true;
        }

        return false;
    }
}