using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegulAnimScript : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim; 			      //���������� ���� Animator ��� ������ �� ��������
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();   //������������� ����������� �������� 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { anim.SetTrigger("Hitten"); }
        //Debug.Log("ds");
    }
}
