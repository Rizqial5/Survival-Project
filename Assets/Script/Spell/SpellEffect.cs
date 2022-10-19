using System;
using UnityEngine;
using Survival.Stats;
using Survival.Movement;

[System.Serializable]
public class SpellEffect
{
  
    public StatEffect[] statEffects;
    public BehaviourEffect[] behaviourEffects;
}

[System.Serializable]
public class StatEffect
{
    public Stat stat;
    public int amount;
}

[System.Serializable]
public class BehaviourEffect
{
    public BehaveEnum bodyEffect;
    public int amount;

    
}