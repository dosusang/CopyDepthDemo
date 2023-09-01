using UnityEngine;
using UnityEngine.Rendering;

public class Test : MonoBehaviour
{
    public Camera cam;
    public RenderTexture depth;
    public Material mat;
    public Material CopyDepthMat;

    public RenderTexture SrcColorTex;
    public RenderTexture SrcDepthTex;

    private void OnPreRender()
    {
        SrcDepthTex.DiscardContents();
        cam.SetTargetBuffers(SrcColorTex.colorBuffer, SrcDepthTex.depthBuffer);
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        SrcColorTex = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.RGB111110Float);
        SrcColorTex.name = "SrcColorTex";
        SrcColorTex.Create();
        
        SrcDepthTex = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.Depth);
        SrcDepthTex.name = "SrcDepthTex";
        SrcDepthTex.Create();
        
        depth = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.Depth);
        var cmd = new CommandBuffer();

        mat.mainTexture = depth;
        cam = GetComponent<Camera>();
        
        CopyDepthMat.SetTexture("_MyDepthTex", SrcDepthTex);
        cmd.Blit(SrcDepthTex, depth, CopyDepthMat);
        cmd.Blit(SrcColorTex, BuiltinRenderTextureType.CameraTarget);

        cam.AddCommandBuffer(CameraEvent.AfterForwardOpaque,cmd);
    }
}
