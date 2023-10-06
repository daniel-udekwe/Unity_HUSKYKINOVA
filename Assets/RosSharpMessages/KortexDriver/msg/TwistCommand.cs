/* 
 * This message is auto generated by ROS#. Please DO NOT modify.
 * Note:
 * - Comments from the original code will be written in their own line 
 * - Variable sized arrays will be initialized to array of size 0 
 * Please report any issues at 
 * <https://github.com/siemens/ros-sharp> 
 */



namespace RosSharp.RosBridgeClient.MessageTypes.KortexDriver
{
    public class TwistCommand : Message
    {
        public const string RosMessageName = "kortex_driver/TwistCommand";

        public uint reference_frame { get; set; }
        public Twist twist { get; set; }
        public uint duration { get; set; }

        public TwistCommand()
        {
            this.reference_frame = 0;
            this.twist = new Twist();
            this.duration = 0;
        }

        public TwistCommand(uint reference_frame, Twist twist, uint duration)
        {
            this.reference_frame = reference_frame;
            this.twist = twist;
            this.duration = duration;
        }
    }
}