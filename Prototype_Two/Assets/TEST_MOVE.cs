using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TEST_MOVE : MonoBehaviour
{
    PlayerControls Controls;
    Vector2 Move;

    void Awake()
    {
        Controls = new PlayerControls();

        Controls.Gameplay.TESTGrow.performed += ctx => Grow();

        Controls.Gameplay.Move.performed += ctx => Move = ctx.ReadValue<Vector2>();
        Controls.Gameplay.Move.canceled += ctx => Move = Vector2.zero;
    }

    void Grow()
    {
        transform.localScale *= 1.1f;
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
        Vector2 m = new Vector2(Move.x, Move.y) * 5.0f * Time.deltaTime;
        transform.Translate(m, Space.World);
    }
}
