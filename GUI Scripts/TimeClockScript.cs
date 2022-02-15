using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeClockScript : MonoBehaviour
{
    public RectTransform TimePiece;
    GameObject gameManager;
    GameManagement gameManage;
    int counter=0;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManage = gameManager.GetComponent<GameManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        counter+=1;
        float rotationOffset=(float)ConvertToDegrees((360/(gameManage.DayLength/8)));
        //TimePiece.rotation = Quaternion.Euler(0, 0,counter);
        TimePiece.eulerAngles= new Vector3(0,0,270);//((float)ConvertToDegrees(((gameManage.TimeClockCounter)/(gameManage.DayLength/8)))-rotationOffset)-90f);
        Debug.Log((float)ConvertToDegrees((360/(gameManage.DayLength/4))));
        //Debug.Log(Mathf.PI);
    }

    public double ConvertToDegrees(double radians)
    {
        return (180/Mathf.PI)* radians;
    }
}
