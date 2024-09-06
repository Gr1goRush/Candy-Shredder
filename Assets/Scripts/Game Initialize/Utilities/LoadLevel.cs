using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public static void LoadNextLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    } 

    public static void LoadPreviousLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    } 

    public static void LoadLevelByIndex(int index){
        SceneManager.LoadScene(index);
    } 

    public static void LoadLevelByRelativeIndex(int index){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + index);
    } 
}
