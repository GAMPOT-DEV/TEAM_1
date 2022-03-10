using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, UnitInterface
{
	private int x = 0, y = 0;
	bool isUnitClick;
	[SerializeField] int coast;
	[SerializeField] int level;
	[SerializeField] Units unit;
	[SerializeField] SpriteRenderer character;

	enum Units { boom, stealer, crystal };

	void Unitablity()
    {
        switch (unit)
        {
			case Units.boom:
				boom();
				break;
			case Units.stealer:
				stealer();
				break;
        }
    }
	void stealer()
    {
		int stealCount;
		switch (level)
		{
			// ���ϰ��� ���� raycast�� ���� ���� Ÿ���� ���絵 �ǰ�, ������ ���缭 ���� ���� �տ� �����ִ��� �Ǻ��ϸ�
			// ���ڴٰ� �����߽��ϴ�.
			case 1:
				coast = 3;
				stealCount = 1;
				break;
			case 2:
				coast = 5;
				stealCount = 3;
				break;
			case 3:
				coast = 7;
				stealCount = 5;
				break;
		}

	}
	void boom()
    {
		int attackPower;
		switch (level)
		{
			// ��ź�� ���� ��Ȯ�� ���� �ƴ�����, ������ ��ź�� ��� �ڱ� �������� ���� �׷� �ý����̶��
			// layer�� ����Ͽ� ��� ������ ���� �������� �ڵ带 ¥�� ��� �����غý��ϴ�
			case 1:
				coast = 3;
				attackPower = 3;
				break;
			case 2:
				coast = 5;
				attackPower = 5;
				break;
			case 3:
				coast = 7;
				attackPower = 7;
				break;
		}
	}
	void crystal()
    {
		switch (level)
		{
			// �� �Ŵ������� �� ���� �޾ƿ� �� ���� ���� �� �� ���� level�� �����ϵ��� ¥�� ��� �����߽��ϴ�.
			case 1:
				coast = 3;
				break;
			case 2:
				coast = 5;
				break;
			case 3:
				coast = 7;
				break;
		}
	}
	void OnMouseDown()
    {
		isUnitClick = true;
    }
	public void clickfunc() {
        if (isUnitClick)
        {

        }
	}
    public void Movefuc(int x,int y)
    {
		transform.position = Vector2.MoveTowards(transform.position, new Vector2(x, y), 3);
    }

	public bool checkPos(int a,int b) {
		if(a == x && b == y)
			return true;
		return false;
	}
}
