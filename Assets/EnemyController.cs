using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform playerObj;
    protected NavMeshAgent enemyMesh;
    // Start is called before the first frame update
    void Start()
    {
        enemyMesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIManager.isGameStarted)
        {
            return; // ������� �� ������ Update, ���� ���� ��� �� ��������
        }
        enemyMesh.SetDestination(playerObj.position);
    }
}
