using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;//무기 아이디
    public int prefabId;//프리펩 아이디

    //무기의 기초 능력치
    public float damage;//데미지
    public int count;//갯수
    public float speed;//속도
    public float Life_time;//존재시간
    


    float timer;
    protected Player player;
    Gear gear;
    protected bool is_rotation_setted = false;
    protected Quaternion base_rotation;

    void Awake()
    {
        player = GameManager.Instance.player;
    }

    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        timer += Time.deltaTime * GameManager.Instance.player.fire_rate;
        if (timer > speed)
        {
            if (!player.scanner.nearstTargets)//지정되는 목표가 없을시
                return;
            timer = 0f;


            Fire();
        }
    }

    public virtual void LevelUp(float damage, int count, float speed, int bust_num)
    {
        this.damage = damage;
        this.count += count;
        this.speed += speed;

    }

    public virtual void Init(ItemData data)//item Data에서 정보를 받아옴
    {
        //초기 설정
        // name = "Weapon " + data.sub_type;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;//지역위치를 플레이어 위치로 변경

        //이후 캐릭터 정보에 따른 설정
        id = data.sub_type;
        damage = data.baseDamage;
        count = data.baseCount;
        speed = data.baseSpeed;
        Life_time = data.Life_time;

        for (int index = 0; index < GameManager.Instance.pool.WeaponPrefabs.Length; index++)
        {
            //프리펩 아이디 찾는 코드, 풀링 매니저의 변수에서 찾아서 초기화
            if (data.projectile == GameManager.Instance.pool.WeaponPrefabs[index])
            {
                prefabId = index;
                break;
            }
        }

        speed = 0.5f * GameManager.Instance.player.fire_rate;
        speed = data.baseSpeed;

    }

    public virtual void Fire()
    {
        Vector3 start = transform.position;
        Vector3 targetPos = player.scanner.nearstTargets.position;//가까운 적의 위치
        Vector3 dir = targetPos - transform.position;//목표위치-나의위치
        dir = dir.normalized;

        Transform bullet = GameManager.Instance.pool.WeaponGet(prefabId).transform;//프리펩ID목록에서 쏠 오브젝트 지정

        if (!is_rotation_setted)
        {
            base_rotation = bullet.rotation;
            is_rotation_setted = true;
        }

        bullet.position = start;//해당 목표 가리킴
        bullet.rotation = base_rotation * Quaternion.FromToRotation(Vector3.up, dir);//지정된 축을 중심으로 목표를 향해 회전
        bullet.GetComponent<Bullet>().Init(damage * GameManager.Instance.player.power, count, dir,Life_time);

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range);//소리임
    }

}
