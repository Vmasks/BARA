using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSave;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.Text;
using System.Security.Cryptography;
using System;

namespace GameSave 
{
    public class GameSaveMgr : MonoBehaviour
    {
        //当前脚本单例
        private static GameSaveMgr _Instance = null;
        private string rootPath;
        private string targetPath;

        private void Awake()
        {
            rootPath = Application.persistentDataPath;
            targetPath = rootPath + "/REGameData";
        }

        public static GameSaveMgr GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new GameObject("_GameSaveMgr").AddComponent<GameSaveMgr>();
            }
            return _Instance;
        }
        public string[] GetFileNameList()
        {
            string[] dirList = Directory.GetFiles(targetPath);
            //string[] dirList = Directory
            //    .EnumerateFiles(targetPath, "*", SearchOption.AllDirectories)
            //    .Select(Path.GetFileName);
            for (int i = 0; i < dirList.Length; i++)
            {
                //dirList[i].Replace(@'\', '/');

                //print(dirList[i]);
                //dirList[i] = dirList[i].Substring(dirList[i].LastIndexOf('/') + 1);
                //从路径中获取文件名
                dirList[i] = Path.GetFileName(dirList[i]);
                //print(dirList[i]);
                //print(dirList[i]);
            }
            return dirList;
        }
        /// <summary>
        /// 对当前游戏进度进行存档
        /// </summary>
        /// <param name="storeName">存档名称</param>
        /// <returns>存档成功返回真</returns>
        public bool Save(string storeName)
        {
            //print(targetPath);
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = File.Create(targetPath + '/' + storeName);
            GameSaveData gd = new GameSaveData();
            gd.storeName = storeName;
            gd.globalSceneName = GameMgr.GetInstance().sceneName;
            gd.gameTime = Global.gameTime;
            gd.saveTime = System.DateTime.Now;
            gd.player = GameMgr.GetInstance().mainCharacter;
            gd.name = GameMgr.GetInstance().playerName;
            gd.sceneName = SceneManager.GetActiveScene().name;
            gd.posX = GameObject.FindWithTag("Player").transform.position.x;
            gd.posY = GameObject.FindWithTag("Player").transform.position.y;
            gd.allDialogue = Global.dialogCollection;
            gd.requirements = Global.requirements;
            gd.npcMood = Global.npcMood;
            gd.hasOpenTipsPanel = Global.hasOpenTipsPanel;
            gd.hasCollectedMood = Global.hasCollectedMood;
            // log 先记录 在保存
            Global.AddDetailedLog($"存档 {storeName}");
            gd.detailedLog = Global.detailedLog;
            gd.simpleLog = Global.simpleLog;


            string saveStr = Encrypt(JsonConvert.SerializeObject(gd));
            formatter.Serialize(fs, saveStr);
            fs.Close();
            return true;
            //print(saveStr);
        }

        // 其实导出和存档是一回事，只是保存的路径不同，应该写成一个函数，以后有空再改吧。
        public bool ExportResult(string storeName, string savePath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = File.Create(savePath + '/' + storeName);
            GameSaveData gd = new GameSaveData();
            gd.storeName = storeName;
            gd.globalSceneName = GameMgr.GetInstance().sceneName;
            gd.gameTime = Global.gameTime;
            gd.saveTime = System.DateTime.Now;
            gd.player = GameMgr.GetInstance().mainCharacter;
            gd.name = GameMgr.GetInstance().playerName;
            gd.sceneName = SceneManager.GetActiveScene().name;
            gd.posX = GameObject.FindWithTag("Player").transform.position.x;
            gd.posY = GameObject.FindWithTag("Player").transform.position.y;
            gd.allDialogue = Global.dialogCollection;
            gd.requirements = Global.requirements;
            gd.npcMood = Global.npcMood;
            gd.hasOpenTipsPanel = Global.hasOpenTipsPanel;
            gd.hasCollectedMood = Global.hasCollectedMood;
            // log 先记录 再导出
            // Global.AddDetailedLog($"导出成果 {storeName}");
            gd.detailedLog = Global.detailedLog;
            gd.simpleLog = Global.simpleLog;


            string saveStr = Encrypt(JsonConvert.SerializeObject(gd));

            // string saveStr = JsonConvert.SerializeObject(gd);

            formatter.Serialize(fs, saveStr);
            fs.Close();
            return true;
            //print(saveStr);
        }

        /// <summary>
        /// 自动存档，实现部分挂在Player身上，间隔固定时间调用。
        /// </summary>
        public void AutoSave()
        {
            Save($"{GameMgr.GetInstance().playerName}的自动存档");
        }

        public GameSaveData Load(string storeName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(Path.Combine(targetPath, storeName)))
            {
                FileStream fs = File.Open(targetPath + '/' + storeName, FileMode.Open);
                //先解密再转化为对象
                return JsonConvert.DeserializeObject<GameSaveData>(Decrypt((string)formatter.Deserialize(fs)));
            }
            else
            {
                return null;
            }
        }
        public static string ConvertTimeStr(float gameTime)
        {
            return $"{Mathf.Floor(gameTime / 3600)}时 {Mathf.Floor((gameTime % 3600) / 60)}分 {Mathf.Floor(gameTime % 60)}秒";
        }
        public static string Encrypt(string toE)
        {
            //加密和解密采用相同的key,具体自己填，但是必须为32位//
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes("11112222333344445555666677778888");
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toE);
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string Decrypt(string toD)
        {
            //加密和解密采用相同的key,具体值自己填，但是必须为32位//
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes("11112222333344445555666677778888");
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] toEncryptArray = Convert.FromBase64String(toD);
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}


