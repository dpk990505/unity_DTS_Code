using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Item", menuName ="Scritble Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType {Weapon, Attribute, Heal}//�������ǼӼ�, ���ݷ�, <<�̼����迭�̿���ФФФФ�
                /*
                 �ڵ� ���� Item id ���� �Ⱦ��淡 ������ ��ġ�� ���� Ÿ���̶�� �ٲ�
                    �������� ������ Ÿ�Կ��� ���� / �ɷ�ġ / �� �� ������
                    ���� ������ ����Ÿ�� 1, 2, 3, 4, 5.... ���� ������ �߰��� ��������.
                    �����ϰ� �ɷ�ġ �������� ���� ��ȭ�� ��Ÿ�� ��.
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
    public int baseCount;//�ٰŸ��� ����, ���Ÿ��� �����,-100���� ���ڸ� ����
    public float baseSpeed;
    public float Life_time;
    public float[] damages;
    public int[] counts;
    public float[] speeds;// ó�� ���� 0 ����
    

    [Header("# Weapon")]
    public GameObject projectile;
}
