/*
© Siemens AG, 2017-2019
Author: Dr. Martin Bischoff (martin.bischoff@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
//using System.Collections.Generic;

namespace RosSharp.RosBridgeClient
{
    public class JointStateSubscriber : UnitySubscriber<MessageTypes.Sensor.JointState>
    {

        public Transform shoulder; 
        public Transform half_arm1; 
        public Transform half_arm2; 
        public Transform forearm; 
        public Transform spherical_wrist1; 
        public Transform spherical_wrist2; 
        public Transform braceletlink;

        public List<string> JointNames;
        public List<JointStateWriter> JointStateWriters;

        float link_1;
        float link_2;
        float link_3;
        float link_4;
        float link_5;
        float link_6;
        float link_7;

        MessageTypes.Sensor.JointState message;


        protected override void Start()
        {
            base.Start();
      
        }

        protected override void ReceiveMessage(MessageTypes.Sensor.JointState message)
        {
       
            for (int i = 0; i < message.name.Length; i++)
            {
                link_1 = (float)message.position[0];
                link_2 = (float)message.position[1];
                link_3 = (float)message.position[2];
                link_4 = (float)message.position[3];
                link_5 = (float)message.position[4];
                link_6 = (float)message.position[5];
                link_7 = (float)message.position[6];
                
                
                /*
                print("joint 1 " + message.position[0]);
                print("joint 2 " + message.position[1]);
                print("joint 3 " + message.position[2]);
                print("joint 4 " + message.position[3]);
                print("joint 5 " + message.position[4]);
                print("joint 6 " + message.position[5]);
                print("joint 7 " + message.position[6]);
                print("joint 8 " + message.position[7]);
                print("joint 9 " + message.position[8]);*/

            }
        }

        void FixedUpdate()
        {
            
               float shoulderX = shoulder.localEulerAngles.x;
               float shoulderZ = shoulder.localEulerAngles.z;
               shoulder.localEulerAngles = new Vector3(shoulderX, link_1 * Mathf.Rad2Deg, shoulderZ);

               float half_arm1Y = half_arm1.localEulerAngles.y;
               float half_arm1Z = half_arm1.localEulerAngles.z;
               half_arm1.localEulerAngles = new Vector3(Mathf.Clamp(-link_2 * Mathf.Rad2Deg, -90, 90), half_arm1Y, half_arm1Z);

               float half_arm2Y = half_arm2.localEulerAngles.y;
               float half_arm2Z = half_arm2.localEulerAngles.z;
               half_arm2.localEulerAngles = new Vector3(Mathf.Clamp(link_3 * Mathf.Rad2Deg, -90, 90), half_arm2Y, half_arm2Z);

               float forearmY = forearm.localEulerAngles.y;
               float forearmZ = forearm.localEulerAngles.z;
               forearm.localEulerAngles = new Vector3(Mathf.Clamp(-link_4 * Mathf.Rad2Deg, -90, 90), forearmY, forearmZ);

               float spherical_wrist1Y = spherical_wrist1.localEulerAngles.y;
               float spherical_wrist1Z = spherical_wrist1.localEulerAngles.z;
               spherical_wrist1.localEulerAngles = new Vector3(Mathf.Clamp(link_5 * Mathf.Rad2Deg, -90, 90), spherical_wrist1Y, spherical_wrist1Z);

               float spherical_wrist2Y = spherical_wrist2.localEulerAngles.y;
               float spherical_wrist2Z = spherical_wrist2.localEulerAngles.z;
               spherical_wrist2.localEulerAngles = new Vector3(Mathf.Clamp(-link_6 * Mathf.Rad2Deg, -90, 90), spherical_wrist2Y, spherical_wrist2Z);

               float braceletlinkY = braceletlink.localEulerAngles.y; 
               float braceletlinkZ = braceletlink.localEulerAngles.z;
               braceletlink.localEulerAngles = new Vector3(Mathf.Clamp(link_7 * Mathf.Rad2Deg, -90, 90), braceletlinkY, braceletlinkZ);


          

        }

    }
}
