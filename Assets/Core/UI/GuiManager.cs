using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class GuiManager : MonoBehaviour
    {
        public static GuiManager Instance;
        
        public Image hurtVignette;
        public TextMeshProUGUI bossNameText;
        public GameObject Setting;

        private bool isSetting = false;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        public void Update()
        {
            TimeFreeze();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isSetting = !isSetting;
                Setting.SetActive(isSetting);
            }
        }

        public void FadeHurtVignette(float intensity)
        {
            FadeVignette(hurtVignette, intensity);
        }

        private void FadeVignette(Image vignette, float intensity)
        {
            vignette.gameObject.SetActive(true);
            DOTween.Sequence()
                .Append(vignette.DOFade(intensity, 0.05f))
                .AppendInterval(1.5f)
                .Append(vignette.DOFade(0.0f, 0.5f))
                .SetEase(Ease.OutCubic);
        }

        public void ShowBossName(string bossName)
        {
            bossNameText.gameObject.SetActive(true);
            bossNameText.text = bossName;
            bossNameText.color = new Color(1, 1, 1, 0);
            DOTween.Sequence()
                .Append(bossNameText.DOFade(1.0f, 0.5f))
                .AppendInterval(2.0f)
                .Append(bossNameText.DOFade(0.0f, 0.5f));
        }

        public void TimeFreeze()
        {
            if(Setting.activeSelf == true)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        public void ReturnGame()
        {
            Setting.SetActive(false);
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//如果是在unity编译器中
#else
        Application.Quit();//否则在打包文件中
#endif

        }

    }
}