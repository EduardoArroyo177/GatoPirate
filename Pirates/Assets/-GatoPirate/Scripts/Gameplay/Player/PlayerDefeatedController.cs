using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PlayerDefeatedController : MonoBehaviour
{
    [SerializeField]
    private GameObject finalExplosionOrigin;
    [SerializeField]
    private float animationDelay;
    [SerializeField]
    private DOTweenAnimation doTweenAnimation;
    // TODO: Add needed references for cats animations

    public VoidEvent TriggerPlayerLostAnimationEvent { get; set; }
    public CharacterTypeEvent ShowResultScreenEvent { get; set; }
    public FloatEvent TriggerShakingCameraEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerPlayerLostAnimationEvent, TriggerPlayerLostAnimationEventCallback));
    }

    private void TriggerPlayerLostAnimationEventCallback(Void _item)
    {
        StartCoroutine("DefeatedAnimation");
    }

    private IEnumerator DefeatedAnimation()
    {
        GameObject explosionHelper;
        yield return new WaitForSeconds(animationDelay);
        explosionHelper = ObjectPooling.Instance.GetSpecialProjectileExplosionParticle();
        if (explosionHelper)
        {
            explosionHelper.transform.position = finalExplosionOrigin.transform.position;
            explosionHelper.SetActive(true);
        }
        TriggerShakingCameraEvent.Raise(1.0f);
        VibrationController.Instance.TriggerReceiveSpecialAttackVibration(0.5f);
        yield return new WaitForSeconds(animationDelay);
        // TODO: Trigger cats faces animations
        // Trigger dotween animation
        
        doTweenAnimation.DOPlayAllById("Defeated");
    }

    public void DefeatedAnimationFinished()
    {
        ShowResultScreenEvent.Raise(CharacterType.ENEMY);
    }

    private void OnDestroy()
    {
        foreach (var item in _eventHandlers)
        {
            item.UnregisterListener();
        }

        _eventHandlers.Clear();
    }
}
