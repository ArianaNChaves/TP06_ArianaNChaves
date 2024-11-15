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

    private void Start()
    {
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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(hitPoint.position, hitRadius);
        foreach (var obj in colliders)
        {
            if (obj.gameObject == this.gameObject) continue;
            if(!Utilities.CompareLayerAndMask(obj.gameObject.layer, damageableLayer)) continue;
            
            IHealthHandler healthHandler = obj.gameObject.GetComponent<IHealthHandler>();
            if (healthHandler == null) return;
            
            healthHandler.UpdateHealth(-damage); //No es costoso el metodo, tiene un debug adentro.
        }
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
