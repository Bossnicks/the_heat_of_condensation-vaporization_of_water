using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static TaskControl;


public class NagrevScript : MonoBehaviour
{
    private Renderer objectRenderer;
    public Material newMaterial; // Новый материал, который нужно присвоить при наведении мыши
    private Material originalMaterial;
    //public Text message;
    public TextMeshProUGUI podskazka;
    
    //Renderer renderer;
    private void Start()
    {
        //renderer = gameObject.AddComponent<MeshRenderer>();
        objectRenderer = GetComponent<Renderer>(); // Получаем компонент Renderer

        // Сохраняем оригинальный материал объекта
        originalMaterial = objectRenderer.material;
        //Debug.Log($"{originalMaterial}");
    }
    public void ChangCol()
    {
        // Если новый материал определен, присваиваем его при наведении мыши
            objectRenderer.material = newMaterial;

        //podskazka.text
        if (currentStep == Step.Step1 || currentStep == Step.Step11)
        {
            switch (gameObject.name)
            {
                case "LATR":
                    podskazka.text = "ЛАТР - это компонент, который позволяет регулировать напряжение, подаваемое на ТЭН";
                    break;
                case "Cylinder01":
                    podskazka.text = "Парогенератор нагревает воду до температуры кипения воду, с использованием ТЭНа";
                    break;
                case "Cylinder02":
                    podskazka.text = "Парогенератор нагревает воду до температуры кипения воду, с использованием ТЭНа";
                    break;
                case "ForColdWat":
                    podskazka.text = "Напорный бак содержит воду, для заполнения парогенератора и охлаждения калориметра";
                    break;
                case "VneshniKa0":
                    podskazka.text = "В калориметр подается пар, который взаимодействует с холодной водой и конденсируется, передавая теплоту воде";
                    break;
                case "KonturFor0":
                    podskazka.text = "В мерный стакан собирается конденсат, после взаимодействия с холодной водой";
                    break;
                case "Tube001":
                    podskazka.text = "Ротаметр используется для измерения расхода холодной воды";
                    break;
                default:
                    Debug.Log("Недопустимый элемент"); // В случае, если elementNumber не совпадает ни с одним case
                    break;
            }
        }


    }

    public void ChangCol1()
    {
        objectRenderer.material = originalMaterial;
        if (currentStep == Step.Step1 || currentStep == Step.Step12)
        {
            podskazka.text = "Симулятор предназначен для проведения лабораторного практикума в виртуальном режиме с установкой, представленной на экране компьютера";
        }
    }
}
