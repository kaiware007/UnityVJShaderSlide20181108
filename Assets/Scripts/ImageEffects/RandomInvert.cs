using UnityEngine;
using KUtil;

public class RandomInvert : MonoBehaviour {
    const string SHADER_NAME = "Hidden/RandomInvert";

    public float fadeTime = 0.5f;

    public bool invert = false;    // true:反転 flase:通常
    public EaseType easeType = EaseType.QuadOut;
    public float noiseScale = 1000;

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

    // PropertyID
    int m_PID_threshold = 1;
    int m_PID_invert = 1;
    int m_PID_startTime = 1;
    int m_PID_scale = 1;

    float fadeDuration = 0;
    float threshold = 0;
    float startTime = 0;

    private void Awake()
    {
        m_PID_threshold = Shader.PropertyToID("_Threshold");
        m_PID_invert = Shader.PropertyToID("_Invert");
        m_PID_startTime = Shader.PropertyToID("_StartTime");
        m_PID_scale = Shader.PropertyToID("_Scale");
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat(m_PID_threshold, threshold);
        material.SetInt(m_PID_invert, (invert ? 1 : 0));
        material.SetFloat(m_PID_startTime, startTime);
        material.SetFloat(m_PID_scale, noiseScale);

        Graphics.Blit(source, destination, material);
    }

    private void Update()
    {
        if (fadeDuration > 0f)
        {
            fadeDuration -= Time.deltaTime;
            float d = Mathf.Clamp01(fadeDuration / fadeTime);
            threshold = Easing.Ease(easeType, 1f, 0f, d);

            if(d <= 0f)
            {
                invert = !invert;
                threshold = 0;
            }
        }

        // test
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartInvert();
        }
    }

    public void StartInvert()
    {
        fadeDuration = fadeTime;
        startTime = Time.time;
    }
}
