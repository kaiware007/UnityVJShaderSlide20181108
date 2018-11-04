using UnityEngine;

[ExecuteInEditMode]
public class Reflection : MonoBehaviour
{

    const string SHADER_NAME = "Hidden/Reflection";

    /// <summary>
    /// 左右鏡
    /// </summary>
    public bool horizontalReflect = false;

    /// <summary>
    /// 上下鏡
    /// </summary>
    public bool verticalReflect = false;

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

    // 左右反転PropertyID
    int m_PID_horizontal = -1;
    int m_PID_vertical = -1;

    private void Awake()
    {
        m_PID_horizontal = Shader.PropertyToID("_Horizontal");
        m_PID_vertical = Shader.PropertyToID("_Vertical");
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetInt(m_PID_horizontal, (horizontalReflect ? 1 : 0));
        material.SetInt(m_PID_vertical, (verticalReflect ? 1 : 0));

        Graphics.Blit(source, destination, material);
    }
}
