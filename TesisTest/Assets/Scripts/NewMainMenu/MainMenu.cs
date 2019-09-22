using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NewMainMenu
{
    public class MainMenu : MonoBehaviour
    {
        #region menuShards
        private MenuShard[] shards;
        public void SetMenuShardsAnimations()
        {
            shards = GetComponentsInChildren<MenuShard>();
            foreach (MenuShard shard in shards)
            {
                shard.SetAnimator();
            }
        }
        #endregion
    }
}
