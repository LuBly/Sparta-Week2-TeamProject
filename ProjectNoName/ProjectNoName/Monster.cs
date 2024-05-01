namespace ProjectNoName
{
	internal class Monster
	{
		int monsterLv { get; set; };
		string monsterName { get; set; };
		float monsterHealth { get; set; };
		float monsterAttackPower { get; set; };
		float monsterDefensePower { get; set; };
		int monsterExp { get; set; }

		public Monster(int lv, string name, float health, float attackPower, float defensePower, int exp)
		{
			this.Lv = lv;
			this.Name = name;
			this.Health = health;
			this.AttackPower = attackPower;
			this.DefensePower = defensePower;
			this.Exp = exp;
		}

		//���� �ݰ�          
		public int CounterAttack()
		{
			return monsterAttackPower * 0.1f;
		}
	}

	//���� ���� ����
	public void GenerateMonster()
	{		
		Monster[] monster = new Monster[6]
		{ monster[0] = new Monster(1, "������", 20, 10, 1, 10),
		monster[1] = new Monster(2, "��������", 30, 20, 3, 15),
		monster[2] = new Monster(3, "���", 40, 30, 3, 20),
		monster[3] = new Monster(4, "�����Ź�", 50, 30, 4, 30),
		monster[4] = new Monster(4, "����", 40, 50, 2, 40),
		monster[5] = new Monster(5, "��ũ", 70, 50, 10, 50) }


		if (stageLevel == 1)
		{			
				Random random = new Random();
				Console.WriteLine(monster[i = random.Next(0, 3)]);
		}

		else if (stageLevel == 2)
		{			
				Random random = new Random();
				Console.WriteLine(monster[i = random.Next(2, 6)]);
		}
	}

	//���� ����
	public void EncounterMonster()
	{
		Encounter[] encounter = new Encounter[3];
		
		for (int n = 0; n < encounter.Length; n++)
		{
			encounter[n] = Monster.GenerateMonster();
			console.WriteLine(encounter[n]);
		}		
	}

	//���� ���
	public void DeadMonster()
	{
		if (monsterHealth <= 0)
		{ 
			//�ش� ���� ��ο� ������ ó��, ���̻� ���� ���� X
		}
	}
}


