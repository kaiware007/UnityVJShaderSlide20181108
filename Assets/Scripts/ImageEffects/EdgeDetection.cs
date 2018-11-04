using UnityEngine;

[ExecuteInEditMode]
public class EdgeDetection : MonoBehaviour
{
    public enum FilterMode
    {
        Sobel,
        Laplacian,
        Depth,
    }

    const string SHADER_NAME = "Hidden/EdgeDetection";

    /// <summary>
    /// フィルターモード
    /// </summary>
    public FilterMode filterMode = FilterMode.Sobel;

    /// <summary>
    /// エッジの強さ
    /// </summary>
    public float edgePowor = 1f;

    /// <summary>
    /// 輪郭しきい値
    /// </summary>
    [Range(0, 1)]
    public float edgeThreshold = 0.5f;

    /// <summary>
    /// ダウンサンプリング数
    /// </summary>
    public int downSampling = 1;

    /// <summary>
    /// デプスの輪郭しきい値
    /// </summary>
    [Range(0f, 1f)]
    public float depthThreshold = 0;

    /// <summary>
    /// 元画像とのブレンド率(0で100%エッジの色になる）
    /// </summary>
    [Range(0f, 1f)]
    public float blend = 0;

    public Color backColor = Color.white;
    public Color edgeColor = Color.black;

    [SerializeField, HideInInspector]
    private Shader m_Shader;

    // Sobel Filter 横
    static float[] hSobelFilter =
    {
        1, 0, -1,
        2, 0, -2,
        1, 0, -1,
    };
    // Sobel Filter 縦
    static float[] vSobelFilter =
    {
         1,  2,  1,
         0,  0,  0,
        -1, -2, -1,
    };

    // Laplacian Filter
    static float[] laplacianFilter =
    {
        1, 1, 1,
        1,-8, 1,
        1, 1, 1,
    };

    // Edge 
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
    int m_PID_HCoef = -1;
    int m_PID_VCoef = -1;
    int m_PID_Coef = -1;
    int m_PID_MainTex = -1;
    int m_PID_EdgeTex = -1;
    int m_PID_EdgePower = -1;
    int m_PID_Blend = -1;
    int m_PID_DepthThreshold = -1;
    int m_PID_backColor = -1;
    int m_PID_edgeColor = -1;
    int m_PID_threshold = -1;

    private void Awake()
    {
        m_PID_HCoef = Shader.PropertyToID("_HCoef");
        m_PID_VCoef = Shader.PropertyToID("_VCoef");
        m_PID_Coef = Shader.PropertyToID("_Coef");
        m_PID_MainTex = Shader.PropertyToID("_MainTex");
        m_PID_EdgeTex = Shader.PropertyToID("_EdgeTex");
        m_PID_EdgePower = Shader.PropertyToID("_EdgePower");
        m_PID_Blend = Shader.PropertyToID("_Blend");
        m_PID_DepthThreshold = Shader.PropertyToID("_DepthThreshold");
        m_PID_backColor = Shader.PropertyToID("_BackColor");
        m_PID_edgeColor = Shader.PropertyToID("_EdgeColor");
        m_PID_threshold = Shader.PropertyToID("_Threshold");
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        int pass = 0;

        var tw = source.width;
        var th = source.height;
        var ts = downSampling;
        var format = RenderTextureFormat.ARGBFloat;
        var rwMode = RenderTextureReadWrite.Linear;
        var edgeTex = RenderTexture.GetTemporary(tw / ts, th / ts, 0, format, rwMode);

        material.SetFloat(m_PID_threshold, edgeThreshold);
        material.SetFloat(m_PID_Blend, blend);
        material.SetColor(m_PID_backColor, backColor);
        material.SetColor(m_PID_edgeColor, edgeColor);

        // 小さいサイズで輪郭検出
        switch (filterMode)
        {
            case FilterMode.Sobel:
                material.SetFloatArray(m_PID_HCoef, hSobelFilter);
                material.SetFloatArray(m_PID_VCoef, vSobelFilter);
                pass = 1;
                break;
            case FilterMode.Laplacian:
                material.SetFloatArray(m_PID_Coef, laplacianFilter);
                pass = 2;
                break;
            case FilterMode.Depth:
                material.SetFloat(m_PID_DepthThreshold, depthThreshold);
                pass = 3;
                break;
        }
        Graphics.Blit(source, edgeTex, material, pass);

        if (filterMode != FilterMode.Depth)
        {
            Graphics.Blit(source, edgeTex, material, pass);

            // 合成
            material.SetTexture(m_PID_MainTex, source); // 明示的に渡さないとなぜかグレーになる
            material.SetTexture(m_PID_EdgeTex, edgeTex);
            material.SetFloat(m_PID_EdgePower, edgePowor);

            Graphics.Blit(source, destination, material, 0);
        }
        else
        {
            material.SetTexture(m_PID_MainTex, source); // 明示的に渡さないとなぜかグレーになる
            Graphics.Blit(source, destination, material, 3);
        }

        // 一時バッファの解放
        RenderTexture.ReleaseTemporary(edgeTex);
    }
}
