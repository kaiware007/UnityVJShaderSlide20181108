using UnityEngine;

//[ExecuteInEditMode]
public class Glitch : MonoBehaviour
{

    const string SHADER_NAME = "Hidden/Glitch";

    /// <summary>
    /// ノイズテクスチャの隣の色になる確率
    /// </summary>
    [Range(0, 1)]
    public float noiseColorChange = 0.85f;

    /// <summary>
    /// ノイズの更新頻度
    /// </summary>
    [Range(0, 1)]
    public float noiseSpeed = 0.85f;

    /// <summary>
    /// ずらす強さ
    /// </summary>
    [Range(0,1)]
    public float intensity = 1f;

    public int glitchScale = 32;

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

    Texture2D noiseTexture;

    private void CreateTexture()
    {
        noiseTexture = new Texture2D(Screen.width / glitchScale, Screen.height / glitchScale, TextureFormat.RGBA32, false);
        //noiseTexture = new Texture2D(64, 32, TextureFormat.RGBA32, false);

        noiseTexture.hideFlags = HideFlags.DontSave;
        noiseTexture.wrapMode = TextureWrapMode.Clamp;
        noiseTexture.filterMode = FilterMode.Point;

        UpdateNoiseTexture();
    }

    private Color RandomColor()
    {
        return new Color(Random.value, Random.value, Random.value, Random.value);
    }

    private void UpdateNoiseTexture()
    {
        Color color = RandomColor();

        for (int y = 0; y < noiseTexture.height; y++)
        {
            for (int x = 0; x < noiseTexture.width; x++)
            {
                // 確率で隣と同じ色になる
                if (Random.value > noiseColorChange) color = RandomColor();
                noiseTexture.SetPixel(x, y, color);
            }
        }

        noiseTexture.Apply();
    }

    private void Start()
    {
        CreateTexture();
    }

    private void Update()
    {
        if (Random.value > noiseSpeed)
        {
            UpdateNoiseTexture();
        }
    }

    // PropertyID
    int m_PID_intensity = 1;
    int m_PID_noiseTex = 1;

    private void Awake()
    {
        m_PID_intensity = Shader.PropertyToID("_Intensity");
        m_PID_noiseTex = Shader.PropertyToID("_NoiseTex");
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        
        material.SetFloat(m_PID_intensity, intensity);
        material.SetTexture(m_PID_noiseTex, noiseTexture);

        Graphics.Blit(source, destination, material);
    }
}
