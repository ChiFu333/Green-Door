using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer targetRenderer;
    private AnimationDataSO currentAnimation;
    private readonly Timer animationTimer = new Timer();
    private int frame = 0;

    public void SetAnimation(AnimationDataSO newAnimation, bool updateFrame = true) {

        if (updateFrame || currentAnimation != newAnimation) {
            currentAnimation = newAnimation;
            animationTimer.SetFrequency(currentAnimation.framerate);
            targetRenderer.sprite = currentAnimation.frames[0];
            targetRenderer.transform.localPosition = currentAnimation.animationOffset;
            frame = 0;
        } else {
            currentAnimation = newAnimation;
        }
    }

    public void Update() {
        if (currentAnimation == null) return;
        if (animationTimer.Execute()) {
            animationTimer.SetFrequency(currentAnimation.framerate);
            frame = (frame + 1) % currentAnimation.frames.Count;
            targetRenderer.sprite = currentAnimation.frames[frame];
        }
    }
}
