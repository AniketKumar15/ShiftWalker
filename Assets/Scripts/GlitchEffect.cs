using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GlitchEffect : MonoBehaviour
{
    public Material glitchMaterial;
    [Range(0, 1)] public float glitchAmount = 0.1f;
    [Range(0, 10)] public float glitchSpeed = 5f;

   
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (glitchMaterial != null)
        {
            glitchMaterial.SetFloat("_GlitchAmount", glitchAmount);
            glitchMaterial.SetFloat("_Speed", glitchSpeed);
            Graphics.Blit(src, dest, glitchMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
