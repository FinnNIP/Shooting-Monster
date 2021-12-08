using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class SpawnEnemy : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] spawnPoint;
        [SerializeField]
        private GameObject _Enemy;
        [SerializeField]
        private float minSpawnTime = 0.2f;
        [SerializeField]
        private float maxSpawnTime = 1f;
        [SerializeField]
        private float lastSpawnTime = 0f;
        [SerializeField]
        private float spawnTime = 0;

        // Start is called before the first frame update
        void Start()
        {
            spawnPoint = GameObject.FindGameObjectsWithTag("Respawn");
            UpdateSpawnTime();
        }

        private void UpdateSpawnTime()
        {
            lastSpawnTime = Time.time;
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }

        private void Spawn()
        {
            int point = Random.Range(0, spawnPoint.Length);
            Instantiate(_Enemy, spawnPoint[point].transform.position, Quaternion.identity);
            UpdateSpawnTime();
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time >= lastSpawnTime + spawnTime)
            {
                Spawn();
            }
        }
    }
}