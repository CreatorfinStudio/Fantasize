public interface IStageInfo
{
    #region StageData
    public (int,bool) GetIsBattleAreaCompleted();

    #endregion

    #region CurrStageInfo
    public int GetCurrStageNum();
    public int GetCurrBattleAreaNum();

    #endregion

    public void NextStage();
}
