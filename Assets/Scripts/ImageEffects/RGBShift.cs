using UnityEngine;

[ExecuteInEditMode]
public class RGBShift : MonoBehaviour {
    const string SHADER_NAME = "Hidden/RGBShift";

    /// <summary>
    /// ずらす幅
    /// </summary>
    [Range(-100,100)]
    public float shiftPower = 1;

    /// <summary>
    /// ずらす方向のノイズ速度
    /// </summary>
    [Range(0,10)]
    public float noiseSpeed = 1;

    [SerializeField, HideInInspector]
    private Shader m_Shader;

    private Vector2 noisePos;

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

    // PropertyID
    int m_PID_shiftPower = 1;

    private void Awake()
    {
        m_PID_shiftPower = Shader.PropertyToID("_ShiftPower");
    }

    Vector4 shiftUV;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        float rad = Mathf.PerlinNoise(noisePos.x, noisePos.y) * Mathf.PI * 2f;
        shiftUV.x = Mathf.Cos(rad) * shiftPower;
        shiftUV.y = Mathf.Sin(rad) * shiftPower;

        material.SetVector(m_PID_shiftPower, shiftUV);
        Graphics.Blit(source, destination, material);
    }

    private void Update()
    {
        noisePos.x += Time.deltaTime * noiseSpeed;
    }
}
