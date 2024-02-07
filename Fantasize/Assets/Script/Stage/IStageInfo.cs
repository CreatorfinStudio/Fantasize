using System;

public interface IStageInfo
{
    #region StageData
    public (int,bool) GetIsBattleAreaCompleted();
    public void GetIsOpenLastArea(Action<bool> resultCallback);
    #endregion

    #region CurrStageInfo
    public int GetCurrStageNum();
    public int GetCurrBattleAreaNum();

    #endregion

}
