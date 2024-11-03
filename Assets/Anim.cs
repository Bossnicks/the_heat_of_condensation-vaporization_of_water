using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.Mime.MediaTypeNames;

public class Anim : MonoBehaviour
{
    Animator anim;                //переменная типа Animator для ссылки на анимацию
                                  // Start is called before the first frame update
    private TextMeshProUGUI podskazka;
    private GameObject voltageObj;
    private TextMeshPro voltage;
    private GameObject tx1Obj;
    private TextMeshPro tx1;
    private GameObject tx2Obj;
    private TextMeshPro tx2;
    private GameObject tvgObj;
    private TextMeshPro tvg;
    private bool isOpening = false;
    private bool isOpened = false;
    private float openTime = 0f;
    private bool isOpeningCalorCran = false;
    private bool isOpenedCalorCran = false;
    private float openTimeCalorCran = 0f;
    private bool isCooldownNagrev = false;
    private GameObject description;
    private GameObject waterDrop;
    private ParticleSystem waterDropParticleSystem;
    private double rashodCalorSredn;
    private GameObject textField;
    private TMP_InputField textFieldtext;
    private GameObject coldWaterBox;
    private GameObject connToObm;
    private GameObject cran;
    private GameObject regul;
    private GameObject vneshniCalor;
    private GameObject waterCylinder;








    void Start()
    {
        anim = GetComponent<Animator>();   //инициализация контроллера анимации
        description = GameObject.Find("Description");
        podskazka = description.GetComponent<TextMeshProUGUI>();
        waterDrop = GameObject.Find("Water_Drop");
        waterDropParticleSystem = waterDrop.GetComponent<ParticleSystem>();
        waterDropParticleSystem.Stop();
        voltageObj = GameObject.Find("voltage");
        voltage = voltageObj.GetComponent<TextMeshPro>();
        tx1Obj = GameObject.Find("tx1");
        tx1 = tx1Obj.GetComponent<TextMeshPro>();
        tx2Obj = GameObject.Find("tx2");
        tx2 = tx2Obj.GetComponent<TextMeshPro>();
        tvgObj = GameObject.Find("tvg");
        tvg = tvgObj.GetComponent<TextMeshPro>();
        textField = GameObject.Find("InputField (TMP)");
        textFieldtext = textField.GetComponent<TMP_InputField>();
        cran = GameObject.Find("Cran");
        coldWaterBox = GameObject.Find("ForColdWat");
        connToObm = GameObject.Find("ConnToObm0");
        regul = GameObject.Find("Regul");
        vneshniCalor = GameObject.Find("VneshniKa0");
        waterCylinder = GameObject.Find("Cylinder02");


    }

    // Update is called once per frame
    void Update()
    {
        if (TaskControl.CheckClick("Cran") && Input.GetMouseButtonDown(0))
        {
            if (!isOpening && !isOpened)
            {
                if(TaskControl.currentStep == TaskControl.Step.Step2)
                {
                    StartCoroutine(OpenCrane());
                    cran.GetComponent<AudioSource>().Play();
                    coldWaterBox.GetComponent<AudioSource>().Play();
                }
                else
                {
                }
            }
            if (isOpened && TaskControl.currentStep == TaskControl.Step.Step3)
            {
                StartCoroutine(CloseCrane());
                connToObm.GetComponent<AudioSource>().Play();
            }
        }
        if (TaskControl.CheckClick("CranForKal") && Input.GetMouseButtonDown(0))
        {
            if (!isOpeningCalorCran && !isOpenedCalorCran && TaskControl.currentStep == TaskControl.Step.Step5)
            {
                cran.GetComponent<AudioSource>().Play();
                StartCoroutine(OpenCalorCran());
                StartWaterFlow();
                vneshniCalor.GetComponent<AudioSource>().Play();
            }
            else if (isOpenedCalorCran && TaskControl.currentStep == TaskControl.Step.Step9)
            {
                connToObm.GetComponent <AudioSource>().Play();
                StartCoroutine(CloseCalorCran());
            }
        }

        if (Input.GetMouseButtonDown(0) && TaskControl.CheckClick("Regul") && TaskControl.currentStep == TaskControl.Step.Step4)
        {
            StartCoroutine(latrOn());
            regul.GetComponent<AudioSource>().Play();
            StartCoroutine(ChangeTextCoroutine(true));
            
        }
        if (Input.GetMouseButtonDown(0) && TaskControl.CheckClick("Regul") && TaskControl.currentStep == TaskControl.Step.Step10)
        {
            StartCoroutine(latrOff());
            waterCylinder.GetComponent<AudioSource>().loop = false;
            regul.GetComponent<AudioSource>().Play();
            StartCoroutine(ChangeTextCoroutine(false));

        }


        //if (Input.GetKeyDown(KeyCode.Q))   //если нажата клавиша q
        //{
        //    anim.SetBool("OpenKran", true);  // переменная, отвечающая за переход имеет значение true
        //    //Debug.Log("ndnsla");
        //}
        //if(Input.GetKeyDown(KeyCode.W)) 	//если нажата клавиша w отпускается
        //{
        //    anim.SetBool("OpenKran", false); // переменная, отвечающая за переход имеет значение false
        //}
    }

    IEnumerator latrOn()
    {
        anim.SetTrigger("RegulTrig");
        isCooldownNagrev = true;
        yield return new WaitForSeconds(4f);
        waterCylinder.GetComponent<AudioSource>().Play();
        waterCylinder.GetComponent<AudioSource>().loop = true;
        isCooldownNagrev = false;
        TableController.textMeshProVolt.text = "220";
        podskazka.text = "Подождите немного пока вода нагреется и откройте кран для подачи воды в калориметр";
        TaskControl.currentStep = TaskControl.Step.Step5;
    }

    IEnumerator latrOff()
    {
        anim.SetTrigger("ReverseRegulTrig");
        isCooldownNagrev = true;
        yield return new WaitForSeconds(3f);
        isCooldownNagrev = false;
        TableController.textMeshProVolt.text = "0";
        podskazka.text = "Выключите установку";
        TaskControl.currentStep = TaskControl.Step.Step11;
    }

    IEnumerator OpenCalorCran()
    {
        isOpeningCalorCran = true;
        anim.SetTrigger("OpenCalorCranTrig");

        // Записываем время открытия крана
        openTimeCalorCran = Time.time;

        yield return new WaitForSeconds(3f);
        isOpenedCalorCran = true;
        isOpeningCalorCran = false;
        podskazka.text = "Нажмите кнопку расчет 1 для получения и ввода значений в таблицу, инструкция на стене в помощь";
        TaskControl.currentStep = TaskControl.Step.Step6;
    }

    IEnumerator CloseCalorCran()
    {
        if (Time.time - openTimeCalorCran >= 4.5f)
        {
            isOpenedCalorCran = false;
            anim.SetTrigger("RevOpenCalorCranTrig");
            yield return null;
            podskazka.text = "Отключите ЛАТР";
            TaskControl.currentStep = TaskControl.Step.Step10;
        }
        else
        {
            yield return null;
        }
    }

    IEnumerator OpenCrane()
    {
        isOpening = true;
        anim.SetTrigger("OpenCran");

        // Записываем время открытия крана
        openTime = Time.time;

        yield return new WaitForSeconds(4f);
        isOpened = true;
        isOpening = false;
        podskazka.text = "Закройте кран";
        TaskControl.currentStep = TaskControl.Step.Step3;
    }

    IEnumerator CloseCrane()
    {
        if (Time.time - openTime >= 3f)
        {
            isOpened = false;
            anim.SetTrigger("CloseCran");
            yield return null;
            podskazka.text = "Включите ЛАТР для нагрева воды";
            TaskControl.currentStep = TaskControl.Step.Step4;
        }
        else
        {
            yield return null;
        }
    }
    IEnumerator FlowWaterForSeconds(float duration)
    {
        waterDropParticleSystem.Play();

        // Ждем указанное количество секунд
        yield return new WaitForSeconds(duration);

        // После ожидания сбрасываем анимацию к начальному состоянию
        waterDropParticleSystem.Stop();
        waterDropParticleSystem.Clear();
    }
    public void StartWaterFlow()
    {
        StartCoroutine(FlowWaterForSeconds(10f));
    }
    IEnumerator ChangeTextCoroutine(bool direct)
    {
        if(direct)
        {
            yield return new WaitForSeconds(0.45f);

            voltage.text = "99 V";

            yield return new WaitForSeconds(0.55f);

            voltage.text = "220 V";
        }
        else
        {
            yield return new WaitForSeconds(0.55f);

            voltage.text = "99 V";

            yield return new WaitForSeconds(0.45f);

            voltage.text = "0 V";
        }

    }
    private void MakeCalculations()
    {
        float randTx1 = UnityEngine.Random.Range(19f, 21f);
        float randTx2 = UnityEngine.Random.Range(31f, 33f);
        float randTvg = UnityEngine.Random.Range(19f, 21f);
        tx1.text = randTx1.ToString("F1") + " °C";
        new WaitForSeconds(1f);
        tx2.text = randTx2.ToString("F1") + " °C";
        new WaitForSeconds(0.5f);
        tvg.text = randTvg.ToString("F1") + " °C";
    }

    public void Calc1()
    {
        if (TaskControl.Step.Step6 == TaskControl.currentStep)
        {
            MakeCalculations();
            double mass = GetMassValue(60);
            TableController.textMeshProArray[3].text = Math.Round(mass, 2).ToString();
            double time = Convert.ToDouble(TableController.textMeshProArray[2].text);
            Debug.Log(mass);
            Debug.Log(time);
            double rashodCalor = mass / time;
            rashodCalor *= 100;
            TableController.textMeshProArray[4].text = Math.Round(rashodCalor, 2).ToString() + "*E-5";
            rashodCalorSredn += rashodCalor;
            podskazka.text = "Введите новые значения и нажмите расчет 2. Выполните аналогичные действия, когда появятся новые значения";
            TaskControl.currentStep = TaskControl.Step.Step7;
            TaskControl.currentWriteStep = TaskControl.WriteStep.WriteStep2;
            new WaitForSeconds(5f);
        }
        else
        {
        }
    }

    public void SaveInput()
    {
        if(TaskControl.WriteStep.WriteStep2 == TaskControl.currentWriteStep)
        {
            double result;
            if (double.TryParse(textFieldtext.text, out result))
            {
                if(TableController.textMeshProArray[1].text == "")
                {
                    TableController.textMeshProArray[1].text = result.ToString(); 
                }
                else if (TableController.textMeshProArray[5].text == "" && TableController.textMeshProArray[1].text != "")
                {
                    TableController.textMeshProArray[5].text = result.ToString();
                }
                else
                {
                    TableController.textMeshProArray[6].text = result.ToString();
                    TableController.textMeshProArray[9].text =
                        PerformCalculationWithWarm(TableController.textMeshProArray[8].text,
                        TableController.textMeshProArray[4].text,
                        TableController.textMeshProArray[6].text,
                        TableController.textMeshProArray[5].text,
                        TableController.textMeshProArray[1].text);
                    TaskControl.currentWriteStep = TaskControl.WriteStep.WriteStep3;
                }
            }
            textFieldtext.text = string.Empty;
        }
        if (TaskControl.WriteStep.WriteStep3 == TaskControl.currentWriteStep)
        {
            double result;
            if (double.TryParse(textFieldtext.text, out result))
            {
                if (TableController.textMeshProArray[11].text == "")
                {
                    TableController.textMeshProArray[11].text = result.ToString();
                }
                else if (TableController.textMeshProArray[15].text == "" && TableController.textMeshProArray[11].text != "")
                {
                    TableController.textMeshProArray[15].text = result.ToString();
                }
                else
                {
                    TableController.textMeshProArray[16].text = result.ToString();
                    TableController.textMeshProArray[19].text =
                        PerformCalculationWithWarm(TableController.textMeshProArray[18].text,
                        TableController.textMeshProArray[14].text,
                        TableController.textMeshProArray[16].text,
                        TableController.textMeshProArray[15].text,
                        TableController.textMeshProArray[11].text);
                    TaskControl.currentWriteStep = TaskControl.WriteStep.WriteStep4;
                }
            }
            textFieldtext.text = string.Empty;
        }
        if (TaskControl.WriteStep.WriteStep4 == TaskControl.currentWriteStep)
        {
            double result;
            if (double.TryParse(textFieldtext.text, out result))
            {
                if (TableController.textMeshProArray[21].text == "")
                {
                    TableController.textMeshProArray[21].text = result.ToString();
                }
                else if (TableController.textMeshProArray[25].text == "" && TableController.textMeshProArray[21].text != "")
                {
                    TableController.textMeshProArray[25].text = result.ToString();
                }
                else
                {
                    TableController.textMeshProArray[26].text = result.ToString();
                    TableController.textMeshProArray[29].text =
                        PerformCalculationWithWarm(TableController.textMeshProArray[28].text,
                        TableController.textMeshProArray[24].text,
                        TableController.textMeshProArray[26].text,
                        TableController.textMeshProArray[25].text,
                        TableController.textMeshProArray[21].text);
                    TableController.textMeshProArray[39].text = PerformCalculation(TableController.textMeshProArray[9].text,
                        TableController.textMeshProArray[19].text,
                        TableController.textMeshProArray[29].text);
                    TaskControl.currentWriteStep = TaskControl.WriteStep.WriteStep5;
                }
            }
            textFieldtext.text = string.Empty;
        }
        if(TaskControl.WriteStep.WriteStep5 == TaskControl.currentWriteStep)
        {
            TableController.textMeshProArray[31].text =
                PerformCalculation(TableController.textMeshProArray[1].text,
                TableController.textMeshProArray[11].text,
                TableController.textMeshProArray[21].text);
            TableController.textMeshProArray[35].text =
                PerformCalculation(TableController.textMeshProArray[5].text,
                TableController.textMeshProArray[15].text,
                TableController.textMeshProArray[25].text);
            TableController.textMeshProArray[36].text =
                PerformCalculation(TableController.textMeshProArray[6].text,
                TableController.textMeshProArray[16].text,
                TableController.textMeshProArray[26].text);

        }
    }

    public static string PerformCalculation(string input1, string input2, string input3)
    {
        if (double.TryParse(input1, out double num1) &&
            double.TryParse(input2, out double num2) &&
            double.TryParse(input3, out double num3))
        {
            double result = (num1 + num2 + num3) / 3.0;

            result = Math.Round(result, 2);

            return result.ToString();
        }
        else { return ""; }
    }

    public static string PerformCalculationWithWarm(string coldWaterRash,
        string kondensRash,
        string tempOthod,
        string tempPrihod,
        string tempColdKondens)
    {
        coldWaterRash = coldWaterRash.Substring(0, coldWaterRash.Length - 4);
        kondensRash = kondensRash.Substring(0, kondensRash.Length - 4);


        if (double.TryParse(coldWaterRash, out double num1) &&
            double.TryParse(kondensRash, out double num2) &&
            double.TryParse(tempOthod, out double num3) &&
            double.TryParse(tempPrihod, out double num4) &&
            double.TryParse(tempColdKondens, out double num5))
        {
            double result = (num1 / num2 * 100 * 4.2 * (num3 - num4)) - 2.2 * (100 - num5);

            result = Math.Round(result, 2);

            return result.ToString();
        }
        else { return ""; }
    }



    public void Calc2()
    {
        if (TaskControl.Step.Step7 == TaskControl.currentStep)
        {
            StartCoroutine(MakeRaschet());
            vneshniCalor.GetComponent<AudioSource>().Play();
            double mass = GetMassValue(300);
            TableController.textMeshProArray[13].text = Math.Round(mass, 2).ToString();
            double time = Convert.ToDouble(TableController.textMeshProArray[12].text);
            Debug.Log(mass);
            Debug.Log(time);
            double rashodCalor = mass / time;
            rashodCalor *= 100;
            TableController.textMeshProArray[14].text = Math.Round(rashodCalor, 2).ToString() + "*E-5";
            rashodCalorSredn += rashodCalor;
            podskazka.text = "Введите новые значения и нажмите расчет 3. Выполните аналогичные действия, когда появятся новые значения";
            TaskControl.currentStep = TaskControl.Step.Step8;
            TaskControl.currentWriteStep = TaskControl.WriteStep.WriteStep3;
        }
        else
        {
        }
    }

    IEnumerator MakeRaschet()
    {
        StartWaterFlow();
        yield return new WaitForSeconds(5f);
        MakeCalculations();
        new WaitForSeconds(10f);
    }

    public void Calc3()
    {
        if (TaskControl.Step.Step8 == TaskControl.currentStep)
        {
            StartCoroutine(MakeRaschet());
            vneshniCalor.GetComponent<AudioSource>().Play();
            double mass = GetMassValue(600);
            TableController.textMeshProArray[23].text = Math.Round(mass, 2).ToString();
            double time = Convert.ToDouble(TableController.textMeshProArray[22].text);
            Debug.Log(mass);
            Debug.Log(time);
            double rashodCalor = mass / time;
            rashodCalor *= 100;
            TableController.textMeshProArray[24].text = Math.Round(rashodCalor, 2).ToString() + "*E-5";
            rashodCalorSredn += rashodCalor;
            rashodCalorSredn /= 3;
            TableController.textMeshProArray[34].text = Math.Round(rashodCalorSredn, 2).ToString() + "*E-5";
            podskazka.text = "Закройте кран для подачи воды в калориметр";
            TaskControl.currentStep = TaskControl.Step.Step9;
            TaskControl.currentWriteStep = TaskControl.WriteStep.WriteStep4;
        }
        else
        {
        }
    }

    public void ClearCalc()
    {
        TableController.textMeshProArray[1].text = "";
        TableController.textMeshProArray[3].text = "";
        TableController.textMeshProArray[4].text = "";
        TableController.textMeshProArray[5].text = "";
        TableController.textMeshProArray[6].text = "";
        TableController.textMeshProArray[9].text = "";
        TableController.textMeshProArray[11].text = "";
        TableController.textMeshProArray[13].text = "";
        TableController.textMeshProArray[14].text = "";
        TableController.textMeshProArray[15].text = "";
        TableController.textMeshProArray[16].text = "";
        TableController.textMeshProArray[19].text = "";
        TableController.textMeshProArray[21].text = "";
        TableController.textMeshProArray[23].text = "";
        TableController.textMeshProArray[24].text = "";
        TableController.textMeshProArray[25].text = "";
        TableController.textMeshProArray[26].text = "";
        TableController.textMeshProArray[29].text = "";
        TableController.textMeshProArray[31].text = "";
        TableController.textMeshProArray[34].text = "";
        TableController.textMeshProArray[35].text = "";
        TableController.textMeshProArray[36].text = "";
        TableController.textMeshProArray[39].text = "";

    }
    public float GetMassValue(float input)
    {
        // Коэффициенты наклона и смещения для линейной зависимости
        float slope = 0.0333f;

        // Вычисление значения с учетом линейной зависимости
        float value = slope * input;

        // Добавление случайного разброса
        float randomOffset = UnityEngine.Random.Range(-0.5f, 0.5f);
        value += randomOffset;
        return value;
    }
}


//for (int i = 0; i < 3; i++)
//{
//    if (double.TryParse([numbers[i]]))
//    {

//    }
//}