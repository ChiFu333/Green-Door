using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CursorStateSO : ScriptableObject {
    public abstract Texture2D GetTexture();
    public abstract Vector2 GetHotspot();
}
