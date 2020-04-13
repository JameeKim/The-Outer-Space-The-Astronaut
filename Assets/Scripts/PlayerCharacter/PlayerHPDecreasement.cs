using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CombatEntity))]
public class PlayerHPDecreasement : MonoBehaviour
{
    public int hpDecreasingValue = 1;
    public float decreasingInterval = 1.0f;

    private CombatEntity combatEntity;

    private void Start()
    {
        combatEntity = GetComponent<CombatEntity>();
        StartCoroutine(HealthDecreaseCorutine(decreasingInterval));
    }

    private IEnumerator HealthDecreaseCorutine(float interval)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(interval);

        while (true)
        {
            combatEntity.DecreasePlayerHealth(hpDecreasingValue);
            yield return waitForSeconds;
        }
    }
}
