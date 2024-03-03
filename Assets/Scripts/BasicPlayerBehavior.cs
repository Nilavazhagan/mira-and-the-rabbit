using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerBehavior : MonoBehaviour
{

    public float horizontalSpeed = 1f;
    public float verticalSpeed = 1f;
    public BoxCollider2D floor;

    [Space(30)]
    public float screenTransitionSpeed = 1f;

    private bool facingRight = false;
    Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        InputManager.Instance.onMove += OnMove;
    }

    private void LateUpdate()
    {
        if (moving)
        {
            moving = false;
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    bool moving = false;
    void OnMove(Vector2 movement)
    {
        float h = movement.x;
        float v = movement.y;

        Vector3 pos = transform.position;

        pos.x += h * Time.deltaTime * horizontalSpeed;
        pos.y += v * Time.deltaTime * verticalSpeed;

        Flip(h);

        if ((Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0) && floor.bounds.Contains(pos))
        {
            transform.position = pos;
            animator.SetBool("isMoving", true);
            moving = true;
        }
    }

    void Flip(float horizontalMovement)
    {
        if(
            (horizontalMovement < 0 && facingRight) ||
            (horizontalMovement > 0 && !facingRight)
          )
        {
            facingRight = !facingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "ScreenTransitionRegion")
        {
            if (!Mathf.Approximately(other.transform.parent.transform.position.x, Camera.main.transform.position.x))        //Basic check to ensure the collider is not of the current used background
            {
                Debug.Log($"Area: {other.transform.parent.name}\tOther X: {other.transform.parent.transform.position.x}\tCamera X: {Camera.main.transform.position.x}");
                StartCoroutine(MoveCameraHorizontally(other.transform.parent.transform.position.x));
            }
        }
    }

    IEnumerator MoveCameraHorizontally(float posX)
    {
        Transform mainCamera = Camera.main.transform;
        Vector3 initialPos = mainCamera.position;
        float t = 0f;
        while(t < 1.0f)
        {
            mainCamera.position = new Vector3(Mathf.Lerp(initialPos.x, posX, t), initialPos.y, initialPos.z);

            t += screenTransitionSpeed * Time.deltaTime;
            yield return null;
        }
        mainCamera.position = new Vector3(posX, initialPos.y, initialPos.z);
    }

}
