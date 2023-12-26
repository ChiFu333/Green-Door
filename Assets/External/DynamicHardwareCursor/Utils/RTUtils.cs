using UnityEngine;

public static class RTUtils {
    static RenderTexture previousRt;
    public static void BeginOrthographicRendering(this RenderTexture rt, float orthoSize, in Vector3 position, in Quaternion rotation, float zBegin = -100, float zEnd = 100) {
        float aspect = (float)rt.width / rt.height;
        Matrix4x4 projectionMatrix = Matrix4x4.Ortho(-orthoSize * aspect, orthoSize * aspect, -orthoSize, orthoSize, zBegin, zEnd);
        Matrix4x4 viewMatrix = Matrix4x4.TRS(position, rotation, new Vector3(1, 1, -1));
        Matrix4x4 cameraMatrix = projectionMatrix * viewMatrix.inverse;
       
        rt.BeginRendering(cameraMatrix);
    }
    public static void BeginPerspectiveRendering(this RenderTexture rt, float fov, in Vector3 position, in Quaternion rotation, float zBegin = -100, float zEnd = 100) {
        float aspect = (float)rt.width / rt.height;
        Matrix4x4 projectionMatrix = Matrix4x4.Perspective(fov, aspect, zBegin, zEnd);
        Matrix4x4 viewMatrix = Matrix4x4.TRS(position, rotation, new Vector3(1, 1, -1));
        Matrix4x4 cameraMatrix = projectionMatrix * viewMatrix.inverse;
        rt.BeginRendering(cameraMatrix);
    }

    public static void BeginRendering(this RenderTexture rt, Matrix4x4 projectionMatrix) {
        if (Camera.current != null) projectionMatrix *= Camera.current.worldToCameraMatrix.inverse;
        previousRt = RenderTexture.active;
        RenderTexture.active = rt;
        GL.PushMatrix();
        GL.LoadProjectionMatrix(projectionMatrix);
    }

    public static void EndRendering(this RenderTexture rt) {
        GL.PopMatrix();
        GL.invertCulling = false;
        RenderTexture.active = previousRt;
    }

    public static void DrawMesh(this RenderTexture rt, Mesh mesh, Material material, in Matrix4x4 objectMatrix, int pass = 0) {
        bool canRender = material.SetPass(0);
        if (canRender) {
            Graphics.DrawMeshNow(mesh, objectMatrix);
        }
    }

    public static void DrawRenderTexture(RenderTexture rt, Mesh quadMesh, Matrix4x4 textureMatrix, Material material, Camera camera) {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        mpb.SetTexture("_MainTex", rt);
        Graphics.DrawMesh(quadMesh, textureMatrix, material, 0, camera, 0, mpb);
    }
}
