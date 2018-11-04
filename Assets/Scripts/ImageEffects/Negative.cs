using UnityEngine;

[ExecuteInEditMode]
public class Negative : MonoBehaviour {
    const string SHADER_NAME = "Hidden/Negative";

    /// <summary>
    /// 色反転の比率
    /// </summary>
    [Range(0,1)]
    public float ratio = 1f;

    [SerializeField, HideInInspector]
    private Shader m_Shader;

    public Shader shader
    {
        get
        {
            if (m_Shader == null)
            {
                m_Shader = Shader.Find(SHADER_NAME);
            }

            return m_Shader;
        }
    }

    private Material m_Material;
    public Material material
    {
        get
        {
            if (m_Material == null)
            {
                m_Material = new Material(shader);
                m_Material.hideFlags = HideFlags.DontSave;
            }

            return m_Material;
        }
    }

    private void OnDisable()
    {
        if (m_Material != null)
            DestroyImmediate(m_Material);

        m_Material = null;
    }

    // ネガティブ率PropertyID
    int m_PID_negaRatio = 1;

    private void Awake()
    {
        m_PID_negaRatio = Shader.PropertyToID("_NegativeRatio");
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat(m_PID_negaRatio, ratio);
        Graphics.Blit(source, destination, material);
    }
}
