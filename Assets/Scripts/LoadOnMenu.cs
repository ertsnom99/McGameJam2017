using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnMenu : StateMachineBehaviour {
    public int sceneToLoad = 1;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        SceneManager.LoadScene(sceneToLoad);
        AkSoundEngine.PostEvent("EndToMenu", GameObject.Find("Music"));
    }

}
