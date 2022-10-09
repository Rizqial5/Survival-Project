using Survival.Combat;

namespace Survival.Spell
{
    public interface ISpell
    {
        float GetDirection();
        TargetCharacter GetTargetCharacter();
    }
}