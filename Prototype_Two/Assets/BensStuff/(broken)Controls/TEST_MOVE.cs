using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TEST_MOVE : MonoBehaviour
{
    PlayerControls Controls;
    public float Speed = 5.0f;
    private Vector2 MovementInput;

    void Awake()
    {
        Controls = new PlayerControls();

        Controls.Gameplay.Move.performed += ctx => MovementInput = ctx.ReadValue<Vector2>();
        Controls.Gameplay.Move.canceled += ctx => MovementInput = Vector2.zero;
    }


    void OnEnable()
    {
        Controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        Controls.Gameplay.Disable();
    }

    void Update()
    {
        transform.Translate(new Vector3(MovementInput.x, MovementInput.y, 0.0f) * Speed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext ctx) => MovementInput = ctx.ReadValue<Vector2>();
}
