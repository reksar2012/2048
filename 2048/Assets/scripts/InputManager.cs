using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection
{
    Left, Right, Up, Down
}
[System.Serializable]
public enum InputMethod
{
    with_swipe,without_swipe
}
public class InputManager : MonoBehaviour {
    public InputMethod Method;
    private GameManager _gm;
    private void Awake()
    {
        _gm = GameObject.FindObjectOfType<GameManager>();
        switch (Method)
        {
            case InputMethod.with_swipe:
                _gm = GameObject.FindObjectOfType<GameManager>();
                break;
            case InputMethod.without_swipe:
                _gm = GameObject.FindObjectOfType<GameManager>();
                break;
            default:
                break;
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Swipe.instance.Tap)
        {
            Debug.Log("tap");
        }
        
        if (Input.GetKeyDown(KeyCode.D)|| Swipe.instance.SwipeRight)
        {
            //right move
            _gm.Move(MoveDirection.Right);

        }
        else if (Input.GetKeyDown(KeyCode.A)|| Swipe.instance.SwipeLeft)
        {
            //right move
            _gm.Move(MoveDirection.Left);

        }
        else if (Input.GetKeyDown(KeyCode.W)|| Swipe.instance.SwipeUp)
        {
            //right move
            _gm.Move(MoveDirection.Up);

        }
        else if (Input.GetKeyDown(KeyCode.S)|| Swipe.instance.SwipeDown)
        {
            //right move
            _gm.Move(MoveDirection.Down);

        }
     

    }
}
