using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public RectTransform text;
    public CanvasGroup group;
    public AudioSource audioSource;
    public AudioClip deathSound;

    public float delay = 0.4f;       
    public float flyDuration = 0.6f;
    public float startScale = 10f;   
    public float minScale = 2f;     
    public bool restartAfter = true;
    public float restartDelay = 2.5f; 

    void Start()
    {
        text.gameObject.SetActive(false);
    }

    public void Show()
    {
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        yield return new WaitForSeconds(delay);

        text.gameObject.SetActive(true);
        if (audioSource != null && deathSound != null)
            audioSource.PlayOneShot(deathSound);

        float t = 0f;
        while (t < flyDuration)
        {
            t += Time.deltaTime;
            float p = Mathf.Clamp01(t / flyDuration);

            // ease out back: slight overshoot, then settles
            float c1 = 1.70158f, c3 = c1 + 1f;
            float ease = 1f + c3 * Mathf.Pow(p - 1f, 3) + c1 * Mathf.Pow(p - 1f, 2);

            float scale = Mathf.LerpUnclamped(startScale, minScale, ease);
            text.localScale = Vector3.one * scale;
            group.alpha = Mathf.Clamp01(p * 3f);
            yield return null;
        }

        text.localScale = Vector3.one;
        group.alpha = 1f;

        if (restartAfter)
        {
            yield return new WaitForSeconds(restartDelay);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}