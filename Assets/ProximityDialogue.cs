using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneTimeProximityDialogue : MonoBehaviour {
    public float dist = 10f;
    public float fadeInRate = 0.35f;
    public AnimationCurve fadeCurve;
    public Transform player;
    public Text text;
    public float textSolidTime = 2f;

    public bool activated = false;
    
	// Use this for initialization
	void Start () {
        //StartCoroutine(CheckDist());
        InvokeRepeating("checkDist", 0, 1f);
	}

    void checkDist()
    {
        if (activated == true)
            return;

        if (Vector3.Distance(transform.position, player.position) <= dist)
        {
            activated = true;
            StartCoroutine(FadeIn());
          //  Invoke("FadeOut", 3f);
        }
    }

    IEnumerator CheckDist()
    {
        while (true)
        {
            CheckDist();
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator FadeOut()
    {
        float t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime * fadeInRate;
            float a = fadeCurve.Evaluate(t);
            text.color = new Color(1f, 1f, 1f, a);
            yield return 0;
        }

    }

    IEnumerator FadeIn()
    {
        float t = 0f;
        transform.LookAt(Camera.main.transform.position);
        while (t < 1f)
        {
            t += Time.deltaTime * fadeInRate;
            float a = fadeCurve.Evaluate(t);
            text.color = new Color(1f, 1f, 1f, a);

            yield return 0;
        }
        yield return new WaitForSeconds(textSolidTime);
        StartCoroutine(FadeOut());
    }
}
