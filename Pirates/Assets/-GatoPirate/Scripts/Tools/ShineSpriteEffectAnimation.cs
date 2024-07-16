using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShineSpriteEffectAnimation : MonoBehaviour
{
    [SerializeField]
    private float animationDuration;
    [SerializeField]
    private float shineReversePauseDuration;
    [SerializeField]
    private float loopDuration;
    [SerializeField]
    private bool automaticActivation;

    private Material currentMaterial;

    private void Awake()
    {
        if(GetComponent<Image>() != null)
            currentMaterial = GetComponent<Image>().material;
        else if(GetComponent<SpriteRenderer>() != null)
            currentMaterial = GetComponent<SpriteRenderer>().material;

        if (automaticActivation)
            StartShineAnimation();
    }

    public void StartShineAnimation()
    {
        StartCoroutine("ShineAnimation");
    }

    public void StopAnimation()
    {
        StopCoroutine("ShineAnimation");
        currentMaterial.SetFloat("_ShineLocation", 0);
    }

    private IEnumerator ShineAnimation()
    {
        float timer;
        float shineLocation;
        while (true)
        {
            timer = 0;
            while (timer < animationDuration)
            {
                float progress = timer / animationDuration;
                shineLocation = Mathf.Lerp(0, 1, progress);
                currentMaterial.SetFloat("_ShineLocation", shineLocation);
                yield return null;
                timer += Time.deltaTime;
            }
            currentMaterial.SetFloat("_ShineLocation", 1);
            yield return new WaitForSeconds(shineReversePauseDuration);
            timer = 0;

            while (timer < animationDuration)
            {
                float progress = timer / animationDuration;
                shineLocation = Mathf.Lerp(1, 0, progress);
                currentMaterial.SetFloat("_ShineLocation", shineLocation);
                yield return null;
                timer += Time.deltaTime;
            }
            currentMaterial.SetFloat("_ShineLocation", 0);
            yield return new WaitForSeconds(loopDuration);
        }
    }

}
