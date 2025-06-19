using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusUI : MonoBehaviour
{
    [SerializeField]
    private PlayerStatusItemUI _playerStatusItemUIPrefab;
    [SerializeField]
    private Transform _playerStatusUIRect;

    private List<(PlayerCharacter character, PlayerStatusItemUI item)> _bindings = new();

    public void InitializePlayerStatusItem(List<PlayerCharacter> characters)
    {
        _bindings.Clear();

        foreach (PlayerCharacter character in characters)
        {
            PlayerStatusItemUI item = Instantiate<PlayerStatusItemUI>(_playerStatusItemUIPrefab, _playerStatusUIRect);
            item.SetPotraitImage(character.Data.IconImage);
            item.SetHealthPoint(character.HealthPoint, character.Data.MaximumHealthPoint);
            item.SetSkillPoint(character.SkillPoint, character.Data.MaximumSkillPoint);
            item.SetPotraitImage(character.Data.IconImage);
            character.OnDamage.AddListener(item.SetHealthPoint);
            character.OnPerformedSkill.AddListener(item.SetSkillPoint);
            character.OnDeath.AddListener(character => item.ShowDeadOverlay());
            _bindings.Add((character, item));
        }
    }
}
