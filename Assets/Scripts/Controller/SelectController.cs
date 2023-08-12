using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : MonoBehaviour
{
    public Transform[] Player;
    public Transform Select;
    bool isclick = false;
    public void Start()
    {
        Select = Player[2];
    }

    public void Update()
    {
        if (GameManager.Instance.isPaused != true) { return; }
        if (Input.GetKeyDown(KeyCode.A) && !isclick)
        {
            Prev();
            isclick = true;  // 키가 눌러졌다고 표시
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isclick)
        {
            Next();
            isclick = true;  // 키가 눌러졌다고 표시
        }

        // A나 D 키가 떼어졌을 때 isclick을 다시 false로 설정
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            isclick = false;
        }
    }

    private void Next()
    {
        // Save the last object's position
        Vector3 lastPosition = Player[Player.Length - 1].position;

        // Shift all object positions to the right
        for (int i = Player.Length - 1; i > 0; i--)
        {
            Player[i].position = Player[i - 1].position;
        }

        // Place the last object to the first position
        Player[0].position = lastPosition;

        // Rearrange the array elements accordingly
        Transform firstTransform = Player[0];
        for (int i = 0; i < Player.Length - 1; i++)
        {
            Player[i] = Player[i + 1];
        }
        Player[Player.Length - 1] = firstTransform;

        Select = Player[2];
        ScaleSetting();
    }

    private void Prev()
    {

        // Save the first object's position
        Vector3 firstPosition = Player[0].position;

        // Shift all object positions to the left
        for (int i = 0; i < Player.Length - 1; i++)
        {
            Player[i].position = Player[i + 1].position;
        }

        // Place the first object to the last position
        Player[Player.Length - 1].position = firstPosition;
        // Rearrange the array elements accordingly
        Transform lastTransform = Player[Player.Length - 1];
        for (int i = Player.Length - 1; i > 0; i--)
        {
            Player[i] = Player[i - 1];
        }
        Player[0] = lastTransform;

        Select = Player[2];
        ScaleSetting();
    }


    void ScaleSetting()
    {
        Player[0].localScale = Vector3.one * 0.5f;
        Player[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        Player[4].localScale = Vector3.one * 0.5f;
        Player[4].GetComponent<Image>().color = new Color(1,1,1, 0.5f);
                                                          
        Player[1].localScale = Vector3.one * 0.7f;       
        Player[1].GetComponent<Image>().color = new Color(1,1,1, 0.5f);
                                                          
        Player[3].localScale = Vector3.one * 0.7f;        
        Player[3].GetComponent<Image>().color = new Color(1,1,1, 0.5f);

        Player[2].localScale = Vector3.one;
        Player[2].GetComponent<Image>().color = new Color(1, 1, 1, 1f);

    }
    public string GetSelectName() {
        return Select.name;
    }

}