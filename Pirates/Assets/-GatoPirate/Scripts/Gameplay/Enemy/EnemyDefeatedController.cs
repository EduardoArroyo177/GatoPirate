using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class EnemyDefeatedController : MonoBehaviour
{
    [Header("Main ship")]
    [SerializeField]
    private GameObject enemyShip;

    [Header("Animation")]
    [SerializeField]
    private float explosionsDelay;
    [SerializeField]
    private GameObject[] explosionParentList;
    [SerializeField]
    private GameObject finalExplosion;

    public VoidEvent TriggerEnemyLostAnimationEvent { get; set; }
    public CharacterTypeEvent ShowResultScreenEvent { get; set; }

    private List<IAtomEventHandler> _eventHandlers = new List<IAtomEventHandler>();

    public void Initialize()
    {
        _eventHandlers.Add(EventHandlerFactory.BuildEventHandler(TriggerEnemyLostAnimationEvent, TriggerEnemyLostAnimationEventCallback));
    }

    private void TriggerEnemyLostAnimationEventCallback(Void _item)
    {
        StartCoroutine("ExplosionParticlesAnimation");
    }

    private IEnumerator ExplosionParticlesAnimation()
    {
        GameObject explosionHelper;

        for (int index = 0; index < explosionParentList.Length; index++)
        {
            explosionHelper = ObjectPooling.Instance.GetNormalProjectileExplosionParticle();
            if (explosionHelper)
            {
                explosionHelper.transform.localScale = new Vector3(3, 3, 3);
                explosionHelper.transform.position = explosionParentList[index].transform.position;
                explosionHelper.SetActive(true);
            }
            yield return new WaitForSeconds(explosionsDelay);
        }
        explosionHelper = ObjectPooling.Instance.GetSpecialProjectileExplosionParticle();
        if (explosionHelper)
        {
            explosionHelper.transform.localScale = new Vector3(3, 3, 3);
            explosionHelper.transform.position = finalExplosion.transform.position;
            explosionHelper.SetActive(true);
        }
        enemyShip.SetActive(false);
        // Trigger show result screen (Player won)
        ShowResultScreenEvent.Raise(CharacterType.PLAYER);
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
