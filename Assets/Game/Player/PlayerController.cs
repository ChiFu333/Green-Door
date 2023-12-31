using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {
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
    private bool isInputLocked = false;
    private Vector2 targetPosition;
    private UnityAction arrivalCallback;

    public void MoveTo(Vector2 _targetPosition, UnityAction callback) {
        targetPosition = _targetPosition;
        arrivalCallback = callback;
        currentMovementType = MovementType.Mouse;
        isInputLocked = true;
    }

    private void Update() {
        HandleInput();
        HandleMovement();
        UpdatePerspective();
    }

    private void HandleInput() {
        if (!isInputLocked && Input.GetMouseButtonDown(0) && CanWalkToMouse()) {
            targetPosition = ScreenUtils.WorldMouse();
            arrivalCallback = null;
            currentMovementType = MovementType.Mouse;
        }
        isInputLocked = false;
    }

    private void HandleMovement() {
        switch (currentMovementType) {
            case MovementType.None:
                physicalBody.velocity = Vector2.zero;
                break;
            case MovementType.Mouse:
                Vector2 direction = targetPosition - (Vector2)transform.position;
                //Modify direction to account for perspective
                direction.y /= roomPerspectiveYModifier;
                physicalBody.velocity = movementSpeed * direction.normalized * new Vector2(1, roomPerspectiveYModifier);
                if (Vector2.Distance(targetPosition, transform.position) < minTargetDistance) {
                    arrivalCallback?.Invoke();
                    currentMovementType = MovementType.None;
                }
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

    private enum MovementType {
        None,
        Mouse
    }
}
