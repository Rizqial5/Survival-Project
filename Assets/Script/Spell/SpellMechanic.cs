using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Survival.Stats;
using Survival.Combat;

namespace Survival.Spell
{
    public class SpellMechanic : MonoBehaviour
    {
        
        [SerializeField] int spellReserves = 4;
        [SerializeField] float speedLaunch = 50f;
        [SerializeField] GameObject spawnPosition;

        [SerializeField] float spellCooldown = 4f;
        [SerializeField] float spellTime = 3f;
        [SerializeField] TargetCharacter targetCharacter;

        private GameObject enemySpell;
        private SpellSO spellObject;
        private Animator spellAnimation;
        private Attributes attributes;
        private ISpell spellInterface;

        
         

        private void Start() 
        {
            // spellConfig = GetComponent<SpellConfig>();
            // spellAnimation = GetComponent<Animator>();
            spellInterface = GetComponent<ISpell>();
           
            attributes = GetComponent<Attributes>();

        }

        private void Update() 
        {
    
            spellCooldown += Time.deltaTime;
            spellObject = attributes.GetSpell();

            
        }

        
        public void Spawn()
        {
            spellReserves -= 1;
            if(spellReserves > 0 )
            {
                SpawnMechanic(spawnPosition.transform.position);
            }

        
            
            
        }

        public void SpawnMechanic(Vector3 spawnPosition )
        {
            if(!spellObject) return;
            
            var newSpell = Instantiate(spellObject.GetSpellPrefab(),spawnPosition,transform.rotation);

            
            newSpell.GetComponent<SpellConfig>().SetTargetCharacter(targetCharacter);
            
            float direction = GetDirection();

            if(!newSpell) return ;

            if(direction == 1)
            {
                newSpell.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right*speedLaunch);
                newSpell.gameObject.GetComponent<Animator>().Play("LaunchRight");
                
            }
            else if(direction == -1)
            {
                newSpell.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left*speedLaunch);
                newSpell.gameObject.GetComponent<Animator>().Play("LaunchLeft");
                
            }

            
        }

        public void AddSpellReserves()
        {
            if(spellReserves >= 4) return;
            spellReserves += 1;
        }

        

        public int SpellReserves
        {
            get{return spellReserves;} set{spellReserves = value;}
        }

        public float SpellCooldown
        {
            get{return spellCooldown;} set{spellCooldown = value;}
        }

        public float SpellTime
        {
            get{return spellTime;}
        }

        public bool isEquipped()
        {
            if(!spellObject) return false;
            if(spellObject) return true;

            return false;
        }
        

        public void PlayAnim(string nameAnim)
        {
            spellAnimation.Play(nameAnim);
            
        }

        public float GetDirection()
        {
            return spellInterface.GetDirection();
        }

        public TargetCharacter GetTargetCharacter()
        {
            return spellInterface.GetTargetCharacter();
        }
    }

}