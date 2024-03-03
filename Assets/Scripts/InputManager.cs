using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [HideInInspector]
    public OnMove onMove;
    [HideInInspector]
    public OnInteract onInteract;
    [HideInInspector]
    public OnPickup onPickup;

    bool inputsFrozen = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inputsFrozen) return;

        if (Input.GetKeyDown(KeyCode.E))
            onInteract?.Invoke();
        if (Input.GetKeyDown(KeyCode.F))
            onPickup?.Invoke();

        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (movement != Vector2.zero)
            onMove?.Invoke(movement);
    }

    public void Freeze()
    {
        inputsFrozen = true;
    }

    public void UnFreeze()
    {
        inputsFrozen = false;
    }
}

public delegate void OnMove(Vector2 movement);
public delegate void OnInteract();
public delegate void OnPickup();
