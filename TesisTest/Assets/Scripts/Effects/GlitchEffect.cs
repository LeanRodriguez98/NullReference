using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GlitchEffect : MonoBehaviour
{
    public static GlitchEffect glitchEffectInstance;

    public Texture2D displacementMap;
    public Shader Shader;
    [Header("Glitch Intensity")]

    [Range(0, 1)]
    public float intensity;

    [Range(0, 1)]
    public float flipIntensity;

    [Range(0, 1)]
    public float colorIntensity;

    private float _glitchup;
    private float _glitchdown;
    private float flicker;
    private float _glitchupTime = 0.05f;
    private float _glitchdownTime = 0.05f;
    private float _flickerTime = 0.5f;
    private Material _material;
    private bool displayGlitch = false;
    public float glitcDefaulthDuration = 0.5f;
    private void Awake()
    {
        glitchEffectInstance = this;
    }

    void Start()
    {
        _material = new Material(Shader);
    }

    public void DisplayGlitchOn()
    {
        displayGlitch = true;
        CancelInvoke("DisplayGlitchOff");
        Invoke("DisplayGlitchOff", glitcDefaulthDuration);
    }

    public void DisplayGlitchOn(float duration)
    {
        displayGlitch = true;
        CancelInvoke("DisplayGlitchOff");
        Invoke("DisplayGlitchOff", duration);
    }

    public void DisplayGlitchOff()
    {
        displayGlitch = false;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (displayGlitch)
        {
            _material.SetFloat("_Intensity", intensity);
            _material.SetFloat("_ColorIntensity", colorIntensity);
            _material.SetTexture("_DispTex", displacementMap);

            flicker += Time.deltaTime * colorIntensity;
            if (flicker > _flickerTime)
            {
                _material.SetFloat("filterRadius", Random.Range(-3f, 3f) * colorIntensity);
                _material.SetVector("direction", Quaternion.AngleAxis(Random.Range(0, 360) * colorIntensity, Vector3.forward) * Vector4.one);
                flicker = 0;
                _flickerTime = Random.value;
            }

            if (colorIntensity == 0)
                _material.SetFloat("filterRadius", 0);

            _glitchup += Time.deltaTime * flipIntensity;
            if (_glitchup > _glitchupTime)
            {
                if (Random.value < 0.1f * flipIntensity)
                    _material.SetFloat("flip_up", Random.Range(0, 1f) * flipIntensity);
                else
                    _material.SetFloat("flip_up", 0);

                _glitchup = 0;
                _glitchupTime = Random.value / 10f;
            }

            if (flipIntensity == 0)
                _material.SetFloat("flip_up", 0);

            _glitchdown += Time.deltaTime * flipIntensity;
            if (_glitchdown > _glitchdownTime)
            {
                if (Random.value < 0.1f * flipIntensity)
                    _material.SetFloat("flip_down", 1 - Random.Range(0, 1f) * flipIntensity);
                else
                    _material.SetFloat("flip_down", 1);

                _glitchdown = 0;
                _glitchdownTime = Random.value / 10f;
            }

            if (flipIntensity == 0)
                _material.SetFloat("flip_down", 1);

            if (Random.value < 0.05 * intensity)
            {
                _material.SetFloat("displace", Random.value * intensity);
                _material.SetFloat("scale", 1 - Random.value * intensity);
            }
            else
                _material.SetFloat("displace", 0);

            Graphics.Blit(source, destination, _material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
