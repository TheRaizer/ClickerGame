using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : Singleton<SaveSystem>
{
    private const float ANDROID_SAVE_INTERVAL = 0.5f;
    private float androidSaveTimer;

    private string clickerDataPath;
    private string storeItemDataPath;
    private string miniGameDataPath;

    private ClickCountManager clickManager;
    private StoreManager storeManager;
    private MiniGameManager miniGameManager;

    private void Awake()
    {
        clickManager = FindObjectOfType<ClickCountManager>();
        storeManager = FindObjectOfType<StoreManager>();
        miniGameManager = FindObjectOfType<MiniGameManager>();

        clickerDataPath = Path.Combine(Application.persistentDataPath, "clickerData.dat");
        storeItemDataPath = Path.Combine(Application.persistentDataPath, "storeItemData.dat");
        miniGameDataPath = Path.Combine(Application.persistentDataPath, "miniGameData.dat");

        if (clickManager == null || storeManager == null || miniGameManager == null)
        {
            Debug.LogError("Needed save system objects not found");
        }

        androidSaveTimer = ANDROID_SAVE_INTERVAL;
    }

    private void Update()
    {
        AndroidSave();
    }

    private void AndroidSave()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            androidSaveTimer -= Time.deltaTime;
            if (androidSaveTimer <= 0)
            {
                androidSaveTimer = ANDROID_SAVE_INTERVAL;
                SaveAllData();
            }
        }
    }

    private void SaveClickerData()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(clickerDataPath, FileMode.Create))
        {
            ClickerData data = clickManager.GenerateClickerData();

            formatter.Serialize(stream, data);
            stream.Close();
        }

    }

    public ClickerData LoadClickerData()
    {
        if (File.Exists(clickerDataPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(clickerDataPath, FileMode.Open))
            {
                ClickerData data = formatter.Deserialize(stream) as ClickerData;
                stream.Close();
                return data;
            }
        }
        else
        {
            Debug.Log("no file found in " + clickerDataPath);
            return null;
        }
    }

    private void SaveStoreItemData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(storeItemDataPath, FileMode.Create))
        {
            StoreItemData data = storeManager.GenerateStoreItemData();

            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    public StoreItemData LoadStoreItemData()
    {
        if (File.Exists(storeItemDataPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(storeItemDataPath, FileMode.Open))
            {
                StoreItemData data = formatter.Deserialize(stream) as StoreItemData;
                stream.Close();
                return data;
            }
        }
        else
        {
            Debug.Log("no file found in " + storeItemDataPath);
            return null;
        }
    }

    public void SaveMiniGameData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(miniGameDataPath, FileMode.Create))
        {
            MiniGameData data = miniGameManager.GenerateMiniGameData();

            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    public MiniGameData LoadMiniGameData()
    {
        if (File.Exists(miniGameDataPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(miniGameDataPath, FileMode.Open))
            {
                MiniGameData data = formatter.Deserialize(stream) as MiniGameData;
                stream.Close();
                return data;
            }
        }
        else
        {
            Debug.Log("no file found in " + miniGameDataPath);
            return null;
        }
    }

    private void SaveAllData()
    {
        SaveClickerData();
        SaveStoreItemData();
        SaveMiniGameData();
    }

    public void OnApplicationQuit()
    {
        SaveAllData();
    }
}
