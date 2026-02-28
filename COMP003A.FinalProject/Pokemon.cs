using System;

public class Pokemon
{
	public string Species;
	public string Nickname;
	public string Type1;
	public string Type2;
	public string Item;

	public string Ability;
	public string Nature;
	public string TeraType;
	public string Offense;
	public string Defense;

	public string TeamRole;
	public int HitPoints;
	public int PhysAttack;
	public int SpecAttack;
	public int PhysDefense;

	public int SpecDefense;
	public int BaseStatTotal;
	public int Speed;
	public int Level;
	public int DexNumber;

	public bool Shiny;
	public bool FullEvolved;
	public bool Legendary;
	public bool Mythical;
	public bool CanMegaEvolve;
		
	public Pokemon(List<string> stringValues, List<int> intValues, List<bool> boolValues)
	{
		Species = stringValues[0];
		Nickname = stringValues[1];
		Type1 = stringValues[2];
		Type2 = stringValues[3];
		Ability = stringValues[4];
		Nature = stringValues[5];
		TeraType = stringValues[6];
		Item = stringValues[7];
		Offense = stringValues[8];
		Defense = stringValues[9];
		TeamRole = stringValues[10];
		HitPoints = intValues[0];
		PhysAttack = intValues[1];
		SpecAttack = intValues[2];
		PhysDefense = intValues[3];
		SpecDefense = intValues[4];
		Speed = intValues[5];
		BaseStatTotal = intValues[6];
		Level = intValues[7];
		DexNumber = intValues[8];
		Shiny = boolValues[0];
		FullEvolved = boolValues[1];
		CanMegaEvolve = boolValues[2];
		Legendary = boolValues[3];
		Mythical = boolValues[4];
	}

	public void DisplayValue()
	{
		Console.WriteLine($"\n{Species}, {Nickname}, {Type1}, {Type2}, {Ability}, {Nature}, {TeraType}, {Item}, {Offense}, {Defense}, {TeamRole}, {HitPoints}, {PhysAttack}, {SpecAttack}, {PhysDefense}, {SpecDefense}, {Speed}, {BaseStatTotal}, {Level}, {DexNumber}, {Shiny}, {FullEvolved}, {CanMegaEvolve}, {Legendary}, {Mythical}");
	}
}
