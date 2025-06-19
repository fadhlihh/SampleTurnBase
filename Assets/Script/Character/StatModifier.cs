using UnityEngine;

public class StatModifier
{
    public EStatType Type;
    public int Value;
    public int Duration;

    public StatModifier(EStatType type, int value, int duration)
    {
        this.Type = type;
        this.Value = value;
        this.Duration = duration;
    }
}
