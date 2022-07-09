using System;
using System.Collections;
using UnityEngine;

public enum BossState
{
    Idle,
    Wandering,
    BoxThrow,
    GunShoot
}

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] private float idleProbability,
                                   wanderingProbability,
                                   throwProbability,
                                   shootProbability;
    
    [SerializeField] private int bossHealth;
    private BossState state;
    private BossPattern pattern;

    public void BossAction()
    {
        switch (state)
        {
            case BossState.Idle:
                StartCoroutine(pattern.IdlePattern());
                break;
            case BossState.Wandering:
                StartCoroutine(pattern.WanderPattern());
                break;
            case BossState.BoxThrow:
                StartCoroutine(pattern.ThrowPattern());
                break;
            case BossState.GunShoot:
                StartCoroutine(pattern.ShootPattern());
                break;
            default:
                throw new ArgumentOutOfRangeException("Invalid State");
        }
    }

    public void Damage(int damage)
    {
        bossHealth -= damage;
        if (bossHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        state = BossState.Idle;
        pattern = GetComponent<BossPattern>();
        StartCoroutine("DeterminePattern");
    }

    private IEnumerator DeterminePattern()
    {
        while (true)
        {
            float actionVar = UnityEngine.Random.Range(0f, 1f);

            if (actionVar <= idleProbability)
            {
                state = BossState.Idle;
            }
            else if (actionVar <= idleProbability + wanderingProbability)
            {
                state = BossState.Wandering;
            }
            else if (actionVar <= idleProbability + wanderingProbability + throwProbability)
            {
                state = BossState.BoxThrow;
            }
            else
            {
                state = BossState.GunShoot;
            }

            BossAction();

            yield return new WaitForSeconds(pattern.patternDuration);
        }
    }
}
