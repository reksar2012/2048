using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour {
    private bool isTap, isSwipeLeft, isSwipeRight, isSwipeUp, isSwipeDown,isDraging=false;
    public static Swipe instance;
    private Vector2 startTouch, swipeDelta;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        isTap = isSwipeUp = isSwipeDown = isSwipeLeft = isSwipeRight = false;
        #region Standalone Input
        if (Input.GetMouseButtonDown(0))
        {
            isDraging = true;
            isTap = true;
            startTouch = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            Reset();
        }
        #endregion
        #region Mobile Input
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDraging = true;
                isTap = true;
                startTouch = Input.touches[0].position;

            }
            else if(Input.touches[0].phase == TouchPhase.Ended|| Input.touches[0].phase == TouchPhase.Canceled){
                Reset();
            }
        }
        #endregion
        swipeDelta = Vector2.zero;
        if (isDraging == true)
        {
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }
        if(swipeDelta.magnitude>0.5){
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x)>Mathf.Abs(y))
            {
                if (x < 0)
                {
                    isSwipeLeft = true;
                }
                else
                {
                    isSwipeRight = true;
                }
            }
            else
            {
                if (y < 0) { isSwipeDown = true; }
                else { isSwipeUp = true; }
            }
            Reset();

        }
    }
    private void Reset()
    {
        isDraging = false;
        startTouch=swipeDelta = Vector2.zero; 
    }

    public Vector2 SwipeDelta   { get { return swipeDelta; } }
    public bool SwipeLeft       { get { return isSwipeLeft; } }
    public bool SwipeRight      { get { return isSwipeRight; } }
    public bool SwipeUp         { get { return isSwipeUp; } }
    public bool SwipeDown       { get { return isSwipeDown; } }
    public bool Tap             { get { return isTap; } }

}

