using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PointScore : MonoBehaviour
{
    public Text scoreComma;
    public int scoreValue=0;
    public static PointScore instance;
    private void Start()
    {
        instance = this;
    }
    void Update()
    {
        if (scoreValue != 0)
            scoreComma.text = GetThousandCommaText(scoreValue).ToString();
        else
            scoreComma.text = scoreValue.ToString();
    }
    public string GetThousandCommaText(int data) 
    { 
        return string.Format("{0:#,###}", data);
    }
    public void PointUp(int point)
    {
        if (!UserDataManager.Instance.isGuest)
        {
            scoreValue += point;
            Debug.Log(scoreValue);
            UserDataManager.Instance.GetComponent<PointHttp>().ReqPoint(point);
        }
    }

    public void PointUp(string eventName, int point)
    {
        Debug.Log( eventName + " : "+ point);
        scoreValue += point;
    }

    public void PointDown(int point)
    {
        scoreValue -= point;
        UserDataManager.Instance.GetComponent<PointHttp>().ReqPoint(-point);
    }

    public void PointDown(string eventName, int point)
    {
        Debug.Log(eventName + " : " + point);
        scoreValue -= point;
    }
}

