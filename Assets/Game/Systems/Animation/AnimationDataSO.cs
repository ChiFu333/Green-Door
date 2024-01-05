using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationData", menuName = "Animation/AnimationData")]
public class AnimationDataSO : ScriptableObject {
    [field: SerializeField] public float framerate { get; private set; }
    [field: SerializeField] public List<Sprite> frames { get; private set; } = new List<Sprite>();
}
