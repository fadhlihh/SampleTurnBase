using UnityEngine;

public class DefenseAction : TurnBasedAction
{
    public DefenseAction()
    {
        Type = EActionCategory.Defense;
    }

    public override void Execute(TurnBasedCharacter instigator)
    {
        instigator.IsDefending = true;
        Debug.Log($"{instigator.Data.Name} Defending");
        instigator.EndTurn();
    }
}
