/*
using ICities;
using HideDistricts.ModdingSkeletonCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace HideDistricts
{
    public class HideDistrictsSave : SerializableDataExtensionBase
    {
        private readonly string dataKey = "HideDistrictsDataKey";

        public override void OnSaveData()
        {
            MemoryStream districtStateStream = new MemoryStream();
            HideDistrictsLogic logic = HideDistrictsMod.gameObject.GetComponent<HideDistrictsLogic>();
            BinaryFormatter bFormatter = new BinaryFormatter();
            DistrictDataContainer[] dataContainer = logic.showDistricts.GetDistrictDataContainerArray();
            try
            {
                if (dataContainer != null)
                {
                    bFormatter.Serialize(districtStateStream, dataContainer);
                    serializableDataManager.SaveData(dataKey, districtStateStream.ToArray());
                    Debug.Log("[HideDistricts] Data was serialized and saved.");
                }
                // logic.Refresh();
            }
            catch (Exception e)
            {
                Debug.LogError("[HideDistricts] Data wasn't saved due to " + e.GetType().ToString() + " : \"" + e.Message + "\"");
            }
            finally
            {
                districtStateStream.Close();
            }
        }
        public override void OnLoadData()
        {
            Debug.Log("[HideDistricts] Starting data loading");
            byte[] byteDistritDataArray = serializableDataManager.LoadData(dataKey);
            if (byteDistritDataArray != null)
            {
                MemoryStream districtStateStream = new MemoryStream();
                districtStateStream.Write(byteDistritDataArray, 0, byteDistritDataArray.Length);
                districtStateStream.Position = 0;
                try
                {
                    DistrictDataContainer[] data = new BinaryFormatter().Deserialize(districtStateStream) as DistrictDataContainer[];
                    HideDistrictsLogic logic = HideDistrictsMod.gameObject.GetComponent<HideDistrictsLogic>();
                    if (data.Count() > 0)
                    {
                        logic.LoadDistrictData(data);
                        logic.Apply();
                    }
                    else
                        Debug.LogWarning("[HideDistricts] Data was found but couldn't be neither loaded nor applied - Internal mod error");
                }
                catch (Exception e)
                {
                    Debug.LogError("[HideDistricts] Data wasn't loaded due to " + e.GetType().ToString() + " : \"" + e.Message + "\"");
                }
                finally
                {
                    districtStateStream.Close();
                }
            }
            else
            {
                Debug.Log("[HideDistricts] No data was found to load!");
            }
        }
    }
    
    [Serializable]
    public class DistrictDataContainer
    {
        public int DistrictId;
        public bool HideState;

        public DistrictDataContainer() { }
        public DistrictDataContainer(int districtId, bool state)
        {
            this.DistrictId = districtId;
            this.HideState = state;
        }
    } 
} */
