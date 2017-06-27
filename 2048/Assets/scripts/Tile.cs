using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public bool isMergedThisTurn = false;
    public int indCol;
    public int indRow;
    public Animator animator;
    public int Number
    {
        get
        {
            return number;
        }
        set
        {
            
            number = value;
            if (number == 0) { SetEmpty(); }
            else
            {
                ApplyStyle(number);
                SetActive();
            }
        }
    }
    private int number;
    private Text TileText;
    private Image TileImage;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        TileText = GetComponentInChildren<Text>();
        TileImage = transform.Find("NumberedCell").GetComponent<Image>();
    }

    public void ApplyStyleFromHolder(int index)
    {
        TileText.text = TileStyleHolder.Instance.TileStyles[index].Number.ToString();
        TileText.color = TileStyleHolder.Instance.TileStyles[index].TextColor;
        TileImage.color = TileStyleHolder.Instance.TileStyles[index].TileColor;
    }

    public void ApplyStyle(int number)
    {
        switch (number)
        {
            case 2:
                ApplyStyleFromHolder(0);
                break;
            case 4:
                ApplyStyleFromHolder(1);
                break;
            case 8:
                ApplyStyleFromHolder(2);
                break;
            case 16:
                ApplyStyleFromHolder(3);
                break;
            case 32:
                ApplyStyleFromHolder(4);
                break;
            case 64:
                ApplyStyleFromHolder(5);
                break;
            case 128:
                ApplyStyleFromHolder(6);
                break;
            case 256:
                ApplyStyleFromHolder(7);
                break;
            case 512:
                ApplyStyleFromHolder(8);
                break;
            case 1024:
                ApplyStyleFromHolder(9);
                break;
            case 2048:
                ApplyStyleFromHolder(10);
                break;
            case 4096:
                ApplyStyleFromHolder(11);
                break;
            default:
                Debug.LogError("Check numbers");
                break;
        }
    }
    private void SetActive()
    {
        TileImage.enabled= true;
        TileText.enabled= true;
    }

    private void SetEmpty()
    {
        TileImage.enabled = false;
        TileText.enabled = false;
    }
   

}
