using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    // Start is called before the first frame update
    public int n = 0;
   public void OnClick(){
      n++;
      Debug.Log("Button clicked " + n + " times.");
      SceneManager.LoadScene("MiniGame");
   }
}
