using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class PetOnlyPatrol : MonoBehaviour
{
    public Vector3 basePosition;
    public float patrolDistance = 10f;
    public float stopDelay = 2f;
    public NavMeshAgent navMeshAgent;
    public Animator anim;
    public bool isGrabbed = false;
    private Coroutine patrolCoroutine;
    void Start()
    {
        // 초기 basePosition이 설정되지 않았다면 현재 위치로 설정
        if (basePosition == Vector3.zero)
            basePosition = transform.position;

        if (navMeshAgent == null)
            navMeshAgent = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();
        // 순찰 루틴 시작
        patrolCoroutine = StartCoroutine(PatrolRoutine());

    }

    public void PetGrabIn() //vr selected 넣는용
    {
        isGrabbed = true;
    }
    public void PetGrabOut() //Vr selected exit
    {
        isGrabbed = false;
    }
    void HandleGrabState()
    {
        if (isGrabbed)
        {
            // 잡힌 상태: AI 이동 정지 및 경로 초기화
            if (navMeshAgent.enabled)
            {
                navMeshAgent.isStopped = true;
                navMeshAgent.enabled = false;
            }
        }
        else
        {
            // 놓아준 상태: NavMeshAgent 다시 활성화
            if (!navMeshAgent.enabled)
            {
                navMeshAgent.enabled = true;
                // 땅에 착지한 후 에이전트가 다시 작동하도록 위치 보정
                NavMeshHit hit;
                if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
                {
                    navMeshAgent.Warp(hit.position);
                }
            }
        }
    }
    void Update()
    {
        // 1. 잡혔을 때와 놓았을 때의 처리
        HandleGrabState();

        if(navMeshAgent.hasPath)
        {
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
    }
    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            // 2. 잡혀있는 동안은 여기서 대기 (이동 로직 실행 안 함)
            if (isGrabbed)
            {
                yield return new WaitUntil(() => !isGrabbed);
                yield return new WaitForSeconds(0.5f); // 내려놓고 잠시 후 이동 시작
            }

            // 3. 랜덤 목적지 설정
            Vector3 nextDestination = GetRandomPosition(basePosition, patrolDistance);

            if (navMeshAgent.enabled)
            {
                navMeshAgent.SetDestination(nextDestination);
            }

            // 4. 도착 확인 (잡히면 루프 탈출 후 위쪽 WaitUntil에서 대기)
            while (!isGrabbed && (navMeshAgent.pathPending || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance))
            {
                yield return null;
            }

            // 5. 도착 후 대기
            if (!isGrabbed)
            {
                yield return new WaitForSeconds(stopDelay);
            }
        }
    }

    Vector3 GetRandomPosition(Vector3 center, float distance)
    {
        Vector2 randomPoint = Random.insideUnitCircle * distance;
        Vector3 targetPos = new Vector3(randomPoint.x, 0, randomPoint.y) + center;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPos, out hit, 5.0f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return center;
    }
}
