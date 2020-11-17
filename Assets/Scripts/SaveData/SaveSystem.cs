using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : Singleton<SaveSystem>
{
    private string clickerDataPath;
    private string storeItemDataPath;

    private ClickCountManager clickManager;
    private StoreManager storeManager;

    private void Awake()
    {
        clickManager = FindObjectOfType<ClickCountManager>();
        storeManager = FindObjectOfType<StoreManager>();
        clickerDataPath = Path.Combine(Application.persistentDataPath, "clickerData.dat");
        storeItemDataPath = Path.Combine(Application.persistentDataPath, "storeItemData.dat");

        if (clickManager == null || storeManager == null)
        {
            Debug.LogError("Needed save system objects not found");
        }
    }

    public void EraseFiles()
    {
        File.Delete(clickerDataPath);
        File.Delete(storeItemDataPath);
    }

    public void SaveClickerData()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(clickerDataPath, FileMode.Create))
        {
            ClickerData data = clickManager.GenerateClickerData();

            formatter.Serialize(stream, data);
            Debug.Log("save click data");
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
                Debug.Log("load click data");
                return data;
            }
        }
        else
        {
            Debug.Log("no file found in " + clickerDataPath);
            return null;
        }
    }

    public void SaveStoreItemData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(storeItemDataPath, FileMode.Create))
        {

            StoreItemData data = storeManager.GenerateStoreItemData();

            formatter.Serialize(stream, data);
            Debug.Log("save store data");
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
                Debug.Log("load store data");
                return data;
            }
        }
        else
        {
            Debug.Log("no file found in " + storeItemDataPath);
            return null;
        }
    }
}
