using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering;
public class HealthBarManager : MonoBehaviour
{
    [SerializeField] PlayerHealthManager playerHealth;
    [SerializeField] float lossFadeDuration = 0.5f;
    [SerializeField] float healFadeMultiplier = 2f;
    [SerializeField] Volume vignette;
    
    Tween fadeTween;

    const float INITIAL = 0f, 
        MAXIMUM = 1f;
    
    float starting = 0f,
        final = 0f;

    private void Start()
    {
        if(vignette == null)
        {
            Debug.Log("No se asigno la viñeta en el inspector");
        }
        if (vignette != null)
        {
            vignette.weight = 0f;
        }

        playerHealth.OnRecibeDamage.AddListener(OnHPLoss);
        playerHealth.OnStartHealing.AddListener(OnHPGain);
    }

    public void OnHPLoss()
    {
        if(fadeTween != null) fadeTween.Kill();
        
        //starting = vignette.weight;
        //Mathf.Clamp(starting, INITIAL, MAXIMUM);
        
        final = (INITIAL + (1 - (playerHealth.CURHP / playerHealth.MAXHP)));
        Mathf.Clamp(final, INITIAL, MAXIMUM);
        
        fadeTween = DOTween.To(()=> vignette.weight, x => vignette.weight = x, final, lossFadeDuration);
    }

    public void OnHPGain()
    {
        if (fadeTween != null) fadeTween.Kill();

        float duration = (playerHealth.CURHP / playerHealth.MAXHP) * healFadeMultiplier;

        fadeTween = DOTween.To(() => vignette.weight, x => vignette.weight = x, INITIAL, duration);

    }
}
