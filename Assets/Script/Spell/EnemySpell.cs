using System.Collections;
using System.Collections.Generic;
using Survival.Stats;
using UnityEngine;

namespace Survival.Spell
{
    public class EnemySpell : MonoBehaviour
    {
        // Start is called before the first frame update
        public void SpellToPlayer()
        {
            GameObject playerSpell = GameObject.FindGameObjectWithTag("Player");

            SpellSO droppedSpell = GetComponent<Attributes>().GetSpell();

            playerSpell.GetComponent<Attributes>().SetSpell(droppedSpell);
        }
    }
}
