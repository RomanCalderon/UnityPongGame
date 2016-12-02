using UnityEngine;

public class GameSetup : MonoBehaviour {

    [SerializeField]
    GameType gameType;

    [SerializeField]
    [Range(1,20)]
    int aiDifficulty1 = 1;
    [SerializeField]
    [Range(1, 20)]
    int aiDifficulty2 = 1;

    [SerializeField]
    Camera mainCam;

    [SerializeField]
    BoxCollider2D topWall;
    [SerializeField]
    BoxCollider2D bottomWall;
    [SerializeField]
    BoxCollider2D rightWall;
    [SerializeField]
    BoxCollider2D leftWall;

    [SerializeField]
    GameObject PlayerPrefab;
    [SerializeField]
    int startingPos = 75;

    enum GameType
    {
        PVP,
        PVAI,
        AIVAI
    }

    void Start () {
        CreateBoundaries();
        SpawnPlayers(gameType);
    }

    void SpawnPlayers(GameType type)
    {
        Vector2 Player1Pos = mainCam.ScreenToWorldPoint(new Vector3(startingPos, 0, 0));
        Vector2 Player2Pos = mainCam.ScreenToWorldPoint(new Vector3(Screen.width - startingPos, 0, 0));

        GameObject p1 = (GameObject)Instantiate(PlayerPrefab, Player1Pos, Quaternion.identity);
        GameObject p2 = (GameObject)Instantiate(PlayerPrefab, Player2Pos, Quaternion.identity);

        if (type == GameType.PVP)
        {
            p1.GetComponent<PlayerControls>().playerID = 1;
            p2.GetComponent<PlayerControls>().playerID = 2;

        } else if(type == GameType.PVAI)
        {
            p1.GetComponent<PlayerControls>().playerID = 1;
            p2.GetComponent<PlayerControls>().playerID = -1;

            float difficultyLevel = (aiDifficulty1 + 1) * 2;
            p2.GetComponent<PlayerControls>().aiDifficulty = difficultyLevel;
        } else if(type == GameType.AIVAI)
        {
            p1.GetComponent<PlayerControls>().playerID = -1;
            p2.GetComponent<PlayerControls>().playerID = -1;

            float difficultyLevel1 = (aiDifficulty1 + 1) * 2;
            float difficultyLevel2 = (aiDifficulty2 + 1) * 2;
            p1.GetComponent<PlayerControls>().aiDifficulty = difficultyLevel1;
            p2.GetComponent<PlayerControls>().aiDifficulty = difficultyLevel2;
        }
    }

    void CreateBoundaries()
    {
        topWall.size = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(Screen.width * 2, 0, 0)).x, 1);
        topWall.offset = new Vector2(0, mainCam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y + 0.5f);

        bottomWall.size = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(Screen.width * 2, 0, 0)).x, 1);
        bottomWall.offset = new Vector2(0, mainCam.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - 0.5f);


        rightWall.size = new Vector2(1, mainCam.ScreenToWorldPoint(new Vector3(0, Screen.height * 2, 0)).y);
        rightWall.offset = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - 0.5f, 0);

        leftWall.size = new Vector2(1, mainCam.ScreenToWorldPoint(new Vector3(0, Screen.height * 2, 0)).y);
        leftWall.offset = new Vector2(mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 0.5f, 0);
    }
}
