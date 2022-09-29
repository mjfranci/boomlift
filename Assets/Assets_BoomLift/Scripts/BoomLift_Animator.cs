using UnityEngine;
using System.Collections;

// This is a class that handles the animation of all of the parts for the boom lift
// Things such as the lift arms moving up/down and extending,  and the doors opening, etc..

public class BoomLift_Animator : MonoBehaviour
{
    public Animator boomLiftAnimator;

    public float    ArmExtension = 0;
    public float    BaseTurretTurn = 0;
    public float    MainArmLift = 0;
    public float    MainArmPitch    = 0;
    public float    PlatformYaw = 0;
    public float    WheelSpin = 0;
    public float    WheelTurn = 0;
    public bool     DoorOpen = false;

    public float    Controls_ArmExtend;
    public float    Controls_ArmPitch;
    public float    Controls_ArmYaw;
    public float    Controls_JoystickLeft_X;
    public float    Controls_JoystickLeft_Y;
    
    public bool     Controls_EmergencyStop;
    public float    Controls_JoystickRight_X;
    public float    Controls_JoystickRight_Y;
    public float    Controls_Platform_Pitch;
    public float    Controls_Platform_Yaw;
    public float    Controls_SpeedDial;

    public Material cableWireFront;
    public Material cableWireEnd;

    public Vector2  cableWireFront_Scale;
    public Vector2  cableWireEnd_Scale;

	void Start ()
    {
	    cableWireEnd_Scale = new Vector2(1,1);
        cableWireFront_Scale = new Vector2(1,1);
	}
	

	void Update ()
    {
        if (boomLiftAnimator != null)
        {
            // The following code will adjust the scale of the wirecables based on how far the arm extends ( The front part of the cable shrinks as the Arm extends;  while the end part of the cable grows as the arm extends ).
            ArmExtension = Mathf.Clamp( ArmExtension, 0f, 100f );

            cableWireEnd_Scale.x = (ArmExtension / 4.0f) + 1.0f;
            cableWireFront_Scale.x = 1.0f - ( ArmExtension / 100f );

            cableWireFront.SetTextureScale( "_MainTex", cableWireFront_Scale );
            cableWireFront.SetTextureScale( "_BumpMap", cableWireFront_Scale );    
            cableWireFront.SetTextureScale( "_SpecGlossMap", cableWireFront_Scale );        

            cableWireEnd.SetTextureScale( "_MainTex", cableWireEnd_Scale );
            cableWireEnd.SetTextureScale( "_BumpMap", cableWireEnd_Scale );    
            cableWireEnd.SetTextureScale( "_SpecGlossMap", cableWireEnd_Scale );        



            boomLiftAnimator.SetFloat( "ArmExtension", ArmExtension );
            boomLiftAnimator.SetFloat( "BaseTurretTurn", Mathf.Abs(BaseTurretTurn) % 360 );

            boomLiftAnimator.SetFloat( "MainArmLift", MainArmLift * 0.1f );
            boomLiftAnimator.SetFloat( "MainArmPitch", MainArmPitch * 0.1f );
            boomLiftAnimator.SetFloat( "PlatformYaw", PlatformYaw * 0.1f );
            boomLiftAnimator.SetFloat( "WheelSpin", WheelSpin * 0.05f );
            boomLiftAnimator.SetFloat( "WheelTurn", WheelTurn * 0.5f );

            boomLiftAnimator.SetBool( "PlatformDoorOpen", DoorOpen );

            boomLiftAnimator.SetFloat( "CTRL_ArmExtend", Controls_ArmExtend );
            boomLiftAnimator.SetFloat( "CTRL_ArmPitch", Controls_ArmPitch );
            boomLiftAnimator.SetFloat( "CTRL_ArmYaw", Controls_ArmYaw );
            boomLiftAnimator.SetFloat( "CTRL_JoystickLeft_X", Controls_JoystickLeft_X );
            boomLiftAnimator.SetFloat( "CTRL_JoystickLeft_Y", Controls_JoystickLeft_Y );

            boomLiftAnimator.SetFloat( "CTRL_JoystickRight_X", Controls_JoystickRight_X );
            boomLiftAnimator.SetFloat( "CTRL_JoystickRight_Y", Controls_JoystickRight_Y );
            boomLiftAnimator.SetFloat( "CTRL_PlatformPitch", Controls_Platform_Pitch );
            boomLiftAnimator.SetFloat( "CTRL_PlatformYaw", Controls_Platform_Yaw );
            boomLiftAnimator.SetFloat( "CTRL_SpeedDial", Controls_SpeedDial );

            // ################ Speed Dial ################
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Controls_SpeedDial = 0;
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                Controls_SpeedDial = 50;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                Controls_SpeedDial = 100;
            }

            //// ################ Platform Yaw ################
            if (Input.GetKeyDown(KeyCode.A) && Controls_Platform_Yaw > -1)
            {
                Controls_Platform_Yaw += -1f;
            }

            if (Input.GetKeyDown(KeyCode.Q) && Controls_Platform_Yaw < 1)
            {
                Controls_Platform_Yaw += 1f;
            }

            if (PlatformYaw >= -10 && PlatformYaw <= 10)
            {
                PlatformYaw = PlatformYaw + (Controls_SpeedDial / 100 * Controls_Platform_Yaw * 0.05f);
                if(PlatformYaw<-10)
                {
                    PlatformYaw = -10;
                }
                if (PlatformYaw > 10)
                {
                    PlatformYaw = 10;
                }
            }

            // ################ Arm Pitch ################
            if (Input.GetKeyDown(KeyCode.S) && Controls_ArmPitch > -1)
            {
                Controls_ArmPitch += -1f;
            }

            if (Input.GetKeyDown(KeyCode.W) && Controls_ArmPitch < 1)
            {
                Controls_ArmPitch += 1f;
            }

            if (MainArmLift >= 0 && MainArmLift <= 10)
            {
                MainArmLift = MainArmLift + (Controls_SpeedDial / 100 * Controls_ArmPitch * 0.0125f);
                if(MainArmLift < 0)
                {
                    MainArmLift = 0;
                }
                if(MainArmLift > 10)
                {
                    MainArmLift = 10;
                }
            }

            // ################ Arm Yaw ################
            if (Input.GetKeyDown(KeyCode.D) && Controls_ArmYaw > -1)
            {
                Controls_ArmYaw += -1f;
            }

            if (Input.GetKeyDown(KeyCode.E) && Controls_ArmYaw < 1)
            {
                Controls_ArmYaw += 1f;
            }

            if (true)
            {
                BaseTurretTurn = BaseTurretTurn + (Controls_SpeedDial / 100 * Controls_ArmYaw * 0.125f);
            }

            // ################ Platform Pitch ################
            if (Input.GetKeyDown(KeyCode.F) && Controls_Platform_Pitch > -1)
            {
                Controls_Platform_Pitch += -1f;
            }

            if (Input.GetKeyDown(KeyCode.R) && Controls_Platform_Pitch < 1)
            {
                Controls_Platform_Pitch += 1f;
            }

            if (MainArmPitch >= 0 && MainArmPitch <= 10)
            {
                MainArmPitch = MainArmPitch + (Controls_SpeedDial / 100 * Controls_Platform_Pitch * 0.0125f);
                if(MainArmPitch < 0)
                {
                    MainArmPitch = 0;
                }
                if (MainArmPitch > 10)
                {
                    MainArmPitch = 10;
                }
            }

            // ################ Arm Extend ################
            if (Input.GetKeyDown(KeyCode.G) && Controls_ArmExtend > -1)
            {
                Controls_ArmExtend += -1f;
            }

            if (Input.GetKeyDown(KeyCode.T) && Controls_ArmExtend < 1)
            {
                Controls_ArmExtend += 1f;
            }

            if (ArmExtension >= 0 && ArmExtension <= 100)
            {
                ArmExtension = ArmExtension + (Controls_SpeedDial / 100 * Controls_ArmExtend * 0.0555f);
                if (ArmExtension < 0)
                {
                    ArmExtension = 0;
                }
                if (ArmExtension > 100)
                {
                    ArmExtension = 100;
                }
            }

        }
    }
}
