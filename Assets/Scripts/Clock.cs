using UnityEngine;
using UnityEngine.UI;

 public interface IClock
{
    int H { get; set; }
    int M { get; set; }
    int S { get; set; }

    bool CanMove { get; set; }
    bool PersonEnteredValues { get; set; }
}

public class Clock : MonoBehaviour, IClock
{
    static public Clock main;

    public RectTransform arrowH, arrowM, arrowS;
    public Text textTime;
    private string timeString;

    private int h, m, s;
    private float dTime=0f;
    private bool canMove;
    private bool personEnteredValues;

    public int H
    {
        get { return h; }
        set 
        {
            if (value > -1 && value < 24) h = value;
            else h = 0;
        }
    }
    public int M
    {
        get { return m; }
        set
        {
            if (value > -1 && value < 60) m = value;
            else m = 0;
        }
    }
    public int S
    {
        get { return s; }
        set
        {
            if (value > -1 && value < 60) s = value;
            else s = 0;
        }
    }
    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }
    public bool PersonEnteredValues
    {
        get { return personEnteredValues; }
        set { personEnteredValues = value; }
    }


    private void Awake()
    {
        main = this;
    }

    private void Update()
    {
        if (canMove)
        {
            dTime += Time.deltaTime;
            if (dTime >= 1f)
            {
                dTime = 0f;
                AddTime();
                TextingTime();
                RotateArrows(arrowH, h, 360 / 12);
                RotateArrows(arrowM, m, 360 / 60);
            }
            RotateArrows(arrowS, s + dTime, 360 / 60);
        }
    }
    private void AddTime()
    {
        s++;
        m = s == 60 ? m + 1 : m;
        h = m == 60 ? h + 1 : h;

        if (s == 60) s = 0;
        if (m == 60) m = 0;
        if (h == 24) h = 0;
    }

    private void TextingTime()
    {
        timeString = h.ToString() + ":" + m.ToString() + ":" + s.ToString();
        textTime.text = timeString;
    }

    private void RotateArrows(RectTransform arrow, float typeArrow, int k)
    {
        arrow.localRotation = Quaternion.Euler(0f, 0f, -k * typeArrow);
    }

  

}
