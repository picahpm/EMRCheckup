using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;

namespace BKvs2010.EmrClass
{
    public class FavoriteCls
    {
        public FavoriteCls()
        {
            
        }

        public List<mst_conclusion_favorite_dtl> getFavorite(string order, string type)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    var favdesc = (from hfavorite in cdc.mst_conclusion_favorite_hdrs
                                   join lfavorite in cdc.mst_conclusion_favorite_dtls
                                   on hfavorite.mcfh_id equals lfavorite.mcfh_id
                                   where hfavorite.mcfh_order == order && hfavorite.mcfh_type == type && lfavorite.mcfd_active == true
                                   select lfavorite).ToList();
                    return favdesc;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("GetFavorite", "getfavorite", ex, false);
                return new List<mst_conclusion_favorite_dtl>();
            }
        }

        public bool saveFavorite(string order, string type, string description,string username)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    mst_conclusion_favorite_hdr hdr = cdc.mst_conclusion_favorite_hdrs.Where(x => x.mcfh_order == order && x.mcfh_type == type).FirstOrDefault();
                    if (hdr == null)
                    {
                        hdr = new mst_conclusion_favorite_hdr();
                        hdr.mcfh_active = true;
                        hdr.mcfh_order = order;
                        hdr.mcfh_type = type;
                        cdc.mst_conclusion_favorite_hdrs.InsertOnSubmit(hdr);
                    }
                    mst_conclusion_favorite_dtl dtl = hdr.mst_conclusion_favorite_dtls.Where(x => x.mcfd_description == description).FirstOrDefault();
                    if (dtl == null)
                    {
                        dtl = new mst_conclusion_favorite_dtl();

                        dtl.mcfd_description = description;
                        hdr.mst_conclusion_favorite_dtls.Add(dtl);
                    }
                    dtl.mcfd_active = true;
                    dtl.mcfd_create_by = username;
                    dtl.mcfd_create_date = dateNow;
                    cdc.SubmitChanges();


                    //cdc.mst_conclusion_favorite_dtls. = 1;   //รหัสกลุ่ม Order ดูได้จาก mst_favorite_h
                    //mst_favorite_d.mcfd_description = "ประโยคที่ใส่";
                    //mst_favorite_d.mcfd_active = true;
                    //mst_favorite_d.mcfd_create_by = username;
                    //mst_favorite_d.mcfd_create_date = DateTime.Now;

                    return true;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("saveFavorite", "saveFavorite", ex, false);
                return false;
            }


        }



        public bool removeFavorite(string order, string type, string description, string username)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                   
                    mst_conclusion_favorite_dtl dtl = cdc.mst_conclusion_favorite_dtls.Where(x => x.mcfd_description == description && x.mst_conclusion_favorite_hdr.mcfh_order == order &&
                                                        x.mst_conclusion_favorite_hdr.mcfh_type == type && x.mcfd_active == true).FirstOrDefault();
                    if (dtl != null)
                    {
                        dtl.mcfd_active = false;
                    }
                    cdc.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("saveFavorite", "saveFavorite", ex, false);
                return false;
            }


        }
        //get (order, type) return List String

        //save (order, type, description)
    }
}
