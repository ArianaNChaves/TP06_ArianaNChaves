using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private EntitySO entityData;
    [SerializeField] private Transform hitPoint;
    [SerializeField] private LayerMask damageableLayer;
    [SerializeField] private float hitRadius;

    private float _timer;
    private int _damage;
    private Animator _animator;
    private RaycastHit2D[] _hits;
    private const float AttackAnimationDuration = 0.35f;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _damage = entityData.Damage;
        GameplayUi.Instance.UpdateDamageText(_damage);
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && _timer >= entityData.AttackRate)
        {   
            Attack(_damage);
            _timer = 0;
        }
    }
    
    private void Attack(int damage)
    {
        StartCoroutine(AttackAnimation());
        _hits = Physics2D.CircleCastAll(hitPoint.position, hitRadius, transform.right, 0f, damageableLayer);

        for (int i = 0; i < _hits.Length; i++)
        {
            if (_hits[i].collider.gameObject == this.gameObject) continue;
            IHealthHandler healthHandler = _hits[i].collider.gameObject.GetComponent<IHealthHandler>();
            healthHandler?.UpdateHealth(-damage);
        }
    }

    private IEnumerator AttackAnimation()
    {
        float resetAnim = AttackAnimationDuration;
        if (resetAnim > entityData.AttackRate)
        {
            resetAnim = entityData.AttackRate;
        }
        _animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(resetAnim);
        _animator.SetBool("isAttacking", false);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitPoint.position, hitRadius);
    }
    public int IncreaseDamage
    {
        get => _damage;
        set => _damage = value;
    }
}
