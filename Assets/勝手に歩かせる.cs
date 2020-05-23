using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class 勝手に歩かせる : MonoBehaviour
{
    private NavMeshAgent NavMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        var agent = this.NavMeshAgent=this.GetComponent<NavMeshAgent>();
        var Sphere=GameObject.Find("Sphere");
        //agent.SetDestination(Sphere.transform.position);
        //agent.Move(new Vector3(10,10));
        //agent.transform.position=Sphere.transform.position;//瞬間移動
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            RaycastHit hit;

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit,100)) {
                if(this.NavMeshAgent.pathStatus!=NavMeshPathStatus.PathInvalid) {
                    this.NavMeshAgent.SetDestination(hit.point);
                }
            }
        }
    }
}
