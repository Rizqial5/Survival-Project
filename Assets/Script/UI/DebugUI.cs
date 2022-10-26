using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Survival.UI
{
    public class DebugUI : MonoBehaviour
    {
        [SerializeField] Text deadCount;
        [SerializeField] Text textEnemyInStage;
        [SerializeField] Text textTotalEnemy;
        private string enemyDeadcount;
        private string enemyInStage;
        private string totalEnemy;


        private void Update() 
        {
            deadCount.text = EnemyDeadcount;
            textEnemyInStage.text = EnemyInStage;
            textTotalEnemy.text = TotalEnemy;
        }

        public string EnemyDeadcount
        {
            get{return enemyDeadcount;} set{enemyDeadcount = value;}
        }
        public string EnemyInStage
        {
            get{return enemyInStage;} set{enemyInStage = value;}
        }
        public string TotalEnemy
        {
            get{return totalEnemy;} set{totalEnemy = value;}
        }

    }
}
