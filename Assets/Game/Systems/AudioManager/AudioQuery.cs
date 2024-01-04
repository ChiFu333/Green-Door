using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioQuery {
    [field: SerializeField] public AudioClip clip { get; private set; }
    [field: SerializeField] public float pitchVariance { get; private set; }
    public AudioQuery(AudioClip clip, float pitchVariance = 0.0f) {
        this.clip = clip;
        this.pitchVariance = pitchVariance;
    }
}