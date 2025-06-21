using System;
using UnityEngine;

public class AdapterData
{

    // Métodos com o Player

    [Serializable]
    public class PlayerData
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 velocity;
    }
    public static void GetPlayer(PlayerData data, ref Player player){
        player.transform.position = data.position;
        player.transform.rotation = data.rotation;
        player.rb.velocity = data.velocity;
    }
    public static PlayerData GetPlayerData(Player player){
        PlayerData data = new PlayerData();
        data.position = player.transform.position;
        data.rotation = player.transform.rotation;
        data.velocity = player.rb.velocity;

        return data;
    }




    [Serializable]
    public class CameraData{
        public Vector3 position;
    }
    public static void GetCamera(CameraData data, ref Transform camera ){
        camera.position = data.position;
    }
    public static CameraData GetCameraData(Transform camera){
        CameraData data = new CameraData();
        data.position = camera.position;

        return data;
    }




    [Serializable]
    public class LeverData
    {
        public Vector3 position;
        public Quaternion rotation;
        public bool isButton;
        public bool state;
        public bool pressable;
    }

    public static void GetLever(LeverData data, ref Lever lever ){
        lever.transform.position = data.position;
        lever.transform.rotation = data.rotation;
        lever.isButton = data.isButton;
        lever.state = data.state;
        lever.pressable = data.pressable;
    }
    public static LeverData GetLeverData(Lever lever){
        LeverData data = new LeverData();
        data.position = lever.transform.position;
        data.rotation = lever.transform.rotation;
        data.isButton = lever.isButton;
        data.state = lever.state;
        data.pressable = lever.pressable;

        return data;
    }



    [Serializable]
    public class PlatformData
    {
        public Vector3 position;
        public Quaternion rotation;
        public Waypoint target;
        public Vector3 targetPosition;
        public bool moveUntilWaypoint;
        public bool keepMoving;
        public bool moving;
    }

    public static void GetPlatform(PlatformData data, ref Platform platform){
        platform.transform.position = data.position;
        platform.transform.rotation = data.rotation;
        platform.target = data.target;
        platform.targetPosition = data.targetPosition;
        platform.moveUntilWaypoint = data.moveUntilWaypoint;
        platform.keepMoving = data.keepMoving;
        platform.moving = data.moving;
    }
    public static PlatformData GetPlatformData(Platform platform){
        PlatformData data = new PlatformData();
        data.position = platform.transform.position;
        data.rotation = platform.transform.rotation;
        data.target = platform.target;
        data.targetPosition = platform.targetPosition;
        data.moveUntilWaypoint = platform.moveUntilWaypoint;
        data.keepMoving = platform.keepMoving;
        data.moving = platform.moving;

        return data;
    }




    [Serializable]
    public class WaypointData
    {
        public Vector3 position;
        public Vector3 nextPosition;
    }

    public static void GetWaypoint(WaypointData data, ref Waypoint waypoint){
        waypoint.transform.position = data.position;
        waypoint.next.transform.position = data.nextPosition;
    }
    public static WaypointData GetWaypointData(Waypoint waypoint){
        WaypointData data = new WaypointData();
        data.position = waypoint.transform.position;
        data.nextPosition = waypoint.next.transform.position;

        return data;
    }


    
    [Serializable]
    public class GroundData
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
        //public GameObject parent;
    }
    public static void GetGround(GroundData data, ref GameObject ground){
        ground.transform.position = data.position;
        ground.transform.rotation = data.rotation;
        ground.transform.localScale = data.scale;
    }
    public static GroundData GetGroundData(GameObject ground){
        GroundData data = new GroundData();
        data.position = ground.transform.position;
        data.rotation = ground.transform.rotation;
        data.scale = ground.transform.localScale;
        //data.parent = ground.transform.parent.gameObject;

        return data;
    }




    [Serializable]
    public class SceneData
    {
        public PlayerData player;
        public CameraData camera;
        public LeverData[] levers;
        public PlatformData[] platforms;
        public WaypointData[] waypoints;
        public GroundData[] grounds;
    }
}
