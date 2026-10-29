using UnityEngine;
using UnityEngine.AI;

public class NavMeshTank : MonoBehaviour
{

    public Transform helipad;
    public Transform ruins;

    NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToHeli()
    {
        if (helipad != null)
            agent.SetDestination(helipad.position);
    }

    public void GoToRuin()
    {
        if (ruins != null)
            agent.SetDestination(ruins.position);
    }
}
