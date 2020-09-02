using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using UnityEngine;
using UnityEditor.VersionControl;


public class AssetItem : CAsset
{
    public int m_nType = 0;
    public string m_PrefabName = "";
    public float m_fValue = 0;
    public string m_Desc = "";
}

public class AssetMgr
{

    static AssetMgr _instance = null;

    public static AssetMgr Inst
    {
        get
        {
            if (_instance == null) _instance = new AssetMgr();

            return _instance;
        }
    }

    private AssetMgr()
    {
        IsInstalled = false;
    }

    // =============================================================================== //

    public bool IsInstalled { get; set; }

    public List<AssetStage> m_AssStages = new List<AssetStage>();
    public List<AssetItem> m_AssItems = new List<AssetItem>();

    public StringBuilder m_Builder = new StringBuilder();


    public void Initilaize()
    {
        ParsingStage();
        ParsingItem();
    }

    public void ParsingStage()
    {
        List<string[]> dataList = CSVParser.Load("TableData/stage");

        for (int i = 1; i < dataList.Count; i++)
        {
            string[] data = dataList[i];

            AssetStage kAssStage = new AssetStage();

            kAssStage.m_Id = int.Parse(data[0]);
            kAssStage.m_FireDelayTime = float.Parse(data[1]);
            kAssStage.m_BulletSpeed = float.Parse(data[2]);
            kAssStage.m_StageKeepTime = int.Parse(data[3]);
            kAssStage.m_PlayerHP = int.Parse(data[4]);
            kAssStage.m_BulletAttack = int.Parse(data[5]);
            kAssStage.m_ItemAppearDelay = int.Parse(data[6]);
            kAssStage.m_ItemKeepTime = int.Parse(data[7]);
            kAssStage.m_TurretCount = int.Parse(data[8]);

            m_AssStages.Add(kAssStage);
        }

        SetText();
    }

    public void ParsingItem()
    {
        List<string[]> dataList = CSVParser.Load("TableData/item");

        for (int i = 1; i < dataList.Count; i++)
        {
            string[] data = dataList[i];

            AssetItem kAssItem = new AssetItem();

            kAssItem.m_Id = int.Parse(data[0]);
            kAssItem.m_nType = int.Parse(data[1]);
            kAssItem.m_PrefabName = data[2];
            kAssItem.m_fValue = int.Parse(data[3]);
            kAssItem.m_Desc = data[4];

            m_AssItems.Add(kAssItem);
        }

        SetText();
    }

    private void SetText()
    {
        for (int i = 0; i < m_AssItems.Count; i++)
        {
            AssetItem kItem = m_AssItems[i];

            string str = string.Format("{0}, {1}, {2}, {3}, {4} \n",
                kItem.m_Id, kItem.m_nType, kItem.m_PrefabName, kItem.m_fValue, kItem.m_Desc);
            m_Builder.Append(str);
        }
    }


    public AssetStage GetAssetStage(int istage)
    {
        if (istage > 0 && istage <= m_AssStages.Count)
        {
            return m_AssStages[istage - 1];

        }

        return null;
    }

    public int GetAssetItemCount()
    {
        return m_AssItems.Count;
    }

    public AssetItem GetAssetItem(int nId)
    {
        /* 코드가 많을 때
        if (nId < 0 || nId > m_AssItems.Count)
            return null;
        */

        if( nId > 0 && nId <= m_AssItems.Count)
            return m_AssItems[nId-1];

        return null;
    }
}
