using UnityEngine;

public class Shop : MonoBehaviour {

	public TurretBlueprint standardTurret;
	public TurretBlueprint missileLauncher;
	public TurretBlueprint laserBeamer;
	public TurretBlueprint testTurret;
	public TurretBlueprint testSlowTurret;
	public TurretBlueprint bankTower;
	public TurretBlueprint powerUpTower;
	public TurretBlueprint healerTower;
	public TurretBlueprint shielderTower;

	BuildManager buildManager;

	void Start ()
	{
		buildManager = BuildManager.instance;
	}

	public void SelectStandardTurret ()
	{
		Debug.Log("Standard Turret Selected");
		buildManager.SelectTurretToBuild(standardTurret);
	}

	public void SelectMissileLauncher()
	{
		Debug.Log("Missile Launcher Selected");
		buildManager.SelectTurretToBuild(missileLauncher);
	}

	public void SelectLaserBeamer()
	{
		Debug.Log("Laser Beamer Selected");
		buildManager.SelectTurretToBuild(laserBeamer);
	}
	public void SelectTestTurret()
    {
		buildManager.SelectTurretToBuild(testTurret);
    }
    public void SelectTestSlowTurret()
    {
        buildManager.SelectTurretToBuild(testSlowTurret);
    }
	public void SeletBankTower()
    {
		buildManager.SelectTurretToBuild(bankTower);
	}
	public void SeletPowerTower()
	{
		buildManager.SelectTurretToBuild(powerUpTower);
	}
	public void SeletHealerTower()
	{
		buildManager.SelectTurretToBuild(healerTower);
	}
	public void SeletShielderTower()
	{
		buildManager.SelectTurretToBuild(shielderTower);
	}
}
