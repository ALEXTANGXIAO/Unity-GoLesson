using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{

    public int Speed = 10;

    private Transform m_transform;

    Transform m_camTransform;

    Vector3 m_camRot;

    void Start()
    {
        m_transform = this.transform;
        // 获取摄像机
        m_camTransform = Camera.main.transform;

        //// 设置摄像机的旋转方向与主角一致
        m_camTransform.rotation = m_transform.rotation;  //rotation为物体在世界坐标中的旋转角度，用Quaternion赋值
        m_camRot = m_camTransform.eulerAngles;    //在本游戏实例中用欧拉角表示旋转
    }
    void Update()
    {
        Control();

        float h = Input.GetAxisRaw("Horizontal") * Time.deltaTime * Speed;
        float v = Input.GetAxisRaw("Vertical") * Time.deltaTime * Speed;
        var Vector = new Vector3(h, 0, v);
        m_transform.Translate(Vector);
    }

    void Control()
    {
        //获取鼠标移动距离
        float rh = Input.GetAxis("Mouse X");
        float rv = Input.GetAxis("Mouse Y");

        // 旋转摄像机
        m_camRot.x -= rv;
        m_camRot.y += rh;
        m_camTransform.eulerAngles = m_camRot;   //通过改变XYZ轴的旋转改变欧拉角

        // 使主角的面向方向与摄像机一致
        Vector3 camrot = m_camTransform.eulerAngles;
        camrot.x = 0; camrot.z = 0;
        m_transform.eulerAngles = camrot;
    }
}
