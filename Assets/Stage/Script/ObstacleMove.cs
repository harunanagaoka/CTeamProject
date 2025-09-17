using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleMove : MonoBehaviour
{
    GameObject m_conveyor = null;


    Vector3 InitPos = Vector3.zero;

    [SerializeField]
    private float m_rotateAngle = 0;

    Vector3 m_rotateRadius = Vector3.zero;

   // [SerializeField]
    Vector3 m_rotateAxis = Vector3.zero;

    Vector3 m_rotateCenter = Vector3.zero;

    [SerializeField]
    float m_moveVelocity = 0;

    //初期位置決める
    //初期ロテーション決める

    bool isMove = false;


    GameObject test = null;
    Rigidbody testbody = null;
    Bounds m_conveyorBound = new Bounds();

    void Awake()
    {
        
        //コンベアBounds左端取得
        //Dot()の方向に進む
        m_conveyor = this.transform.parent.gameObject;
        Collider conveyorCollider = m_conveyor.GetComponent<Collider>();
        m_conveyorBound = conveyorCollider.bounds;
        InitPos = new Vector3(m_conveyor.transform.position.x, m_conveyorBound.min.y - this.transform.localScale.y, m_conveyorBound.max.z - this.transform.localScale.z / 2);
        //中心点　boundの(左端 + 厚さ/2、、ポスｙ、ポスｚ/2）
        float radiusY = m_conveyorBound.size.y;
        float radiusZ = m_conveyorBound.size.z;

        m_rotateCenter = new Vector3(
            m_conveyorBound.max.x - radiusZ,
            m_conveyorBound.min.y + radiusY,
            m_conveyorBound.min.z + radiusZ
            );
        //m_rotateAxis = new Vector3.right;
        //m_rotateAxis.Normalize();
        InitObstacle();
    }


    void Update()
    {

        if(isMove)
        {
            StartCoroutine(MoveAllObstacles());

            
            //InitObstacle()
            //下にraycastを飛ばす
            //hitがコライダーだったら法線取得
            //InitObstacle()最初のposition、rotationに置く

            //MoveObstacleAlongTheConveyor()
            //this.positionを前に向かって動かす。Rigidbody.MovePositionみたいなやつ
            ////RotateAllObstacles()
            ////MovePosAllObstacles()
            isMove = false;
        }
    }

    public void AddObstacle(GameObject ob)
    {
        test = ob;
        InitObstacle();
        
    }

    private void InitObstacle()
    {
        this.transform.position = InitPos;

        this.transform.Rotate(this.gameObject.transform.right, -180);

        testbody = this.GetComponent<Rigidbody>();

        StartCoroutine(RotateSemiCircle());






        //RaycastHit hit;

        //Ray ray = new Ray(testbody.position, -testbody.transform.up);

        //if(Physics.Raycast(ray,out hit))
        //{
        //    //ヒットしたオブジェクトの表面の法線を取得したい
        //    //その法線を元にオブジェクトの向き（rotation)を変更
        //    //

        //}
    }

    private IEnumerator RotateSemiCircle(){

        int count = 0;

        while (count < 180)//180度回るまで 
        {
            //180
            this.transform.RotateAround(m_rotateCenter, m_rotateAxis, m_rotateAngle);
            //
            //軸　boundのｘ軸
            //test.transform.RotateAround(中心点、軸、角度)
            count++;
            yield return null;
        }

        isMove = true;

    }

    private IEnumerator RotateSemiCircleEnd()
    {

        int count = 0;

        while (count < 180)//180度回るまで 
        {

            //中心点　＝　四角の右端ｘ、四角のまｘｙ/2、四角のマックスｚ
            //180
            this.transform.RotateAround(m_rotateRadius, m_rotateAxis, m_rotateAngle);
            //中心点　boundの(左端 + 厚さ/2、、ポスｙ、ポスｚ/2）
            //軸　boundのｘ軸
            //test.transform.RotateAround(中心点、軸、角度)
            count++;
            yield return null;
        }

        DestroyObstacle();
    }

    private IEnumerator MoveAllObstacles()
    {
        int count = 0;

        while (count < 1000)
        {
            Vector3 a = this.transform.position;
            a = a + new Vector3(0, 0, m_moveVelocity);
            testbody.MovePosition(a);
            count++;
            yield return null;
        }

        StartCoroutine(RotateSemiCircleEnd());
    }

    private void DestroyObstacle()
    {
        Destroy(this);
    }
}
//始点はじまり
//GameObject conveyorObject;
//上記のコライダーのconveyorObject.GetComponent<Collider>().bounds　Boundsクラス
//Vector3 startPosition = conveyorBounds.center - conveyorDirection.normalized * (conveyorBounds.size.z * 0.5f);

// 始点の上空からコンベアの円柱に向かいレイキャスト
//hit.normal;
//test.transform.position = hit.point;

//起き上がる動作と進む動作を別々にする。

//180度回転するまでコルーチン、コルーチン後ムーヴ

//最後にコルーチン

/*
 
        Renderer parentRenderer = this.GetComponent<Renderer>();

        Bounds bounds = parentRenderer.bounds;

        Vector3 leftEdge = new Vector3(bounds.min.x, this.transform.position.y, this.transform.position.z);

        ob.transform.position = leftEdge;
 */