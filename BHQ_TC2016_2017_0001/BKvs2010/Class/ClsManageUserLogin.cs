using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;

namespace BKvs2010.Class
{
    class ClsManageUserLogin
    {
        public static bool closeFormByKick = false;

        public static log_user_login current_log
        {
            get;
            set;
        }

        public static bool checkKickCurrentUser(int mut_id, int mhs_id)
        {
            try
            {
                if (current_log != null)
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        log_user_login _current_log = cdc.log_user_logins.Where(x => x.lug_id == current_log.lug_id).FirstOrDefault();
                        if (_current_log.lug_end_date != null)
                        {
                            return true;
                        }
                        else
                        {
                            List<log_user_login> log = cdc.log_user_logins
                                                          .Where(x => x.mut_id == mut_id &&
                                                                      x.lug_end_date == null &&
                                                                      x.mhs_id == mhs_id)
                                                          .ToList();
                            if (log != null)
                            {
                                log_user_login last_log = log.OrderByDescending(x => x.lug_start_date)
                                                             .FirstOrDefault();
                                if (last_log.lug_id != current_log.lug_id)
                                {
                                    EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                                    List<int?> mrd_zoneB = mst.Get_MrdZoneB;
                                    if (!mrd_zoneB.Contains(last_log.mrd_id))
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        bool chkOtherZone = false;
                                        List<log_user_login> order_log = log.Where(x => x.lug_id != last_log.lug_id)
                                                                            .OrderByDescending(x => x.lug_start_date)
                                                                            .ToList();
                                        foreach (log_user_login result in order_log)
                                        {
                                            if (chkOtherZone == false && !mrd_zoneB.Contains(result.mrd_id))
                                            {
                                                chkOtherZone = true;
                                            }
                                            if (result.lug_id == current_log.lug_id)
                                            {
                                                if (chkOtherZone == true)
                                                {
                                                    return true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            return false;
        }
        public static bool checkKickCurrentUser()
        {
            if (Program.CurrentUser != null && Program.CurrentSite != null)
            {
                bool kickUser = checkKickCurrentUser(Program.CurrentUser.mut_id, Program.CurrentSite.mhs_id);
                closeFormByKick = kickUser;
                return kickUser;
            }
            return false;
        }

        public static bool checkAskingLoginRoom(int mut_id, int mhs_id, int mrd_id)
        {
            // return false =แสดงว่ามีการเข้าใช้งานเครื่องอื่นแล้ว : return true=แสดงว่าเป็นเครื่องที่ใช้งานอยู่
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                    List<int?> mrd_zoneB = mst.Get_MrdZoneB;
                    List<log_user_login> log = cdc.log_user_logins
                                                  .Where(x => x.mut_id == mut_id &&
                                                              x.lug_end_date == null &&
                                                              x.mhs_id == mhs_id)
                                                  .ToList();
                    if (log != null)
                    {
                        if (log.Count() > 0)
                        {
                            log_user_login log_same_mrd_id = log.Where(x => x.mrd_id == mrd_id).FirstOrDefault();
                            if (log_same_mrd_id != null)
                            {
                                return true;
                            }
                            else
                            {
                                if (!mrd_zoneB.Contains(mrd_id))
                                {
                                    return true;
                                }
                                else
                                {
                                    log_user_login last_log = log.OrderByDescending(x => x.lug_start_date)
                                                                 .FirstOrDefault();
                                    if (!mrd_zoneB.Contains(last_log.mrd_id))
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            return false;
        }
        public static bool checkAskingLoginRoom(int mrd_id)
        {
            if (Program.CurrentUser != null && Program.CurrentSite != null)
            {
                return checkAskingLoginRoom(Program.CurrentUser.mut_id, Program.CurrentSite.mhs_id, mrd_id);
            }
            return false;
        }

        public static void kickLoginOldRoom(int mut_id, int mrd_id)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                var log = cdc.log_user_logins.Where(x => x.mut_id == mut_id && /*x.mrd_id == mrd_id &&*/ x.lug_end_date == null).ToList();
                foreach (log_user_login item in log)
                {
                    item.lug_end_date = Program.GetServerDateTime();
                }
                cdc.SubmitChanges();
                List<log_user_login> log_room = cdc.log_user_logins.Where(x => /*x.mrd_id == mrd_id &&*/ x.lug_end_date == null).ToList();
                if (log_room == null || log_room.Count() == 0)
                {
                    List<int?> list_mrd_id = log_room.Select(x => x.mrd_id).ToList();
                    List<mst_room_dtl> list_room_dtl = cdc.mst_room_dtls.Where(x => list_mrd_id.Contains(x.mrd_id)).ToList();
                    list_room_dtl.ForEach(x => x.mrd_rm_status = 'E');
                    //mst_room_dtl mrd = cdc.mst_room_dtls.Where(x => x.mrd_id == mrd_id).FirstOrDefault();
                    //if (mrd != null)
                    //{
                    //    mrd.mrd_rm_status = 'E';
                    //}
                }
                mst_user_type mut = cdc.mst_user_types.Where(x => x.mut_id == mut_id).FirstOrDefault();
                mut.mut_login_status = Program.get_mutLoginStatus;
                cdc.SubmitChanges();
            }
        }
    }
}
