using TMPro;
using UnityEngine;

public class FloatNumberUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0f, 180f, 0f);
    }

    public void Show(string text)
    {
        _text.text = text;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
