using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Survival.Core;
using Survival.Spell;

namespace Survival.Stats
{
    public class Attributes : MonoBehaviour
    {
       
        [SerializeField] int healthPoints = 2;
        [SerializeField] SpellSO spellEquip;
        [SerializeField] int manaPoints = 10;

      

        public UnityEvent OnDie;
        private GameObject enemySpell;
        private GameManager gameManager;



        private void Awake() {
            gameManager = GameObject.FindObjectOfType<GameManager>();
        }
        private void Start() {
            enemySpell = GameObject.FindGameObjectWithTag("Enemy");
        }
        public int health
        {
            get{return healthPoints;} set{healthPoints = value;}
        }



        public void DecreaseHealth()
        {
            healthPoints -= 1;
            if(healthPoints == 0)
            {
                //Menjalankan Animasi Die 
                //Menggunakan Animation Event untuk memanggil Die() function
                Die();
            }
        }

        public SpellSO GetSpell()
        {
            return spellEquip;
        }

        public SpellSO SetSpell(SpellSO value)
        {
           return spellEquip = value;
        }

        public string GetSpellName()
        {
            if(!spellEquip)
            {
                return "None";
            }
            return spellEquip.name;
        }


        private void Die()
        {
            Destroy(gameObject);
            if(gameObject.tag == "Enemy")
            {
                gameManager.enemyDead += 1;
            }
            OnDie.Invoke();
        }

        public void StealSpell()
        {
            //Get enemy spell
            SpellSO dropSpell = enemySpell.GetComponent<Attributes>().GetSpell();
            //player spell == enemy spell
            spellEquip = dropSpell;
            
        }



    }
}
