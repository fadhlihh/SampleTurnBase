using UnityEngine;
using UnityEngine.UI;

public class SelectorUI : MonoBehaviour
{
    [SerializeField]
    private Image _selectorIcon;
    [SerializeField]
    private Color _selectorIconColor = Color.white;
    [SerializeField]
    private Color _turnIconColor = Color.white;
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    public void ShowSelector()
    {
        _selectorIcon.color = _selectorIconColor;
        gameObject.SetActive(true);
    }

    public void ShowTurnIcon()
    {
        _selectorIcon.color = _turnIconColor;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
