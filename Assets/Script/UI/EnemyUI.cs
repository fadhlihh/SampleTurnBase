using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    [SerializeField]
    private Transform _uiRect;
    [SerializeField]
    private EnemyStatusUI _enemyStatusUI;

    public EnemyStatusUI EnemyStatusUI { get => _enemyStatusUI; }

    private void Update()
    {
        _uiRect.LookAt(Camera.main.transform);
    }

    public void Show()
    {
        _uiRect.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _uiRect.gameObject.SetActive(false);
    }
}
