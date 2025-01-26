using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] string playerName;
    [Range(1, 2)]
    [SerializeField] int index;
    [SerializeField] float movementSpeed;
    [SerializeField] float attackDelay;
    [SerializeField] int life;
    [SerializeField] ParticleSystem onHitEffect;
    [SerializeField] Material onHitMaterial;
    [SerializeField] SkinnedMeshRenderer meshRenderer;
    [SerializeField] List<GameObject> lifeIcons = new();

    public static UnityAction<int, Bullet> OnShoot;
    public static UnityAction<int, Bullet> OnHit;
    private int maxLife;
    private PlayerMovementManager playerMovementManager;


    public void Awake()
    {
        maxLife = life;
        playerMovementManager = GetComponentInChildren<PlayerMovementManager>();
        playerMovementManager.Init(index, movementSpeed);
        GetComponentInChildren<PlayerAnimations>().Init(index);
        GetComponentInChildren<PlayerAttackManager>().Init(index, attackDelay);
        GetComponentInChildren<OxygenRenderer>().Init(index);
    }

    public void Reset()
    {
        life = maxLife;
        playerMovementManager.Reset();
        ShowCurrentLife();
    }

    public void Hit(Bullet bullet)
    {
        if (GameManager.instance.gamePaused)
            return;

        Debug.Log($"player took damage {bullet.Damage}");
        life -= bullet.Damage;

        ShowCurrentLife();

        Instantiate(onHitEffect, bullet.transform.position, Quaternion.identity);
        StartCoroutine(ApplyOnHitMaterial());

        OnHit?.Invoke(index, bullet);

        if (life <= 0)
        {
            RoundsManager.instance.SetRoundLoser(index);
        }
    }

    private void ShowCurrentLife()
    {
        for (int i = 0; i < lifeIcons.Count; i++)
        {
            lifeIcons[i].SetActive(i < life);
        }
    }

    private IEnumerator ApplyOnHitMaterial()
    {
        var defaultMaterials = meshRenderer.materials;
        var materialsCount = meshRenderer.materials.Length;
        var newMaterials = new Material[materialsCount];

        for (int i = 0; i < materialsCount; i++)
        {
            newMaterials[i] = onHitMaterial;
        }

        meshRenderer.materials = newMaterials;
        yield return new WaitForSeconds(0.2f);
        meshRenderer.materials = defaultMaterials;
    }
}
