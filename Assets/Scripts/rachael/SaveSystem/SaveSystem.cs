using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Himanshu;
using UnityEngine;

namespace rachael.SaveSystem
{
    public static class SaveSystem
    {
        public static void SavePlayer(PlayerSave _player, bool _isSafeRoom = false)
        {
            
            if(!Directory.Exists(Application.persistentDataPath + "/player/"))
            {    
                //if it doesn't, create it
                Directory.CreateDirectory(Application.persistentDataPath + "/player/");
            }
            
            BinaryFormatter formatter = new BinaryFormatter();
            string path = "";
            if (!_isSafeRoom)
                path = Application.persistentDataPath + "/player/player.default";
            else
                path = Application.persistentDataPath + "/player/player.safeRoom";
            FileStream stream = new FileStream(path, FileMode.Create);

            PlayerData data = new PlayerData(_player);

            formatter.Serialize(stream, data);
            stream.Close();
        }
    
        public static void SaveNarrator()
        {
            if(!Directory.Exists(Application.persistentDataPath + "/narrator/"))
            {    
                //if it doesn't, create it
                Directory.CreateDirectory(Application.persistentDataPath + "/narrator/");
            }
            BinaryFormatter formatter = new BinaryFormatter();
            string path = "";
            path = Application.persistentDataPath + "/narrator/narrator.default";
            
            FileStream stream = new FileStream(path, FileMode.Create);


            NarratorDialogue dialogue = GameObject.FindObjectOfType<Himanshu.Narrator>().m_narratorDialogue;
            dialogue.SetValues(GameObject.FindObjectOfType<Himanshu.Narrator>());
            formatter.Serialize(stream, dialogue);
            stream.Close();
        }

        public static void DeleteNarrator()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = "";
            path = Application.persistentDataPath + "/narrator/";
            DirectoryInfo directory = new DirectoryInfo(path);
            directory.Delete(true);
            Directory.CreateDirectory(path);
        
        }

        public static void DeletePlayer()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = "";
            path = Application.persistentDataPath + "/player/";
            DirectoryInfo directory = new DirectoryInfo(path);
            directory.Delete(true);
            Directory.CreateDirectory(path);

        }

        public static NarratorDialogue LoadNarrator()
        {
            // BinaryFormatter formatter = new BinaryFormatter();
            // string path = "";
            // path = Application.persistentDataPath + "/narrator.default";
            //     
            // FileStream stream = new FileStream(path, FileMode.Create);
            //
            //
            // NarratorDialogue dialogue = GameObject.FindObjectOfType<Narrator>().m_narratorDialogue;
            // dialogue.SetValues(GameObject.FindObjectOfType<Narrator>());
            // formatter.Serialize(stream, dialogue);
            // stream.Close();
        
            string path = Application.persistentDataPath + ("/narrator/narrator.default");

            if(File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                NarratorDialogue dialogue = formatter.Deserialize(stream) as NarratorDialogue;

                dialogue?.RetrieveValues(GameObject.FindObjectOfType<Himanshu.Narrator>());
                stream.Close();
                return dialogue;
            }
            else
            {
                return null;
                Debug.LogError("Save File not found in " + path);
            }
        }

    
        public static PlayerData LoadPlayer(bool _isSafeRoom = false)
        {
        
            string path = Application.persistentDataPath + "/player/player" + (_isSafeRoom ? ".safeRoom" : ".default");

            if(File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.LogError("Save File not found in " + path);
                return null;
            }
        }
    }
}