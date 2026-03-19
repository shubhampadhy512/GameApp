
using UnityEngine;

namespace Season2
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Damageable))]
    public class Demogorgan : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float walkSpeed = 3f;
        public float walkStopRate = 0.6f;

        [Header("Attack")]
        public DetectionZone attackZone;

        private Rigidbody2D rb;
        private Animator anim;
        private Damageable damageable;
        private Transform player;

        private EnemyAudioManager2 enemyAudio;

        private bool _hasTarget = false;
        public bool HasTarget
        {
            get { return _hasTarget; }
            private set
            {
                _hasTarget = value;
                anim.SetBool("hasTarget", value);
            }
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            damageable = GetComponent<Damageable>();

            // Get Enemy Audio Manager
            enemyAudio = GetComponent<EnemyAudioManager2>();
        }

        private void Start()
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");

            if (p != null)
                player = p.transform;
        }

        private void Update()
        {
            if (!damageable.IsAlive)
                return;

            if (player == null)
                return;

            Damageable playerDamageable = player.GetComponent<Damageable>();

            if (playerDamageable == null || !playerDamageable.IsAlive)
            {
                HasTarget = false;
                anim.SetBool("isWalking", false);
                return;
            }

            if (attackZone != null)
            {
                HasTarget = attackZone.detectedColliders.Count > 0;

                if (HasTarget && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    anim.SetTrigger("Attack");

                    if (enemyAudio != null)
                        enemyAudio.PlayAttack();
                }
            }

            float directionX = player.position.x - transform.position.x;

            if (directionX > 0.1f && transform.localScale.x < 0)
            {
                Flip();
            }
            else if (directionX < -0.1f && transform.localScale.x > 0)
            {
                Flip();
            }

            anim.SetBool("isWalking", Mathf.Abs(rb.linearVelocity.x) > 0.1f);
        }

        private void FixedUpdate()
        {
            if (!damageable.IsAlive)
                return;

            if (damageable.LockVelocity)
                return;

            if (player == null)
                return;

            Damageable playerDamageable = player.GetComponent<Damageable>();

            if (playerDamageable != null && playerDamageable.IsAlive && !HasTarget)
            {
                float direction = Mathf.Sign(player.position.x - rb.position.x);

                rb.linearVelocity = new Vector2(
                    direction * walkSpeed,
                    rb.linearVelocity.y
                );
            }
            else
            {
                rb.linearVelocity = new Vector2(
                    Mathf.Lerp(rb.linearVelocity.x, 0, walkStopRate),
                    rb.linearVelocity.y
                );
            }
        }

        private void Flip()
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        public void OnHit(int damage, Vector2 knockback)
        {
            if (!damageable.IsAlive)
                return;

            rb.linearVelocity = new Vector2(
                knockback.x,
                rb.linearVelocity.y + knockback.y
            );

            if (enemyAudio != null)
                enemyAudio.PlayHit();
        }
    }
}
