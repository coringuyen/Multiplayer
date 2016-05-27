//using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{
    public RectTransform healthBar;
    private const int maxHealth = 100;
    public bool destroyOnDeath;
    private NetworkStartPosition[] spawnPoints;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    void Start()
    {
        if (isLocalPlayer)
        { spawnPoints = FindObjectsOfType<NetworkStartPosition>(); }
    }

    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;

        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            if (destroyOnDeath)
                Destroy(gameObject);
            else
            {
                currentHealth = maxHealth;
                RpcRespawn();
            }

                print("Dead!");
        }

    }

    void OnChangeHealth(int currentHealth)
    {

        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        Vector3 spawnPoint = Vector3.zero;
        if(isLocalPlayer)
        {
            if(spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }

            transform.position = spawnPoint;
        }
    }
}
