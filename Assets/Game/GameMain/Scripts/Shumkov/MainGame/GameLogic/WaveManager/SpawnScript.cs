using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class SpawnScript : MonoBehaviour
{
    //�X�|�[��
    [Header("Spawner Object")]
    public GameObject spawner;
    //�X�|�[���v���p�e�B
    [Header("Spawn Properties")]
    [SerializeField] int[] spawnAmount;
    [SerializeField] Vector3 randomSpawnMax;
    [SerializeField] Vector3 randomSpawnMin;
    [SerializeField] LayerMask spawnColliderCheck;
    //�p���[�A�b�v
    [Header("PowerUp")]
    public Transform powerUpTransform;
    public GameObject powerUp;
    Animator powerUpAnimator;
    GameObject powerUpMemory;

    //�e�N�j�J���ϐ�
    //�ŏ��̃X�|�[���̃t���O
    public float firstSpawnTime;    
    bool firstWaveSpawned;
    //����
    float time;
    //�^�[��
    public int wave = -1;
    //�X�|�[���t���O
    public bool spawn = false;
    bool isNewSpawnTime;
    //�G�����Ȃ����̊m�F
    public bool enemiesDead = false;
    public bool enemyWasKilled = false;

    public Text waveText;
    UI_Time uI_Time;
    
    private void Start()
    {
        uI_Time = GameObject.Find("GameManager").GetComponent<UI_Time>();
    }
    void Update()
    {
        if (!uI_Time.gameOver)
        {
            spawn = false;
            //�^�[���̊J�n
            if (isNewSpawnTime && wave < 3)
            {
                Debug.Log("Creating Wave");
                wave++;
                SpawnHoldersInstantiation(spawnAmount[wave]);
                Debug.Log("Created Wave");
                powerUpMemory = Instantiate(powerUp, powerUpTransform.position, Quaternion.identity) as GameObject;
                powerUpAnimator = powerUpMemory.GetComponent<Animator>();
                spawn = true;
                isNewSpawnTime = false;
                Debug.Log("Finished Wave");
            }
            //�ŏ��^�[���܂ł̃J�E���g�_�E��
            if (!firstWaveSpawned)
                time += Time.deltaTime;
            //�G�����Ȃ�������A�V�����^�[���J�n
            if (enemyWasKilled)
            {
                Debug.Log("Checking Dead Enemy");
                enemiesDead = CheckingEnemies(3.0f);
            }
            if (enemiesDead && !isNewSpawnTime || time >= firstSpawnTime && !firstWaveSpawned)
            {
                Debug.Log("Started Wave");
                time = 0;
                if (powerUpAnimator != null)
                    powerUpAnimator.SetBool("destroyObject", true);
                enemiesDead = false;
                isNewSpawnTime = true;
                firstWaveSpawned = true;
            }
            if (wave == 3)
            {
                uI_Time.gameOver = true;
            }
            waveText.text = "Wave: " + (wave + 1);
        }
    }

    //�X�|�[�i�[�̔���
    void SpawnHoldersInstantiation(int amount)
    {
        for (int i = amount - 1; i >= 0; i--)
        {
            Instantiate(spawner, SpawnLocation(), Quaternion.identity);
        }
    }
    //�X�|�[���̈ʒu�����܂�
    Vector3 SpawnLocation()
    {
        Vector3 spawnPosition;
        bool spawnPointfound = false;
        do
        {
            float randX = Random.Range(randomSpawnMin.x, randomSpawnMax.x);
            float randY = Random.Range(randomSpawnMin.y, randomSpawnMax.y);
            float randZ = Random.Range(randomSpawnMin.z, randomSpawnMax.z);
            spawnPosition = new Vector3(randX, randY, randZ);
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPosition, out hit, 1f, -1))
            {
                Debug.Log(spawnPosition + " is initial position " + hit.position + " is end position");
                spawnPosition = hit.position;
                spawnPosition.y += 0.5f;
                if (!Physics.CheckSphere(spawnPosition, 1.5f, spawnColliderCheck))
                {
                    spawnPointfound = true;
                }
            }


        } while (!spawnPointfound);

        return spawnPosition;
    }

    //�G�����邩�ǂ����̊m�F
    public float periodTimer;
    bool CheckingEnemies(float period)
    {
        periodTimer += Time.deltaTime;
        if (period < periodTimer)
        {
            enemyWasKilled = false;
            return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
        }
        return false;
    }

}