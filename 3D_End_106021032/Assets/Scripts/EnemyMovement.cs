using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //玩家位置
    public Transform PlayerTransform;
    //敌人移动速度
    public float speed = 5;
    //敌人每次运行距离
    public int step = 1;
    //下一次运行的x,z坐标
    public int x = 0;//左右
    public int z = 0;//前后
    //运行速度,改变velocity越小速度越快
    public float velocity = 0.5f;
    //敌人上的刚体组件
    public Rigidbody rd;
    //敌人下一步位置
    public Vector3 NextPosition;
    //炮塔
    public GameObject Turret;
    public float TurretRotateSpeed = 20;
    void Start()
    {
        NextPosition = transform.position;
        //重复调用EnemyMove,参数二表示第一次开始执行的时间,velocity越小执行方法频率越快
        //InvokeRepeating("EnemyMove", 1, velocity);
    }
    void FixedUpdate()
    {
        EnemyMove();
        //checkTargetDirForMe(PlayerTransform);
        //Lerp插值移动,当NextPosition值改变时敌人开始移动
        rd.MovePosition(Vector3.Lerp(transform.position, NextPosition, Time.deltaTime * speed));
        
    }
    // 敌人行为
    public void EnemyMove()
    {
        //获得敌人到玩家的偏移
        Vector3 offset = PlayerTransform.position - transform.position;
        //判断,当玩家是否处于自己的攻击距离的时候(小于12),enmey攻击
        if (offset.magnitude < 5)
        {
            attack();
            //当玩家与敌人距离小于12时,敌人开始攻击,因为我写的是射击类游戏所以攻击距离较大,可根据自己项目修改
        }
        //当玩家与敌人距离大于12时,敌人向玩家靠近移动
        else
        {
            //通过偏移判断优先向那个(轴)方向移动
            if (Mathf.Abs(offset.z) > Mathf.Abs(offset.x))
            {
                //z轴移动
                //当offset偏移量大于0时,说明敌人Z轴坐标比玩家Z轴坐标小,下面同理
                if (offset.z > 0)
                {
                    x = 0;
                    z = step;
                }
                else
                {
                    x = 0;
                    z = -step;

                }
            }
            else
            {
                //x轴移动
                if (offset.x > 0)
                {
                    x = step;
                    z = 0;

                }
                else
                {
                    x = -step;
                    z = 0;
                }
            }
            //设置下一步位置
            NextPosition = transform.position + new Vector3(x, 0, z);
            rd.transform.eulerAngles = new Vector3(0, getAngel(transform.position, NextPosition), 0);
        }
        
    }

    public void attack()
    {
        Turret.transform.Rotate(0, 0, -20);
    }

    public float getAngel(Vector3 a, Vector3 b)
    {
        if (a.x == b.x && a.y == b.y)
        {
            return 0;
        }

        b -= a;
        float angel = Mathf.Acos(-b.y / b.magnitude) * (180 / Mathf.PI);

        return (b.x < 0 ? -angel : angel);
    }

    //求角度 及前後左右方位  
    public void checkTargetDirForMe(Transform target)
    {
        //xuqiTest：  target.position = new Vector3(3, 0, 5);  
        Vector3 dir = target.position - transform.position; //位置差，方向  
                                                            //方式1   點乘  
                                                            //點積的計算方式爲: a·b =| a |·| b | cos < a,b > 其中 | a | 和 | b | 表示向量的模 。  
        float dot = Vector3.Dot(transform.forward, dir.normalized);//點乘判斷前後：dot >0在前，<0在後
        float dot1 = Vector3.Dot(transform.right, dir.normalized);//點乘判斷左右： dot1>0在右，<0在左
        float angle = Mathf.Acos(Vector3.Dot(transform.forward.normalized, dir.normalized)) * Mathf.Rad2Deg;//通過點乘求出夾角  

        //方式2   叉乘  
        //叉乘滿足右手準則  公式：模長|c|=|a||b|sin<a,b>    
        Vector3 cross = Vector3.Cross(transform.forward, dir.normalized);//叉乘判斷左右：cross.y>0在左，<0在右   
        Vector3 cross1 = Vector3.Cross(transform.right, dir.normalized); //叉乘判斷前後：cross.y>0在前，<0在後   
        angle = Mathf.Asin(Vector3.Distance(Vector3.zero, Vector3.Cross(transform.forward.normalized, dir.normalized))) * Mathf.Rad2Deg;
        rd.transform.eulerAngles = new Vector3(0, -angle, 0);
    }

}
