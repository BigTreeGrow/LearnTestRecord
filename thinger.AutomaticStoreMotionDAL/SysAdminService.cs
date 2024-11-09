using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thinger.AutomaticStoreMotionModels;

namespace thinger.AutomaticStoreMotionDAL
{
   public  class SysAdminService
    {
        /// <summary>
        /// 返回所有的用户对象
        /// </summary>
        /// <returns>用户对象集合</returns>
        public static List<SysAdmin> GetAllAdminDB()
        {
            return SqlSugarHelper.SqlSugarClient.Queryable<SysAdmin>().Where(c=>c.LoginName.ToLower()!="admin").ToList();      
        }

        /// <summary>
        /// 验证用户登录结果
        /// </summary>
        /// <param name="admin">用户对象</param>
        /// <returns>用户对象</returns>
        public static SysAdmin AdminLogin(SysAdmin admin)
        {
            var listtemp = SqlSugarHelper.SqlSugarClient.Queryable<SysAdmin>().ToList();
            var list= SqlSugarHelper.SqlSugarClient.Queryable<SysAdmin>().Where(c => c.LoginName==admin.LoginName&&c.LoginPwd==admin.LoginPwd).ToList();

            return list.Count == 0 ? null : list[0];
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public static bool AddAdminDB(SysAdmin admin)
        {
            return SqlSugarHelper.SqlSugarClient.Insertable(admin).ExecuteCommand() == 1;
        }


        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public static bool CheckLoginNameExit(string loginName)
        {
            return SqlSugarHelper.SqlSugarClient.Queryable<SysAdmin>().Where(c => c.LoginName == loginName).Count() > 0;
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public static bool UpdateAdminDB(SysAdmin admin)
        {
            return SqlSugarHelper.SqlSugarClient.Updateable(admin).WhereColumns(c => c.LoginName).ExecuteCommand() == 1;
        
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public static bool DeleteAdminDB(string loginName)
        {
            return SqlSugarHelper.SqlSugarClient.Deleteable<SysAdmin>().Where(c => c.LoginName == loginName).ExecuteCommand() == 1;
        }

        /// <summary>
        /// 根据用户名称返回用户对象
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public static SysAdmin GetAdminByLoginName(string loginName)
        {
            var query = SqlSugarHelper.SqlSugarClient.Queryable<SysAdmin>().Where(c => c.LoginName == loginName).ToList();

            if (query.Count > 0)
            {
                return query[0];
            }
            else
            {
                return null;
            }
        }

    }
}
