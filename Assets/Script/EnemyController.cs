using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private Animator ani;

        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private float minMoveSpeed = 0.05f;
        [SerializeField]
        private float maxMoveSpeed = 0.3f;

        [SerializeField]
        private GameObject player;

        

        //[SerializeField]
        //private GameObject LookAtTarget;

        [SerializeField]
        private bool IsAttack = false;

        [SerializeField]
        private float attackTime = 1;

        [SerializeField]
        private float lastAttackTime = 0;

        [SerializeField]
        private bool isShooten;

        [SerializeField]
        private float attackRange = 1;

        [SerializeField]
        private AudioSource audioS;

        [SerializeField]
        private AudioClip DeathSound;

        [SerializeField]
        private float damge = 1;

        [SerializeField]
        private bool isDead = false;

        [SerializeField]
        private bool IsShooten
        {
            get { return isShooten; } 

            set
            {
                isShooten = value;
                ShootenAnimation(isShooten);
                UpdateShootenTime();
            }
        }

        [SerializeField]
        private float lastShootenTime = 0f;



        [SerializeField]
        private int Health = 3;

        [SerializeField]
        public float shootTime = 0.5f;

        [SerializeField]
        private GameObject GameController;

        // Start is called before the first frame update
        void Start()
        {
            ani = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player");
            UpdateMoveSpeed();
            IsShooten = false;
            UpdateShootenTime();
            ani.SetBool("IsDead", false);
            audioS = gameObject.GetComponent<AudioSource>();
            GameController = GameObject.FindGameObjectWithTag("GameController");
        }

        

        private void UpdateShootenTime()
        {
            lastShootenTime = Time.time;
        }

        private void Move()
        {
            if (player == null)
            {
                ani.SetBool("IsWalk", true);

            }
            if (Vector3.Distance(transform.position, player.transform.position) > attackRange)
            {
                //transform.LookAt(player.transform.position);
                transform.position = Vector3.Lerp(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("IsIdle",true);

                gameObject.GetComponent<EnemyController>().IsAttack = true;

                gameObject.GetComponent<EnemyController>().enabled = false;
            }

        }

        private void UpdateAttackTime()
        {
            lastAttackTime = Time.time;
        }

    private void ShootenAnimation(bool IsShoot)
        {
            ani.SetBool("IsShoot", IsShoot);
        }    

        private void UpdateMoveSpeed()
        {
            moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        }

        public void GetHit(int damage)
        {
            if (isDead)
                return;

            audioS.Play();

            IsShooten = true;
            Health -= damage;
            

            if (Health <= 0)
            {
                Dead();
            }

        }

        private void Dead()
        {
            isDead = true;
            audioS.clip = DeathSound;
            audioS.Play();
            ani.SetBool("IsDead", true);
            GameController.GetComponent<GameController>().GetPoint(1);
            Destroy(gameObject, 1f);
        }

        private void AttackAnim(bool IsAttack)
        {
            ani.SetBool("IsAttack", IsAttack);
        }

        private void Attack()
        {
            if (Time.time >= lastAttackTime + attackTime)
            {
                
                AttackAnim(true);
                UpdateAttackTime();
                player.GetComponent<PlayerController>().GetHit(damge);

            }    
            else
            {
                AttackAnim(false);
            }

           
        }

        // Update is called once per frame
        void Update()
        {
            Move();


            if (IsShooten && Time.time >= lastShootenTime + shootTime)
            {
                IsShooten = false;
                UpdateShootenTime();
            }

            if (IsAttack)
            {
                Attack();
            }
        }
    }

}