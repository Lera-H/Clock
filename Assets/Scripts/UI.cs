using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject[] objApply, objChange;
    public Text textButton;
    private bool isChange = true;

    public void B_ChangeApply()
    {
        isChange = !isChange;
        HideShow(objApply, !isChange);
        HideShow(objChange, isChange);

        Clock.main.CanMove = isChange;
        Clock.main.PersonEnteredValues = true;

        textButton.text = isChange ? "change" : "apply";
    }

    private void HideShow(GameObject[] obj, bool b)
    {
        foreach (GameObject o in obj) o.SetActive(b);
    }
}
