using Dapper;
using L.Dapper.AspNetCore;
using L.LCore;
using L.PathogenServices.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace L.PathogenServices.Services
{
    public class TSManageService : ITSManageService
    {
        private readonly DbFactory _factory;
        public TSManageService(DbFactory factory)
        {
            _factory = factory;
        }
        /// <summary>
        /// 获取会员咨询信息
        /// </summary>
        /// <returns></returns>
        //public IList<UserDataOutput> GetPagedUserDatas(UserDataInput input)
        //{
        //    //获取member表筛选where条件
        //    var memParameters = new DynamicParameters();
        //    string memWhereSql = GetMemberWhereSql(input.Member, ref memParameters);
        //    //获取userNews表筛选条件
        //    var userNewsParameters = new DynamicParameters();
        //    string userNewsWhereSql = GetUserNewsWhereSql(input.UserNews, ref userNewsParameters);
        //    //无条件查询
        //    if (input.Member == null && input.UserNews == null)
        //    {

        //    }
        //    if (input.Member != null)//存在会员表条件
        //    {
        //        //分页获取会员信息
        //        var userList = GetUserInfos(memWhereSql, memParameters);
        //        if (input.UserNews != null && input.UserNews.IsStoreDate)
        //        {
        //            //有存款日期
        //            //查询总输赢表中 总储值次数及总储值

        //        }
        //        else
        //        {
        //            //查询储值表数据
        //            var alagents = userList.Select(c => c.Alagent).Distinct().ToList();
        //            var userNewsList=GetUserNews(memWhereSql, GetUserNewsInSql("f_alagent",alagents), memParameters);

        //        }

        //    }
        //    else if(input.UserNews!=null)//存在储值表条件
        //    {
        //        if (input.UserNews.IsStoreDate)
        //        {
        //            //存款日期

        //        }
        //    }
        //}

        /// <summary>
        /// 获取会员表信息
        /// </summary>
        public IList<UserDataOutput> GetUserInfos(string whereSql, DynamicParameters parameters)
        {
            string sql = $"SELECT f_alagent alagent,f_accounts accounts,f_title title,f_joindate joindate,DATEDIFF(dd, f_enterdate, GETDATE()) AS NoDays ,ISNULL(f_StupeSurplus, 0) f_StupeSurplus StupeSurplus,0 isshow,"
            + $"0 StoreTimes,0.0 AS StoreMoney,0.0 as StoreMoneyDate, 0 AS StoreTimesDate, ISNULL(f_residualcredit, 0)  SumWinLose ,f_remark3 remark from FROM t_member  WITH(NOLOCK) and 1=1 {whereSql}";
            using (var db = _factory.GetDbInstance(new Dapper.AspNetCore.Dapper(new DapperConfig()
            {
                ConnectionString = AppSettings.Instance.AccountServiceAddress,
                DbType = DbType.MSSQLServer
            })))
            {
                return db.QueryList<UserDataOutput>(sql, parameters).ToList();
            }
        }

        /// <summary>
        /// 获取储值表信息
        /// </summary>
        /// <param name="whereSql">where条件sql</param>
        /// <param name="parameters"></param>
        /// <param name="inColName"></param>
        /// <param name="values"></param>
        public IList<UserDataOutput> GetUserNews(string whereSql, string inSql, DynamicParameters parameters)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT  '' alagent ,f_accounts accounts,'' title, GETDATE() joindate ,0 AS NoDays ,0 StupeSurplus , (CASE WHEN ISNULL(f_accountType,0)=1 THEN 4 ELSE f_ishow END) ishow,CAST(ISNULL(f_isDeposit,0) AS INT) StoreTimes,ISNULL( f_sumDeposit, 0)  AS StoreMoney,0.0 as StoreMoneyDate, 0 AS StoreTimesDate,ISNULL( f_sumWithdraw, 0)+ISNULL( f_sumfee, 0)+ISNULL(f_sumTurnout,0)- ISNULL(f_sumDeposit, 0)-ISNULL(f_sumTurnin,0) SumWinLose ,'' remark from t_h_UserNews a with(nolock) where 1=1");
            sqlBuilder.Append(whereSql);
            sqlBuilder.Append(inSql);
            using (var db = _factory.GetDbInstance(new Dapper.AspNetCore.Dapper(new DapperConfig()
            {
                ConnectionString = AppSettings.Instance.WEBDBServiceAddress,
                DbType = DbType.MSSQLServer
            })))
            {
                return db.QueryList<UserDataOutput>(sqlBuilder.ToString(), parameters).ToList();
            }
        }

        /// <summary>
        /// 获取储值表信息 （储值日期）
        /// </summary>
        public void GetUserNewsByStoreDate(GetUserNewsInput input)
        {
            using (var db = _factory.GetDbInstance(new Dapper.AspNetCore.Dapper(new DapperConfig()
            {
                ConnectionString = AppSettings.Instance.WEBDBServiceAddress,
                DbType = DbType.MSSQLServer
            })))
            {

            }
        }

        /// <summary>
        /// 获取储值表信息sql （储值日期）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetUserNewsByStoreDateSql(GetUserNewsInput input)
        {
            string storeWhere = string.Empty;
            string auditWhere = string.Empty;
            if (input.StoreTimes.HasValue)
            {
                storeWhere = " HAVING SUM(f_StoreTimesDate) >=" + input.StoreTimes.Value;
            }
            else
            {   //没有填存款次数 默认查有该日期范围内有存款的
                storeWhere = " HAVING SUM(f_StoreTimesDate) >0";
            }
            //储值开始时间
            if (input.BillStart.HasValue)
            {
                auditWhere += " f_AuditTime >= @startDate ";
            }
            //储值结束时间
            if (input.BillEnd.HasValue)
            {
                string andStr = auditWhere.Contains("and") ? "" : " and ";
                auditWhere += andStr + " f_AuditTime <= @endDate";
            }
            //总输赢表sql
            StringBuilder depositSqlBuilder = new StringBuilder();
            //储值表sql
            StringBuilder userNewsSqlBuilder = new StringBuilder();
            depositSqlBuilder.Append($"SELECT f_accounts,0 as f_drawSumM,SUM(ISNULL(f_money,0))AS f_StoreMoneyDate,SUM(1) AS f_StoreTimesDate   FROM t_DeltaRecords with(nolock)  where {auditWhere} AND isnull(f_del,0)=0 AND f_Audit>0 GROUP by f_accounts");
            depositSqlBuilder.Append("union all");
            depositSqlBuilder.Append($"SELECT f_accounts,0 as f_drawSumM,SUM(ISNULL(f_money,0))AS f_StoreMoneyDate,SUM(1) AS f_StoreTimesDate FROM t_DeltaOnline with(nolock) where {auditWhere} AND isnull(f_del,0)=0 AND f_State>0  GROUP by f_accounts");
            depositSqlBuilder.Append("union all");
            depositSqlBuilder.Append($"SELECT f_accounts,0 as f_drawSumM,SUM(ISNULL(f_money,0))AS f_StoreMoneyDate,SUM(1) AS f_StoreTimesDate  FROM t_DeltaAlipay with(nolock) where {auditWhere} AND isnull(f_del,0)=0 AND f_Audit>0 GROUP by f_accounts");
            depositSqlBuilder.Append("union all");
            depositSqlBuilder.Append($"SELECT f_accounts,0 as f_drawSumM,SUM(ISNULL(f_money,0))AS f_StoreMoneyDate,SUM(1) AS f_StoreTimesDate FROM t_DeltaWeChat with(nolock) where {auditWhere} AND isnull(f_del,0)=0 AND f_Audit>0  GROUP by f_accounts");

            userNewsSqlBuilder.AppendFormat(@"select h.f_alagent,h.f_accounts,'' f_title, GETDATE() f_joindate ,0 AS f_NoDays,0 f_StupeSurplus , (CASE WHEN ISNULL(f_accountType,0)=1 THEN 4 ELSE f_ishow END) f_ishow,''f_remark3,CAST(ISNULL(f_isDeposit,0) AS INT) as f_StoreTimes,ISNULL( f_sumWithdraw, 0)+ISNULL( f_sumfee, 0)+ISNULL(f_sumTurnout,0)- ISNULL(f_sumDeposit, 0)-ISNULL(f_sumTurnin,0) AS f_SumWinLose,ISNULL( f_sumDeposit, 0) as f_StoreMoney, f_StoreMoneyDate, f_StoreTimesDate,row_number() over (order by t.f_accounts) as iCount from (SELECT f_accounts,SUM(f_StoreMoneyDate) as f_StoreMoneyDate,SUM(f_StoreTimesDate) as f_StoreTimesDate
                             FROM({0})a GROUP BY a.f_accounts {1})as t
                            inner join t_h_UserNews h on t.f_accounts = h.f_accounts where isnull(f_isDeposit,0) > 0 {2}
            ", depositSqlBuilder.ToString(), storeWhere, "");

            return userNewsSqlBuilder.ToString();
        }

        /// <summary>
        /// 获取会员表查询条件
        /// </summary>
        /// <returns></returns>
        private string GetMemberWhereSql(GetMemberInput input, ref DynamicParameters parameters)
        {
            StringBuilder whereSqlBuilder = new StringBuilder();
            //账号
            if (!string.IsNullOrWhiteSpace(input.Account))
            {
                whereSqlBuilder.Append(" and f_accounts=@f_accounts ");
                parameters.Add("@f_accounts", input.Account);
            }
            //代理
            if (!string.IsNullOrWhiteSpace(input.Algent))
            {
                whereSqlBuilder.Append(" and f_alagent=@f_alagent ");
                parameters.Add("@f_alagent", input.Algent);
            }
            //昵称
            if (!string.IsNullOrEmpty(input.Title))
            {
                whereSqlBuilder.Append(" AND charindex(@f_title,f_title) >0 ");
                parameters.Add("@f_title", input.Title);
            }
            //未登录天数
            if (input.NoEnterDay.HasValue)
            {
                if (input.NoEnterDay.Value == 0)
                {
                    whereSqlBuilder.Append(" AND datediff(dd,f_enterdate,getdate()) = 0 ");
                }
                else
                {
                    whereSqlBuilder.Append(" AND datediff(dd,f_enterdate,getdate()) >=@f_enterdate ");
                    parameters.Add("@f_enterdate", input.NoEnterDay.Value);
                }
            }
            //会员等级
            if (input.Level.HasValue)
            {
                switch (input.Level.Value)
                {
                    case 0: whereSqlBuilder.Append(" AND ISNULL(f_StupeSurplus,0)<1000 "); break;
                    case 1: whereSqlBuilder.Append(" AND ISNULL(f_StupeSurplus,0)>=1000 AND ISNULL(f_StupeSurplus,0)<3000 "); break;
                    case 2: whereSqlBuilder.Append(" AND ISNULL(f_StupeSurplus,0)>=3000 AND ISNULL(f_StupeSurplus,0)<5000 "); break;
                    case 3: whereSqlBuilder.Append(" AND ISNULL(f_StupeSurplus,0)>=5000 AND ISNULL(f_StupeSurplus,0)<7000 "); break;
                    case 4: whereSqlBuilder.Append(" AND ISNULL(f_StupeSurplus,0)>=7000 AND ISNULL(f_StupeSurplus,0)<9000 "); break;
                    default:
                        break;
                }
            }
            //加入开始日期
            if (input.JoinStart.HasValue)
            {
                whereSqlBuilder.Append(" AND f_joindate >= @joinstart ");
                parameters.Add("@joinstart", input.JoinStart.Value);
            }
            //加入结束日期
            if (input.JoinEnd.HasValue)
            {
                whereSqlBuilder.Append(" AND f_joindate < @joinend ");
                parameters.Add("@joinend", input.JoinEnd.Value);
            }

            #region 当前五级状态和五级账号
            whereSqlBuilder.Append(" AND f_partner=@agAcc ");
            parameters.Add("@agAcc", "F21");
            #endregion


            return whereSqlBuilder.ToString();
        }

        /// <summary>
        /// 获取储值表where查询条件
        /// </summary>
        /// <returns></returns>
        private string GetUserNewsWhereSql(GetUserNewsInput input, ref DynamicParameters parameters)
        {
            StringBuilder whereSqlBuilder = new StringBuilder();
            //账号
            if (!string.IsNullOrEmpty(input.Account))
            {
                whereSqlBuilder.Append(" AND f_accounts=@f_accounts ");
                parameters.Add("@f_accounts", input.Account);
            }
            //储值次数
            if (input.StoreTimes.HasValue)
            {
                whereSqlBuilder.Append(" AND  isnull(f_isDeposit,0) >=@f_isDeposit ");
                parameters.Add("@f_isDeposit", input.StoreTimes);
            }
            else
            {
                //是否储值
                if (input.IsStoreDate || (input.IsDeposit.HasValue && input.IsDeposit.Value))
                {//如果有存款日期 或者 选择有存款
                    whereSqlBuilder.Append(" AND  isnull(f_isDeposit,0) >0 ");
                }
                else if (input.IsDeposit.HasValue && !input.IsDeposit.Value)
                {
                    whereSqlBuilder.Append(" AND  isnull(f_isDeposit,0) =0 ");
                }
            }
            //会员状态
            if (input.IsShow.HasValue)
            {
                whereSqlBuilder.Append(" AND (f_ishow =@f_ishow and ISNULL(f_accountType,0)=0) ");
                parameters.Add("@f_ishow", input.IsShow.Value);
            }
            return whereSqlBuilder.ToString();
        }

        /// <summary>
        /// 获取储值表in查询条件
        /// </summary>
        /// <param name="colName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        private string GetUserNewsInSql(string colName, IList<string> values = null)
        {
            StringBuilder builder = new StringBuilder();
            if (values != null)
            {
                builder.Append(" and " + colName + " in('" + string.Join("','", values) + "')");
            }
            return builder.ToString();
        }

        /// <summary>
        /// 获取储值次数及金额（有存款日期）
        /// </summary>
        /// <returns></returns>
        //private string GetStoreTimesAndMoneyWhereSql(GetTimesAndMoneyInput input)
        //{
        //    if (input.StoreTimes.HasValue)
        //    {
        //        storeWhere = " HAVING SUM(f_StoreTimesDate) >=" + Convert.ToInt16(sStoreTimes);
        //    }
        //    else
        //    {//没有填存款次数 默认查有该日期范围内有存款的
        //        storeWhere = " HAVING SUM(f_StoreTimesDate) >0";
        //    }
        //    StringBuilder sAuditTime = new StringBuilder();
        //    StringBuilder sDepositSql = new StringBuilder();
        //    if (!string.IsNullOrEmpty(sBillStart) && !string.IsNullOrEmpty(sBillEnd))
        //    {
        //        sAuditTime.Append("  f_AuditTime BETWEEN @startDate AND @endDate ");
        //    }
        //    else if (!string.IsNullOrEmpty(sBillStart))
        //    {
        //        sAuditTime.Append("  f_AuditTime >= @startDate ");
        //    }
        //    else if (!string.IsNullOrEmpty(sBillEnd))
        //    {
        //        sAuditTime.Append(sAuditTime.ToString().Length != 0 ? " AND " : string.Empty);
        //        sAuditTime.Append("  f_AuditTime <= @endDate ");
        //    }
        //}
    }
}
