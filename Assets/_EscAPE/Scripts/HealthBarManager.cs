using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class HealthBarManager : MonoBehaviour
{
    Tween fadeTween;

    [SerializeField] Image HealthBar;
    [SerializeField] PlayerHealthManager playerHealth;
    [SerializeField] float lossFadeDuration = 0.5f;
    [SerializeField] float healFadeMultiplier = 2f;
    const float INITIAL = 0f, 
        MAXIMUM = 85f;
    
    float starting = 0f,
        final = 0f;

    private void Start()
    {
        playerHealth.OnRecibeDamage.AddListener(OnHPLoss);
        playerHealth.OnStartHealing.AddListener(OnHPGain);
    }

    public void OnHPLoss()
    {
        if(fadeTween != null) fadeTween.Kill();
        
        starting = HealthBar.color.a;
        Mathf.Clamp(starting, INITIAL, MAXIMUM);
        
        final = (starting - ((playerHealth.CURHP / playerHealth.MAXHP) * 100));
        Mathf.Clamp(final, INITIAL, MAXIMUM);
        
        fadeTween = HealthBar.DOFade(final, lossFadeDuration);
    }

    public void OnHPGain()
    {
        if(fadeTween != null) fadeTween.Kill();

        float duration = (playerHealth.CURHP / playerHealth.MAXHP) * healFadeMultiplier;

        fadeTween = HealthBar.DOFade(INITIAL, duration);
    }
}
