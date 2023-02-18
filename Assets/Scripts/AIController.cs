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
    public List<bool> agentEnable;
    // Start is called before the first frame update
    void Start()
    {

        // meshAgent = agent.GetComponent<NavMeshAgent>();
        // meshAgent.destination = goal.transform.position;
        Physics.IgnoreLayerCollision(this.gameObject.layer, this.gameObject.layer);

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
                int ind = random.Next(i, goals.Length);
                GameObject temp = goals[i];
                goals[i] = goals[ind];
                goals[ind] = temp;
            }
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].GetComponent<Agent>().Stand();
                agents[i].GetComponent<NavMeshAgent>().destination = goals[i].transform.position;

                StartCoroutine(standWait(i));
            }
        }
        if (Input.GetKeyDown(spawn))
        {
            agents.Add(Instantiate(prefab, this.transform.position, this.transform.rotation));
            agents[agents.Count - 1].gameObject.transform.SetParent(this.transform);
            agentEnable.Add(true);

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
        int index = 0;
        foreach (GameObject ag in agents)
        {
            if (agentEnable[index] && ag.GetComponent<NavMeshAgent>().enabled && !ag.GetComponent<NavMeshAgent>().pathPending)
            {
                if (ag.GetComponent<NavMeshAgent>().remainingDistance <= ag.GetComponent<NavMeshAgent>().stoppingDistance)
                {
                    if (!ag.GetComponent<Agent>().sitting && !ag.GetComponent<Agent>().standing && ag.GetComponent<NavMeshAgent>().velocity.sqrMagnitude == 0f)
                    {
                        ag.GetComponent<Agent>().Sit();

                    }
                }
            }
            index++;
        }
    }
    IEnumerator standWait(int index)
    {
        agentEnable[index] = false;
        yield return new WaitForSeconds(0.1f);
        agentEnable[index] = true;
    }
}
