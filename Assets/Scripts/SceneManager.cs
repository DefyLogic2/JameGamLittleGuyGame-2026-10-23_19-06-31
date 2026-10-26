using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    private bool triggered = false;
    void Start()
    {
           DontDestroyOnLoad(gameObject);
    }
    public void LoadScene(string Name)
    {
        SceneManager.LoadScene(Name);
    }




    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered || !other.CompareTag("Player")) return;
        triggered = true;
        LoadScene("Menu");
    }

   

}