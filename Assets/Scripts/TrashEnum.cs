using UnityEngine;

public enum BinColor
{
    Brown,
    Yellow,
    Black,
    Blue
}

public enum TrashType
{
    Metal,
    Plastic,
    Glass,
    Compost,
    Cardboard,
    Paper,
    Trash
}

public enum TrashObject
{
    //Blue
    CrumbledPaper,
    PizzaBox,
    BurgerBox,
    CoffeeCup,
    //Yellow
    PlasticBottle,
    GlassBottle,
    BrokenGlass,
    Can,
    CrumbledCan,
    //Brown
    AppleCore,
    Pizza,
    FishBone,
    //Black
    BrokenMug,
    BrokenPlate,
    Cigarette,
    DirtyPizzaBox
}



static class TrashMethods
{

    public static BinColor GetBin(this TrashType trash)
    {
        switch (trash)
        {
            case TrashType.Metal:
            case TrashType.Plastic:
            case TrashType.Glass:
                return BinColor.Yellow;
            case TrashType.Compost:
                return BinColor.Brown;
            case TrashType.Cardboard:
            case TrashType.Paper:
                return BinColor.Blue;
            default:
                return BinColor.Black;
        }
    }

    public static TrashType GetTrashType(this TrashObject trash)
    {
        switch (trash)
        {
            case TrashObject.CrumbledPaper:
                return TrashType.Paper;
            case TrashObject.PizzaBox:
            case TrashObject.BurgerBox:
                return TrashType.Cardboard;
            case TrashObject.GlassBottle:
            case TrashObject.BrokenGlass:
                return TrashType.Glass;
            case TrashObject.PlasticBottle:
                return TrashType.Plastic;
            case TrashObject.Can:
            case TrashObject.CrumbledCan:
                return TrashType.Metal;
            case TrashObject.AppleCore:
            case TrashObject.FishBone:
            case TrashObject.Pizza:
                return TrashType.Compost;
            default:
                return TrashType.Trash;
        }
    }
}