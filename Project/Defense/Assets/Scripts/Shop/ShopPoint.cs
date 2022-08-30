using UnityEngine;
using TMPro;

public class ShopPoint : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pointText;
    [SerializeField]
    private SceneFader sceneFader;
    [HideInInspector]
    public int point;
    void Start()
    {
        point = PlayerPrefs.GetInt("PlayerPoint");
    }
    private void Update()
    {
        if (point != 0)
            pointText.text = GetPointString(point);
        else
            pointText.text = "0000";
    }
    private string GetPointString(int data)
    {
        return string.Format("{0:#,###}", data);
    }
    public void Back()
    {
        sceneFader.FadeTo("MainMenu");
    }
}
