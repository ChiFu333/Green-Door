using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private const float minDetectableInput = 0.1f;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float minTargetDistance;
    [Header("Perspective")]
    [SerializeField] private float baseYPosition;
    [SerializeField] private float roomPerspectiveYModifier;
    [SerializeField] private float roomPerspectiveScaleModifier;
    [SerializeField] private Vector2 scaleRange;
    [Header("Imports")]
    [SerializeField] private Transform appearanceTransform;
    [SerializeField] private Rigidbody2D physicalBody;

    private MovementType currentMovementType;
    Vector2 currentInput;
    private Vector2 targetPosition;

    private void Update() {
        HandleInput();
        HandleMovement();
        UpdatePerspective();
    }

    private void HandleInput() {
        if (Input.GetMouseButtonDown(0) && CanWalkToMouse()) {
            targetPosition = ScreenUtils.WorldMouse();
            currentMovementType = MovementType.Mouse;
        }
        currentInput = GetKeyboardInput();
        if (currentInput.sqrMagnitude > minDetectableInput) {
            currentMovementType = MovementType.Keyboard;
        }
    }

    private void HandleMovement() {
        switch (currentMovementType) {
            case MovementType.Keyboard:
                physicalBody.velocity = movementSpeed * currentInput * new Vector2(1, roomPerspectiveYModifier);
                break;
            case MovementType.Mouse:
                Vector2 direction = targetPosition - (Vector2)transform.position;
                //Modify direction to account for perspective
                direction.y /= roomPerspectiveYModifier;
                physicalBody.velocity = movementSpeed * direction.normalized * new Vector2(1, roomPerspectiveYModifier);
                if (Vector2.Distance(targetPosition, transform.position) < minTargetDistance) currentMovementType = MovementType.Keyboard;
                break;
        }
    }

    private void UpdatePerspective() {
        float dy = baseYPosition - transform.position.y;
        float scale = Mathf.Clamp(1 + dy * roomPerspectiveScaleModifier, scaleRange.x, scaleRange.y);
        appearanceTransform.localScale = Vector3.one * scale;
    }

    //TODO: Move to interaction class, when it'll be present
    private bool CanWalkToMouse() {
        Collider2D collider = Physics2D.OverlapCircle(ScreenUtils.WorldMouse(),0.1f);
        return collider == null || collider.isTrigger;
    }

    private Vector2 GetKeyboardInput() {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private enum MovementType {
        Keyboard,
        Mouse
    }
}
