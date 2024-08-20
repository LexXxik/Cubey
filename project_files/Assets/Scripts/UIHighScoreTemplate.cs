using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHighScoreTemplate : MonoBehaviour {

    public float speed;

    Slider slider;
    float min;
    float max;
    private void Start()
    {
        slider = this.gameObject.GetComponent<Slider>();
    }

    public void Scroll(bool opening)
    {
        if (opening == true)
        {
            min = 0;
            max = 1;
            for (float i = min; i < max; i += 0.1f)
            {
                if (i == 0.1f)
                    speed = 2 * speed;
                slider.value -= 0.1f;
                Scrolling();
            }
        }
        if (opening == true)
        {
            min = 1;
            max = 0;
            for (float i = min; i > max; i -= 0.1f)
            {
                if (i == 0.1f)
                    speed = 2 * speed;
                slider.value -= 0.1f;
                Scrolling();
            }
        }
        opening = !opening;
    }
    IEnumerator Scrolling()//Smooth going
    {
        yield return new WaitForSeconds(speed); 
    }
}
