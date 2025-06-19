using System;
using UnityEngine;

public abstract class SkillData : ScriptableObject
{
    public string Name;
    public int SkillPoint;
    [TextArea]
    public string Description;

    public abstract void Execute(TurnBasedCharacter instigator);
}
