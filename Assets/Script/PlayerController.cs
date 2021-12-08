using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FPS
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private int _damage = 1;

        [SerializeField]
        private float fireTime = 0.3f;

        [SerializeField]
        private float lastFireTime = 0f;

        [SerializeField]
        private GameObject smoke;

        [SerializeField]
        private GameObject gunHead;

        [SerializeField]
        private Animator Anim;

        [SerializeField]
        private GameObject player;

        [SerializeField]
        private float playerHealth = 10;

        [SerializeField]
        private float playerCurrentHealth = 10;

        [SerializeField]
        private Slider healthBar;

        [SerializeField]
        private AudioSource audioS;

        [SerializeField]
        private AudioClip playerDeathSound;

        [SerializeField]
        private GameObject GameController;

        // Start is called before the first frame update
        void Start()
        {
            Anim = gameObject.GetComponent<Animator>();
            UpdateFireTime();
            gameObject.GetComponent<Light>().enabled = true;
            player = GameObject.FindGameObjectWithTag("Player");
            audioS = gameObject.GetComponent<AudioSource>();
            GameController = GameObject.FindGameObjectWithTag("GameController");
            healthBar.maxValue = playerHealth;
            healthBar.value = playerCurrentHealth;
            healthBar.minValue = 0;
            
        }
        private void UpdateFireTime()
        {
            lastFireTime = Time.time;
        }

        private void SetFireAnim(bool isfire)
        {
            Anim.SetBool("IsShooting", isfire);
        }

        public void GetHit(float damage)
        {
            audioS.Play();
            playerCurrentHealth -= damage;
            healthBar.value = playerCurrentHealth;

            if (playerCurrentHealth <= 0)
            {
                Dead();
            }
        }
        private void Dead()
        {
            audioS.clip = playerDeathSound;
            audioS.Play();
            GameController.GetComponent<GameController>().EndGame();
        }

        private void Fire()
        {
            if (Time.time >= lastFireTime + fireTime)
            {
                Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition); ;
#if UNITY_IOS || UNITY_ANDROID
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag.Equals("Enemy"))
                    {
                        SetFireAnim(true);
                        InSmoke();
                        hit.transform.gameObject.GetComponent<EnemyController>().GetHit(_damage);
                    }
                }
#else

                RaycastHit hit;
                if (Physics.Raycast(gunHead.transform.position,gunHead.transform.forward, out hit))
                {
                    if (hit.transform.tag.Equals("Enemy"))
                    {
                        SetFireAnim(true);
                        InSmoke();
                        hit.transform.gameObject.GetComponent<EnemyController>().GetHit(_damage);
                        GameController.GetComponent<GameController>().WinGame();
                    }
                }
#endif
                UpdateFireTime();
            }

            //Chua hieu tai sao khong su dung duoc
            else
            {
                SetFireAnim(false);
            }
        }

        private void InSmoke()
        {
            GameObject sm = Instantiate(smoke, gunHead.transform.position, gunHead.transform.rotation) as GameObject;
            Destroy(sm, 0.5f);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {

                Fire();
            }
        }
    }
}