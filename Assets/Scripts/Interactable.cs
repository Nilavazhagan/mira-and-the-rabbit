using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : Character
{
    public bool movable = true;
    public bool facingRight;
    public float movementSpeed;

    Animator animator;
    bool playerInRange = false;

    //If adding Start method remember to call base.Start() from all behaviors

    private void Awake()
    {
        animator = GetComponent<Animator>();
        InputManager.Instance.onInteract += OnInteract;
    }

    void OnInteract() {
        if (playerInRange)
        {
            Interact();
        }
    }

    public virtual void Interact(){    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
        }
    }

    protected delegate void OnMoveComplete();
    protected IEnumerator MoveTo(Vector3 targetPos, OnMoveComplete moveComplete = null)
    {
        Vector3 initialPos = transform.position;
        float t = 0f;
        animator.SetBool("isMoving", true);
        FlipIfRequired(initialPos, targetPos);
        while (t < 1.0f)
        {
            transform.position = new Vector3(Mathf.Lerp(initialPos.x, targetPos.x, t), Mathf.Lerp(initialPos.y, targetPos.y, t), initialPos.z);

            t += movementSpeed * Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(targetPos.x, targetPos.y, initialPos.z);
        animator.SetBool("isMoving", false);

        moveComplete?.Invoke();
    }

    void FlipIfRequired(Vector3 initialPos, Vector3 targetPos)
    {
        if (
            (facingRight && targetPos.x < initialPos.x) ||
            (!facingRight && targetPos.x > initialPos.x)
           )
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            facingRight = !facingRight;
        }
    }

    public void Teleport(Transform pos)
    {

        FlipIfRequired(transform.position, pos.position);

        Vector3 initialPos = transform.position;
        initialPos.x = pos.position.x;
        initialPos.y = pos.position.y;
        transform.position = initialPos;
    }

}
