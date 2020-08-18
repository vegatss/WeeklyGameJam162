using UnityEngine;

public class MainMenu : MonoBehaviour {

    public GameMaster gameMaster;

    public void StartGame() {
        gameMaster.StartLevel();
    }
}
