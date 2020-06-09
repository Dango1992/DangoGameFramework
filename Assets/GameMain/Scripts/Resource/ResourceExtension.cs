using System.Collections;
using System.Collections.Generic;
using GameFramework.Resource;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Dango
{
    public static class ResourceExtension
    {
        public static void LoadPrefab(this ResourceComponent resourceComponent, string assetName,
            LoadAssetCallbacks loadAssetCallbacks, object userdata)
        {
            resourceComponent.LoadAsset(AssetUtility.GetUIItemAsset(assetName), typeof(GameObject), loadAssetCallbacks,
                userdata);
        }

        public static void LoadSprite(this ResourceComponent resourceComponent, string assetName,
            LoadAssetCallbacks loadAssetCallbacks, object userdata)
        {
            resourceComponent.LoadAsset(AssetUtility.GetUISpritAsset(assetName), typeof(Sprite), loadAssetCallbacks,
                userdata);
        }
    }
}