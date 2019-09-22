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
        [SerializeField] [HideInInspector] private MenuShard[] shards;
        [Range(1.0f, 0.0f)] public float colorSwap = 1.0f;
        public string colorSwapLavel;
        public Substance.Game.SubstanceGraph[] colorSwapGraphs;
        public void SetMenuShardsAnimations()
        {
            shards = GetComponentsInChildren<MenuShard>();
            foreach (MenuShard shard in shards)
            {
                shard.SetAnimator();
            }
        }

        public void OnButtonClicked()
        {
            for (int i = 0; i < colorSwapGraphs.Length; i++)
                colorSwapGraphs[i].SetInputFloat(colorSwapLavel, 0.0f);

            foreach (MenuShard shard in shards)
            {
                shard.FullRotate();
            }
            //StartCoroutine(SubstanceColorsSwap());
        }

        IEnumerator SubstanceColorsSwap()
        {
            if (colorSwapGraphs[0] != null)
            {
                while (colorSwap != 0.0f)
                {
                    colorSwap -= Time.deltaTime;
                    if (colorSwap < 0.0f)
                        colorSwap = 0.0f;
                    for (int i = 0; i < colorSwapGraphs.Length; i++)
                    {
                        colorSwapGraphs[i].SetInputFloat(colorSwapLavel, colorSwap);
                        //colorSwapGraphs[i].QueueForRender();
                    }
                    //Substance.Game.Substance.RenderSubstancesSync();

                    yield return new WaitForFixedUpdate();
                }
            }
        }
    }
}
