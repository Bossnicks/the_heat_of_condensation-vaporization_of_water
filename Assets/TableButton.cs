using UnityEngine;
using UnityEngine.UI;

public class TableButton : MonoBehaviour
{
    public void Start()
    {
        gameObject.SetActive(false);
    }
    // Метод для делания объекта видимым
    public void MakeVisible()
    {
        gameObject.SetActive(true);
    }

    // Метод для делания объекта невидимым
    public void MakeInvisible()
    {
        gameObject.SetActive(false);
    }
}
