using UnityEngine;
using UnityEngine.UI;

public class ScrollObject : MonoBehaviour
{
    private RectTransform rec;
    public float speed = 5f, checkpos = 0f;
    public Image Background;
    public Color32 resultColor;
    public bool start;

    // Use this for initialization
    private void Start()
    {
        rec = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (start)
        {
            if (Background != null)
                Background.color = Color.LerpUnclamped(Background.color, resultColor, 0.05f);
            if (rec.offsetMin.y != checkpos)
            {
                rec.offsetMin += new Vector2(rec.offsetMin.x, speed);
                rec.offsetMax += new Vector2(rec.offsetMax.x, speed);
            }
        }
    }
}