using UnityEngine;

[ExecuteInEditMode]
public class Mosaic : MonoBehaviour {

    const string SHADER_NAME = "Hidden/Mosaic";

    /// <summary>
    /// モザイク倍率
    /// </summary>
    public float scale = 1f;

    /// <summary>
    /// 円でくり抜くフラグ
    /// </summary>
    public bool isCircle = false;

    [SerializeField, HideInInspector]
    private Shader m_Shader;

    private bool isCircleOld = false;

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

    // モザイクのスケールPropertyID
    int m_PID_scale = 1;

    private void Awake()
    {
        m_PID_scale = Shader.PropertyToID("_MosaicScale");
        ChangeCircleFlag();
    }

    private void Update()
    {
        if(isCircle != isCircleOld)
        {
            ChangeCircleFlag();
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat(m_PID_scale, scale);
        Graphics.Blit(source, destination, material);
    }

    public void ChangeCircleFlag()
    {
        if (isCircle)
        {
            material.EnableKeyword("CIRCLE");
        }else
        {
            material.DisableKeyword("CIRCLE");
        }
        isCircleOld = isCircle;
    }
}
