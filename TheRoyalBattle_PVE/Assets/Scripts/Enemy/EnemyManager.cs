using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] players;

    private Transform targetPlayer;

    public int damage;

    [SerializeField]
    private NavMeshAgent m_Agent;

    [SerializeField]
    private Animator enemyAnimator;

    private int IsRunningParameter = Animator.StringToHash("isRunning");

    public EnemyHealthBar healthBar;

    public Health enemyHealth;

    public GameManager gameManager;

    public PhotonView photonView;

    private bool canAttack;

    private float attackTimer;

    private const float attackTime = 0.75f;

    public LayerMask playerMask;

    public TextMeshProUGUI viewID;

    public void Setup(GameManager _gameManager)
    {
        gameManager = _gameManager;
    }

    void Start()
    {
        if (m_Agent == null)
        {
            m_Agent = GetComponent<NavMeshAgent>();
        }
        enemyHealth.SetHealth(100, 100);
        healthBar.SetHealth(100, 100);
        viewID.text = photonView.ViewID.ToString();
    }

    private Transform GetClosestPlayer()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        Transform trackedPoint = null;

        foreach (GameObject g in players)
        {
            NavMeshPath path = new NavMeshPath();

            if (m_Agent.CalculatePath(g.transform.position, path))
            {
                if (path.status != NavMeshPathStatus.PathComplete)
                {
                    break;
                }

                if (trackedPoint == null)
                {
                    trackedPoint = g.transform;
                }
                else
                {
                    float oldDistance = Vector3.Distance(transform.position, trackedPoint.position);
                    float newDistance = Vector3.Distance(transform.position, g.transform.position);

                    if (newDistance < oldDistance)
                    {
                        trackedPoint = g.transform;
                    }
                }
            }
        }

        return trackedPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.InRoom && !PhotonNetwork.IsMasterClient)
        {
            return;
        }

        targetPlayer = GetClosestPlayer();

        if (targetPlayer != null)
        {
            healthBar.T_HealthBar.LookAt(targetPlayer);

            m_Agent.destination = targetPlayer.position;
        }

        if (!canAttack)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                canAttack = true;
            }
        }

        Collider[] enemies = Physics.OverlapSphere(transform.position, 1.5f, playerMask);

        if (enemies.Length > 0)
        {
            if (!canAttack)
            {
                return;
            }

            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].GetComponent<Health>() && enemies[i].GetComponent<EnemyManager>() == null)
                {
                    enemyAnimator.SetTrigger("isAttacking");
                    Health health = enemies[i].GetComponent<Health>();
                    health.UpdateHealth(-1 * damage);
                    attackTimer = attackTime;
                    canAttack = false;
                }
            }
        }


        enemyAnimator.SetBool(IsRunningParameter, m_Agent.velocity.magnitude > 1);
    }
    public void UpdateHealth(float currentHealth, float deltaChange)
    {
        TakeDamage(currentHealth, deltaChange);
    }

    public void TakeDamage(float currentHealth, float deltaChange)
    {
        healthBar.UpdateHealthBar(currentHealth, deltaChange);

        if (currentHealth <= 0)
        {
            if (!PhotonNetwork.InRoom || (PhotonNetwork.IsMasterClient && photonView.IsMine))
            {
                gameManager.EnemiesAliveScore--;
            }
            Destroy(this.gameObject);
        }
    }
}

public class HealthPoints
{
    public float maxHealth;
    public float currentHealth;
}
