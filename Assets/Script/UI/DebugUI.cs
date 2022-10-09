using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Survival.UI
{
    public class DebugUI : MonoBehaviour
    {
        [SerializeField] Text deadCount;
        private string enemyDeadcount;


        private void Update() 
        {
            deadCount.text = EnemyDeadcount;
        }

        public string EnemyDeadcount
        {
            get{return enemyDeadcount;} set{enemyDeadcount = value;}
        }
    }
}
