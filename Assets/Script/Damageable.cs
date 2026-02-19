using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // Required for UnityEvent

public class Damageable : MonoBehaviour
{
    Animator animator;

    // Changes: Added UnityEvent to notify other scripts (like PlayerController) when a hit occurs
    // This allows us to pass the damage amount and the knockback vector
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
    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            animator.SetBool("IsAlive", value); // Ensure string matches Animator parameter exactly
            Debug.Log("IsAlive set to " + value);
        }
    }

    // Changes: Property to check/set the "lockVelocity" boolean in the animator
    // This stops the character from moving during the knockback/hit animation
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

    // Changes: Updated Hit function to accept knockback and return a bool
    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            // Trigger the "hit" animation in the Animator
            animator.SetTrigger("hit");

            // Lock the velocity so movement code doesn't override knockback
            LockVelocity = true;

            // Invoke the event so the PlayerController/Enemy script can apply the physical force
            damageableHit?.Invoke(damage, knockback);

            return true;
        }

        return false;
    }
}