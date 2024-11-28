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
    public float Life_time;


    float timer;
    protected Player player;
    Gear gear;

   


    void Awake()
    {
        player = GameManager.Instance.player;
    }

    void Update()
    {
        onUpdate();
    }

    public virtual void onUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        timer += Time.deltaTime * GameManager.Instance.player.fire_rate;
        if (timer > speed)
        {
            if (!player.scanner.nearstTargets)//지정되는 목표가 없을시
                return;
            timer -= speed;
            

            Fire();
        }
    }

    public virtual void LevelUp(float damage, int count, float speed)
    {
        this.damage = damage;
        this.count += count;
        this.speed += speed;

    }

    protected virtual void setting(ItemData data)//리펙토링코드
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
    }

    public virtual void Init(ItemData data)//item Data에서 정보를 받아옴
    {
        
        setting(data);
        speed = 0.5f * GameManager.Instance.player.fire_rate;
        speed = data.baseSpeed;


        // 더 이상 쓸모 없어져서 전체 주석
        //switch (id)//무기 아이디에 따라 로직을 분리(투사체,플레이어주변돌기)
        //{
        //    //case 0://무기 아이디 0 일경우 플레이어 주변 돌기
        //        //speed = 150 * Character.WeaponSpeed;
        //        //Batch();
        //        //break;
        //    default://나머지는 투사체 취급
        //        speed = 0.5f *Character.WeaponRate;
        //        speed = data.baseSpeed;
        //        break;
        //}

    }

    public virtual void Fire()
    {
        Vector3 start = transform.position;
        Vector3 targetPos = player.scanner.nearstTargets.position;//가까운 적의 위치
        Vector3 dir = targetPos - transform.position;//목표위치-나의위치
        dir = dir.normalized;

        Transform bullet = GameManager.Instance.pool.WeaponGet(prefabId).transform;//프리펩ID목록에서 쏠 오브젝트 지정
        bullet.position = start;//해당 목표 가리킴
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);//지정된 축을 중심으로 목표를 향해 회전
        bullet.GetComponent<Bullet>().Init(damage * GameManager.Instance.player.power, count, dir,Life_time);

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Range);//소리임
    }

}
