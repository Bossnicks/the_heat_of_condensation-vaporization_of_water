using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static TaskControl;


public class NagrevScript : MonoBehaviour
{
    private Renderer objectRenderer;
    public Material newMaterial; // ����� ��������, ������� ����� ��������� ��� ��������� ����
    private Material originalMaterial;
    //public Text message;
    public TextMeshProUGUI podskazka;
    
    //Renderer renderer;
    private void Start()
    {
        //renderer = gameObject.AddComponent<MeshRenderer>();
        objectRenderer = GetComponent<Renderer>(); // �������� ��������� Renderer

        // ��������� ������������ �������� �������
        originalMaterial = objectRenderer.material;
        //Debug.Log($"{originalMaterial}");
    }
    public void ChangCol()
    {
        // ���� ����� �������� ���������, ����������� ��� ��� ��������� ����
            objectRenderer.material = newMaterial;

        //podskazka.text
        if (currentStep == Step.Step1 || currentStep == Step.Step11)
        {
            switch (gameObject.name)
            {
                case "LATR":
                    podskazka.text = "���� - ��� ���������, ������� ��������� ������������ ����������, ���������� �� ���";
                    break;
                case "Cylinder01":
                    podskazka.text = "������������� ��������� ���� �� ����������� ������� ����, � �������������� ����";
                    break;
                case "Cylinder02":
                    podskazka.text = "������������� ��������� ���� �� ����������� ������� ����, � �������������� ����";
                    break;
                case "ForColdWat":
                    podskazka.text = "�������� ��� �������� ����, ��� ���������� �������������� � ���������� �����������";
                    break;
                case "VneshniKa0":
                    podskazka.text = "� ���������� �������� ���, ������� ��������������� � �������� ����� � ��������������, ��������� ������� ����";
                    break;
                case "KonturFor0":
                    podskazka.text = "� ������ ������ ���������� ���������, ����� �������������� � �������� �����";
                    break;
                case "Tube001":
                    podskazka.text = "�������� ������������ ��� ��������� ������� �������� ����";
                    break;
                default:
                    Debug.Log("������������ �������"); // � ������, ���� elementNumber �� ��������� �� � ����� case
                    break;
            }
        }


    }

    public void ChangCol1()
    {
        objectRenderer.material = originalMaterial;
        if (currentStep == Step.Step1 || currentStep == Step.Step12)
        {
            podskazka.text = "��������� ������������ ��� ���������� ������������� ���������� � ����������� ������ � ����������, �������������� �� ������ ����������";
        }
    }
}
