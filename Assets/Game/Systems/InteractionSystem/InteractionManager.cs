using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour {
    public static InteractionManager inst { get; private set; }
    private readonly Dictionary<string, ClickableThing> sceneClickables = new Dictionary<string, ClickableThing>();
    private void Update() {
        HandleInteractions();
    }

    public void RegisterClickable(string name, ClickableThing clickable) {
        sceneClickables.Add(name, clickable);
    }
    public ClickableThing GetClickable(string name) => sceneClickables[name];

    private void HandleInteractions() {
        if (!Input.GetMouseButtonDown(0)) return;
        if (DialogueSystem.inst.isDialogueOngoing) return;
        List<GameObject> objects = ScreenUtils.GetObjectsUnderMouse();
        for (int i = 0; i < objects.Count; i++) {
            ClickableThing clickable = objects[i].GetComponent<ClickableThing>();
            if (clickable == null) continue;
            clickable.HandleClick();
            return;
        }
    }

    private void Awake() {
        if (inst != null && inst != this) {
            Destroy(this);
        } else {
            inst = this;
        }
    }
}
