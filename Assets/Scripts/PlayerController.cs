using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    private float deltaX;
    private Rigidbody2D _rb2D;
    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //MovePC();
        SwipeMove();
    }
    public void SwipeMove()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch(touch.phase)
            {
                case TouchPhase.Began:
                    deltaX = touchPos.x - transform.position.x;
                    //deltaY = touchPos.y - transform.position.y;
                    break;

                case TouchPhase.Moved:
                    //_rb2D.MovePosition(new Vector2(touchPos.x - deltaX, 0));
                    _rb2D.DOMove(new Vector2(touchPos.x - deltaX, 0), 0.05f);
                    break;

                case TouchPhase.Ended:
                    _rb2D.velocity = Vector2.zero;
                    break;
            }
        }
    }
    public void MovePC()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * 5f * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * 5f * Time.deltaTime);
        }
    }
}
