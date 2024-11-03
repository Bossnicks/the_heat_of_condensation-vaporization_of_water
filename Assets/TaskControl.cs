using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TaskControl : MonoBehaviour
{
    bool isMachineActive = false;
    private TextMeshProUGUI podskazka;
    private Animation powerButtonAnimation;
    private GameObject description;
    private GameObject powerButton;
    private bool hasPlayed = false; // Проверка, была ли проиграна анимация
    public enum Step
    {
        Step1,
        Step2,
        Step3,
        Step4,
        Step5,
        Step6,
        Step7,
        Step8,
        Step9,
        Step10,
        Step11,
        Step12,
        Step13
    }
    public enum WriteStep
    {
        WriteStep1,
        WriteStep2,
        WriteStep3,
        WriteStep4,
        WriteStep5,
        WriteStep6,
    }

    public static WriteStep currentWriteStep;
    public static Step currentStep;

    private void Start()
    {
        isMachineActive = false;
        description = GameObject.Find("Description");
        podskazka = description.GetComponent<TextMeshProUGUI>();
        powerButton = GameObject.Find("PowerButton");
        powerButtonAnimation = powerButton.GetComponent<Animation>();
        currentStep = Step.Step1;
        currentWriteStep = WriteStep.WriteStep1;
    }

    private void Update()
    {
        //switch (currentStep)
        //{
        //    case Step.Step1:
        //        // Выполняем действия для шага 1
        //        currentStep = Step.Step2;
        //        break;

        //    case Step.Step2:
        //        // Выполняем действия для шага 2
        //        currentStep = Step.Step3;
        //        break;

        //    case Step.Step3:
        //        // Выполняем действия для шага 3
        //        currentStep = Step.Step4;
        //        break;

        //    case Step.Step4:
        //        // Выполняем действия для шага 4
        //        // Все шаги завершены
        //        Debug.Log("Все шаги завершены!");
        //        break;

        //    default:
        //        break;
        //}
        if (Input.GetMouseButtonDown(0))
        {
           if(CheckClick("PowerButton") && !hasPlayed)
           {
                if(currentStep == Step.Step1)
                {
                    StartCoroutine(PlayAnimationOnce());
                    powerButton.GetComponent<AudioSource>().Play();
                    hasPlayed = true;
                    currentStep = Step.Step2;
                    podskazka.text = "Откройте кран для подачи воды в напорный бак и подождите немного";
                } 
                else if (currentStep == Step.Step11)
                {
                    StartCoroutine(PlayAnimationOnce());
                    powerButton.GetComponent<AudioSource>().Play();
                    hasPlayed = true;
                    podskazka.text = "Лабораторная работа окончена";
                    currentStep = Step.Step12;
                }
                else
                {
                }
           }
           
        }
    }

    IEnumerator PlayAnimationOnce()
    {
        powerButtonAnimation.Play();
        yield return new WaitForSeconds(powerButtonAnimation.clip.length);
        hasPlayed = false;
    }

    public static bool CheckClick(string objectName)
    {
        // Создаем луч из центра экрана в место клика мыши
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Проверяем, столкнулся ли луч с объектом
        if (Physics.Raycast(ray, out hit))
        {
            // Проверяем, является ли объект целевым объектом
            if (hit.collider.gameObject.name == objectName)
            {
                // Возвращаем true, если кликнули по объекту
                return true;
            }
        }

        // Возвращаем false, если не кликнули по объекту
        return false;
    }
}


//if (Input.GetKeyDown(KeyCode.Space) && /*CheckClick("PowerButton")*/ !hasPlayed && (state == 0 || state == 7))
//{
//    StartCoroutine(PlayAnimationOnce());
//    hasPlayed = true;
//    Debug.Log("ddwd");
//    if (state == 0)
//    {
//        isMachineActive = true;
//    }
//    else
//    {
//        isMachineActive = false;
//    }
//}
////else
////{
////    podskazka.text = "В процессе работы установки нельзя отключать установку";
////}
//if (Input.GetMouseButtonDown(0))
//{
//    bool a = CheckClick("PowerButton");
//    Debug.Log(a.ToString());
//}
