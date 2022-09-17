using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : MonoBehaviourPunCallbacks
{
    private int enemiesAlive = 0;

    public int EnemiesAliveScore
    {
        get
        {
            return enemiesAlive;
        }
        set
        {
            enemiesAlive = value;

            if (PhotonNetwork.InRoom)
            {
                Hashtable hash = new Hashtable();
                hash.Add("enemies", enemiesAlive);
                PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
            }
            else
            {
                enemiesAliveUI.DisplayEnemies(enemiesAlive);
            }
        }
    }

    public EnemiesAlive enemiesAliveUI;

    public int round = 0;

    public CurrentRoundUI currentRoundUI;

    [SerializeField]
    private List<Transform> spawnPoints = new List<Transform>();

    public EnemyManager[] enemy = new EnemyManager[1];

    private bool IsRoundEnemyGenerating = true;

    public PhotonView m_PhotonView;

    private void Start()
    {
        spawnPoints.Clear();

        foreach (GizmoPoint gp in FindObjectsOfType<GizmoPoint>())
        {
            spawnPoints.Add(gp.transform);
        }

        IsRoundEnemyGenerating = false;
    }

    private void DisplayNextRound(int round)
    {
        currentRoundUI.Display(round);
    }
    private void Update()
    {
        if (!PhotonNetwork.InRoom || (PhotonNetwork.IsMasterClient && m_PhotonView.IsMine))
        {
            if (round <=0 && PhotonNetwork.CurrentRoom.PlayerCount < 2)
            {
                return;
            }
            if (enemiesAlive == 0 && !IsRoundEnemyGenerating)
            {
                round++;
                NextWave(round);
                if (PhotonNetwork.InRoom)
                {
                    Hashtable hash = new Hashtable();
                    hash.Add("currentRound", round);
                    PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
                }
                else
                {
                    DisplayNextRound(round);

                }
            }
        }
    }
    private void NextWave(int nextRound)
    {
        IsRoundEnemyGenerating = true;

        for (int i = 0; i < nextRound; i++)
        {
            Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Count)];

            int x = UnityEngine.Random.Range(0, enemy.Length);
            EnemyManager temp;

            if (PhotonNetwork.InRoom)
            {
                temp = (PhotonNetwork.Instantiate("Zombie", spawn.position, Quaternion.identity)).GetComponent<EnemyManager>();
            }
            else
            {
                temp = (Instantiate(Resources.Load("Zombie"), spawn.position, Quaternion.identity) as GameObject).GetComponent<EnemyManager>();
            }

            temp.Setup(this);

            EnemiesAliveScore++;
        }

        IsRoundEnemyGenerating = false;
    }

    private EnemyManager GetEnemyToSpawn(int index)
    {
        return enemy[index];
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (m_PhotonView.IsMine)
        {
            if (changedProps["currentRound"] != null)
            {
                round = (int)changedProps["currentRound"];

                DisplayNextRound(round);
            }
            if (changedProps["enemies"] != null)
            {
                enemiesAliveUI.DisplayEnemies((int)changedProps["enemies"]);
            }
        }
    }
}
