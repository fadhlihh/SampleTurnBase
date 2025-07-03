using UnityEngine;

public class SelectorUI : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
