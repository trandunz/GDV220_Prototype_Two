using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float Speed = 5.0f;
    private Vector2 MovementInput;

    private void Update()
    {
        transform.Translate(new Vector3(MovementInput.x, MovementInput.y, 0.0f) * Speed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext ctx) => MovementInput = ctx.ReadValue<Vector2>();
}
