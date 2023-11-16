using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


namespace RosSharp.RosBridgeClient
{
    public class RangeSensor : UnitySubscriber<MessageTypes.Sensor.Range>
    {

        float rangeSensorValue;
       
       MessageTypes.Sensor.Range message;

        protected override void Start()
        {
            base.Start();
            //InitializeMessage();
        }

        // private void InitializeMessage()
        // {
        //     message = new MessageTypes.Sensor.Range();
        // }

        protected override void ReceiveMessage(MessageTypes.Sensor.Range message)
        {
            rangeSensorValue = (float)message.range;
        }

        void FixedUpdate()
        {
           
           print("Object Distance: " + rangeSensorValue + " mm"); 
           
               
        }

    }
}