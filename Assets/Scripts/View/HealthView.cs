using System;
using System.Collections;
using Events;
using TMPro;
using UnityEngine;
using VContainer;

public class HealthView : MonoBehaviour
{
    [Inject] private IReadOnlyEventBus eventBus;
    TextMeshProUGUI healthText;
    private Coroutine animationCoroutine;
    
    [SerializeField] public float showTimeSec = 3f;

    private void Awake()
    {
        healthText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        eventBus.Subscribe<HealthUpdateEvent>(ShowHealth);
    }

    private void OnDisable()
    {
        eventBus.Unsubscribe<HealthUpdateEvent>(ShowHealth);
    }
    
    private void ShowHealth(HealthUpdateEvent healthEvent)
    {
        healthText.text = $"{(int)healthEvent.curHealth} hp";
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
        animationCoroutine = StartCoroutine(Animate(showTimeSec));
    }

    private IEnumerator Animate(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        healthText.text = "";
        animationCoroutine = null;  
    }
}
