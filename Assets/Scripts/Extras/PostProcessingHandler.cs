using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingHandler : MonoBehaviour
{
    public static PostProcessingHandler instance;

    public Volume postProcessVolume;
    private Bloom bloom;
    private Vignette vignette;
    private WhiteBalance whiteBalance;

    [Header("Transition Settings")]
    public float transitionDuration = 2f; // Duration in seconds for the transition

    [Header("Noon Settings")]
    public float NoonBloomIntensity;
    public float NoonBloomScatter;
    [Space(5)]
    public string NoonVignetteColor; // Accepts hex code
    public float NoonVignetteIntensity;
    [Space(5)]
    public float NoonWhiteBalanceTemp;
    public float NoonWhiteBalanceTint;

    [Space(10)]
    [Header("Evening Settings")]
    public float EveningBloomIntensity;
    public float EveningBloomScatter;
    [Space(5)]
    public string EveningVignetteColor; // Accepts hex code
    public float EveningVignetteIntensity;
    [Space(5)]
    public float EveningWhiteBalanceTemp;
    public float EveningWhiteBalanceTint;

    [Space(10)]
    [Header("Dusk Settings")]
    public float DuskBloomIntensity;
    public float DuskBloomScatter;
    [Space(5)]
    public string DuskVignetteColor; // Accepts hex code
    public float DuskVignetteIntensity;
    [Space(5)]
    public float DuskWhiteBalanceTemp;
    public float DuskWhiteBalanceTint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            postProcessVolume = GetComponent<Volume>();

            if (postProcessVolume.profile.TryGet(out bloom) &&
                postProcessVolume.profile.TryGet(out vignette) &&
                postProcessVolume.profile.TryGet(out whiteBalance))
            {
                // Successfully retrieved settings
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NoonEffect()
    {
        StartCoroutine(TransitionEffects(
            bloom.intensity.value, NoonBloomIntensity,
            bloom.scatter.value, NoonBloomScatter,
            vignette.color.value, HexToColor(NoonVignetteColor),
            vignette.intensity.value, NoonVignetteIntensity,
            whiteBalance.temperature.value, NoonWhiteBalanceTemp,
            whiteBalance.tint.value, NoonWhiteBalanceTint));
    }

    public void EveningEffect()
    {
        StartCoroutine(TransitionEffects(
            bloom.intensity.value, EveningBloomIntensity,
            bloom.scatter.value, EveningBloomScatter,
            vignette.color.value, HexToColor(EveningVignetteColor),
            vignette.intensity.value, EveningVignetteIntensity,
            whiteBalance.temperature.value, EveningWhiteBalanceTemp,
            whiteBalance.tint.value, EveningWhiteBalanceTint));
    }

    public void DuskEffect()
    {
        StartCoroutine(TransitionEffects(
            bloom.intensity.value, DuskBloomIntensity,
            bloom.scatter.value, DuskBloomScatter,
            vignette.color.value, HexToColor(DuskVignetteColor),
            vignette.intensity.value, DuskVignetteIntensity,
            whiteBalance.temperature.value, DuskWhiteBalanceTemp,
            whiteBalance.tint.value, DuskWhiteBalanceTint));
    }

    private IEnumerator TransitionEffects(
        float startBloomIntensity, float targetBloomIntensity,
        float startBloomScatter, float targetBloomScatter,
        Color startVignetteColor, Color targetVignetteColor,
        float startVignetteIntensity, float targetVignetteIntensity,
        float startWhiteBalanceTemp, float targetWhiteBalanceTemp,
        float startWhiteBalanceTint, float targetWhiteBalanceTint)
    {
        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            float t = elapsed / transitionDuration;

            // Bloom interpolation
            bloom.intensity.value = Mathf.Lerp(startBloomIntensity, targetBloomIntensity, t);
            bloom.scatter.value = Mathf.Lerp(startBloomScatter, targetBloomScatter, t);

            // Vignette interpolation
            vignette.color.value = Color.Lerp(startVignetteColor, targetVignetteColor, t);
            vignette.intensity.value = Mathf.Lerp(startVignetteIntensity, targetVignetteIntensity, t);

            // White Balance interpolation
            whiteBalance.temperature.value = Mathf.Lerp(startWhiteBalanceTemp, targetWhiteBalanceTemp, t);
            whiteBalance.tint.value = Mathf.Lerp(startWhiteBalanceTint, targetWhiteBalanceTint, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure final values are set at the end
        bloom.intensity.value = targetBloomIntensity;
        bloom.scatter.value = targetBloomScatter;
        vignette.color.value = targetVignetteColor;
        vignette.intensity.value = targetVignetteIntensity;
        whiteBalance.temperature.value = targetWhiteBalanceTemp;
        whiteBalance.tint.value = targetWhiteBalanceTint;
    }

    private Color HexToColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color color))
        {
            return color;
        }
        return Color.black; // Default if hex parsing fails
    }
}
