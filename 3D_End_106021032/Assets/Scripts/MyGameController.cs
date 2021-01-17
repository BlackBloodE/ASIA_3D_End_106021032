using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyGameController : MonoBehaviour
{
    //相機
    public GameObject cam;
    public GameObject enemyTank;
    //坦克移動
    public GameObject tank;
    public float TankMoveSpeed = 30;
    public float TankRotateSpeed = 60;
    //車身
    public GameObject hull;
    //炮塔
    public GameObject Turret;
    public float TurretRotateSpeed = 20;
    //炮管
    public GameObject Gun;
    public float GunRotateSpeed = 30;
    //選單
    public GameObject ui;
    public GameObject Title;

    public Quaternion rotationEuler;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //turret.rotation = rotationEuler;
        //turret.Rotate(rot);
        //rot.y = Input.GetAxis("Mouse X") * Time.deltaTime * TurretRotateSpeed;
        //print(GetInspectorRotationValueMethod(tank.transform).x);
        //Turret.transform.eulerAngles = new Vector3(GetInspectorRotationValueMethod(tank.transform).x , Turret.transform.eulerAngles.x, 0);

        //前進後退
        if (Input.GetKey(KeyCode.W))
        {
            tank.transform.Translate(Vector3.forward * Time.deltaTime * TankMoveSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            tank.transform.Translate(Vector3.forward * Time.deltaTime * -TankMoveSpeed);
        }
        //左右旋轉
        if (Input.GetKey(KeyCode.A))
        {
            tank.transform.Rotate(Vector3.up * Time.deltaTime * -TankRotateSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            tank.transform.Rotate(Vector3.up * Time.deltaTime * TankRotateSpeed);
        }

        //炮塔水平旋轉
        //Turret.transform.Rotate(0, 0, Input.GetAxis("Mouse X") * Time.deltaTime * TurretRotateSpeed);
        Turret.transform.eulerAngles = new Vector3(- 90f, GetInspectorRotationValueMethod(cam.transform).y, 0);
        print(GetInspectorRotationValueMethod(tank.transform).x);

        //炮管垂直旋轉
        var y = -Input.GetAxis("Mouse Y") * GunRotateSpeed * Time.deltaTime;
        //print(GetInspectorRotationValueMethod(Gun.transform));
        //print(y);
        Gun.transform.Rotate(y, 0, 0);
        //if (GetInspectorRotationValueMethod(Gun.transform).x < 10 &&
        //    GetInspectorRotationValueMethod(Gun.transform).x > -25)
        //{
        //    Gun.transform.Rotate(y, 0, 0);
        //}
        //else if (GetInspectorRotationValueMethod(Gun.transform).x > 10)
        //{
        //    Gun.transform.Rotate(-0.1f, 0, 0);
        //}
        //else if (GetInspectorRotationValueMethod(Gun.transform).x < -25)
        //{
        //    Gun.transform.Rotate(0.1f, 0, 0);
        //}
        //print(tank.transform.position.y);
        if (tank.transform.position.y < -2)
        {
            gameOver(false);
            print("輸了");
        }
        else if (enemyTank.transform.position.y < -2)
        {
            gameOver(true);
            print("贏了");
        }
    }

    //获取到旋转的正确数值
    public Vector3 GetInspectorRotationValueMethod(Transform transform)
    {
        // 获取原生值
        System.Type transformType = transform.GetType();
        PropertyInfo m_propertyInfo_rotationOrder = transformType.GetProperty("rotationOrder", BindingFlags.Instance | BindingFlags.NonPublic);
        object m_OldRotationOrder = m_propertyInfo_rotationOrder.GetValue(transform, null);
        MethodInfo m_methodInfo_GetLocalEulerAngles = transformType.GetMethod("GetLocalEulerAngles", BindingFlags.Instance | BindingFlags.NonPublic);
        object value = m_methodInfo_GetLocalEulerAngles.Invoke(transform, new object[] { m_OldRotationOrder });
        string temp = value.ToString();
        //将字符串第一个和最后一个去掉
        temp = temp.Remove(0, 1);
        temp = temp.Remove(temp.Length - 1, 1);
        //用‘，’号分割
        string[] tempVector3;
        tempVector3 = temp.Split(',');
        //将分割好的数据传给Vector3
        Vector3 vector3 = new Vector3(float.Parse(tempVector3[0]), float.Parse(tempVector3[1]), float.Parse(tempVector3[2]));
        return vector3;
    }

    public void gameOver(bool isWin)
    {
        ui.active = true;
        string title;
        if (isWin)
        {
            title = "Win";
        } else
        {
            title = "Lost";
        }
        Title.GetComponent<Text>().text = title;
        //時間暫停
        Time.timeScale = 0f;
    }

    public void reStart()
    {
        
        SceneManager.LoadScene(0);
    }

    public void exit()
    {
        Application.Quit();
    }


}
