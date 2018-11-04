using UnityEngine;

[ExecuteInEditMode]
public class RadiationBlur : MonoBehaviour {
    const string SHADER_NAME = "Hidden/RadiationBlur";

    /// <summary>
    /// ブラーの中心座標
    /// </summary>
    public Vector2 center = new Vector2(0.5f, 0.5f);

    /// <summary>
    /// ブラーの強さ
    /// </summary>
    [Range(0, 100)]
    public float power = 0f;

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

    // ブラーの中心座標
    int m_PID_blurCenter = -1;
    // ブラーの強さPropertyID
    int m_PID_blurPower = -1;

    private void Awake()
    {
        m_PID_blurCenter = Shader.PropertyToID("_BlurCenter");
        m_PID_blurPower = Shader.PropertyToID("_BlurPower");
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetVector(m_PID_blurCenter, center);
        material.SetFloat(m_PID_blurPower, power);
        Graphics.Blit(source, destination, material);
    }
}
