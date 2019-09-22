using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NewMainMenu
{
    public class MainMenu : MonoBehaviour
    {
        #region Singleton
        public static MainMenu instace;
        private void Awake()
        {
            instace = this;
        }
        #endregion
        #region MenuShards
        [SerializeField] [HideInInspector]private MenuShard[] shards;
        public void SetMenuShardsAnimations()
        {
            shards = GetComponentsInChildren<MenuShard>();
            foreach (MenuShard shard in shards)
            {
                shard.SetAnimator();
            }
        }
        #endregion

        public void OnButtonClicked()
        {
            foreach (MenuShard shard in shards)
            {
                shard.FullRotate();
            }
        }
    }
}
