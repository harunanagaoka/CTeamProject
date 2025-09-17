using UnityEngine;


public class ObstacleSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ObstaclePrefab = null;

    private GameObject ob;

    private ObstacleMove m_obstacleMove = null;

    void Start()
    {
        ob = Instantiate(m_ObstaclePrefab, this.transform);

        // ob.transform.position = 

        //ob.transform.rotation = quaternion;

       // m_obstacleMove = GetComponent<ObstacleMove>();

        //m_obstacleMove.AddObstacle(ob);
    }

    void Update()
    {

    }
}
