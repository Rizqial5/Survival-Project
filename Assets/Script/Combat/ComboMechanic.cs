using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival.Combat
{
    public class ComboMechanic : MonoBehaviour
    {
        [SerializeField] int comboCount;
        [SerializeField] bool isAttack;
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();   
             
        }


        public void ComboController()
        {
            isAttack = true;
            animator.SetTrigger("" +comboCount);
        }

        public void StartAttack()
        {
            isAttack = false;
            if(comboCount<2)
            {
                comboCount+=1;
                
            }
            // print("bisa");
        }

        public void FinishAni()
        {
            isAttack = false;
            // print("bisa ani");
            comboCount = 0;
        }

        public bool GetIsAttack()
        {
            return isAttack;
        }

        public void SetIsAttack(bool setAttack)
        {
            isAttack = setAttack;
        }

       
        

        
    }
}
