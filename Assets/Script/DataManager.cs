using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//시스템에서 파일을 생성하기 위한 DLL
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : MonoBehaviour
{
    
    private static DataManager _instance = null;
    public static DataManager instance { get { return _instance; } }
    public string currentScene = "Level1";
    public int playerHP
    {
        get;
        set;
    }
    private void Awake()
    {
        playerHP = 3;
        _instance = this;
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


    void Update()
    {
        
    }

    public void Save()
    {
        SaveData saveData = new SaveData();
        saveData.sceneName = currentScene; 
        saveData.playerHP = playerHP;

        //파일 생성
        FileStream fileStream = File.Create(Application.persistentDataPath+"/save.dat");
        Debug.Log("저장 파일 생성");

        //직렬화
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream,saveData);

        //파일을 닫는다.
        fileStream.Close();
    }

    public void Load()
    {
        //파일이 있는지 확인한다.
        if (File.Exists(Application.persistentDataPath + "/save.dat") == true)
        {
            FileStream fileStream = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Open);
            if (fileStream != null && fileStream.Length > 0)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                SaveData saveData = (SaveData)binaryFormatter.Deserialize(fileStream);
                playerHP = saveData.playerHP;
                UIManager.instance.PlayerHP();
                currentScene = saveData.sceneName;
                fileStream.Close();


            }
        }
    }
}
