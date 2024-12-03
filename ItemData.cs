using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Item", menuName ="Scritble Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType {Weapon, Attribute, Heal}//아이템의속성, 공격력, <<이세끼배열이에요ㅠㅠㅠㅠㅜ
                /*
                 코드 뜯어보니 Item id ㅈ도 안쓰길래 동일한 위치에 서브 타입이라고 바꿈
                    이제부터 아이템 타입에서 무기 / 능력치 / 힐 로 갈리고
                    무기 내에서 서브타입 1, 2, 3, 4, 5.... 같은 식으로 추가로 갈릴것임.
                    동일하게 능력치 내에서도 같은 변화로 나타낼 것.
                 */

    [Header("# Main info")]
    public ItemType itemType;
    public int sub_type;
    public string itemName;
    [TextArea]
    public string itemDesc;
    public Sprite ItemIcon;

    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;//근거리는 갯수, 원거리는 관통력,-100사용시 제자리 고정
    public float baseSpeed;
    public int base_bust_num;//갯수가 존재하는 투사체 전용 코드, 기본
    public float Life_time;
    public float[] damages;
    public int[] counts;
    public float[] speeds;// 처음 값은 0 고정
    public int [] bust_numcount;//해당 코드는 bust_numcount 의 숫자증가
    

    [Header("# Weapon")]
    public GameObject projectile;
}
