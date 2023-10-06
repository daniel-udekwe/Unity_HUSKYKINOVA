using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RosSharp.RosBridgeClient
{
    //public class StopCommandPublisher : UnityPublisher<MessageTypes.Sensor.JointState>
    public class StopCommandPublisher : UnityPublisher<MessageTypes.KortexDriver.TwistCommand>
    {
        //public List<JointStateReader> JointStateReaders;
        private string FrameId = "Unity";
        private BioIK.BioIK hebi;

        //private MessageTypes.Sensor.JointState message;
        private MessageTypes.KortexDriver.TwistCommand message;

        protected override void Start()
        {
            base.Start();
            hebi = this.gameObject.GetComponent<BioIK.BioIK>();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            //int jointStateLength = JointStateReaders.Count;
            //message = new MessageTypes.Sensor.JointState
            message = new MessageTypes.KortexDriver.TwistCommand
            {
                /*header = new MessageTypes.Std.Header { frame_id = FrameId },
                name = new string[] { "joint_1", "joint_2", "joint_3", "joint_4", "joint_5", "joint_6", "joint_7" },
                position = new double[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                velocity = new double[] { 0, 0, 0, 0, 0, 0, 0 },
                effort = new double[] { 0, 0, 0, 0, 0, 0, 0 },*/
                //points = new MessageTypes.Trajectory.JointTrajectoryPoint[1],


                reference_frame = 0,
                twist = new MessageTypes.KortexDriver.Twist { linear_x = 0.0f, linear_y = 0.0f, linear_z = 0.0f, angular_x = 0.0f, angular_y = 0.0f, angular_z = 0.0f },
                duration = 0,
                /* linear_x = 0.0f,
                 linear_y = 0.0f,
                 linear_z = 0.0f,
                 angular_x = 0.0f,
                 angular_y = 0.0f,
                 angular_z = 0.0f,
                 duration = 0,*/

            };

            /*
            message.points[0] = new MessageTypes.Trajectory.JointTrajectoryPoint();
            message.points[0].positions = new double[] { 0, 0, 0, 0, 0, 0, 0 };
            message.points[0].velocities = new double[] { 0, 0, 0, 0, 0, 0 };
            message.points[0].accelerations = new double[] { 0, 0, 0, 0, 0, 0 };
            message.points[0].effort = new double[] { 0, 0, 0, 0, 0, 0 };
            message.points[0].time_from_start = new MessageTypes.Std.Duration { nsecs = 20000000 }; */

        }

        private void FixedUpdate()
        {
            //int i = 0;
            foreach (BioIK.BioSegment segment in hebi.Segments)
            {
                if (segment.Joint != null)
                {
                    double angle = 0.0;

                    if (segment.Joint.X.IsEnabled())
                    {
                        angle = segment.Joint.X.GetCurrentValue();
                    }
                    else if (segment.Joint.Y.IsEnabled())
                    {
                        angle = segment.Joint.Y.GetCurrentValue();
                    }
                    else if (segment.Joint.Z.IsEnabled())
                    {
                        angle = segment.Joint.Z.GetCurrentValue();
                    }
                    
                    //message.twist.linear_x = (float)angle * Mathf.PI / 180;
                    //message.twist.linear_y = (float)angle * Mathf.PI / 180;
                    //message.twist.linear_z = (float)angle * Mathf.PI / 180;
                    //message.twist.angular_x = (float)angle;
                    //message.twist.angular_y = (float)angle;
                    //message.twist.angular_z = (float)angle;
                    
                    message.twist.linear_x = 0.0f;
                    message.twist.linear_y = 0.0f;
                    message.twist.linear_z = 0.0f;
                    message.twist.angular_x = 0.0f;
                    message.twist.angular_y = 0.0f;
                    message.twist.angular_z = 0.0f;
                   
                }
            }
            Publish(message);
        }
    }
}