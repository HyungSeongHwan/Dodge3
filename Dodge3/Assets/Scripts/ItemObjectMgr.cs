using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectMgr : MonoBehaviour
{
    public List<CItemObj> m_listItem = null;
    public List<FXSerialize> m_listFX = null;
    public List<Transform> m_listPosition = null;

    private float m_ItemKeepTime = 0.0f;
    private float m_ItemAppearDelay = 0.0f;

    private bool m_isCreateItem = false;
    

    public void Initialize(float fKeepTime, float fAppearDelay)
    {
        m_ItemKeepTime = fKeepTime;
        m_ItemAppearDelay = fAppearDelay;
        m_isCreateItem = true;

        StartCoroutine("EnumFunc_SpawnItem");
    }

    IEnumerator EnumFunc_SpawnItem()
    {
        float fKeepTime = m_ItemKeepTime;
        float fDelay = m_ItemAppearDelay;

        while (m_isCreateItem) 
        {
            yield return new WaitForSeconds(fDelay);

            if (!m_isCreateItem)
                break;

            int nAssID = 0;
            int idx = MakeRandomItemObjectID(ref nAssID) - 1;
            CItemObj kItem = m_listItem[idx];
            kItem.Initialize(nAssID, MakeRamdomPos());

            yield return new WaitForSeconds(fKeepTime);
            kItem.Show(false);

            fDelay = MakeRandomDelay(m_ItemAppearDelay);
        }

        yield return null;
    }

    private int MakeRandomItemObjectID(ref int rAssId)
    {
        int nItemCount = AssetMgr.Inst.GetAssetItemCount();
        int nId = Random.Range(1, nItemCount + 1);
        AssetItem kAssItem = AssetMgr.Inst.GetAssetItem(nId);

        rAssId = nId;
        return kAssItem.m_nType;
    }

    float MakeRandomDelay(float fDelay)
    {
        int nValue = Random.Range(-2, 2);
        fDelay += nValue;
        return fDelay;
    }

    public void HideItems()
    {
        for (int i = 0; i < m_listItem.Count; i++)
        {
            m_listItem[i].Show(false);
        }
    }

    public void HideItem(GameObject go)
    {
        go.SetActive(false);
    }

    public void SetIsCreateItem(bool bCreate)
    {
        m_isCreateItem = bCreate;
    }

    public void ActionEffect(int id)
    {
        if (id > 0 && id <= m_listFX.Count)
        {
            m_listFX[id - 1].Play();
        }
    }

    public void HideEffect(int id)
    {
        if (id > 0 && id <= m_listFX.Count)
        {
            m_listFX[id - 1].Show(false);
        }
    }

    public Vector3 MakeRamdomPos()
    {
        Vector3 vMax = m_listPosition[0].position;
        Vector3 vMin = m_listPosition[1].position;

        float x = Random.Range(vMin.x, vMax.x);
        float z = Random.Range(vMin.z, vMax.z);

        return new Vector3(x, 1.7f, z);
    }
}
