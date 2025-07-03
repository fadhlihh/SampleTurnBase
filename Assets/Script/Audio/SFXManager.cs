using Fadhli.Framework;
using UnityEngine;

public class SFXManager : SingletonBehaviour<SFXManager>
{
    [SerializeField]
    private AudioSource _maleHurtSFX;
    [SerializeField]
    private AudioSource _maleDeathSFX;
    [SerializeField]
    private AudioSource _femaleHurtSFX;
    [SerializeField]
    private AudioSource _femaleDeathSFX;
    [SerializeField]
    private AudioSource _beamSpellSFX;
    [SerializeField]
    private AudioSource _buffSpellSFX;
    [SerializeField]
    private AudioSource _healSFX;
    [SerializeField]
    private AudioSource _punchSFX;

    public AudioSource MaleHurtSFX { get => _maleHurtSFX; }
    public AudioSource MaleDeathSFX { get => _maleDeathSFX; }
    public AudioSource FemaleHurtSFX { get => _femaleHurtSFX; }
    public AudioSource FemaleDeathSFX { get => _femaleDeathSFX; }
    public AudioSource BeamSpellSFX { get => _beamSpellSFX; }
    public AudioSource BuffSpellSFX { get => _buffSpellSFX; }
    public AudioSource HealSFX { get => _healSFX; }
    public AudioSource PunchSFX { get => _punchSFX; }
}
