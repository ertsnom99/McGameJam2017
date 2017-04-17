using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnReplay : StateMachineBehaviour {

    public int sceneToLoad = 1;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        SceneManager.LoadScene(sceneToLoad);
        // AkSoundEngine.PostEvent("EndToCalm", GameObject.Find("Music"));
    }

}
