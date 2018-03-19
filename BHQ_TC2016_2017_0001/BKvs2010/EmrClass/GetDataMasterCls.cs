using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;

namespace BKvs2010.EmrClass
{
    public class GetDataMasterCls
    {
        #region getDataMaster
        public mst_room_hdr GetMstRoomHdr(int mrm_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    mst_room_hdr mrm = cdc.mst_room_hdrs.Where(x => x.mrm_id == mrm_id).FirstOrDefault();
                    return mrm;
                }
            }
            catch
            {

            }
            return null;
        }
        public mst_room_hdr GetMstRoomHdr(string mrm_code)
        {
            try
            {
                int mhs_id = Program.CurrentRegis.mhs_id;
                int? tpr_site_use = Program.CurrentRegis.tpr_site_use;
                return GetMstRoomHdr(mrm_code, mhs_id, tpr_site_use);
            }
            catch
            {

            }
            return null;
        }
        public mst_room_hdr GetMstRoomHdr(string mrm_code, string mhs_code)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    mst_hpc_site mhs = GetMstHpcSite(mhs_code);
                    if (mhs != null)
                    {
                        int mhs_id = mhs.mhs_id;
                        mst_room_hdr mrm = cdc.mst_room_hdrs.Where(x => x.mrm_code == mrm_code && x.mhs_id == mhs_id).FirstOrDefault();
                        return mrm;
                    }
                }
            }
            catch
            {

            }
            return null;
        }
        public mst_room_hdr GetMstRoomHdr(string mrm_code, int mhs_id, int? tpr_site_use)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    int? site_id;
                    if (mrm_code == "CC" || mrm_code == "PH" || mrm_code == "BK")
                    {
                        site_id = mhs_id;
                    }
                    else
                    {
                        site_id = (tpr_site_use != null) ? tpr_site_use : mhs_id;
                    }
                    mst_room_hdr mrm = cdc.mst_room_hdrs.Where(x => x.mrm_code == mrm_code && x.mhs_id == site_id).FirstOrDefault();
                    return mrm;
                }
            }
            catch
            {

            }
            return null;
        }
        public mst_room_hdr GetMstRoomHdrByMrd_id(int mrd_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    mst_room_hdr mrm = cdc.mst_room_dtls.Where(x => x.mrd_id == mrd_id).Select(x => x.mst_room_hdr).FirstOrDefault();
                    return mrm;
                }
            }
            catch
            {

            }
            return null;
        }

        public List<mst_room_dtl> GetListRoomDtl(int mrm_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    List<mst_room_dtl> mrd = cdc.mst_room_dtls.Where(x => x.mrm_id == mrm_id).ToList();
                    return mrd;
                }
            }
            catch
            {

            }
            return null;
        }
        public List<mst_room_dtl> GetListRoomDtl(string mrm_code, string mhs_code)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    mst_room_hdr mrm = GetMstRoomHdr(mrm_code, mhs_code);
                    if (mrm != null)
                    {
                        int mrm_id = mrm.mrm_id;
                        List<mst_room_dtl> mrd = GetListRoomDtl(mrm_id);
                        return mrd;
                    }
                }
            }
            catch
            {

            }
            return null;
        }

        public mst_event GetMstEvent(int mvt_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    mst_event mvt = cdc.mst_events.Where(x => x.mvt_id == mvt_id).FirstOrDefault();
                    return mvt;
                }
            }
            catch
            {

            }
            return null;
        }
        public mst_event GetMstEvent(string mvt_code)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    mst_event mvt = cdc.mst_events.Where(x => x.mvt_code == mvt_code).FirstOrDefault();
                    return mvt;
                }
            }
            catch
            {

            }
            return null;
        }

        public mst_user_type GetUser(string username)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                mst_user_type mut = cdc.mst_user_types.Where(x => x.mut_username == username).FirstOrDefault();
                return mut;
            }
        }

        public List<int?> Get_MrdZoneB
        {
            get
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    var result = cdc.mst_room_dtls.Where(x => x.mst_room_hdr.mst_zone.mze_code == "B" && x.mst_room_hdr.mst_hpc_site.mhs_code == "01CHK").Select(x => (int?)x.mrd_id);
                    if (result != null)
                    {
                        return result.ToList();
                    }
                    return null;
                }
            }
        }

        public mst_room_hdr GetMstRoomHdrByMvt(int mvt_id, int mhs_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    mst_room_hdr mrm = cdc.mst_room_events.Where(x => x.mvt_id == mvt_id && x.mst_room_hdr.mhs_id == mhs_id).Select(x => x.mst_room_hdr).FirstOrDefault();
                    return mrm;
                }
            }
            catch
            {

            }
            return null;
        }

        public List<mst_room_event> GetMstRoomEventByMrm(int mrm_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    List<mst_room_event> mre = cdc.mst_room_events.Where(x => x.mrm_id == mrm_id).ToList();
                    return mre;
                }
            }
            catch
            {

            }
            return null;
        }

        public mst_zone GetMstZone(int mze_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    mst_zone mze = cdc.mst_zones.Where(x => x.mze_id == mze_id).FirstOrDefault();
                    return mze;
                }
            }
            catch
            {
                return null;
            }
        }

        public mst_hpc_site GetMstHpcSite(int mhs_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    mst_hpc_site mhs = cdc.mst_hpc_sites.Where(x => x.mhs_id == mhs_id).FirstOrDefault();
                    return mhs;
                }
            }
            catch
            {

            }
            return null;
        }
        public mst_hpc_site GetMstHpcSite(string mhs_code)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    mst_hpc_site mhs = cdc.mst_hpc_sites.Where(x => x.mhs_code == mhs_code).FirstOrDefault();
                    return mhs;
                }
            }
            catch
            {

            }
            return null;
        }

        public List<int> GetAllRoomByMvt(int mvt_id)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                List<int> list_mrm_id = new List<int>();
                list_mrm_id.Add(cdc.mst_room_events.Where(x => x.mvt_id == mvt_id).Select(x => x.mrm_id).FirstOrDefault());
                return list_mrm_id;
            }
        }
        #endregion getDataMaster
    }
}
