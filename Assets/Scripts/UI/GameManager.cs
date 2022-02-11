using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int Remain = 20;
    public BarController BarController;
    public PointSpawner PointSpawner;
    public TextMeshProUGUI Text;
    private UnityEvent _clickEvent;
    public Image BackGround;
    public Image Mask;
    private float threshold = 0.03f;
    private Color defaultColor;
    
    void Start()
    {
        defaultColor = BackGround.color;
        Text.text = Remain.ToString();
        _clickEvent = new UnityEvent();
        _clickEvent.AddListener(OnClicked);
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) _clickEvent.Invoke();
    }

    private void Restart()
    {
        Remain = 20;
        Text.text = Remain.ToString();
        BackGround.color = defaultColor;
        Mask.color = defaultColor;
        BarController.CurrentCondition = BarCondition.TurnLeft;
        _clickEvent.RemoveListener(Restart);
        _clickEvent.AddListener(OnClicked);
    }

    private void OnClicked()
    {

        BarController.TimeSpan = 0.5f + (float) Remain / 20; 

        if (Succeed())
        {
            Remain--;
            Text.text = Remain.ToString();
            if (BarController.CurrentCondition == BarCondition.TurnLeft)
                BarController.CurrentCondition = BarCondition.TurnRight;
            else if (BarController.CurrentCondition == BarCondition.TurnRight)
                BarController.CurrentCondition = BarCondition.TurnLeft;
            PointSpawner.Spawn(UnityEngine.Random.Range(0f, 1f));
            if (Remain <= 0)
            {
                BackGround.color = Color.yellow;
                Mask.color = Color.yellow;
                BarController.CurrentCondition = BarCondition.Stop;
                _clickEvent.RemoveListener(OnClicked);
                _clickEvent.AddListener(Restart);
            }
        }
        else
        {
            BackGround.color = Color.red;
            Mask.color = Color.red;
            BarController.CurrentCondition = BarCondition.Stop;
            _clickEvent.RemoveListener(OnClicked);
            _clickEvent.AddListener(Restart);
        }
    }

    /// <summary>
    /// 成功か失敗を返す
    /// </summary>
    /// <returns></returns>
    private bool Succeed()
    {
        if (Mathf.Abs(BarController.normarizedPosition - PointSpawner.normarizedPosition) < threshold) return true;
        var x = Mathf.Repeat(BarController.normarizedPosition + 0.5f, 1f);
        var y = Mathf.Repeat(PointSpawner.normarizedPosition + 0.5f, 1f);
        if (Mathf.Abs(x - y) < threshold) return true;
        return false;
    }
    
}
