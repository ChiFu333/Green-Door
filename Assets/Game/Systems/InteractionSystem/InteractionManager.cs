using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour {
    private void Update() {
        HandleInteractions();
    }

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
}
