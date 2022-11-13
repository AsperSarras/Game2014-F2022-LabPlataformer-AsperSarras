using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaneController : MonoBehaviour
{
    public Transform playerSpawnPoint;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            ReSpawn(other.gameObject);
        }
    }

    public void ReSpawn(GameObject player)
    {
        player.transform.position = playerSpawnPoint.position;
    }
}
