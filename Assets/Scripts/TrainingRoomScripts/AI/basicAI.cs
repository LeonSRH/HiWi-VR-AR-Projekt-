using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson {
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class basicAI : MonoBehaviour {
        public enum State {
            PATROL,
            CHASE
        }

        bool alive;


        // Vars for Chasing
        public float chaseSpeed = 1f;
        public float patrolSpeed = 0.5f;

        public State state;
        public GameObject target;
        int waypointInd;


        // Vars for Patrolling
        public GameObject[] waypoints;

        public NavMeshAgent agent { get; private set; } // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling

        // Use this for initialization
        void Start() {
            agent = GetComponentInChildren<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updatePosition = true;
            agent.updateRotation = false;

            state = State.PATROL;

            alive = true;

            StartCoroutine(FSM());
        }

        IEnumerator FSM() {
            while (alive) {
                switch (state) {
                    case State.PATROL:
                        Patrol();
                        break;
                    case State.CHASE:
                        Chase();
                        break;
                }

                yield return null;
            }
        }

        void Patrol() {
            agent.speed = patrolSpeed;
            if (Vector3.Distance(transform.position, waypoints[waypointInd].transform.position) >= 2) {
                agent.SetDestination(waypoints[waypointInd].transform.position);
                character.Move(agent.desiredVelocity, false, false);
            }
            else if (Vector3.Distance(transform.position, waypoints[waypointInd].transform.position) <= 2) {
                waypointInd += 1;
                if (waypointInd > waypoints.Length) {
                    waypointInd = 0;
                }
            }
            else {
                character.Move(Vector3.zero, false, false);
            }
        }

        void Chase() {
            agent.speed = chaseSpeed;
            agent.SetDestination(target.transform.position);
            character.Move(agent.desiredVelocity, false, false);
        }

        void OnTriggerEnter(Collider coll) {
            if (coll.tag == "Player") {
                state = State.CHASE;
                target = coll.gameObject;
            }
        }
    }
}