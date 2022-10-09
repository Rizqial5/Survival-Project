using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survival.UI;

namespace Survival.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] GameObject enemyPrefab;
        [SerializeField] Transform spawnTransform;
        


        GameObject[] enemy;
        private int enemyDeadcount;
        private DebugUI debugUI;

        private void Awake() 
        {
            debugUI = GameObject.FindObjectOfType<DebugUI>();
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            // Enemy Respawn Controller
            if(Input.GetKeyDown(KeyCode.R))
            {
                Instantiate(enemyPrefab,spawnTransform.position,spawnTransform.rotation);
            }

            debugUI.EnemyDeadcount = enemyDead.ToString();

            // print($"Jumlah enemy {CountEnemy()}");
        }

        public int enemyDead
        {
            get{return enemyDeadcount;} set{enemyDeadcount = value;}
        }

        public int CountEnemy()
        {
            
            enemy = GameObject.FindGameObjectsWithTag("Enemy");
            
            return enemy.Length;
        }
    }
}
