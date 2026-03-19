using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Season2
{
    public class Damageable : MonoBehaviour
    {
        Animator animator;

        public UnityEvent<int, Vector2> damageableHit;
        public Action onDeath;

        [Header("UI")]
        public HealthBar healthBar;

        [Header("Player Settings")]
        public bool isPlayer = false;

        private GameManager gameManager;

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
                    onDeath?.Invoke();
                }
            }
        }

        [SerializeField] private bool _isAlive = true;
        public bool IsAlive
        {
            get { return _isAlive; }
            set
            {
                _isAlive = value;

                if (animator != null)
                    animator.SetBool("isAlive", value);

                Debug.Log("IsAlive set to " + value);

                // CALL GAME OVER IF PLAYER DIES
                if (!_isAlive && isPlayer && gameManager != null)
                {
                    gameManager.gameOver();
                }
            }
        }

        public bool LockVelocity
        {
            get { return animator.GetBool("lockVelocity"); }
            set { animator.SetBool("lockVelocity", value); }
        }

        [SerializeField] private bool isInvincible = false;
        private float timeSinceHit = 0;
        public float invincibilityTime = 0.25f;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            Health = MaxHealth;
            IsAlive = true;

            if (healthBar != null)
            {
                healthBar.SetMaxHealth(MaxHealth);
                healthBar.SetHealth(Health);
            }

            if (isPlayer)
            {
                gameManager = FindObjectOfType<GameManager>();
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

        public bool Hit(int damage, Vector2 knockback)
        {
            if (IsAlive && !isInvincible)
            {
                Health -= damage;

                isInvincible = true;
                timeSinceHit = 0;

                LockVelocity = true;

                damageableHit?.Invoke(damage, knockback);

                Debug.Log("Current Health: " + Health);

                return true;
            }

            return false;
        }

        // Animation Event calls this
        public void DestroyAfterDeath()
        {
            Destroy(gameObject);
        }
    }
}