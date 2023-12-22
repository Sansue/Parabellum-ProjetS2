using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : MonoBehaviour
{
    public GameObject Bird;
    public GameObject Wolf;
    public GameObject Projectile;
    private BossShamanMain bossShamanMain;
    private bool Summoned = false;
    private bool startShootingOnce = false;
    private GameObject currentBird;
    private GameObject currentWolf;
    public Transform firePos;
    public Transform spawnWolf;
    public Transform spawnBird;
    private Animator animator;
    public float shootCooldown = 5f;
    public float summonCooldown = 10f;
    public bool canShoot = true;
    public bool canSummon = true;
    private Coroutine summonCoroutine; // Ajout de la variable pour stocker la coroutine

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        bossShamanMain = GetComponent<BossShamanMain>();
        GetComponent<Animator>().SetBool("Summon", true);
        summonCoroutine = StartCoroutine(SummonEnemy(Wolf, Bird)); // Stocke la référence à la coroutine
    }

    private void Update()
    {
        if (bossShamanMain.evolveOnce && !startShootingOnce)
        {
            Debug.Log("start shooting");
            startShootingOnce = true;
            StartCoroutine(fireProjectile(Projectile));
        }

        if (currentBird == null && currentWolf == null && Summoned && !bossShamanMain.isEvolving && canSummon)
        {
            if (summonCoroutine != null)
            {
                StopCoroutine(summonCoroutine); // Arrête la coroutine en cours
            }
            summonCoroutine = StartCoroutine(SummonAfterDelay(summonCooldown, Wolf, Bird)); // Stocke la référence à la nouvelle coroutine
        }
    }

    private IEnumerator SummonEnemy(GameObject enemy, GameObject enemy2)
    {
        while (true)
        {
            if (!Summoned)
            {
                Summoned = true;

                AnimationClip summonAnimationClip = GetSummonAnimationClip();
                yield return StartCoroutine(WaitForSummonAnimation(summonAnimationClip.length, enemy, enemy2));

                Summoned = false;
            }

            yield return null;
        }
    }

    private IEnumerator SummonAfterDelay(float cd, GameObject enemy, GameObject enemy2)
    {
        yield return new WaitForSeconds(cd);
        animator.SetBool("Summon", true);
        Summoned = true;

        AnimationClip summonAnimationClip = GetSummonAnimationClip();
        yield return StartCoroutine(WaitForSummonAnimation(summonAnimationClip.length, enemy, enemy2));

        Summoned = false;
    }

    private IEnumerator WaitForSummonAnimation(float animationLength, GameObject enemy, GameObject enemy2)
    {
        yield return new WaitForSeconds(animationLength);
        currentWolf = Instantiate(enemy, spawnWolf.position, Quaternion.identity);
        currentBird = Instantiate(enemy2, spawnBird.position, Quaternion.identity);
        animator.SetBool("Summon", false);
    }

    private AnimationClip GetSummonAnimationClip()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "Summon")
            {
                return clip;
            }
        }
        return null;
    }

    private IEnumerator fireProjectile(GameObject projectile)
    {
        while (canShoot)
        {
            yield return new WaitForSeconds(shootCooldown);
            Instantiate(projectile, firePos.position, Quaternion.identity);
        }
    }
}
