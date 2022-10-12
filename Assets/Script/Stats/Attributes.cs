using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Survival.Core;
using Survival.Spell;
using Survival.CharType;

namespace Survival.Stats
{
    public class Attributes : MonoBehaviour
    {
       
        // [SerializeField] int healthPoints = 2;
        // [SerializeField] SpellSO spellEquip;
        // [SerializeField] int manaPoints = 10;
        [SerializeField] CharSO charSO;
        [SerializeField] CharCategories CharCategories;
        [SerializeField] SpellSO[] spellInventory;

        private int healthPoints;
        private int manaPoints;
        SpellSO spellEquip;

      

        public UnityEvent OnDie;
        private GameObject enemySpell;
        private GameManager gameManager;



        private void Awake() {
            gameManager = GameObject.FindObjectOfType<GameManager>();
            healthPoints = (int) charSO.GetStat(Stat.HealthPoint, CharCategories);
            manaPoints = (int) charSO.GetStat(Stat.ManaPoint, CharCategories);
            spellEquip = charSO.GetSpellSO(CharCategories);

        }
        private void Start() {
            enemySpell = GameObject.FindGameObjectWithTag("Enemy");
            if(!spellInventory[0])
            {
                spellInventory[0] = spellEquip;
            }
            
        }

        private void Update() {
            CharDie();
            
        }
        public int health
        {
            get{return healthPoints;} set{healthPoints = value;}
        }

        public int mana
        {
            get{return manaPoints;} set{manaPoints = value;}
        }



        public void DecreaseHealth()
        {
            healthPoints -= 1;
            
        }

        

        

        private void CharDie()
        {
            if (healthPoints <= 0)
            {
                //Menjalankan Animasi Die 
                //Menggunakan Animation Event untuk memanggil Die() function
                OnDie.Invoke();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
            if(gameObject.tag == "Enemy")
            {
                gameManager.enemyDead += 1;
            }
            
        }
        

        public SpellSO GetSpell()
        {
            return spellEquip;
        }

        public SpellSO GetSpell(int spellIndex)
        {
            if(!spellInventory[spellIndex]) return null;
            return spellInventory[spellIndex];
        }

        

        public SpellSO SetSpell(SpellSO value)
        {
           int spellAmount = spellInventory.Length;
           for (int i = 0; i < spellAmount; i++)
           {
                if(!spellInventory[i]) return spellInventory[i] = value;
           }

            return null;
        }

        public string GetSpellName(int index)
        {
            if(!spellInventory[index])
            {
                return "none";
            }
            return spellInventory[index].name;

        }
        
        



    }
}
