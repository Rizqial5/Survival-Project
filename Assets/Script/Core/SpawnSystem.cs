using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival.Core
{
        public class SpawnSystem : MonoBehaviour
    {

        [SerializeField] Transform[] spawnPoints;
        [SerializeField] GameObject spawnGameObject;
        [SerializeField] float spawnCooldown = 0f;
        [SerializeField] float spawnTime = 4f;

        GameManager gameManager;
        
        private void Awake() 
        {
            gameManager = GameObject.FindObjectOfType<GameManager>();    
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

            

            SpawnObject();

            spawnCooldown += Time.deltaTime;
        }

        private void SpawnObject()
        {
            if(!CanSpawn()) return;
            if (spawnCooldown < spawnTime) return;
            
            spawnCooldown = 0;
            SpawnMechanic();
        }

        private void SpawnMechanic()
        {
            int len = spawnPoints.Length;

            int randomNumber = Random.Range(0,len);

            //Persyaratan untuk instantiate enemy khusus
            //Sesuai dengan ronde

            Instantiate(spawnGameObject, spawnPoints[randomNumber]);

        }

        private bool CanSpawn()
        {
            if(gameManager.CountEnemy() < 5)
            {
                return true;
            }
            return false;   
        }
    }

}