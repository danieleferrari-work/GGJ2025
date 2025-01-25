using UnityEngine;

public class BubbleMoves : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] float intensity = 25;
    RectTransform rect;
    Vector2 startPos;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        startPos = rect.anchoredPosition;
    }

    void Update()
    {
        rect.anchoredPosition = new Vector2(startPos.x , startPos.y + Mathf.Sin(Time.time * speed) * intensity);
    }
}
