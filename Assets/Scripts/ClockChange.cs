using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum TypeArrow { H, M, S}

public class ClockChange : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    private RectTransform rect;
    private Vector2 startPoint;

    public InputField inputField;
    public TypeArrow typeArrow;
    float ygol;

    private void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        inputField.onEndEdit.AddListener(EndChangeTime);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!Clock.main.CanMove)
        {
            ygol += Vector3.Angle(startPoint, eventData.position);
            startPoint = eventData.position;


            switch (typeArrow)
            {
                case TypeArrow.H:
                    EndChangeTime(((int)(ygol / 360 * 12)).ToString());
                    break;
                case TypeArrow.M:
                case TypeArrow.S:
                    EndChangeTime(((int)(ygol / 360 * 60)).ToString());
                    break;

            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPoint = eventData.position;
        switch (typeArrow)
        {
            case TypeArrow.H:
                ygol = Clock.main.H / 24 * 360;
                break;
            case TypeArrow.M:
                ygol = Clock.main.M / 60 * 360;
                break;
            case TypeArrow.S:
                ygol = Clock.main.S / 60 * 360;
                break;

        }
    }

    void EndChangeTime(string timeString)
    {
        int time = int.Parse(timeString);

        switch (typeArrow)
        {
            case TypeArrow.H:
                time %= 24;
                rect.localRotation = Quaternion.Euler(0f, 0f, -360 / 12 * time);
                Clock.main.H = time;
                break;
            case TypeArrow.M:
                time %= 60;
                rect.localRotation = Quaternion.Euler(0f, 0f, -360 / 60 * time);
                Clock.main.M = time;
                break;
            case TypeArrow.S:
                time %= 60;
                rect.localRotation = Quaternion.Euler(0f, 0f, -360 / 60 * time);
                Clock.main.S = time;
                break;

        }

        inputField.text = time.ToString();

    }
}
