using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _effect;
    [Space(5)]
    [SerializeField] private int _health = 10;
    [SerializeField] private TextMesh _healthText;
    [Space(5)]
    [SerializeField] private int _killReward = 25;

    NavMeshAgent agent;
    GameObject EndCube;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        EndCube = GameObject.FindGameObjectWithTag("EndCube");

        _healthText.text = _health.ToString();
    }

    void Update()
    {
        agent.SetDestination(EndCube.transform.position);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            GameM.instance._gold += _killReward;
            GameM.instance.UpdateGold();
            Instantiate(_effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        _healthText.text = _health.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EndCube"))
        {
            GameM.instance.TakeDamage(_health);
            Instantiate(_effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}