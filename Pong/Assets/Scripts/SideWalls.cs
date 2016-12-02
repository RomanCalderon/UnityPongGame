using UnityEngine;

public class SideWalls : MonoBehaviour {
    
	void OnTriggerEnter2D (Collider2D col) {
	    if(col.tag == "Ball")
        {
            string wallName = transform.name;
            GameManager.Score(wallName);
            col.GetComponent<BallControl>().ResetBall();
        }
	}
}
