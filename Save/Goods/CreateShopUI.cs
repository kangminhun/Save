using UnityEngine;
using UnityEditor;

public class CreateShopUI : MonoBehaviour
{
    public ScriptableObject scriptable;
    public GameObject shopUi;
    public void GoodsData(int p, string n, Texture img)
    {
        GameObject ui = Instantiate(shopUi, transform.position, Quaternion.identity);
        ui.transform.parent = transform;
        ui.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        CreateAssets();
        ui.GetComponent<GoodsData>().item = (ItemInformation)scriptable;
        ui.GetComponent<GoodsData>().item.goodsName = n;
        ui.GetComponent<GoodsData>().item.goodsImage = img;
        ui.GetComponent<GoodsData>().item.price = p;
        ui.GetComponent<GoodsData>().Setting();
    }

    public void CreateAssets()
    {
        scriptable = Resources.Load<ScriptableObject>("GoodsScriptableObject/Goods");
        Instantiate(scriptable);
        //scriptable = ScriptableObject.CreateInstance<ItemInformation>();

        //string path = "Assets/Prefab/GoodsScriptableObject/";

        //AssetDatabase.CreateAsset(scriptable, AssetDatabase.GenerateUniqueAssetPath(path + "/Goods.asset"));

        //AssetDatabase.SaveAssets();
    }
}
