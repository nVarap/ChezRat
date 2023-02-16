using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI;

public class AIController : MonoBehaviour
{

    public GameObject prefab;
    public KeyCode spawn = KeyCode.Q;
    public KeyCode randomizeGoals = KeyCode.R;
    public List<GameObject> agents;
    public GameObject[] goals;
    // Start is called before the first frame update
    void Start()
    {
        // meshAgent = agent.GetComponent<NavMeshAgent>();
        // meshAgent.destination = goal.transform.position;

        goals = GameObject.FindGameObjectsWithTag("Chair");

        Debug.Log(goals.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(randomizeGoals))
        {
            System.Random random = new System.Random();
            for (int i = 0; i < goals.Length; i++)
            {
                int index = random.Next(i, goals.Length);
                GameObject temp = goals[i];
                goals[i] = goals[index];
                goals[index] = temp;
            }
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].GetComponent<NavMeshAgent>().destination = goals[i].transform.position;
                agents[i].GetComponent<Agent>().Stand();
            }
        }
        if (Input.GetKeyDown(spawn))
        {
            agents.Add(Instantiate(prefab, this.transform.position, this.transform.rotation));
            if (agents[agents.Count - 1].GetComponent<NavMeshAgent>() == null)
            {
                agents[agents.Count - 1].AddComponent<NavMeshAgent>();
            }
            else
            {
                Debug.Log(agents.Count - 1);
                Debug.Log(goals[agents.Count - 1].name);
                agents[agents.Count - 1].GetComponent<NavMeshAgent>().destination = goals[agents.Count - 1].transform.position;
            }
        }
        foreach (GameObject ag in agents)
        {
            if (!ag.GetComponent<NavMeshAgent>().pathPending)
            {
                if (ag.GetComponent<NavMeshAgent>().remainingDistance <= ag.GetComponent<NavMeshAgent>().stoppingDistance)
                {
                    if (!ag.GetComponent<Agent>().sitting && ag.GetComponent<NavMeshAgent>().velocity.sqrMagnitude == 0f)
                    {
                        ag.GetComponent<Agent>().Sit();
                    }
                }
            }
        }
    }
}
