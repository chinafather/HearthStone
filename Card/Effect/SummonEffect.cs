﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Card.Effect
{
    public static class SummonEffect
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="singleEffect"></param>
        /// <param name="game"></param>
        /// <param name="Seed"></param>
        /// <returns></returns>
        public static List<string> RunEffect(EffectDefine singleEffect, Client.GameManager game,int Seed)
        {
            List<String> Result = new List<string>();
            var MinionLst = singleEffect.AddtionInfo.Split(Card.CardUtility.strSplitMark.ToCharArray());
            Random t = new Random(DateTime.Now.Millisecond + Seed);
            var CardSN = MinionLst[t.Next(0,MinionLst.Length)];
            var Minion = Card.CardUtility.GetCardInfoBySN(CardSN);
            switch (singleEffect.EffectTargetSelectDirect)
            {
                case CardUtility.TargetSelectDirectEnum.本方:
                    if (game.MySelf.RoleInfo.BattleField.MinionCount < Card.Client.BattleFieldInfo.MaxMinionCount)
                    {
                        game.MySelf.RoleInfo.BattleField.AppendToBattle(CardSN);
                        //SUMMON#YOU#M000001
                        Result.Add(Card.Server.ActionCode.strSummon + Card.CardUtility.strSplitMark + Card.CardUtility.strMe + Card.CardUtility.strSplitMark + CardSN);
                    }
                    break;
                case CardUtility.TargetSelectDirectEnum.对方:
                    if (game.YourInfo.BattleField.MinionCount < Card.Client.BattleFieldInfo.MaxMinionCount)
                    {
                        game.YourInfo.BattleField.AppendToBattle(CardSN);
                        Result.Add(Card.Server.ActionCode.strSummon + Card.CardUtility.strSplitMark + Card.CardUtility.strYou + Card.CardUtility.strSplitMark + CardSN);
                    }
                    break;
                case CardUtility.TargetSelectDirectEnum.双方:
                    if (game.MySelf.RoleInfo.BattleField.MinionCount < Card.Client.BattleFieldInfo.MaxMinionCount)
                    {
                        game.MySelf.RoleInfo.BattleField.AppendToBattle(CardSN);
                        Result.Add(Card.Server.ActionCode.strSummon + Card.CardUtility.strSplitMark + Card.CardUtility.strMe + Card.CardUtility.strSplitMark + CardSN);
                    }
                    if (game.YourInfo.BattleField.MinionCount < Card.Client.BattleFieldInfo.MaxMinionCount)
                    {
                        game.YourInfo.BattleField.AppendToBattle(CardSN);
                        Result.Add(Card.Server.ActionCode.strSummon + Card.CardUtility.strSplitMark + Card.CardUtility.strYou + Card.CardUtility.strSplitMark + CardSN);
                    }
                    break;
                default:
                    break;
            }
            return Result;
        }
    }
}
