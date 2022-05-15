using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieTrigger : MonoBehaviour
{

    public GameObject zombie;
    public GameObject player;
    private int status = 0;

    private Vector3 playerPosition;
    private NavMeshAgent zombieAgent;
    private bool following;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            status = 2;
        }
    }

    private void Start()
    {
        zombieAgent = zombie.GetComponent<NavMeshAgent>();
        StartCoroutine(TE());
    }

    private void Update()
    {
        if (status == 2 && following)
        {
            playerPosition = player.transform.position;
            zombieAgent.SetDestination(new Vector3(player.transform.position.x + (0.5f), player.transform.position.y, player.transform.position.z + (0.5f)));
        }

        if ((Vector3.Distance(player.transform.position, zombie.transform.position) > 10) && following)
        {
            status = 1;

        }
    }

    IEnumerator TE()
    {
        while (true)
        {
            switch (status)
            {

                case 2:
                    zombie.GetComponent<Animator>().SetBool("playerEntered", true);
                    yield return new WaitForSeconds(3);
                    zombie.GetComponent<Animator>().SetBool("following", true);
                    following = true;
                    break;

                case 1:
                    zombie.GetComponent<Animator>().SetBool("following", false);
                    zombie.GetComponent<Animator>().SetBool("outOfRange", true);
                    zombieAgent.SetDestination(zombie.transform.position);
                    following = false;
                    break;
                case 0:
                    break;

            }
            yield return null;
        }


    }

}
