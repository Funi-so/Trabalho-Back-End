using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static AdapterData;
using Unity.Mathematics;
using UnityEngine.Networking;

public class SaveController : MonoBehaviour
{
    public Player player;
    public Transform cameraHolder;
    public GameObject groundPrefab, leverPrefab, platformPrefab, waypointPrefab;
    public string fileName = "save";
    string path;
    string level;
    void Start()
    {
        path = Application.persistentDataPath + "/" + fileName + ".json";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            Save();
        }
        if(Input.GetKeyDown(KeyCode.L)){
            LoadOffline();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1)){

            level = "Level1";
            StartCoroutine("LoadOnline");
        }
    }
    void Save(){
        List<GroundData>GroundDataList = new List<GroundData>();
        GameObject[] grounds = GameObject.FindGameObjectsWithTag("Ground"); 
        foreach(GameObject ground in grounds){
            GroundDataList.Add(GetGroundData(ground));
        }

        List<PlatformData>PlatformDataList = new List<PlatformData>();
        Platform[] platforms = FindObjectsByType<Platform>(FindObjectsSortMode.None);
        foreach(Platform platform in platforms){
            PlatformDataList.Add(GetPlatformData(platform));
        }

        List<LeverData>LeverDataList = new List<LeverData>();
        Lever[] levers = FindObjectsByType<Lever>(FindObjectsSortMode.None);
        foreach(Lever lever in levers){
            LeverDataList.Add(GetLeverData(lever));
        }

        List<WaypointData>WaypointDataList = new List<WaypointData>();
        Waypoint[] waypoints = FindObjectsByType<Waypoint>(FindObjectsSortMode.None);
        foreach(Waypoint waypoint in waypoints){
            WaypointDataList.Add(GetWaypointData(waypoint));
        }

        SceneData sceneData = new SceneData();

        sceneData.player = GetPlayerData(player);
        sceneData.camera = GetCameraData(cameraHolder);
        sceneData.grounds = GroundDataList.ToArray();
        sceneData.levers = LeverDataList.ToArray();
        sceneData.platforms = PlatformDataList.ToArray();
        sceneData.waypoints = WaypointDataList.ToArray();

        string json = JsonUtility.ToJson(sceneData);
        Debug.Log(json);
        Debug.Log(path);
        File.WriteAllText(path, json);
    }

    void LoadOffline(){
        string json = File.ReadAllText(path);
        Load(json);
    }
    void Load(string json){
        Clear();

        SceneData sceneData = JsonUtility.FromJson<SceneData>(json);
        GetPlayer(sceneData.player, ref player);
        GetCamera(sceneData.camera, ref cameraHolder);

        foreach(GroundData ground in sceneData.grounds){
            GameObject instance = Instantiate(groundPrefab, ground.position, ground.rotation);
            instance.transform.localScale = ground.scale;
        }
        foreach(LeverData lever in sceneData.levers){
            Debug.Log("lever instanced");
            GameObject instance = Instantiate(leverPrefab, lever.position, lever.rotation/*, lever.parent.transform*/);
            Lever instanceScript = instance.GetComponent<Lever>();
            instanceScript.isButton = lever.isButton;
            instanceScript.state = lever.state;
            instanceScript.pressable = lever.pressable;
        }
        foreach(WaypointData waypoint in sceneData.waypoints){
            GameObject instance = Instantiate(waypointPrefab, waypoint.position, quaternion.identity);
            instance.GetComponent<Waypoint>().nextPosition = waypoint.nextPosition;
        }
        foreach(PlatformData platform in sceneData.platforms){
            GameObject instance = Instantiate(platformPrefab, platform.position, platform.rotation);
            Platform instanceScript = instance.GetComponent<Platform>();
            instanceScript.target = platform.target;
            instanceScript.targetPosition = platform.targetPosition;
            instanceScript.moveUntilWaypoint = platform.moveUntilWaypoint;
            instanceScript.moving = platform.moving;
            instanceScript.keepMoving = platform.keepMoving;
        }
    }

    void Clear(){
        GameObject[] grounds = GameObject.FindGameObjectsWithTag("Ground"); 
        foreach(GameObject ground in grounds){
            Destroy(ground);
        }

        Platform[] platforms = FindObjectsByType<Platform>(FindObjectsSortMode.None);
        foreach(Platform platform in platforms){
            Destroy(platform.gameObject);
        }

        Lever[] levers = FindObjectsByType<Lever>(FindObjectsSortMode.None);
        foreach(Lever lever in levers){
            Destroy(lever.gameObject);
        }

        Waypoint[] waypoints = FindObjectsByType<Waypoint>(FindObjectsSortMode.None);
        foreach(Waypoint waypoint in waypoints){
            Destroy(waypoint.gameObject);
        }

        Debug.Log("Clear");
    }

    IEnumerator LoadOnline(){
        Debug.Log("Load Level");

        string url = "https://funi-so.github.io/Trabalhos-Back-End/" + level + ".json";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success){
            string json = request.downloadHandler.text;
            Load(json);
        }
    }
}
