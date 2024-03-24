using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROS2;

public class DataListener : MonoBehaviour
{
    public static DataListener instance;

    private ROS2UnityComponent ros2Unity;
    private ROS2Node ros2Node;
    private Subscription<std_msgs.msg.Float32MultiArray> listener;

    public float[] wheel_velocities;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Init Ros2 stuff
        ros2Unity = transform.parent.GetComponent<ROS2UnityComponent>();

        if (ros2Unity.Ok())
        {
            ros2Node = ros2Unity.CreateNode("ROS2UnityNode");
            // publisher = ros2Node.CreatePublisher<std_msgs.msg.Float32MultiArray>("/arm/control_instruction"); 
            
            listener = ros2Node.CreateSubscription<std_msgs.msg.Float32MultiArray>("/cmd_vel", Controls_Callback);
        
        }

    }


    void Controls_Callback(std_msgs.msg.Float32MultiArray msg)
    {
        wheel_velocities = new float[2];


        wheel_velocities[0] = msg.Data[0];
        wheel_velocities[1] = msg.Data[1];

        print(wheel_velocities[0] + " " + wheel_velocities[1]);
    }


    // Update is called once per frame
    void Update()
    {
        // if (ros2Unity.Ok())
        // {

        // }
    }

}
