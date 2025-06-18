using UnityEngine;
using UnityEngine.UI;

public class EnemyStatusUI : MonoBehaviour
{
    [SerializeField]
    private Image _healthBar;

    public void SetHealthBar(float health, float maximumHealth)
    {
        _healthBar.fillAmount = health / maximumHealth;
    }
}
