
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.AI;

public class Test : MonoBehaviour
{



    private NavMeshAgent agent;//寻路者

    public Transform target;//寻路目标



    private void Start()

    {

        agent = this.GetComponent<NavMeshAgent>();

    }



    private void Update()

    {

        if (agent != null)

        {

            agent.SetDestination(target.position);//寻路算法

        }

    }

}
