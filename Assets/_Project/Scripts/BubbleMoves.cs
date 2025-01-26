using UnityEngine;

public class BubbleMoves : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] float intensity = 25;
    RectTransform rect;
    Vector2 startPos;
    float offset;

    private void Awake()
    {
        offset = Random.Range(0f, 1f);
        intensity += Random.Range(-intensity/2, intensity/2);
        rect = GetComponent<RectTransform>();
        startPos = rect.anchoredPosition;
    }

    void Update()
    {
        rect.anchoredPosition = new Vector2(startPos.x , startPos.y + Mathf.Sin(Time.time * speed + offset) * intensity);
    }
}
