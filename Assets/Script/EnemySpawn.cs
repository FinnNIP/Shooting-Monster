using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class EnemySpawn : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] SpawnPoint;

        [SerializeField]
        private GameObject Enemy;

        [SerializeField]
        private float minSpawnTime = 0.2f;

        [SerializeField]
        private float maxSpawnTime = 1f;

        [SerializeField]
        private float lastSpawnTime = 0f;

        [SerializeField]
        private float spawnTime = 0f;

        //[SerializeField]
        //public GameObject theEnemy;

        //[SerializeField]
        //public int xPos;

        //[SerializeField]
        //public int yPos;

        //[SerializeField]
        //public int zPos;

        [SerializeField]
        public int enemyCount;


        // Start is called before the first frame update
        void Start()
        {
            SpawnPoint = GameObject.FindGameObjectsWithTag("Respawn");
            UpdateSpawnTime();
        }
        private void UpdateSpawnTime()
        {
            lastSpawnTime = Time.time;
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }

        private void Spawn()
        {
            int point = Random.Range(0, SpawnPoint.Length);
            Instantiate(Enemy, SpawnPoint[point].transform.position, Quaternion.identity);
            UpdateSpawnTime();

            //StartCoroutine(EnemyDrop());
            
        }
        //IEnumerator EnemyDrop()
        //{
        //    while (enemyCount < 10)
        //    {
        //        xPos = Random.Range(1, 30);
                
        //        zPos = Random.Range(1, 30);
        //        Instantiate(theEnemy, new Vector3(xPos, yPos, zPos), Quaternion.identity);
        //        yield return new WaitForSeconds(0.1f);
        //        enemyCount += 1;
        //    }
        //}

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