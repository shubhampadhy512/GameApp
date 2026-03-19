// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Events;

// public class Damageable : MonoBehaviour
// {
//     Animator animator;

//     public UnityEvent<int, Vector2> damageableHit;

//     [Header("UI Reference")]
//     public HealthBar healthBar;

//     [Header("Character Type")]
//     public bool isPlayer = false;
//     public bool isEnemy = false;

//     private GameManager gameManager;

//     [SerializeField] private int _maxHealth = 100;
//     public int MaxHealth
//     {
//         get { return _maxHealth; }
//         set
//         {
//             _maxHealth = value;
//             if (healthBar != null)
//                 healthBar.SetMaxHealth(_maxHealth);
//         }
//     }

//     [SerializeField] private int _health = 100;
//     public int Health
//     {
//         get { return _health; }
//         set
//         {
//             _health = Mathf.Clamp(value, 0, MaxHealth);

//             if (healthBar != null)
//                 healthBar.SetHealth(_health);

//             if (_health <= 0 && IsAlive)
//             {
//                 IsAlive = false;
//             }
//         }
//     }

//     [SerializeField] private bool _isAlive = true;
//     public bool IsAlive
//     {
//         get { return _isAlive; }
//         set
//         {
//             if (_isAlive == value) return;

//             _isAlive = value;

//             if (animator != null)
//             {
//                 animator.SetBool("IsAlive", value);

//                 if (!value)
//                 {
//                     Debug.Log("DEATH TRIGGER CALLED");
//                     animator.SetTrigger("die");

//                     if (gameManager != null)
//                     {
//                         if (isPlayer)
//                         {
//                             gameManager.gameOver();
//                         }
//                         else if (isEnemy)
//                         {
//                             gameManager.victory();
//                         }
//                     }
//                 }
//             }
//         }
//     }

//     public bool LockVelocity
//     {
//         get { return animator != null && animator.GetBool("lockVelocity"); }
//         set
//         {
//             if (animator != null)
//                 animator.SetBool("lockVelocity", value);
//         }
//     }

//     [SerializeField] private bool isInvincible = false;
//     private float timeSinceHit = 0f;
//     public float invincibilityTime = 0.25f;

//     private void Awake()
//     {
//         animator = GetComponent<Animator>();

//         if (animator == null)
//         {
//             Debug.LogWarning("Animator not found on Damageable object!");
//         }
//     }

//     private void Start()
//     {
//         if (healthBar != null)
//         {
//             healthBar.SetMaxHealth(MaxHealth);
//             healthBar.SetHealth(Health);
//         }

//         // Find GameManager if player or enemy
//         if (isPlayer || isEnemy)
//         {
//             gameManager = FindObjectOfType<GameManager>();
//         }
//     }

//     private void Update()
//     {
//         if (isInvincible)
//         {
//             timeSinceHit += Time.deltaTime;

//             if (timeSinceHit > invincibilityTime)
//             {
//                 isInvincible = false;
//                 timeSinceHit = 0f;
//             }
//         }
//     }

//     public bool Hit(int damage, Vector2 knockback)
//     {
//         if (!IsAlive || isInvincible)
//             return false;

//         Health -= damage;

//         isInvincible = true;
//         timeSinceHit = 0f;

//         if (animator != null)
//         {
//             animator.SetTrigger("hit");
//             LockVelocity = true;
//         }

//         damageableHit?.Invoke(damage, knockback);

//         Debug.Log("Current Health: " + Health);

//         return true;
//     }

//     // Animation event can call this after death animation
//     public void DestroyAfterDeath()
//     {
//         Destroy(gameObject);
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    Animator animator;

    public UnityEvent<int, Vector2> damageableHit;

    [Header("UI Reference")]
    public HealthBar healthBar;

    [Header("Character Type")]
    public bool isPlayer = false;
    public bool isEnemy = false;

    private GameManager gameManager;

    // AUDIO
    private PlayerAudioManager playerAudio;
    private EnemyAudioManager enemyAudio;

    [SerializeField] private int _maxHealth = 100;
    public int MaxHealth
    {
        get { return _maxHealth; }
        set
        {
            _maxHealth = value;
            if (healthBar != null)
                healthBar.SetMaxHealth(_maxHealth);
        }
    }

    [SerializeField] private int _health = 100;
    public int Health
    {
        get { return _health; }
        set
        {
            _health = Mathf.Clamp(value, 0, MaxHealth);

            if (healthBar != null)
                healthBar.SetHealth(_health);

            if (_health <= 0 && IsAlive)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField] private bool _isAlive = true;
    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            if (_isAlive == value) return;

            _isAlive = value;

            if (animator != null)
            {
                animator.SetBool("IsAlive", value);

                if (!value)
                {
                    Debug.Log("DEATH TRIGGER CALLED");
                    animator.SetTrigger("die");

                    // PLAY DEATH SOUND
                    if (playerAudio != null)
                        playerAudio.PlayDeath();

                    if (enemyAudio != null)
                        enemyAudio.PlayDeath();

                    if (gameManager != null)
                    {
                        if (isPlayer)
                        {
                            gameManager.gameOver();
                        }
                        else if (isEnemy)
                        {
                            gameManager.victory();
                        }
                    }
                }
            }
        }
    }

    public bool LockVelocity
    {
        get { return animator != null && animator.GetBool("lockVelocity"); }
        set
        {
            if (animator != null)
                animator.SetBool("lockVelocity", value);
        }
    }

    [SerializeField] private bool isInvincible = false;
    private float timeSinceHit = 0f;
    public float invincibilityTime = 0.25f;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // GET AUDIO MANAGERS
        playerAudio = GetComponent<PlayerAudioManager>();
        enemyAudio = GetComponent<EnemyAudioManager>();

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

        if (isPlayer || isEnemy)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    }

    private void Update()
    {
        if (isInvincible)
        {
            timeSinceHit += Time.deltaTime;

            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0f;
            }
        }
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (!IsAlive || isInvincible)
            return false;

        Health -= damage;

        // PLAY HIT SOUND
        if (playerAudio != null)
            playerAudio.PlayHit();

        if (enemyAudio != null)
            enemyAudio.PlayHit();

        isInvincible = true;
        timeSinceHit = 0f;

        if (animator != null)
        {
            animator.SetTrigger("hit");
            LockVelocity = true;
        }

        damageableHit?.Invoke(damage, knockback);

        Debug.Log("Current Health: " + Health);

        return true;
    }

    // Animation event can call this after death animation
    public void DestroyAfterDeath()
    {
        Destroy(gameObject);
    }
}