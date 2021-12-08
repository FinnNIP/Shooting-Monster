using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FPS
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private GameObject menu;

        [SerializeField]
        private TextMeshProUGUI txtPoint;

        [SerializeField]
        private int currentPoint = 0;

        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1;
            menu.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void BackMenu()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        }

        public void GetPoint (int point)
        {
            currentPoint++;
            txtPoint.text = "Enemy killed: " + currentPoint.ToString();
            
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(0);
        }

        public void EndGame()
        {
            menu.SetActive(true);
            Time.timeScale = 0;
        }

        public void WinGame()
        {
            if (currentPoint >= 5)
            {
                RestartGame();
            }
        }    
    }
}