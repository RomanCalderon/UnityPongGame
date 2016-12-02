using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    static int playerScore1 = 0;
    static int playerScore2 = 0;

    [SerializeField]
    Text score;

    void Update()
    {
        score.text = playerScore1.ToString() + " | " + playerScore2.ToString();
    }

    // Update is called once per frame
    public static void Score (string wallName) {
	    switch(wallName)
        {
            case "RightWall":
                playerScore1++;
                break;
            case "LeftWall":
                playerScore2++;
                break;
            default:
                Debug.LogError("Invalid wall name!");
                break;
        }
    }
}
