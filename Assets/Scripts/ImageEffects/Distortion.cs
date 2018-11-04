using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Distortion : MonoBehaviour {

    const string SHADER_NAME = "Hidden/Distortion";

    [Range(0,10)]
    public float noiseScale = 0.1f;
    public Vector3 noiseSpeed = Vector3.one;
    [Range(0,1)]
    public float power = 0.1f;

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

    private Vector3 noisePosition;

    int m_PID_noiseScale = 1;
    int m_PID_noisePosition = 1;
    int m_PID_power = 1;

    private void Awake()
    {
        m_PID_noiseScale = Shader.PropertyToID("_DistortionNoiseScale");
        m_PID_noisePosition = Shader.PropertyToID("_DistortionNoisePosition");
        m_PID_power = Shader.PropertyToID("_DistortionPower");
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat(m_PID_noiseScale, noiseScale);
        material.SetVector(m_PID_noisePosition, noisePosition);
        material.SetFloat(m_PID_power, power);
        Graphics.Blit(source, destination, material);
    }

    private void Update()
    {
        float dt = Time.deltaTime;
        noisePosition.x += dt * noiseSpeed.x;
        noisePosition.y += dt * noiseSpeed.y;
        noisePosition.z += dt * noiseSpeed.z;
    }
}
