using TMPro;
using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    private int currentScore = 0;
    private int currentTrashCount = 0;

    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI trashCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentScore = GlobalProperties.currentScore;
        scoreText.text = "Score: " + GlobalProperties.currentScore.ToString();

        currentTrashCount = GlobalProperties.remainingTrash;
        trashCount.text = (GlobalProperties.totalTrash - GlobalProperties.remainingTrash).ToString() + "/" + GlobalProperties.totalTrash.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScore != GlobalProperties.currentScore)
        {
                scoreText.text = "Score: " + GlobalProperties.currentScore.ToString();
                StopAllCoroutines();
                StartCoroutine(ScorePopEffect(currentScore > GlobalProperties.currentScore ? Color.red : Color.yellow));
                currentScore = GlobalProperties.currentScore;
        }

        if (currentTrashCount != GlobalProperties.remainingTrash)
        {
            trashCount.text = (GlobalProperties.totalTrash - GlobalProperties.remainingTrash).ToString() +"/" +GlobalProperties.totalTrash.ToString();
            currentTrashCount = GlobalProperties.remainingTrash;
        }
    }

    //public int AddScore(int score)
    //{
    //    GlobalProperties.currentScore = Mathf.Max(GlobalProperties.currentScore + score, 0);
    //    scoreText.text = "Score: " + GlobalProperties.currentScore.ToString();
    //    return GlobalProperties.currentScore;
    //}


    // ---- ANIMATION DU SCORE ---- //
    private IEnumerator ScorePopEffect(Color startColor)
    {
        // POP SCALE
        Vector3 baseScale = Vector3.one;
        Vector3 popScale = baseScale * 1.25f;
        float duration = 0.15f;

        // Étape 1 : grossit
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            float p = t / duration;
            scoreText.transform.localScale = Vector3.Lerp(baseScale, popScale, p);
            yield return null;
        }

        // Étape 2 : revient à la normale
        t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            float p = t / duration;
            scoreText.transform.localScale = Vector3.Lerp(popScale, baseScale, p);
            yield return null;
        }

        scoreText.transform.localScale = baseScale;


        // ---- FLASH COULEUR ---- //
        Color endColor = Color.white;
        float colorTime = 0.25f;

        t = 0;
        while (t < colorTime)
        {
            t += Time.deltaTime;
            float p = t / colorTime;
            scoreText.color = Color.Lerp(startColor, endColor, p);
            yield return null;
        }

        scoreText.color = endColor;
    }


}
