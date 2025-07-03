using UnityEngine;
using UnityEngine.Events;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Character _ownerCharacter;

    public UnityEvent OnHitTarget;
    public UnityEvent OnEndActionAnimation;

    private void Awake()
    {
        if (!_animator)
        {
            _animator = GetComponent<Animator>();
        }
        if (!_ownerCharacter)
        {
            _ownerCharacter = GetComponent<Character>();
        }
    }

    public void PlayAttackAnimation()
    {
        _animator.Play("Attack");
    }

    public void PlayDamageAnimation()
    {
        _animator.Play("Hit Reaction");
    }

    public void PlayDeathAnimation()
    {
        _animator.Play("Death");
    }

    public void PlaySkillAnimation()
    {
        _animator.Play("Perform Skill");
        Debug.Log(_ownerCharacter.gameObject.name);
    }

    public void PlayConsumeItemAnimation()
    {
        _animator.Play("Drink");
        Debug.Log(_ownerCharacter.gameObject.name);
    }

    public void HitTarget()
    {
        OnHitTarget?.Invoke();
    }

    public void EndActionAnimation()
    {
        OnEndActionAnimation?.Invoke();
    }

    public void SetDefenseAnimation(bool value)
    {
        _animator.SetBool("IsDefending", value);
    }
}
