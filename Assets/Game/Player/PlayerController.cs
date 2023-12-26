using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float movementSpeed;
    [Header("Perspective")]
    [SerializeField] private float baseYPosition;
    [SerializeField] private float roomPerspectiveYModifier;
    [SerializeField] private float roomPerspectiveScaleModifier;
    [SerializeField] private Vector2 scaleRange;
    [Header("Imports")]
    [SerializeField] private Transform appearanceTransform;
    private void Update() {
        HandleMovement();
        UpdateAppearance();
    }

    private void HandleMovement() {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        transform.Translate(movementSpeed * Time.deltaTime * input * new Vector2(1, roomPerspectiveYModifier));
    }

    private void UpdateAppearance() {
        float dy = baseYPosition - transform.position.y;
        //float scale = Mathf.Clamp(1 + dy * (1 - roomPerspectiveYModifier), scaleRange.x, scaleRange.y);
        float scale = Mathf.Clamp(1 + dy * roomPerspectiveScaleModifier, scaleRange.x, scaleRange.y);
        appearanceTransform.localScale = Vector3.one * scale;
    }
}
