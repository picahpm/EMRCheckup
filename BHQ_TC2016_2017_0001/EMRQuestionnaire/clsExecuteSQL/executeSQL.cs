using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using QuestionnaireWebSite.clsConnection;
using QuestionnaireWebSite.clsUtility;
using EMRQuestionnaire.clsQuestionaire;
namespace QuestionnaireWebSite.clsExecuteSQL
{

    public class executeDC
    {

        public string _BHQ_ConnectionString = connectDatabase.EMRQConnectionString;
        public string _DB_QuestionaireConnectionString = connectDatabase.QuestionaireConnectionString;


        public DataTable get_history_questionaire(string keySearch)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                DataTable pDs = new DataTable();
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }
                pConn.Open();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.CommandText = "SP_GET_PATIENT";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();
                        pCmd.Parameters.AddWithValue("@HN", keySearch);

                        SqlDataAdapter pDta = new SqlDataAdapter(pCmd);
                        pDta.Fill(pDs);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
                return pDs;
            }
        }

        public DataTable get_user_check_duplicate(string keySearch)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                DataTable pDs = new DataTable();
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }
                pConn.Open();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.CommandText = "SP_GET_DUPLICATE";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();
                        pCmd.Parameters.AddWithValue("@keySearch", keySearch);

                        SqlDataAdapter pDta = new SqlDataAdapter(pCmd);
                        pDta.Fill(pDs);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
                return pDs;
            }

        }

        public DataTable get_draft(string HN)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                DataTable pDs = new DataTable();
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }
                pConn.Open();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.CommandText = "SP_GET_DRAFT";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();
                        pCmd.Parameters.AddWithValue("@HN", HN);

                        SqlDataAdapter pDta = new SqlDataAdapter(pCmd);
                        pDta.Fill(pDs);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
                return pDs;
            }

        }

        public DataTable get_mas_patient_from_BHQ(string UID)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                DataTable pDs = new DataTable();
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }
                pConn.Open();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.CommandText = "sp_Get_PA_Adm";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();
                        pCmd.Parameters.AddWithValue("@UID", UID);

                        SqlDataAdapter pDta = new SqlDataAdapter(pCmd);
                        pDta.Fill(pDs);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
                return pDs;
            }

        }

        public DataTable get_mas_label(string label_id, string label_type, string table_name)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                DataTable pDs = new DataTable();
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }
                pConn.Open();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.CommandText = "SP_GET_MAS_LABEL";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();
                        pCmd.Parameters.AddWithValue("@label_id", label_id);
                        pCmd.Parameters.AddWithValue("@label_type", label_type);
                        pCmd.Parameters.AddWithValue("@table_name", table_name);

                        SqlDataAdapter pDta = new SqlDataAdapter(pCmd);
                        pDta.Fill(pDs);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
                return pDs;
            }

        }
        public void insert_trn_patients(clsPatientINFO CLS_PATIENT_INFO_SMS)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PATIENTS";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.VarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = CLS_PATIENT_INFO_SMS.HN;
                        pCmd.Parameters.Add(mParm1);

                        SqlParameter mParm0 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm0.Direction = ParameterDirection.Input;
                        mParm0.Value = CLS_PATIENT_INFO_SMS.KEY_GEN;
                        pCmd.Parameters.Add(mParm0);

                        SqlParameter mParm2 = new SqlParameter("@TITLE_NAME_TH", SqlDbType.VarChar);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = CLS_PATIENT_INFO_SMS.TITLE_NAME_TH;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@TITLE_NAME_EN", SqlDbType.VarChar);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = CLS_PATIENT_INFO_SMS.TITLE_NAME_EN;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@FIRST_NAME_TH", SqlDbType.VarChar);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = CLS_PATIENT_INFO_SMS.FIRST_NAME_TH;
                        pCmd.Parameters.Add(mParm4);

                        SqlParameter mParm5 = new SqlParameter("@FIRST_NAME_EN", SqlDbType.VarChar);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = CLS_PATIENT_INFO_SMS.FIRST_NAME_EN;
                        pCmd.Parameters.Add(mParm5);

                        SqlParameter mParm6 = new SqlParameter("@LAST_NAME_TH", SqlDbType.VarChar);
                        mParm6.Direction = ParameterDirection.Input;
                        mParm6.Value = CLS_PATIENT_INFO_SMS.LAST_NAME_TH;
                        pCmd.Parameters.Add(mParm6);

                        SqlParameter mParm7 = new SqlParameter("@LAST_NAME_EN", SqlDbType.VarChar);
                        mParm7.Direction = ParameterDirection.Input;
                        mParm7.Value = CLS_PATIENT_INFO_SMS.LAST_NAME_EN;
                        pCmd.Parameters.Add(mParm7);

                        SqlParameter mParm8 = new SqlParameter("@FULL_NAME", SqlDbType.VarChar);
                        mParm8.Direction = ParameterDirection.Input;
                        mParm8.Value = CLS_PATIENT_INFO_SMS.FULL_NAME;
                        pCmd.Parameters.Add(mParm8);


                        SqlParameter mParm9 = new SqlParameter("@ROOM", SqlDbType.VarChar);
                        mParm9.Direction = ParameterDirection.Input;
                        mParm9.Value = CLS_PATIENT_INFO_SMS.ROOM;
                        pCmd.Parameters.Add(mParm9);

                        SqlParameter mParm10 = new SqlParameter("@PHYSICIAN", SqlDbType.VarChar);
                        mParm10.Direction = ParameterDirection.Input;
                        mParm10.Value = CLS_PATIENT_INFO_SMS.PHYSICIAN;
                        pCmd.Parameters.Add(mParm10);

                        SqlParameter mParm11 = new SqlParameter("@VISIT_DATE", SqlDbType.VarChar);
                        mParm11.Direction = ParameterDirection.Input;
                        mParm11.Value = CLS_PATIENT_INFO_SMS.VISIT_DATE;
                        pCmd.Parameters.Add(mParm11);

                        SqlParameter mParm12 = new SqlParameter("@DEPARTMENT", SqlDbType.VarChar);
                        mParm12.Direction = ParameterDirection.Input;
                        mParm12.Value = CLS_PATIENT_INFO_SMS.DEPARTMENT;
                        pCmd.Parameters.Add(mParm12);

                        SqlParameter mParm13 = new SqlParameter("@BIRTH_DATE", SqlDbType.VarChar);
                        mParm13.Direction = ParameterDirection.Input;
                        mParm13.Value = CLS_PATIENT_INFO_SMS.BIRTH_DATE;
                        pCmd.Parameters.Add(mParm13);

                        SqlParameter mParm14 = new SqlParameter("@AGE", SqlDbType.VarChar);
                        mParm14.Direction = ParameterDirection.Input;
                        mParm14.Value = CLS_PATIENT_INFO_SMS.AGE;
                        pCmd.Parameters.Add(mParm14);

                        SqlParameter mParm15 = new SqlParameter("@SEX", SqlDbType.VarChar);
                        mParm15.Direction = ParameterDirection.Input;
                        mParm15.Value = CLS_PATIENT_INFO_SMS.SEX;
                        pCmd.Parameters.Add(mParm15);

                        SqlParameter mParm16 = new SqlParameter("@ALLERGIES", SqlDbType.VarChar);
                        mParm16.Direction = ParameterDirection.Input;
                        mParm16.Value = CLS_PATIENT_INFO_SMS.ALLERGIES;
                        pCmd.Parameters.Add(mParm16);


                        SqlParameter mParm19 = new SqlParameter("@LANGUAGE", SqlDbType.VarChar);
                        mParm19.Direction = ParameterDirection.Input;
                        mParm19.Value = CLS_PATIENT_INFO_SMS.LANGUAGE;
                        pCmd.Parameters.Add(mParm19);

                        SqlParameter mParm20 = new SqlParameter("@DRAFT", SqlDbType.VarChar);
                        mParm20.Direction = ParameterDirection.Input;
                        mParm20.Value = CLS_PATIENT_INFO_SMS.DRAFT;
                        pCmd.Parameters.Add(mParm20);

                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.VarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = CLS_PATIENT_INFO_SMS.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.VarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = CLS_PATIENT_INFO_SMS.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        public void insert_trn_general_informational(clsGeneralInformation clsGeneral)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_GENERAL_INFORMATIONAL";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.VarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsGeneral.HN;
                        pCmd.Parameters.Add(mParm1);

                        SqlParameter mParm20 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm20.Direction = ParameterDirection.Input;
                        mParm20.Value = clsGeneral.KEY_GEN;
                        pCmd.Parameters.Add(mParm20);

                        SqlParameter mParm2 = new SqlParameter("@EMPLOYMENT_DATE", SqlDbType.VarChar);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsGeneral.EMPLOYMENT_DATE;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@EMPLOYER", SqlDbType.VarChar);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsGeneral.EMPLOYER;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@WORK_LOCATION", SqlDbType.VarChar);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsGeneral.WORK_LOCATION;
                        pCmd.Parameters.Add(mParm4);

                        SqlParameter mParm5 = new SqlParameter("@EMAIL_ADDRESS", SqlDbType.VarChar);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsGeneral.EMAIL_ADDRESS;
                        pCmd.Parameters.Add(mParm5);

                        SqlParameter mParm6 = new SqlParameter("@FUNCTIONAL_GROUP", SqlDbType.VarChar);
                        mParm6.Direction = ParameterDirection.Input;
                        mParm6.Value = clsGeneral.FUNCTIONAL_GROUP;
                        pCmd.Parameters.Add(mParm6);

                        SqlParameter mParm7 = new SqlParameter("@CURRENT_POSITION", SqlDbType.VarChar);
                        mParm7.Direction = ParameterDirection.Input;
                        mParm7.Value = clsGeneral.CURRENT_POSITION;
                        pCmd.Parameters.Add(mParm7);

                        SqlParameter mParm8 = new SqlParameter("@TELEPHONE", SqlDbType.VarChar);
                        mParm8.Direction = ParameterDirection.Input;
                        mParm8.Value = clsGeneral.TELEPHONE;
                        pCmd.Parameters.Add(mParm8);


                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.VarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsGeneral.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.VarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsGeneral.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        //public void insert_trn_working_history(clsWorkingHistory clsWorkHistory)
        //{
        //    using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
        //    {
        //        if (pConn.State == ConnectionState.Open)
        //        {
        //            pConn.Close();
        //        }

        //        pConn.Open();

        //        SqlTransaction pTrans;
        //        pTrans = pConn.BeginTransaction();
        //        try
        //        {
        //            using (SqlCommand pCmd = new SqlCommand())
        //            {
        //                pCmd.Connection = pConn;
        //                pCmd.Transaction = pTrans;
        //                pCmd.CommandText = "SP_INSERT_TRN_WORKING_HISTORY";
        //                pCmd.CommandType = CommandType.StoredProcedure;
        //                pCmd.Parameters.Clear();

        //                SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.VarChar);
        //                mParm1.Direction = ParameterDirection.Input;
        //                mParm1.Value = clsWorkHistory.HN;
        //                pCmd.Parameters.Add(mParm1);

        //                SqlParameter mParm2 = new SqlParameter("@WORKING_HISTORY_PAST_AND_FUTURE", SqlDbType.VarChar);
        //                mParm2.Direction = ParameterDirection.Input;
        //                mParm2.Value = clsWorkHistory.WORKING_HISTORY_PAST_AND_FUTURE;
        //                pCmd.Parameters.Add(mParm2);

        //                SqlParameter mParm3 = new SqlParameter("@WORKING_HISTORY_TYPE", SqlDbType.VarChar);
        //                mParm3.Direction = ParameterDirection.Input;
        //                mParm3.Value = clsWorkHistory.WORKING_HISTORY_TYPE;
        //                pCmd.Parameters.Add(mParm3);

        //                SqlParameter mParm4 = new SqlParameter("@WORKING_HISTORY_SUB_TYPE", SqlDbType.VarChar);
        //                mParm4.Direction = ParameterDirection.Input;
        //                mParm4.Value = clsWorkHistory.WORKING_HISTORY_SUB_TYPE;
        //                pCmd.Parameters.Add(mParm4);

        //                SqlParameter mParm5 = new SqlParameter("@EMPLOYER_DEPARTMENT_ONE", SqlDbType.VarChar);
        //                mParm5.Direction = ParameterDirection.Input;
        //                mParm5.Value = clsWorkHistory.EMPLOYER_DEPARTMENT_ONE;
        //                pCmd.Parameters.Add(mParm5);

        //                SqlParameter mParm6 = new SqlParameter("@EMPLOYER_DEPARTMENT_TWO", SqlDbType.VarChar);
        //                mParm6.Direction = ParameterDirection.Input;
        //                mParm6.Value = clsWorkHistory.EMPLOYER_DEPARTMENT_TWO;
        //                pCmd.Parameters.Add(mParm6);

        //                SqlParameter mParm7 = new SqlParameter("@EMPLOYER_DEPARTMENT_THREE", SqlDbType.VarChar);
        //                mParm7.Direction = ParameterDirection.Input;
        //                mParm7.Value = clsWorkHistory.EMPLOYER_DEPARTMENT_THREE;
        //                pCmd.Parameters.Add(mParm7);

        //                SqlParameter mParm8 = new SqlParameter("@EMPLOYER_DEPARTMENT_FOUR", SqlDbType.VarChar);
        //                mParm8.Direction = ParameterDirection.Input;
        //                mParm8.Value = clsWorkHistory.EMPLOYER_DEPARTMENT_FOUR;
        //                pCmd.Parameters.Add(mParm8);

        //                SqlParameter mParm9 = new SqlParameter("@EMPLOYER_DEPARTMENT_FIVE", SqlDbType.VarChar);
        //                mParm9.Direction = ParameterDirection.Input;
        //                mParm9.Value = clsWorkHistory.EMPLOYER_DEPARTMENT_FIVE;
        //                pCmd.Parameters.Add(mParm9);

        //                SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.VarChar);
        //                mParm17.Direction = ParameterDirection.Input;
        //                mParm17.Value = clsWorkHistory.CREATE_BY;
        //                pCmd.Parameters.Add(mParm17);


        //                SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.VarChar);
        //                mParm18.Direction = ParameterDirection.Input;
        //                mParm18.Value = clsWorkHistory.UPDATE_BY;
        //                pCmd.Parameters.Add(mParm18);

        //                pCmd.ExecuteNonQuery();
        //                pTrans.Commit();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            pTrans.Rollback();
        //            ex.Message.ToString();
        //        }
        //        finally
        //        {

        //            if (pConn.State == ConnectionState.Open)
        //            {
        //                pConn.Close();
        //                pConn.Dispose();
        //            }
        //        }
        //    }
        //}

        public void insert_trn_working_history_All(clsWorkingHistory clsWorkHistory)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_WORKING_HISTORY_ALL";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.VarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsWorkHistory.HN;
                        pCmd.Parameters.Add(mParm1);

                        SqlParameter mParm2 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsWorkHistory.KEY_GEN;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT", SqlDbType.VarChar);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsWorkHistory.WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT", SqlDbType.VarChar);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsWorkHistory.WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT;
                        pCmd.Parameters.Add(mParm4);

                        SqlParameter mParm5 = new SqlParameter("@WORKING_HISTORY_TYPE_OF_WORK", SqlDbType.VarChar);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsWorkHistory.WORKING_HISTORY_TYPE_OF_WORK;
                        pCmd.Parameters.Add(mParm5);

                        SqlParameter mParm6 = new SqlParameter("@WORKING_HISTORY_PERIOD_DATE_FROM", SqlDbType.VarChar);
                        mParm6.Direction = ParameterDirection.Input;
                        mParm6.Value = clsWorkHistory.WORKING_HISTORY_PERIOD_DATE_FROM;
                        pCmd.Parameters.Add(mParm6);

                        SqlParameter mParm7 = new SqlParameter("@WORKING_HISTORY_PERIOD_DATE_TO", SqlDbType.VarChar);
                        mParm7.Direction = ParameterDirection.Input;
                        mParm7.Value = clsWorkHistory.WORKING_HISTORY_PERIOD_DATE_TO;
                        pCmd.Parameters.Add(mParm7);

                        SqlParameter mParm8 = new SqlParameter("@WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS", SqlDbType.VarChar);
                        mParm8.Direction = ParameterDirection.Input;
                        mParm8.Value = clsWorkHistory.WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS;
                        pCmd.Parameters.Add(mParm8);

                        SqlParameter mParm9 = new SqlParameter("@WORKING_HISTORYWORK_PPE", SqlDbType.VarChar);
                        mParm9.Direction = ParameterDirection.Input;
                        mParm9.Value = clsWorkHistory.WORKING_HISTORYWORK_PPE;
                        pCmd.Parameters.Add(mParm9);

                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.VarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsWorkHistory.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.VarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsWorkHistory.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        public void insert_trn_working_history_sub_type(clsWorkingHistorySubType clsWorkHistorySubType)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_WORKING_HISTORY_SUB_TYPE";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.VarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsWorkHistorySubType.HN;
                        pCmd.Parameters.Add(mParm1);

                        SqlParameter mParm2 = new SqlParameter("@WORKING_HISTORY_SUB_TYPE", SqlDbType.VarChar);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsWorkHistorySubType.WORKING_HISTORY_SUB_TYPE;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@PERIOD_DATE_FORM", SqlDbType.VarChar);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsWorkHistorySubType.PERIOD_DATE_FORM;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@PERIOD_DATE_TO", SqlDbType.VarChar);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsWorkHistorySubType.PERIOD_DATE_TO;
                        pCmd.Parameters.Add(mParm4);



                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.VarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsWorkHistorySubType.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.VarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsWorkHistorySubType.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        public void insert_trn_working_history_classification_of_employment(clsWorkingHistoryClassificationOfEmployment clsClassification)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.VarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsClassification.HN;
                        pCmd.Parameters.Add(mParm1);

                        SqlParameter mParm19 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm19.Direction = ParameterDirection.Input;
                        mParm19.Value = clsClassification.KEY_GEN;
                        pCmd.Parameters.Add(mParm19);

                        SqlParameter mParm2 = new SqlParameter("@OIL_NATURAL_GAS", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsClassification.OIL_NATURAL_GAS;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@NON_METAL_PRODUCTS", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsClassification.NON_METAL_PRODUCTS;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@MANUFACTURE_OF_FOOD", SqlDbType.Char);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsClassification.MANUFACTURE_OF_FOOD;
                        pCmd.Parameters.Add(mParm4);

                        SqlParameter mParm5 = new SqlParameter("@MANUFACTURE_OF_BASIC_METALS", SqlDbType.Char);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsClassification.MANUFACTURE_OF_BASIC_METALS;
                        pCmd.Parameters.Add(mParm5);

                        SqlParameter mParm6 = new SqlParameter("@MANUFACTURE_OF_TEXTILES", SqlDbType.Char);
                        mParm6.Direction = ParameterDirection.Input;
                        mParm6.Value = clsClassification.MANUFACTURE_OF_TEXTILES;
                        pCmd.Parameters.Add(mParm6);

                        SqlParameter mParm7 = new SqlParameter("@METALS_PRODUCTS", SqlDbType.Char);
                        mParm7.Direction = ParameterDirection.Input;
                        mParm7.Value = clsClassification.METALS_PRODUCTS;
                        pCmd.Parameters.Add(mParm7);

                        SqlParameter mParm8 = new SqlParameter("@FORESTRY_AND_LOGGING", SqlDbType.Char);
                        mParm8.Direction = ParameterDirection.Input;
                        mParm8.Value = clsClassification.FORESTRY_AND_LOGGING;
                        pCmd.Parameters.Add(mParm8);



                        SqlParameter mParm10 = new SqlParameter("@MANUFACTURE_OF_MOTOR", SqlDbType.Char);
                        mParm10.Direction = ParameterDirection.Input;
                        mParm10.Value = clsClassification.MANUFACTURE_OF_MOTOR;
                        pCmd.Parameters.Add(mParm10);

                        SqlParameter mParm11 = new SqlParameter("@MANUFACTURE_OF_PAPER", SqlDbType.Char);
                        mParm11.Direction = ParameterDirection.Input;
                        mParm11.Value = clsClassification.MANUFACTURE_OF_PAPER;
                        pCmd.Parameters.Add(mParm11);


                        SqlParameter mParm12 = new SqlParameter("@PUBLIC_UTILITY", SqlDbType.Char);
                        mParm12.Direction = ParameterDirection.Input;
                        mParm12.Value = clsClassification.PUBLIC_UTILITY;
                        pCmd.Parameters.Add(mParm12);


                        SqlParameter mParm13 = new SqlParameter("@MANUFACTURE_OF_CHEMICAL", SqlDbType.Char);
                        mParm13.Direction = ParameterDirection.Input;
                        mParm13.Value = clsClassification.MANUFACTURE_OF_CHEMICAL;
                        pCmd.Parameters.Add(mParm13);

                        SqlParameter mParm14 = new SqlParameter("@OTHERS", SqlDbType.Char);
                        mParm14.Direction = ParameterDirection.Input;
                        mParm14.Value = clsClassification.OTHERS;
                        pCmd.Parameters.Add(mParm14);


                        SqlParameter mParm15 = new SqlParameter("@OTHERS_DETAILS", SqlDbType.NVarChar);
                        mParm15.Direction = ParameterDirection.Input;
                        mParm15.Value = clsClassification.OTHERS_DETAILS;
                        pCmd.Parameters.Add(mParm15);


                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.VarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsClassification.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.VarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsClassification.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        public void insert_trn_working_history_type_of_work(clsWorkingHistoryTypeOfWork clsTypeOfWork)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_WORKING_HISTORY_TYPE_OF_WORK";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.VarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsTypeOfWork.HN;
                        pCmd.Parameters.Add(mParm1);

                        SqlParameter mParm11 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm11.Direction = ParameterDirection.Input;
                        mParm11.Value = clsTypeOfWork.KEY_GEN;
                        pCmd.Parameters.Add(mParm11);

                        SqlParameter mParm2 = new SqlParameter("@OFFICE_WORK", SqlDbType.VarChar);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsTypeOfWork.OFFICE_WORK;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@ONSHORE_WORK", SqlDbType.VarChar);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsTypeOfWork.ONSHORE_WORK;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@OFFSHORE_WORK", SqlDbType.VarChar);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsTypeOfWork.OFFSHORE_WORK;
                        pCmd.Parameters.Add(mParm4);



                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.VarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsTypeOfWork.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.VarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsTypeOfWork.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_insert_trn_working_history_special_assignment(clsWorkingHistorySpecialAssignment clsAssignment)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_WORKING_HISTORY_SPECIAL_ASSIGNMENT";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.VarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsAssignment.HN;
                        pCmd.Parameters.Add(mParm1);

                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsAssignment.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@FIRE_FIGHTING_STAFF", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsAssignment.FIRE_FIGHTING_STAFF;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@CONFINED_SPACE_WORKER", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsAssignment.CONFINED_SPACE_WORKER;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@PROFESSIONAL_DRIVER", SqlDbType.Char);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsAssignment.PROFESSIONAL_DRIVER;
                        pCmd.Parameters.Add(mParm4);

                        SqlParameter mParm5 = new SqlParameter("@LABORATORY_TECHNICIAN", SqlDbType.Char);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsAssignment.LABORATORY_TECHNICIAN;
                        pCmd.Parameters.Add(mParm5);

                        SqlParameter mParm6 = new SqlParameter("@CRANE_OPERATOR", SqlDbType.Char);
                        mParm6.Direction = ParameterDirection.Input;
                        mParm6.Value = clsAssignment.CRANE_OPERATOR;
                        pCmd.Parameters.Add(mParm6);

                        SqlParameter mParm7 = new SqlParameter("@PAINTER", SqlDbType.Char);
                        mParm7.Direction = ParameterDirection.Input;
                        mParm7.Value = clsAssignment.PAINTER;
                        pCmd.Parameters.Add(mParm7);


                        SqlParameter mParm8 = new SqlParameter("@CATERING_AND_FOOD", SqlDbType.Char);
                        mParm8.Direction = ParameterDirection.Input;
                        mParm8.Value = clsAssignment.CATERING_AND_FOOD;
                        pCmd.Parameters.Add(mParm8);


                        SqlParameter mParm9 = new SqlParameter("@OTHERS", SqlDbType.Char);
                        mParm9.Direction = ParameterDirection.Input;
                        mParm9.Value = clsAssignment.OTHERS;
                        pCmd.Parameters.Add(mParm9);


                        SqlParameter mParm10 = new SqlParameter("@OTHERS_DETAILS", SqlDbType.NVarChar);
                        mParm10.Direction = ParameterDirection.Input;
                        mParm10.Value = clsAssignment.OTHERS_DETAILS;
                        pCmd.Parameters.Add(mParm10);


                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsAssignment.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsAssignment.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_working_history_physical_health_hazard(clsWorkingHistoryPhysicalHealthHazard clsPhysical)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_WORKING_HISTORY_PHYSICAL_HEALTH_HAZARD";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsPhysical.HN;
                        pCmd.Parameters.Add(mParm1);

                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsPhysical.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@NO_PHYSICAL_HEALTH_HAZARD", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsPhysical.NO_PHYSICAL_HEALTH_HAZARD;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@LIGHT", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsPhysical.LIGHT;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@COLD", SqlDbType.Char);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsPhysical.COLD;
                        pCmd.Parameters.Add(mParm4);

                        SqlParameter mParm5 = new SqlParameter("@NOISE", SqlDbType.Char);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsPhysical.NOISE;
                        pCmd.Parameters.Add(mParm5);

                        SqlParameter mParm6 = new SqlParameter("@RADIATION", SqlDbType.Char);
                        mParm6.Direction = ParameterDirection.Input;
                        mParm6.Value = clsPhysical.RADIATION;
                        pCmd.Parameters.Add(mParm6);

                        SqlParameter mParm7 = new SqlParameter("@HEAT", SqlDbType.Char);
                        mParm7.Direction = ParameterDirection.Input;
                        mParm7.Value = clsPhysical.HEAT;
                        pCmd.Parameters.Add(mParm7);


                        SqlParameter mParm9 = new SqlParameter("@OTHERS", SqlDbType.Char);
                        mParm9.Direction = ParameterDirection.Input;
                        mParm9.Value = clsPhysical.OTHERS;
                        pCmd.Parameters.Add(mParm9);


                        SqlParameter mParm10 = new SqlParameter("@OTHERS_DETAILS", SqlDbType.NVarChar);
                        mParm10.Direction = ParameterDirection.Input;
                        mParm10.Value = clsPhysical.OTHERS_DETAILS;
                        pCmd.Parameters.Add(mParm10);


                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsPhysical.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsPhysical.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_working_history_biological_health_hazard(clsWorkingHistoryBiologicalHealthHazard clsBiological)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_WORKING_HISTORY_BIOLOGICAL_HEALTH_HAZARD";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsBiological.HN;
                        pCmd.Parameters.Add(mParm1);


                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsBiological.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);


                        SqlParameter mParm2 = new SqlParameter("@NO_BIOLOGICAL_HEALTH_HAZARD", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsBiological.NO_BIOLOGICAL_HEALTH_HAZARD;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@ANIMAL_CARRIERS", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsBiological.ANIMAL_CARRIERS;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@BLOOD_OR_OTHER", SqlDbType.Char);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsBiological.BLOOD_OR_OTHER;
                        pCmd.Parameters.Add(mParm4);

                        SqlParameter mParm5 = new SqlParameter("@BACTERIA", SqlDbType.Char);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsBiological.BACTERIA;
                        pCmd.Parameters.Add(mParm5);

                        SqlParameter mParm6 = new SqlParameter("@FUNGUS", SqlDbType.Char);
                        mParm6.Direction = ParameterDirection.Input;
                        mParm6.Value = clsBiological.FUNGUS;
                        pCmd.Parameters.Add(mParm6);

                        SqlParameter mParm7 = new SqlParameter("@VIRUS", SqlDbType.Char);
                        mParm7.Direction = ParameterDirection.Input;
                        mParm7.Value = clsBiological.VIRUS;
                        pCmd.Parameters.Add(mParm7);


                        SqlParameter mParm9 = new SqlParameter("@OTHERS", SqlDbType.Char);
                        mParm9.Direction = ParameterDirection.Input;
                        mParm9.Value = clsBiological.OTHERS;
                        pCmd.Parameters.Add(mParm9);


                        SqlParameter mParm10 = new SqlParameter("@OTHERS_DETAILS", SqlDbType.NVarChar);
                        mParm10.Direction = ParameterDirection.Input;
                        mParm10.Value = clsBiological.OTHERS_DETAILS;
                        pCmd.Parameters.Add(mParm10);


                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsBiological.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsBiological.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_working_history_chemical_health_hazard(clsWorkingHistoryChemicalHealthHazard clsChemical)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_WORKING_HISTORY_CHEMICAL_HEALTH_HAZARD";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsChemical.HN;
                        pCmd.Parameters.Add(mParm1);

                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsChemical.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);


                        SqlParameter mParm2 = new SqlParameter("@NO_CHEMICAL_HEALTH_HAZARD", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsChemical.NO_CHEMICAL_HEALTH_HAZARD;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@ORGANIC", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsChemical.ORGANIC;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@GAS", SqlDbType.Char);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsChemical.GAS;
                        pCmd.Parameters.Add(mParm4);

                        SqlParameter mParm5 = new SqlParameter("@HEAVY_METAL", SqlDbType.Char);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsChemical.HEAVY_METAL;
                        pCmd.Parameters.Add(mParm5);

                        SqlParameter mParm6 = new SqlParameter("@ACID", SqlDbType.Char);
                        mParm6.Direction = ParameterDirection.Input;
                        mParm6.Value = clsChemical.ACID;
                        pCmd.Parameters.Add(mParm6);

                        SqlParameter mParm7 = new SqlParameter("@METAL_FUME", SqlDbType.Char);
                        mParm7.Direction = ParameterDirection.Input;
                        mParm7.Value = clsChemical.METAL_FUME;
                        pCmd.Parameters.Add(mParm7);

                        SqlParameter mParm8 = new SqlParameter("@HERBICIDE", SqlDbType.Char);
                        mParm8.Direction = ParameterDirection.Input;
                        mParm8.Value = clsChemical.HERBICIDE;
                        pCmd.Parameters.Add(mParm8);

                        SqlParameter mParm9 = new SqlParameter("@DUST", SqlDbType.Char);
                        mParm9.Direction = ParameterDirection.Input;
                        mParm9.Value = clsChemical.DUST;
                        pCmd.Parameters.Add(mParm9);

                        SqlParameter mParm10 = new SqlParameter("@METAL_POWDERS", SqlDbType.Char);
                        mParm10.Direction = ParameterDirection.Input;
                        mParm10.Value = clsChemical.METAL_POWDERS;
                        pCmd.Parameters.Add(mParm10);


                        SqlParameter mParm15 = new SqlParameter("@OTHERS", SqlDbType.Char);
                        mParm15.Direction = ParameterDirection.Input;
                        mParm15.Value = clsChemical.OTHERS;
                        pCmd.Parameters.Add(mParm15);


                        SqlParameter mParm16 = new SqlParameter("@OTHERS_DETAILS", SqlDbType.NVarChar);
                        mParm16.Direction = ParameterDirection.Input;
                        mParm16.Value = clsChemical.OTHERS_DETAILS;
                        pCmd.Parameters.Add(mParm16);


                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsChemical.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsChemical.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_working_history_phychological_health_hazard(clsWorkingHistoryPsychologicalHealthHazard clsPsycho)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_WORKING_HISTORY_PHYCHOLOGICAL_HEALTH_HAZARD";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsPsycho.HN;
                        pCmd.Parameters.Add(mParm1);


                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsPsycho.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@NO", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsPsycho.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@YES", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsPsycho.YES;
                        pCmd.Parameters.Add(mParm3);


                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsPsycho.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsPsycho.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_working_history_ergonomic_health_hazard(clsWorkingHistoryErgonomicHealthHazard clsErgonomic)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_WORKING_HISTORY_ERGONOMIC_HEALTH_HAZARD";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsErgonomic.HN;
                        pCmd.Parameters.Add(mParm1);


                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsErgonomic.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@NO_ERGONOMIC_HEALTH_HAZARD", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsErgonomic.NO_ERGONOMIC_HEALTH_HAZARD;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@POOR_POSTURE", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsErgonomic.POOR_POSTURE;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@INAPPROPRIATE", SqlDbType.Char);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsErgonomic.INAPPROPRIATE;
                        pCmd.Parameters.Add(mParm4);

                        SqlParameter mParm5 = new SqlParameter("@REPEATING", SqlDbType.Char);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsErgonomic.REPEATING;
                        pCmd.Parameters.Add(mParm5);


                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsErgonomic.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsErgonomic.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_working_history_ppe(clsWorkingHistoryPPE clsPPE)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_WORKING_HISTORY_PPE";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsPPE.HN;
                        pCmd.Parameters.Add(mParm1);

                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsPPE.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);



                        SqlParameter mParm2 = new SqlParameter("@NO", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsPPE.NO;
                        pCmd.Parameters.Add(mParm2);


                        SqlParameter mParm8 = new SqlParameter("@EARPLUG_EARMUFF", SqlDbType.Char);
                        mParm8.Direction = ParameterDirection.Input;
                        mParm8.Value = clsPPE.EARPLUG_EARMUFF;
                        pCmd.Parameters.Add(mParm8);

                        SqlParameter mParm3 = new SqlParameter("@SAFETY_GLASSES", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsPPE.SAFETY_GLASSES;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@HELMET", SqlDbType.Char);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsPPE.HELMET;
                        pCmd.Parameters.Add(mParm4);

                        SqlParameter mParm5 = new SqlParameter("@SAFETY_SHOES", SqlDbType.Char);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsPPE.SAFETY_SHOES;
                        pCmd.Parameters.Add(mParm5);

                        SqlParameter mParm6 = new SqlParameter("@GLOVES", SqlDbType.Char);
                        mParm6.Direction = ParameterDirection.Input;
                        mParm6.Value = clsPPE.GLOVES;
                        pCmd.Parameters.Add(mParm6);

                        SqlParameter mParm7 = new SqlParameter("@COVERALLS", SqlDbType.Char);
                        mParm7.Direction = ParameterDirection.Input;
                        mParm7.Value = clsPPE.COVERALLS;
                        pCmd.Parameters.Add(mParm7);

                        SqlParameter mParm15 = new SqlParameter("@OTHERS", SqlDbType.Char);
                        mParm15.Direction = ParameterDirection.Input;
                        mParm15.Value = clsPPE.OTHERS;
                        pCmd.Parameters.Add(mParm15);


                        SqlParameter mParm16 = new SqlParameter("@OTHERS_DETAILS", SqlDbType.NVarChar);
                        mParm16.Direction = ParameterDirection.Input;
                        mParm16.Value = clsPPE.OTHERS_DETAILS;
                        pCmd.Parameters.Add(mParm16);


                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsPPE.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsPPE.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_personal_iiiness_medication_regularly(clsPersonalIllnessMedicationRegularly clsMediRegulary)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PERSONAL_IIINESS_MEDICATION_REGULARLY";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsMediRegulary.HN;
                        pCmd.Parameters.Add(mParm1);

                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsMediRegulary.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);


                        SqlParameter mParm2 = new SqlParameter("@NO", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsMediRegulary.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@YES", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsMediRegulary.YES;
                        pCmd.Parameters.Add(mParm3);


                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsMediRegulary.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsMediRegulary.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_personal_iiiness_medication_you_are_taking(clsPersonalIllnessMedicationThatYouAreTaking clsTaking)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PERSONAL_IIINESS_MEDICATION_YOU_ARE_TAKING";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsTaking.HN;
                        pCmd.Parameters.Add(mParm1);


                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsTaking.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);


                        SqlParameter mParm2 = new SqlParameter("@HEART_DISEASE_MEDICATION", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsTaking.HEART_DISEASE_MEDICATION;
                        pCmd.Parameters.Add(mParm2);


                        SqlParameter mParm8 = new SqlParameter("@HIGH_BLOOD_PRESSURE_MEDICATION", SqlDbType.Char);
                        mParm8.Direction = ParameterDirection.Input;
                        mParm8.Value = clsTaking.HIGH_BLOOD_PRESSURE_MEDICATION;
                        pCmd.Parameters.Add(mParm8);

                        SqlParameter mParm3 = new SqlParameter("@HIGH_BLOOD_LIPIDS_MEDICATION", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsTaking.HIGH_BLOOD_LIPIDS_MEDICATION;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@BLOOD_THINNER", SqlDbType.Char);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsTaking.BLOOD_THINNER;
                        pCmd.Parameters.Add(mParm4);

                        SqlParameter mParm5 = new SqlParameter("@DIABETES", SqlDbType.Char);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsTaking.DIABETES;
                        pCmd.Parameters.Add(mParm5);



                        SqlParameter mParm15 = new SqlParameter("@OTHERS", SqlDbType.Char);
                        mParm15.Direction = ParameterDirection.Input;
                        mParm15.Value = clsTaking.OTHERS;
                        pCmd.Parameters.Add(mParm15);


                        SqlParameter mParm16 = new SqlParameter("@OTHERS_DETAILS", SqlDbType.NVarChar);
                        mParm16.Direction = ParameterDirection.Input;
                        mParm16.Value = clsTaking.OTHERS_DETAILS;
                        pCmd.Parameters.Add(mParm16);


                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsTaking.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsTaking.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_personal_iiiness_medication_medicine_or_food(clsPersonalIllnessMedicineOrFood clsMedFood)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PERSONAL_IIINESS_MEDICATION_MEDICINE_OR_FOOD";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsMedFood.HN;
                        pCmd.Parameters.Add(mParm1);

                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsMedFood.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@NO", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsMedFood.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@NOT_SURE", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsMedFood.YES;
                        pCmd.Parameters.Add(mParm3);


                        SqlParameter mParm15 = new SqlParameter("@OTHERS", SqlDbType.Char);
                        mParm15.Direction = ParameterDirection.Input;
                        mParm15.Value = clsMedFood.OTHERS;
                        pCmd.Parameters.Add(mParm15);


                        SqlParameter mParm16 = new SqlParameter("@OTHERS_DETAILS", SqlDbType.NVarChar);
                        mParm16.Direction = ParameterDirection.Input;
                        mParm16.Value = clsMedFood.OTHERS_DETAILS;
                        pCmd.Parameters.Add(mParm16);


                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsMedFood.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsMedFood.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_personal_iiiness_impairment(clsPersonalIllnessillnessImpairment clsImpairment)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PERSONAL_IIINESS_IMPAIRMENT";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsImpairment.HN;
                        pCmd.Parameters.Add(mParm1);

                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsImpairment.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@NO", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsImpairment.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@YES", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsImpairment.YES;
                        pCmd.Parameters.Add(mParm3);


                        SqlParameter mParm15 = new SqlParameter("@DETAILS_ONE", SqlDbType.Char);
                        mParm15.Direction = ParameterDirection.Input;
                        mParm15.Value = clsImpairment.DETAILS_ONE;
                        pCmd.Parameters.Add(mParm15);


                        SqlParameter mParm16 = new SqlParameter("@DETAILS_TWO", SqlDbType.NVarChar);
                        mParm16.Direction = ParameterDirection.Input;
                        mParm16.Value = clsImpairment.DETAILS_TWO;
                        pCmd.Parameters.Add(mParm16);

                        SqlParameter mParm4 = new SqlParameter("@YEAR_ONE", SqlDbType.Char);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsImpairment.YEAR_ONE;
                        pCmd.Parameters.Add(mParm4);


                        SqlParameter mParm5 = new SqlParameter("@YEAR_TWO", SqlDbType.NVarChar);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsImpairment.YEAR_TWO;
                        pCmd.Parameters.Add(mParm5);



                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsImpairment.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsImpairment.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_personal_iiiness_had_an_operation(clsPersonalIllnessHadAnOperation clsOperation)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PERSONAL_IIINESS_HAD_AN_OPERATION";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsOperation.HN;
                        pCmd.Parameters.Add(mParm1);

                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsOperation.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@NO", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsOperation.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@YES", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsOperation.YES;
                        pCmd.Parameters.Add(mParm3);



                        SqlParameter mParm5 = new SqlParameter("@YES_DETAILS", SqlDbType.NVarChar);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsOperation.OTHERS_DETAILS;
                        pCmd.Parameters.Add(mParm5);



                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsOperation.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsOperation.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_personal_iiiness_underlying_deceases(clsPersonalIllnessUnderlyingDeceases clsDecease)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PERSONAL_IIINESS_UNDERLYING_DECEASES";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsDecease.HN;
                        pCmd.Parameters.Add(mParm1);


                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsDecease.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@NO", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsDecease.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@YES", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsDecease.YES;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@SLE", SqlDbType.Char);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsDecease.SLE;
                        pCmd.Parameters.Add(mParm4);

                        SqlParameter mParm5 = new SqlParameter("@CANCER", SqlDbType.Char);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsDecease.CANCER;
                        pCmd.Parameters.Add(mParm5);

                        SqlParameter mParm6 = new SqlParameter("@DIABETES", SqlDbType.Char);
                        mParm6.Direction = ParameterDirection.Input;
                        mParm6.Value = clsDecease.DIABETES;
                        pCmd.Parameters.Add(mParm6);

                        SqlParameter mParm7 = new SqlParameter("@ASTHMA", SqlDbType.Char);
                        mParm7.Direction = ParameterDirection.Input;
                        mParm7.Value = clsDecease.ASTHMA;
                        pCmd.Parameters.Add(mParm7);


                        SqlParameter mParm8 = new SqlParameter("@PEPTIC_ULCER", SqlDbType.Char);
                        mParm8.Direction = ParameterDirection.Input;
                        mParm8.Value = clsDecease.PEPTIC_ULCER;
                        pCmd.Parameters.Add(mParm8);

                        SqlParameter mParm9 = new SqlParameter("@EPILEPSY", SqlDbType.Char);
                        mParm9.Direction = ParameterDirection.Input;
                        mParm9.Value = clsDecease.EPILEPSY;
                        pCmd.Parameters.Add(mParm9);

                        SqlParameter mParm10 = new SqlParameter("@HIGH_BLOOD_PRESSURE", SqlDbType.Char);
                        mParm10.Direction = ParameterDirection.Input;
                        mParm10.Value = clsDecease.HIGH_BLOOD_PRESSURE;
                        pCmd.Parameters.Add(mParm10);

                        SqlParameter mParm11 = new SqlParameter("@CHRONIC_OBSTRUCTIVE", SqlDbType.Char);
                        mParm11.Direction = ParameterDirection.Input;
                        mParm11.Value = clsDecease.CHRONIC_OBSTRUCTIVE;
                        pCmd.Parameters.Add(mParm11);

                        SqlParameter mParm12 = new SqlParameter("@ANEMIA", SqlDbType.Char);
                        mParm12.Direction = ParameterDirection.Input;
                        mParm12.Value = clsDecease.ANEMIA;
                        pCmd.Parameters.Add(mParm12);

                        SqlParameter mParm13 = new SqlParameter("@LUNG_EMPHYSEMA", SqlDbType.Char);
                        mParm13.Direction = ParameterDirection.Input;
                        mParm13.Value = clsDecease.LUNG_EMPHYSEMA;
                        pCmd.Parameters.Add(mParm13);

                        SqlParameter mParm14 = new SqlParameter("@CARDIOVASCULAR", SqlDbType.Char);
                        mParm14.Direction = ParameterDirection.Input;
                        mParm14.Value = clsDecease.CARDIOVASCULAR;
                        pCmd.Parameters.Add(mParm14);

                        SqlParameter mParm19 = new SqlParameter("@KIDNEY_DISEASE", SqlDbType.Char);
                        mParm19.Direction = ParameterDirection.Input;
                        mParm19.Value = clsDecease.KIDNEY_DISEASE;
                        pCmd.Parameters.Add(mParm19);


                        SqlParameter mParm20 = new SqlParameter("@HEPATITIS", SqlDbType.Char);
                        mParm20.Direction = ParameterDirection.Input;
                        mParm20.Value = clsDecease.HEPATITIS;
                        pCmd.Parameters.Add(mParm20);

                        SqlParameter mParm21 = new SqlParameter("@HIGH_BLOOD_LIPIDS", SqlDbType.Char);
                        mParm21.Direction = ParameterDirection.Input;
                        mParm21.Value = clsDecease.HIGH_BLOOD_LIPIDS;
                        pCmd.Parameters.Add(mParm21);

                        SqlParameter mParm15 = new SqlParameter("@OTHERS", SqlDbType.Char);
                        mParm15.Direction = ParameterDirection.Input;
                        mParm15.Value = clsDecease.OTHERS;
                        pCmd.Parameters.Add(mParm15);

                        SqlParameter mParm16 = new SqlParameter("@OTHERS_DETAILS", SqlDbType.NVarChar);
                        mParm16.Direction = ParameterDirection.Input;
                        mParm16.Value = clsDecease.OTHERS_DETAILS;
                        pCmd.Parameters.Add(mParm16);



                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsDecease.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsDecease.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_personal_iiiness_vaccination_or_immunity(clsPersonalIllnessVaccinationOrImmunity clsImmunity)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PERSONAL_IIINESS_VACCINATION_OR_IMMUNITY";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsImmunity.HN;
                        pCmd.Parameters.Add(mParm1);


                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsImmunity.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@NO", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsImmunity.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@YES", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsImmunity.YES;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@JE", SqlDbType.Char);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsImmunity.JE;
                        pCmd.Parameters.Add(mParm4);

                        SqlParameter mParm5 = new SqlParameter("@CHICKENPOX", SqlDbType.Char);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsImmunity.CHICKENPOX;
                        pCmd.Parameters.Add(mParm5);

                        SqlParameter mParm6 = new SqlParameter("@INFLUENZA", SqlDbType.Char);
                        mParm6.Direction = ParameterDirection.Input;
                        mParm6.Value = clsImmunity.INFLUENZA;
                        pCmd.Parameters.Add(mParm6);

                        SqlParameter mParm7 = new SqlParameter("@HEPATITIS_A", SqlDbType.Char);
                        mParm7.Direction = ParameterDirection.Input;
                        mParm7.Value = clsImmunity.HEPATITIS_A;
                        pCmd.Parameters.Add(mParm7);


                        SqlParameter mParm8 = new SqlParameter("@YELLOW_FEVER", SqlDbType.Char);
                        mParm8.Direction = ParameterDirection.Input;
                        mParm8.Value = clsImmunity.YELLOW_FEVER;
                        pCmd.Parameters.Add(mParm8);

                        SqlParameter mParm9 = new SqlParameter("@MENING", SqlDbType.Char);
                        mParm9.Direction = ParameterDirection.Input;
                        mParm9.Value = clsImmunity.MENING;
                        pCmd.Parameters.Add(mParm9);

                        SqlParameter mParm10 = new SqlParameter("@HEPATITIS_B", SqlDbType.Char);
                        mParm10.Direction = ParameterDirection.Input;
                        mParm10.Value = clsImmunity.HEPATITIS_B;
                        pCmd.Parameters.Add(mParm10);

                        SqlParameter mParm11 = new SqlParameter("@TETANUS", SqlDbType.Char);
                        mParm11.Direction = ParameterDirection.Input;
                        mParm11.Value = clsImmunity.TETANUS;
                        pCmd.Parameters.Add(mParm11);

                        SqlParameter mParm12 = new SqlParameter("@TYPHOID", SqlDbType.Char);
                        mParm12.Direction = ParameterDirection.Input;
                        mParm12.Value = clsImmunity.TYPHOID;
                        pCmd.Parameters.Add(mParm12);


                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsImmunity.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsImmunity.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_personal_iiiness_do_you_smoke(clsPersonalIllnessDoYouSmoke clsDoyouSmoke)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PERSONAL_IIINESS_DO_YOU_SMOKE";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsDoyouSmoke.HN;
                        pCmd.Parameters.Add(mParm1);



                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsDoyouSmoke.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);


                        SqlParameter mParm2 = new SqlParameter("@NO", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsDoyouSmoke.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@YES", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsDoyouSmoke.YES;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@YES_BUT", SqlDbType.NVarChar);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsDoyouSmoke.OTHERS;
                        pCmd.Parameters.Add(mParm4);

                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsDoyouSmoke.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsDoyouSmoke.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_personal_iiiness_smoke_before_quitting(clsPersonalIllnessDoYouSmoke clsSmoke_before_quitting)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PERSONAL_IIINESS_SMOKE_BEFORE_QUITTING";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsSmoke_before_quitting.HN;
                        pCmd.Parameters.Add(mParm1);


                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsSmoke_before_quitting.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);


                        SqlParameter mParm2 = new SqlParameter("@FIVE_YEAR", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsSmoke_before_quitting.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@SIX_YEAR", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsSmoke_before_quitting.YES;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@OVER_TEN_YEAR", SqlDbType.NVarChar);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsSmoke_before_quitting.OTHERS;
                        pCmd.Parameters.Add(mParm4);

                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsSmoke_before_quitting.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsSmoke_before_quitting.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_personal_iiiness_how_many_smoke_before_quitting(clsPersonalIllnessDoYouSmoke clsMany_cigarettes)
        {

            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PERSONAL_IIINESS_HOW_MANY_SMOKE_BEFORE_QUITTING";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsMany_cigarettes.HN;
                        pCmd.Parameters.Add(mParm1);


                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsMany_cigarettes.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);


                        SqlParameter mParm2 = new SqlParameter("@FIVE_ROLLS", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsMany_cigarettes.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@TEN_ROWS", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsMany_cigarettes.YES;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@OVER_TEN_ROLLS", SqlDbType.NVarChar);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsMany_cigarettes.OTHERS;
                        pCmd.Parameters.Add(mParm4);

                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsMany_cigarettes.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsMany_cigarettes.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_personal_iiiness_how_long_have_you_been_smoking(clsPersonalIllnessDoYouSmoke clsHow_long_have_you_been_smoking)
        {

            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PERSONAL_IIINESS_HOW_LONG_HAVE_YOU_BEEN_SMOKING";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsHow_long_have_you_been_smoking.HN;
                        pCmd.Parameters.Add(mParm1);


                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsHow_long_have_you_been_smoking.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@FIVE_YEAR", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsHow_long_have_you_been_smoking.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@SIX_YEAR", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsHow_long_have_you_been_smoking.YES;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@OVER_TEN_YEAR", SqlDbType.NVarChar);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsHow_long_have_you_been_smoking.OTHERS;
                        pCmd.Parameters.Add(mParm4);

                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsHow_long_have_you_been_smoking.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsHow_long_have_you_been_smoking.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_personal_iiiness_how_many_cigarettes_do_you_smoke_in_a_day(clsPersonalIllnessDoYouSmoke clsHow_many_cigarettes_do_you_smoke_in_a_day)
        {

            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PERSONAL_IIINESS_HOW_MANY_CIGARETTES_DO_YOU_SMOKE_IN_A_DAY";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsHow_many_cigarettes_do_you_smoke_in_a_day.HN;
                        pCmd.Parameters.Add(mParm1);

                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsHow_many_cigarettes_do_you_smoke_in_a_day.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@FIVE_ROLLS", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsHow_many_cigarettes_do_you_smoke_in_a_day.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@TEN_ROWS", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsHow_many_cigarettes_do_you_smoke_in_a_day.YES;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@OVER_TEN_ROLLS", SqlDbType.NVarChar);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsHow_many_cigarettes_do_you_smoke_in_a_day.OTHERS;
                        pCmd.Parameters.Add(mParm4);

                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsHow_many_cigarettes_do_you_smoke_in_a_day.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsHow_many_cigarettes_do_you_smoke_in_a_day.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_personal_iiiness_have_you_ever_thinking_about_quit_smoking(clsPersonalIllnessDoYouSmoke clsHave_you_ever_thinking_about_quit_smoking)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PERSONAL_IIINESS_HAVE_YOU_EVER_THINKING_ABOUT_QUIT_SMOKING";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsHave_you_ever_thinking_about_quit_smoking.HN;
                        pCmd.Parameters.Add(mParm1);


                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsHave_you_ever_thinking_about_quit_smoking.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);


                        SqlParameter mParm2 = new SqlParameter("@NO", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsHave_you_ever_thinking_about_quit_smoking.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@YES", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsHave_you_ever_thinking_about_quit_smoking.YES;
                        pCmd.Parameters.Add(mParm3);


                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsHave_you_ever_thinking_about_quit_smoking.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsHave_you_ever_thinking_about_quit_smoking.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_personal_iiiness_have_you_ever_consumed_alcohol(clsPersonalIllnessDoYouSmoke clsHave_you_ever_consumed_alcohol)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PERSONAL_IIINESS_HAVE_YOU_EVER_CONSUMED_ALCOHOL";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsHave_you_ever_consumed_alcohol.HN;
                        pCmd.Parameters.Add(mParm1);


                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsHave_you_ever_consumed_alcohol.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@NO", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsHave_you_ever_consumed_alcohol.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@YES", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsHave_you_ever_consumed_alcohol.YES;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@YES_BUT", SqlDbType.Char);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsHave_you_ever_consumed_alcohol.OTHERS;
                        pCmd.Parameters.Add(mParm4);


                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsHave_you_ever_consumed_alcohol.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsHave_you_ever_consumed_alcohol.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_personal_iiiness_how_long_did_you_drink_alcohol_before_stop_drinking(clsPersonalIllnessDoYouSmoke clsHow_long_did_you_drink_alcohol_before_stop_drinking)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PERSONAL_IIINESS_HOW_LONG_DID_YOU_DRINK_ALCOHOL_BEFORE_STOP_DRINKING";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsHow_long_did_you_drink_alcohol_before_stop_drinking.HN;
                        pCmd.Parameters.Add(mParm1);


                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsHow_long_did_you_drink_alcohol_before_stop_drinking.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);


                        SqlParameter mParm2 = new SqlParameter("@FIVE_YEAR", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsHow_long_did_you_drink_alcohol_before_stop_drinking.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@SIX_YEAR", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsHow_long_did_you_drink_alcohol_before_stop_drinking.YES;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@OVER_TEN_YEAR", SqlDbType.Char);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsHow_long_did_you_drink_alcohol_before_stop_drinking.OTHERS;
                        pCmd.Parameters.Add(mParm4);


                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsHow_long_did_you_drink_alcohol_before_stop_drinking.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsHow_long_did_you_drink_alcohol_before_stop_drinking.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_personal_iiiness_how_often_did_you_drink_before_you_stopped(clsPersonalIllnessDoYouSmoke clsHow_often_did_you_drink_before_you_stopped)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PERSONAL_IIINESS_HOW_OFTEN_DID_YOU_DRINK_BEFORE_YOU_STOPPED";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsHow_often_did_you_drink_before_you_stopped.HN;
                        pCmd.Parameters.Add(mParm1);


                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsHow_often_did_you_drink_before_you_stopped.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@ONE_TIME", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsHow_often_did_you_drink_before_you_stopped.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@TWO_TIME", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsHow_often_did_you_drink_before_you_stopped.YES;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@THREE_TIME", SqlDbType.Char);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsHow_often_did_you_drink_before_you_stopped.OTHERS;
                        pCmd.Parameters.Add(mParm4);


                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsHow_often_did_you_drink_before_you_stopped.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsHow_often_did_you_drink_before_you_stopped.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_personal_iiiness_how_often_do_you_consume_alcohol(clsPersonalIllnessDoYouSmoke clsHow_often_do_you_consume_alcohol)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PERSONAL_IIINESS_HOW_OFTEN_DO_YOU_CONSUME_ALCOHOL";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsHow_often_do_you_consume_alcohol.HN;
                        pCmd.Parameters.Add(mParm1);


                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsHow_often_do_you_consume_alcohol.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@LESS_THAN_ONE_TIME", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsHow_often_do_you_consume_alcohol.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@ONE_TIME", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsHow_often_do_you_consume_alcohol.YES;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@TWO_TIME", SqlDbType.Char);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsHow_often_do_you_consume_alcohol.OTHERS;
                        pCmd.Parameters.Add(mParm4);

                        SqlParameter mParm5 = new SqlParameter("@MORE_THAN_THREE_TIME", SqlDbType.Char);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsHow_often_do_you_consume_alcohol.JE;
                        pCmd.Parameters.Add(mParm5);

                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsHow_often_do_you_consume_alcohol.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsHow_often_do_you_consume_alcohol.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_personal_iiiness_have_you_ever_think_about_stop_drinking(clsPersonalIllnessDoYouSmoke clsHave_you_ever_think_about_stop_drinking)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PERSONAL_IIINESS_HAVE_YOU_EVER_THINK_ABOUT_STOP_DRINKING";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsHave_you_ever_think_about_stop_drinking.HN;
                        pCmd.Parameters.Add(mParm1);



                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsHave_you_ever_think_about_stop_drinking.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@NO", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsHave_you_ever_think_about_stop_drinking.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@YES", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsHave_you_ever_think_about_stop_drinking.YES;
                        pCmd.Parameters.Add(mParm3);


                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsHave_you_ever_think_about_stop_drinking.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsHave_you_ever_think_about_stop_drinking.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_personal_iiiness_have_you_use_or_tried_any_drugs(clsPersonalIllnessDoYouSmoke clsHave_you_use_or_tried_any_drugs)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PERSONAL_IIINESS_HAVE_YOU_USE_OR_TRIED_ANY_DRUGS";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsHave_you_use_or_tried_any_drugs.HN;
                        pCmd.Parameters.Add(mParm1);



                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsHave_you_use_or_tried_any_drugs.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@NO", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsHave_you_use_or_tried_any_drugs.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@YES", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsHave_you_use_or_tried_any_drugs.YES;
                        pCmd.Parameters.Add(mParm3);


                        SqlParameter mParm5 = new SqlParameter("@YES_BUT", SqlDbType.Char);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsHave_you_use_or_tried_any_drugs.OTHERS;
                        pCmd.Parameters.Add(mParm5);

                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsHave_you_use_or_tried_any_drugs.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsHave_you_use_or_tried_any_drugs.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_personal_iiiness_what_type_of_drugs_did_you_used(clsPersonalIllnessDoYouSmoke clsWhat_type_of_drugs_did_you_used)
        {

            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PERSONAL_IIINESS_WHAT_TYPE_OF_DRUGS_DID_YOU_USED";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsWhat_type_of_drugs_did_you_used.HN;
                        pCmd.Parameters.Add(mParm1);


                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsWhat_type_of_drugs_did_you_used.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@MARIJUANA", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsWhat_type_of_drugs_did_you_used.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@AMPHETAMINE", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsWhat_type_of_drugs_did_you_used.YES;
                        pCmd.Parameters.Add(mParm3);


                        SqlParameter mParm5 = new SqlParameter("@VOLATILE", SqlDbType.Char);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsWhat_type_of_drugs_did_you_used.JE;
                        pCmd.Parameters.Add(mParm5);

                        SqlParameter mParm6 = new SqlParameter("@OTHERS", SqlDbType.Char);
                        mParm6.Direction = ParameterDirection.Input;
                        mParm6.Value = clsWhat_type_of_drugs_did_you_used.OTHERS;
                        pCmd.Parameters.Add(mParm6);


                        SqlParameter mParm7 = new SqlParameter("@OTHERS_DETAILS", SqlDbType.Char);
                        mParm7.Direction = ParameterDirection.Input;
                        mParm7.Value = clsWhat_type_of_drugs_did_you_used.OTHERS_DETAILS;
                        pCmd.Parameters.Add(mParm7);


                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsWhat_type_of_drugs_did_you_used.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsWhat_type_of_drugs_did_you_used.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_personal_family_iiiness(clsFamilyIllness clsFamily)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_PERSONAL_FAMILY_IIINESS";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsFamily.HN;
                        pCmd.Parameters.Add(mParm1);



                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsFamily.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@FAMILY_ID", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsFamily.FAMILY_ID;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@NONE", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsFamily.NONE;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@ANEMIA", SqlDbType.Char);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsFamily.ANEMIA_FAMILY;
                        pCmd.Parameters.Add(mParm4);


                        SqlParameter mParm5 = new SqlParameter("@CANCER", SqlDbType.Char);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsFamily.CANCER_FAMILY;
                        pCmd.Parameters.Add(mParm5);

                        SqlParameter mParm6 = new SqlParameter("@DIABETES", SqlDbType.Char);
                        mParm6.Direction = ParameterDirection.Input;
                        mParm6.Value = clsFamily.DIABETES_FAMILY;
                        pCmd.Parameters.Add(mParm6);

                        SqlParameter mParm7 = new SqlParameter("@ASTHMA", SqlDbType.Char);
                        mParm7.Direction = ParameterDirection.Input;
                        mParm7.Value = clsFamily.ASTHMA_FAMILY;
                        pCmd.Parameters.Add(mParm7);

                        SqlParameter mParm8 = new SqlParameter("@HIGH_BLOOD_PRESSURE", SqlDbType.Char);
                        mParm8.Direction = ParameterDirection.Input;
                        mParm8.Value = clsFamily.HIGH_BLOOD_PRESSURE_FAMILY;
                        pCmd.Parameters.Add(mParm8);


                        SqlParameter mParm9 = new SqlParameter("@ALLERGY", SqlDbType.Char);
                        mParm9.Direction = ParameterDirection.Input;
                        mParm9.Value = clsFamily.ALLERGY_FAMILY;
                        pCmd.Parameters.Add(mParm9);

                        SqlParameter mParm10 = new SqlParameter("@CARDIOVASCULAR", SqlDbType.Char);
                        mParm10.Direction = ParameterDirection.Input;
                        mParm10.Value = clsFamily.CARDIOVASCULAR_FAMILY;
                        pCmd.Parameters.Add(mParm10);

                        SqlParameter mParm11 = new SqlParameter("@TUBERCULOSIS", SqlDbType.Char);
                        mParm11.Direction = ParameterDirection.Input;
                        mParm11.Value = clsFamily.TUBERCULOSIS_FAMILY;
                        pCmd.Parameters.Add(mParm11);

                        SqlParameter mParm12 = new SqlParameter("@OTHERS", SqlDbType.Char);
                        mParm12.Direction = ParameterDirection.Input;
                        mParm12.Value = clsFamily.OTHERS;
                        pCmd.Parameters.Add(mParm12);


                        SqlParameter mParm13 = new SqlParameter("@OTHERS_DETAILS", SqlDbType.Char);
                        mParm13.Direction = ParameterDirection.Input;
                        mParm13.Value = clsFamily.OTHERS_DETAILS;
                        pCmd.Parameters.Add(mParm13);


                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsFamily.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsFamily.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_other_health_issues_favorite_food(clsOtherHealthIssuesfavoritefood clsFavoriteFood)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_OTHER_HEALTH_ISSUES_FAVORITE_FOOD";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsFavoriteFood.HN;
                        pCmd.Parameters.Add(mParm1);



                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsFavoriteFood.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@RICE", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsFavoriteFood.RICE;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@DEEP_FRIED_FOOD", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsFavoriteFood.DEEP_FRIED_FOOD;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@FAST_FOOD", SqlDbType.Char);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsFavoriteFood.FAST_FOOD;
                        pCmd.Parameters.Add(mParm4);


                        SqlParameter mParm5 = new SqlParameter("@INSTANT_NOODLE", SqlDbType.Char);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsFavoriteFood.INSTANT_NOODLE;
                        pCmd.Parameters.Add(mParm5);

                        SqlParameter mParm6 = new SqlParameter("@VEGETABLE", SqlDbType.Char);
                        mParm6.Direction = ParameterDirection.Input;
                        mParm6.Value = clsFavoriteFood.VEGETABLE;
                        pCmd.Parameters.Add(mParm6);

                        SqlParameter mParm7 = new SqlParameter("@SNACK", SqlDbType.Char);
                        mParm7.Direction = ParameterDirection.Input;
                        mParm7.Value = clsFavoriteFood.SNACK;
                        pCmd.Parameters.Add(mParm7);

                        SqlParameter mParm8 = new SqlParameter("@FISH", SqlDbType.Char);
                        mParm8.Direction = ParameterDirection.Input;
                        mParm8.Value = clsFavoriteFood.FISH;
                        pCmd.Parameters.Add(mParm8);


                        SqlParameter mParm12 = new SqlParameter("@OTHERS", SqlDbType.Char);
                        mParm12.Direction = ParameterDirection.Input;
                        mParm12.Value = clsFavoriteFood.OTHERS;
                        pCmd.Parameters.Add(mParm12);


                        SqlParameter mParm13 = new SqlParameter("@OTHERS_DETAILS", SqlDbType.Char);
                        mParm13.Direction = ParameterDirection.Input;
                        mParm13.Value = clsFavoriteFood.OTHERS_DETAILS;
                        pCmd.Parameters.Add(mParm13);


                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsFavoriteFood.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsFavoriteFood.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_other_health_issues_do_you_exercise(clsOtherHealthIssuesExerciseSport clsSport)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_OTHER_HEALTH_ISSUES_DO_YOU_EXERCISE";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsSport.HN;
                        pCmd.Parameters.Add(mParm1);

                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsSport.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@NO", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsSport.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@LESS_THAN_THREE_TIME", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsSport.YES;
                        pCmd.Parameters.Add(mParm3);

                        SqlParameter mParm4 = new SqlParameter("@MORE_THAN_THREE_TIME", SqlDbType.Char);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsSport.OTHERS;
                        pCmd.Parameters.Add(mParm4);



                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsSport.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsSport.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_other_health_issues_do_you_exercise_duration(clsOtherHealthIssuesExerciseSport clsSportDuration)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_OTHER_HEALTH_ISSUES_DO_YOU_EXERCISE_DURATION";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsSportDuration.HN;
                        pCmd.Parameters.Add(mParm1);

                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsSportDuration.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@LESS_THAN_30_MINUTS", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsSportDuration.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@MORE_THAN_30_MINUTS", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsSportDuration.YES;
                        pCmd.Parameters.Add(mParm3);



                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsSportDuration.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsSportDuration.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_other_health_issues_do_you_have_menstrual_periods_at_present(clsOtherHealthMenstrualPeriodsAtPresent clsMen)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_OTHER_HEALTH_ISSUES_DO_YOU_HAVE_MENSTRUAL_PERIODS_AT_PRESENT";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsMen.HN;
                        pCmd.Parameters.Add(mParm1);

                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsMen.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@MENOPAUSE", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsMen.MENOPAUSE;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@YES", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsMen.YES;
                        pCmd.Parameters.Add(mParm3);



                        SqlParameter mParm4 = new SqlParameter("@DATE_FORM", SqlDbType.NVarChar);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsMen.DATE_FORM;
                        pCmd.Parameters.Add(mParm4);


                        SqlParameter mParm5 = new SqlParameter("@DATE_TO", SqlDbType.NVarChar);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsMen.DATE_TO;
                        pCmd.Parameters.Add(mParm5);


                        SqlParameter mParm6 = new SqlParameter("@NORMAL", SqlDbType.Char);
                        mParm6.Direction = ParameterDirection.Input;
                        mParm6.Value = clsMen.NORMAL;
                        pCmd.Parameters.Add(mParm6);

                        SqlParameter mParm7 = new SqlParameter("@ABNORMAL", SqlDbType.Char);
                        mParm7.Direction = ParameterDirection.Input;
                        mParm7.Value = clsMen.ABNORMAL;
                        pCmd.Parameters.Add(mParm7);


                        SqlParameter mParm8 = new SqlParameter("@PRE_NO", SqlDbType.Char);
                        mParm8.Direction = ParameterDirection.Input;
                        mParm8.Value = clsMen.PRE_NO;
                        pCmd.Parameters.Add(mParm8);


                        SqlParameter mParm9 = new SqlParameter("@PRE_PREGNANCY", SqlDbType.Char);
                        mParm9.Direction = ParameterDirection.Input;
                        mParm9.Value = clsMen.PRE_PREGNANCY;
                        pCmd.Parameters.Add(mParm9);


                        SqlParameter mParm10 = new SqlParameter("@PRE_SUSPECTED", SqlDbType.Char);
                        mParm10.Direction = ParameterDirection.Input;
                        mParm10.Value = clsMen.PRE_SUSPECTED;
                        pCmd.Parameters.Add(mParm10);


                        SqlParameter mParm11 = new SqlParameter("@HAS_YES", SqlDbType.Char);
                        mParm11.Direction = ParameterDirection.Input;
                        mParm11.Value = clsMen.HAS_YES;
                        pCmd.Parameters.Add(mParm11);

                        SqlParameter mParm12 = new SqlParameter("@HAS_NO", SqlDbType.Char);
                        mParm12.Direction = ParameterDirection.Input;
                        mParm12.Value = clsMen.HAS_NO;
                        pCmd.Parameters.Add(mParm12);

                        SqlParameter mParm13 = new SqlParameter("@HAS_NOT_SURE", SqlDbType.Char);
                        mParm13.Direction = ParameterDirection.Input;
                        mParm13.Value = clsMen.HAS_NOT_SURE;
                        pCmd.Parameters.Add(mParm13);



                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsMen.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsMen.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }

        internal void insert_trn_other_health_issues_do_you_want_to_declare_personal(clsOtherHealthIssuesExerciseSport clsDeclarePersonal)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "SP_INSERT_TRN_OTHER_HEALTH_ISSUES_DO_YOU_WANT_TO_DECLARE_PERSONAL";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@HN", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsDeclarePersonal.HN;
                        pCmd.Parameters.Add(mParm1);

                        SqlParameter mParm1_1 = new SqlParameter("@KEY_GEN", SqlDbType.VarChar);
                        mParm1_1.Direction = ParameterDirection.Input;
                        mParm1_1.Value = clsDeclarePersonal.KEY_GEN;
                        pCmd.Parameters.Add(mParm1_1);

                        SqlParameter mParm2 = new SqlParameter("@NO", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsDeclarePersonal.NO;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@YES", SqlDbType.Char);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsDeclarePersonal.YES;
                        pCmd.Parameters.Add(mParm3);



                        SqlParameter mParm4 = new SqlParameter("@DDMMYY", SqlDbType.NVarChar);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsDeclarePersonal.DDMMYY;
                        pCmd.Parameters.Add(mParm4);


                        SqlParameter mParm5 = new SqlParameter("@INJURY_OR_ILLNESS", SqlDbType.NVarChar);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsDeclarePersonal.INJURY_OR_ILLNESS;
                        pCmd.Parameters.Add(mParm5);


                        SqlParameter mParm6 = new SqlParameter("@CAUSE_OF_INJURY", SqlDbType.Char);
                        mParm6.Direction = ParameterDirection.Input;
                        mParm6.Value = clsDeclarePersonal.CAUSE_OF_INJURY;
                        pCmd.Parameters.Add(mParm6);

                        SqlParameter mParm7 = new SqlParameter("@DISABLED", SqlDbType.Char);
                        mParm7.Direction = ParameterDirection.Input;
                        mParm7.Value = clsDeclarePersonal.DISABLED;
                        pCmd.Parameters.Add(mParm7);


                        SqlParameter mParm8 = new SqlParameter("@LOSS_OF_LIMBS", SqlDbType.Char);
                        mParm8.Direction = ParameterDirection.Input;
                        mParm8.Value = clsDeclarePersonal.LOSS_OF_LIMBS;
                        pCmd.Parameters.Add(mParm8);


                        SqlParameter mParm9 = new SqlParameter("@LESS_THAN_THREE", SqlDbType.Char);
                        mParm9.Direction = ParameterDirection.Input;
                        mParm9.Value = clsDeclarePersonal.LESS_THAN_THREE;
                        pCmd.Parameters.Add(mParm9);


                        SqlParameter mParm10 = new SqlParameter("@MORE_THAN_THREE", SqlDbType.Char);
                        mParm10.Direction = ParameterDirection.Input;
                        mParm10.Value = clsDeclarePersonal.MORE_THAN_THREE;
                        pCmd.Parameters.Add(mParm10);




                        SqlParameter mParm17 = new SqlParameter("@CREATE_BY", SqlDbType.NVarChar);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsDeclarePersonal.CREATE_BY;
                        pCmd.Parameters.Add(mParm17);


                        SqlParameter mParm18 = new SqlParameter("@UPDATE_BY", SqlDbType.NVarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsDeclarePersonal.UPDATE_BY;
                        pCmd.Parameters.Add(mParm18);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
        }



        internal DataTable get_history_patients(string rowid_key)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                DataTable pDs = new DataTable();
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }
                pConn.Open();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.CommandText = "SP_GET_PATIENT";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();
                        pCmd.Parameters.AddWithValue("@HN", rowid_key);

                        SqlDataAdapter pDta = new SqlDataAdapter(pCmd);
                        pDta.Fill(pDs);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
                return pDs;
            }
        }

        internal DataTable get_history_questionaire(string KEY_GEN,string stName)
        {

            string stStoreNameResult = string.Empty;
            switch (stName)
            {
                case "GENERAL_INFORMATION":
                    stStoreNameResult = "SP_GET_GENERAL_INFORMATION";
                    break;
                case "HISTORY_CLASSIFICATION_OF_EMPLOYMENT":
                    stStoreNameResult = "SP_GET_WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT";
                    break;
                case "WORKING_HISTORY_TYPE_OF_WORK":
                    stStoreNameResult = "SP_GET_WORKING_HISTORY_TYPE_OF_WORK";
                    break;
                case "WORKING_HISTORY_SPECIAL_ASSIGNMENT":
                    stStoreNameResult = "SP_GET_WORKING_HISTORY_SPECIAL_ASSIGNMENT";
                    break;
                case "WORKING_HISTORY_PHYSICAL_HEALTH_HAZARD":
                    stStoreNameResult = "SP_GET_WORKING_HISTORY_PHYSICAL_HEALTH_HAZARD";
                    break;
                case "WORKING_HISTORY_BIOLOGICAL_HEALTH_HAZARD":
                    stStoreNameResult = "SP_GET_WORKING_HISTORY_BIOLOGICAL_HEALTH_HAZARD";
                    break;
                case "WORKING_HISTORY_CHEMICAL_HEALTH_HAZARD":
                    stStoreNameResult = "SP_GET_WORKING_HISTORY_CHEMICAL_HEALTH_HAZARD";
                    break;
                case "WORKING_HISTORY_PHYCHOLOGICAL_HEALTH_HAZARD":
                    stStoreNameResult = "SP_GET_WORKING_HISTORY_PHYCHOLOGICAL_HEALTH_HAZARD";
                    break;
                case "WORKING_HISTORY_ERGONOMIC_HEALTH_HAZARD":
                    stStoreNameResult = "SP_GET_WORKING_HISTORY_ERGONOMIC_HEALTH_HAZARD";
                    break;
                case "WORKING_HISTORY_PPE":
                    stStoreNameResult = "SP_GET_WORKING_HISTORY_PPE";
                    break;
                case "PERSONAL_FAMILY_IIINESS":
                    stStoreNameResult = "SP_GET_PERSONAL_FAMILY_IIINESS";
                    break;
                case "PERSONAL_IIINESS_MEDICATION_REGULARLY":
                    stStoreNameResult = "SP_GET_PERSONAL_IIINESS_MEDICATION_REGULARLY";
                    break;
                case "PERSONAL_IIINESS_MEDICATION_YOU_ARE_TAKING":
                    stStoreNameResult = "SP_GET_PERSONAL_IIINESS_MEDICATION_YOU_ARE_TAKING";
                    break;
                case "PERSONAL_IIINESS_MEDICATION_MEDICINE_OR_FOOD":
                    stStoreNameResult = "SP_GET_PERSONAL_IIINESS_MEDICATION_MEDICINE_OR_FOOD";
                    break;
                case "PERSONAL_IIINESS_IMPAIRMENT":
                    stStoreNameResult = "SP_GET_PERSONAL_IIINESS_IMPAIRMENT";
                    break;
                case "PERSONAL_IIINESS_HAD_AN_OPERATION":
                    stStoreNameResult = "SP_GET_PERSONAL_IIINESS_HAD_AN_OPERATION";
                    break;
                case "PERSONAL_IIINESS_UNDERLYING_DECEASES":
                    stStoreNameResult = "SP_GET_PERSONAL_IIINESS_UNDERLYING_DECEASES";
                    break;
                case "PERSONAL_IIINESS_VACCINATION_OR_IMMUNITY":
                    stStoreNameResult = "SP_GET_PERSONAL_IIINESS_VACCINATION_OR_IMMUNITY";
                    break;
                case "PERSONAL_IIINESS_DO_YOU_SMOKE":
                    stStoreNameResult = "SP_GET_PERSONAL_PERSONAL_IIINESS_DO_YOU_SMOKE";
                    break;
                case "PERSONAL_IIINESS_SMOKE_BEFORE_QUITTING":
                    stStoreNameResult = "SP_GET_PERSONAL_IIINESS_SMOKE_BEFORE_QUITTING";
                    break;
                case "PERSONAL_IIINESS_HOW_MANY_SMOKE_BEFORE_QUITTING":
                    stStoreNameResult = "SP_GET_PERSONAL_IIINESS_HOW_MANY_SMOKE_BEFORE_QUITTING";
                    break;
                case "PERSONAL_IIINESS_HOW_LONG_HAVE_YOU_BEEN_SMOKING":
                    stStoreNameResult = "SP_GET_PERSONAL_IIINESS_HOW_LONG_HAVE_YOU_BEEN_SMOKING";
                    break;
                case "PERSONAL_IIINESS_HOW_MANY_CIGARETTES_DO_YOU_SMOKE_IN_A_DAY":
                    stStoreNameResult = "SP_GET_PERSONAL_IIINESS_HOW_MANY_CIGARETTES_DO_YOU_SMOKE_IN_A_DAY";
                    break;
                case "PERSONAL_IIINESS_HAVE_YOU_EVER_THINKING_ABOUT_QUIT_SMOKING":
                    stStoreNameResult = "SP_GET_PERSONAL_IIINESS_HAVE_YOU_EVER_THINKING_ABOUT_QUIT_SMOKING";
                    break;
                case "PERSONAL_IIINESS_HAVE_YOU_EVER_CONSUMED_ALCOHOL":
                    stStoreNameResult = "SP_GET_PERSONAL_IIINESS_HAVE_YOU_EVER_CONSUMED_ALCOHOL";
                    break;
                case "PERSONAL_IIINESS_HOW_LONG_DID_YOU_DRINK_ALCOHOL_BEFORE_STOP_DRINKING":
                    stStoreNameResult = "SP_GET_PERSONAL_IIINESS_HOW_LONG_DID_YOU_DRINK_ALCOHOL_BEFORE_STOP_DRINKING";
                    break;
                case "PERSONAL_IIINESS_HOW_OFTEN_DID_YOU_DRINK_BEFORE_YOU_STOPPED":
                    stStoreNameResult = "SP_GET_PERSONAL_PERSONAL_IIINESS_HOW_OFTEN_DID_YOU_DRINK_BEFORE_YOU_STOPPED";
                    break;
                case "PERSONAL_IIINESS_HOW_OFTEN_DO_YOU_CONSUME_ALCOHOL":
                    stStoreNameResult = "SP_GET_PERSONAL_IIINESS_HOW_OFTEN_DO_YOU_CONSUME_ALCOHOL";
                    break;
                case "PERSONAL_IIINESS_HAVE_YOU_EVER_THINK_ABOUT_STOP_DRINKING":
                    stStoreNameResult = "SP_GET_PERSONAL_IIINESS_HAVE_YOU_EVER_THINK_ABOUT_STOP_DRINKING";
                    break;
                case "PERSONAL_IIINESS_HAVE_YOU_USE_OR_TRIED_ANY_DRUGS":
                    stStoreNameResult = "SP_GET_PERSONAL_IIINESS_HAVE_YOU_USE_OR_TRIED_ANY_DRUGS";
                    break;
                case "PERSONAL_IIINESS_WHAT_TYPE_OF_DRUGS_DID_YOU_USED":
                    stStoreNameResult = "SP_GET_PERSONAL_IIINESS_WHAT_TYPE_OF_DRUGS_DID_YOU_USED";
                    break;
                case "OTHER_HEALTH_ISSUES_FAVORITE_FOOD":
                    stStoreNameResult = "SP_GET_OTHER_HEALTH_ISSUES_FAVORITE_FOOD";
                    break;
                case "OTHER_HEALTH_ISSUES_DO_YOU_EXERCISE":
                    stStoreNameResult = "SP_GET_OTHER_HEALTH_ISSUES_DO_YOU_EXERCISE";
                    break;
                case "OTHER_HEALTH_ISSUES_DO_YOU_EXERCISE_DURATION":
                    stStoreNameResult = "SP_GET_OTHER_HEALTH_ISSUES_DO_YOU_EXERCISE_DURATION";
                    break;
                case "OTHER_HEALTH_ISSUES_DO_YOU_WANT_TO_DECLARE_PERSONAL":
                    stStoreNameResult = "SP_GET_OTHER_HEALTH_ISSUES_DO_YOU_WANT_TO_DECLARE_PERSONAL";
                    break;
                case "OTHER_HEALTH_ISSUES_DO_YOU_HAVE_MENSTRUAL_PERIODS_AT_PRESENT":
                    stStoreNameResult = "SP_GET_OTHER_HEALTH_ISSUES_DO_YOU_HAVE_MENSTRUAL_PERIODS_AT_PRESENT";
                    break;
                case "WORKING_HISTORY_ALL":
                    stStoreNameResult = "SP_GET_WORKING_HISTORY_ALL";
                    break;
                default: 
                    stStoreNameResult = string.Empty;
                    break;
            }
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                DataTable pDs = new DataTable();
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }
                pConn.Open();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.CommandText = stStoreNameResult;
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();
                        pCmd.Parameters.AddWithValue("@KEY_GEN", KEY_GEN);

                        SqlDataAdapter pDta = new SqlDataAdapter(pCmd);
                        pDta.Fill(pDs);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
                return pDs;
            }
        }

        internal DataTable get_history_classification_of_employment(string KEY_GEN)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                DataTable pDs = new DataTable();
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }
                pConn.Open();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.CommandText = "SP_GET_WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();
                        pCmd.Parameters.AddWithValue("@KEY_GEN", KEY_GEN);

                        SqlDataAdapter pDta = new SqlDataAdapter(pCmd);
                        pDta.Fill(pDs);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
                return pDs;
            }
        }

        public void insert_trn_questionaire_history(clsQuestionaireHealthHistory clsQHH)
        {
            using (SqlConnection pConn = new SqlConnection(_BHQ_ConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "insertQuestionairePatiant";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@p_hn_no", SqlDbType.NVarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsQHH.P_HN;
                        pCmd.Parameters.Add(mParm1);

                        SqlParameter mParm2 = new SqlParameter("@p_type", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsQHH.P_type;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@p_confirm_doctor", SqlDbType.NVarChar);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsQHH.P_confirm_doctor;
                        pCmd.Parameters.Add(mParm3);



                        SqlParameter mParm4 = new SqlParameter("@p_confirm_date", SqlDbType.NVarChar);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsQHH.P_confirm_date;
                        pCmd.Parameters.Add(mParm4);


                        SqlParameter mParm5 = new SqlParameter("@p_his_smok", SqlDbType.Char);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsQHH.P_his_smok;
                        pCmd.Parameters.Add(mParm5);


                        SqlParameter mParm6 = new SqlParameter("@p_his_nsmok_yrs", SqlDbType.Float);
                        mParm6.Direction = ParameterDirection.Input;
                        mParm6.Value = clsQHH.P_his_nsmok_yrs;
                        pCmd.Parameters.Add(mParm6);

                        SqlParameter mParm7 = new SqlParameter("@p_his_qsmok_yrs", SqlDbType.Float);
                        mParm7.Direction = ParameterDirection.Input;
                        mParm7.Value = clsQHH.P_his_qsmok_yrs;
                        pCmd.Parameters.Add(mParm7);


                        SqlParameter mParm8 = new SqlParameter("@p_his_smok_amt", SqlDbType.Float);
                        mParm8.Direction = ParameterDirection.Input;
                        mParm8.Value = clsQHH.P_his_smok_amt;
                        pCmd.Parameters.Add(mParm8);


                        SqlParameter mParm9 = new SqlParameter("@p_his_smok_dur", SqlDbType.Float);
                        mParm9.Direction = ParameterDirection.Input;
                        mParm9.Value = clsQHH.P_his_smok_dur;
                        pCmd.Parameters.Add(mParm9);


                        SqlParameter mParm10 = new SqlParameter("@p_his_smok_remark", SqlDbType.NVarChar);
                        mParm10.Direction = ParameterDirection.Input;
                        mParm10.Value = clsQHH.P_his_smok_remark;
                        pCmd.Parameters.Add(mParm10);



                        SqlParameter mParm11 = new SqlParameter("@p_his_alcohol", SqlDbType.Char);
                        mParm11.Direction = ParameterDirection.Input;
                        mParm11.Value = clsQHH.P_his_alcohol;
                        pCmd.Parameters.Add(mParm11);


                        SqlParameter mParm12 = new SqlParameter("@p_his_alco_yrs", SqlDbType.Float);
                        mParm12.Direction = ParameterDirection.Input;
                        mParm12.Value = clsQHH.P_his_alco_yrs;
                        pCmd.Parameters.Add(mParm12);



                        SqlParameter mParm13 = new SqlParameter("@p_his_alco_social", SqlDbType.Char);
                        mParm13.Direction = ParameterDirection.Input;
                        mParm13.Value = clsQHH.P_his_alco_social;
                        pCmd.Parameters.Add(mParm13);

                        SqlParameter mParm14 = new SqlParameter("@p_his_exercise", SqlDbType.Char);
                        mParm14.Direction = ParameterDirection.Input;
                        mParm14.Value = clsQHH.P_his_exercise;
                        pCmd.Parameters.Add(mParm14);

                        SqlParameter mParm15 = new SqlParameter("@p_ill_concern", SqlDbType.Char);
                        mParm15.Direction = ParameterDirection.Input;
                        mParm15.Value = clsQHH.P_ill_concern;
                        pCmd.Parameters.Add(mParm15);

                        SqlParameter mParm16 = new SqlParameter("@p_ill_conc_oth", SqlDbType.NVarChar);
                        mParm16.Direction = ParameterDirection.Input;
                        mParm16.Value = clsQHH.P_ill_conc_oth;
                        pCmd.Parameters.Add(mParm16);

                        SqlParameter mParm17 = new SqlParameter("@p_ill_chkup", SqlDbType.Char);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsQHH.P_ill_chkup;
                        pCmd.Parameters.Add(mParm17);

                        SqlParameter mParm18 = new SqlParameter("@p_ill_psycho", SqlDbType.Char);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsQHH.P_ill_psycho;
                        pCmd.Parameters.Add(mParm18);

                        SqlParameter mParm19 = new SqlParameter("@p_ill_psycho_oth", SqlDbType.NVarChar);
                        mParm19.Direction = ParameterDirection.Input;
                        mParm19.Value = clsQHH.P_ill_psycho_oth;
                        pCmd.Parameters.Add(mParm19);

                        SqlParameter mParm20 = new SqlParameter("@p_ill_med_his", SqlDbType.Char);
                        mParm20.Direction = ParameterDirection.Input;
                        mParm20.Value = clsQHH.P_ill_med_his;
                        pCmd.Parameters.Add(mParm20);

                        SqlParameter mParm21 = new SqlParameter("@p_ill_med_hyper", SqlDbType.Bit);
                        mParm21.Direction = ParameterDirection.Input;
                        mParm21.Value = clsQHH.P_ill_med_hyper;
                        pCmd.Parameters.Add(mParm21);

                        SqlParameter mParm22 = new SqlParameter("@p_ill_med_heart", SqlDbType.Bit);
                        mParm22.Direction = ParameterDirection.Input;
                        mParm22.Value = clsQHH.P_ill_med_heart;
                        pCmd.Parameters.Add(mParm22);

                        SqlParameter mParm23 = new SqlParameter("@p_ill_med_heart_txt", SqlDbType.NVarChar);
                        mParm23.Direction = ParameterDirection.Input;
                        mParm23.Value = clsQHH.P_ill_med_heart_txt;
                        pCmd.Parameters.Add(mParm23);

                        SqlParameter mParm24 = new SqlParameter("@p_ill_med_diab", SqlDbType.Bit);
                        mParm24.Direction = ParameterDirection.Input;
                        mParm24.Value = clsQHH.P_ill_med_diab;
                        pCmd.Parameters.Add(mParm24);

                        SqlParameter mParm25 = new SqlParameter("@p_ill_med_coro", SqlDbType.Bit);
                        mParm25.Direction = ParameterDirection.Input;
                        mParm25.Value = clsQHH.P_ill_med_coro;
                        pCmd.Parameters.Add(mParm25);

                        SqlParameter mParm26 = new SqlParameter("@p_ill_med_dysl", SqlDbType.Bit);
                        mParm26.Direction = ParameterDirection.Input;
                        mParm26.Value = clsQHH.P_ill_med_dysl;
                        pCmd.Parameters.Add(mParm26);


                        SqlParameter mParm27 = new SqlParameter("@p_ill_med_cper", SqlDbType.Bit);
                        mParm27.Direction = ParameterDirection.Input;
                        mParm27.Value = clsQHH.P_ill_med_cper;
                        pCmd.Parameters.Add(mParm27);


                        SqlParameter mParm28 = new SqlParameter("@p_ill_med_gout", SqlDbType.Bit);
                        mParm28.Direction = ParameterDirection.Input;
                        mParm28.Value = clsQHH.P_ill_med_gout;
                        pCmd.Parameters.Add(mParm28);


                        SqlParameter mParm29 = new SqlParameter("@p_ill_med_abdd", SqlDbType.Bit);
                        mParm29.Direction = ParameterDirection.Input;
                        mParm29.Value = clsQHH.P_ill_med_abdd;
                        pCmd.Parameters.Add(mParm29);


                        SqlParameter mParm30 = new SqlParameter("@p_ill_med_pulm", SqlDbType.Bit);
                        mParm30.Direction = ParameterDirection.Input;
                        mParm30.Value = clsQHH.P_ill_med_pulm;
                        pCmd.Parameters.Add(mParm30);

                        SqlParameter mParm31 = new SqlParameter("@p_ill_med_para", SqlDbType.Bit);
                        mParm31.Direction = ParameterDirection.Input;
                        mParm31.Value = clsQHH.P_ill_med_para;
                        pCmd.Parameters.Add(mParm31);

                        SqlParameter mParm32 = new SqlParameter("@p_ill_med_stro", SqlDbType.Bit);
                        mParm32.Direction = ParameterDirection.Input;
                        mParm32.Value = clsQHH.P_ill_med_stro;
                        pCmd.Parameters.Add(mParm32);

                        SqlParameter mParm33 = new SqlParameter("@p_ill_med_putb", SqlDbType.Bit);
                        mParm33.Direction = ParameterDirection.Input;
                        mParm33.Value = clsQHH.P_ill_med_putb;
                        pCmd.Parameters.Add(mParm33);

                        SqlParameter mParm34 = new SqlParameter("@p_ill_med_sist", SqlDbType.Bit);
                        mParm34.Direction = ParameterDirection.Input;
                        mParm34.Value = clsQHH.P_ill_med_sist;
                        pCmd.Parameters.Add(mParm34);

                        SqlParameter mParm35 = new SqlParameter("@p_ill_med_kidn", SqlDbType.Bit);
                        mParm35.Direction = ParameterDirection.Input;
                        mParm35.Value = clsQHH.P_ill_med_kidn;
                        pCmd.Parameters.Add(mParm35);


                        SqlParameter mParm36 = new SqlParameter("@p_ill_med_epil", SqlDbType.Bit);
                        mParm36.Direction = ParameterDirection.Input;
                        mParm36.Value = clsQHH.P_ill_med_epil;
                        pCmd.Parameters.Add(mParm36);


                        SqlParameter mParm37 = new SqlParameter("@p_ill_med_hepa", SqlDbType.Bit);
                        mParm37.Direction = ParameterDirection.Input;
                        mParm37.Value = clsQHH.P_ill_med_hepa;
                        pCmd.Parameters.Add(mParm37);


                        SqlParameter mParm38 = new SqlParameter("@p_ill_med_resp", SqlDbType.Bit);
                        mParm38.Direction = ParameterDirection.Input;
                        mParm38.Value = clsQHH.P_ill_med_resp;
                        pCmd.Parameters.Add(mParm38);


                        SqlParameter mParm39 = new SqlParameter("@p_ill_med_asth", SqlDbType.Bit);
                        mParm39.Direction = ParameterDirection.Input;
                        mParm39.Value = clsQHH.P_ill_med_asth;
                        pCmd.Parameters.Add(mParm39);

                        SqlParameter mParm40 = new SqlParameter("@p_ill_med_emph", SqlDbType.Bit);
                        mParm40.Direction = ParameterDirection.Input;
                        mParm40.Value = clsQHH.P_ill_med_emph;
                        pCmd.Parameters.Add(mParm40);

                        SqlParameter mParm41 = new SqlParameter("@p_ill_med_chro", SqlDbType.Bit);
                        mParm41.Direction = ParameterDirection.Input;
                        mParm41.Value = clsQHH.P_ill_med_chro;
                        pCmd.Parameters.Add(mParm41);


                        SqlParameter mParm42 = new SqlParameter("@p_ill_med_bron", SqlDbType.Bit);
                        mParm42.Direction = ParameterDirection.Input;
                        mParm42.Value = clsQHH.P_ill_med_bron;
                        pCmd.Parameters.Add(mParm42);


                        SqlParameter mParm43 = new SqlParameter("@p_ill_med_cough", SqlDbType.Bit);
                        mParm43.Direction = ParameterDirection.Input;
                        mParm43.Value = clsQHH.P_ill_med_cough;
                        pCmd.Parameters.Add(mParm43);



                        SqlParameter mParm44 = new SqlParameter("@p_ill_med_rhin", SqlDbType.Bit);
                        mParm44.Direction = ParameterDirection.Input;
                        mParm44.Value = clsQHH.P_ill_med_rhin;
                        pCmd.Parameters.Add(mParm44);


                        SqlParameter mParm45 = new SqlParameter("@p_ill_med_canc", SqlDbType.Bit);
                        mParm45.Direction = ParameterDirection.Input;
                        mParm45.Value = clsQHH.P_ill_med_canc;
                        pCmd.Parameters.Add(mParm45);

                        SqlParameter mParm46 = new SqlParameter("@p_ill_med_canc_oth", SqlDbType.NVarChar);
                        mParm46.Direction = ParameterDirection.Input;
                        mParm46.Value = clsQHH.P_ill_med_canc_oth;
                        pCmd.Parameters.Add(mParm46);


                        SqlParameter mParm47 = new SqlParameter("@p_ill_med_alle", SqlDbType.Bit);
                        mParm47.Direction = ParameterDirection.Input;
                        mParm47.Value = clsQHH.P_ill_med_alle;
                        pCmd.Parameters.Add(mParm47);



                        SqlParameter mParm48 = new SqlParameter("@p_ill_med_pept", SqlDbType.Bit);
                        mParm48.Direction = ParameterDirection.Input;
                        mParm48.Value = clsQHH.P_ill_med_pept;
                        pCmd.Parameters.Add(mParm48);


                        SqlParameter mParm49 = new SqlParameter("@p_ill_med_oth", SqlDbType.Bit);
                        mParm49.Direction = ParameterDirection.Input;
                        mParm49.Value = clsQHH.P_ill_med_oth;
                        pCmd.Parameters.Add(mParm49);


                        SqlParameter mParm50 = new SqlParameter("@p_ill_med_others", SqlDbType.NVarChar);
                        mParm50.Direction = ParameterDirection.Input;
                        mParm50.Value = clsQHH.P_ill_med_others;
                        pCmd.Parameters.Add(mParm50);

                        SqlParameter mParm51 = new SqlParameter("@p_ill_med_rmk", SqlDbType.Bit);
                        mParm51.Direction = ParameterDirection.Input;
                        mParm51.Value = clsQHH.P_ill_med_rmk;
                        pCmd.Parameters.Add(mParm51);

                        SqlParameter mParm51_1 = new SqlParameter("@p_ill_med_rmk_oth", SqlDbType.NVarChar);
                        mParm51_1.Direction = ParameterDirection.Input;
                        mParm51_1.Value = clsQHH.P_ill_med_rmk_oth;
                        pCmd.Parameters.Add(mParm51_1);


                        SqlParameter mParm52 = new SqlParameter("@p_fam_med_asth", SqlDbType.Bit);
                        mParm52.Direction = ParameterDirection.Input;
                        mParm52.Value = clsQHH.P_fam_med_asth;
                        pCmd.Parameters.Add(mParm52);


                        SqlParameter mParm53 = new SqlParameter("@p_fam_med_bron", SqlDbType.Bit);
                        mParm53.Direction = ParameterDirection.Input;
                        mParm53.Value = clsQHH.P_fam_med_bron;
                        pCmd.Parameters.Add(mParm53);

                        SqlParameter mParm54 = new SqlParameter("@p_fam_med_alle", SqlDbType.Bit);
                        mParm54.Direction = ParameterDirection.Input;
                        mParm54.Value = clsQHH.P_fam_med_alle;
                        pCmd.Parameters.Add(mParm54);


                        SqlParameter mParm55 = new SqlParameter("@p_fam_med_cough", SqlDbType.Bit);
                        mParm55.Direction = ParameterDirection.Input;
                        mParm55.Value = clsQHH.P_fam_med_cough;
                        pCmd.Parameters.Add(mParm55);

                        SqlParameter mParm56 = new SqlParameter("@p_fam_med_rhin", SqlDbType.Bit);
                        mParm56.Direction = ParameterDirection.Input;
                        mParm56.Value = clsQHH.P_fam_med_rhin;
                        pCmd.Parameters.Add(mParm56);

                        SqlParameter mParm57 = new SqlParameter("@p_fam_med_oth", SqlDbType.Bit);
                        mParm57.Direction = ParameterDirection.Input;
                        mParm57.Value = clsQHH.P_fam_med_oth;
                        pCmd.Parameters.Add(mParm57);

                        SqlParameter mParm58 = new SqlParameter("@p_envi_hme_dust", SqlDbType.Bit);
                        mParm58.Direction = ParameterDirection.Input;
                        mParm58.Value = clsQHH.P_envi_hme_dust;
                        pCmd.Parameters.Add(mParm58);

                        SqlParameter mParm59 = new SqlParameter("@p_envi_hme_smoke", SqlDbType.Bit);
                        mParm59.Direction = ParameterDirection.Input;
                        mParm59.Value = clsQHH.P_envi_hme_smoke;
                        pCmd.Parameters.Add(mParm59);

                        //SqlParameter mParm60 = new SqlParameter("@p_envi_hme_smoke", SqlDbType.Bit);
                        //mParm60.Direction = ParameterDirection.Input;
                        //mParm60.Value = clsQHH.P_envi_hme_smoke;
                        //pCmd.Parameters.Add(mParm60);

                        SqlParameter mParm61 = new SqlParameter("@p_envi_hme_chem", SqlDbType.Bit);
                        mParm61.Direction = ParameterDirection.Input;
                        mParm61.Value = clsQHH.P_envi_hme_chem;
                        pCmd.Parameters.Add(mParm61);

                        SqlParameter mParm62 = new SqlParameter("@p_envi_hme_pollen", SqlDbType.Bit);
                        mParm62.Direction = ParameterDirection.Input;
                        mParm62.Value = clsQHH.P_envi_hme_pollen;
                        pCmd.Parameters.Add(mParm62);

                        SqlParameter mParm63 = new SqlParameter("@p_envi_hme_pet", SqlDbType.Bit);
                        mParm63.Direction = ParameterDirection.Input;
                        mParm63.Value = clsQHH.P_envi_hme_pet;
                        pCmd.Parameters.Add(mParm63);

                        SqlParameter mParm64 = new SqlParameter("@p_envi_other", SqlDbType.NVarChar);
                        mParm64.Direction = ParameterDirection.Input;
                        mParm64.Value = clsQHH.P_envi_other;
                        pCmd.Parameters.Add(mParm64);

                        SqlParameter mParm65 = new SqlParameter("@p_envi_hme_other", SqlDbType.Bit);
                        mParm65.Direction = ParameterDirection.Input;
                        mParm65.Value = clsQHH.P_envi_hme_other;
                        pCmd.Parameters.Add(mParm65);

                        SqlParameter mParm66 = new SqlParameter("@p_envi_off_dust", SqlDbType.Bit);
                        mParm66.Direction = ParameterDirection.Input;
                        mParm66.Value = clsQHH.P_envi_off_dust;
                        pCmd.Parameters.Add(mParm66);

                        SqlParameter mParm67 = new SqlParameter("@p_envi_off_smoke", SqlDbType.Bit);
                        mParm67.Direction = ParameterDirection.Input;
                        mParm67.Value = clsQHH.P_envi_off_smoke;
                        pCmd.Parameters.Add(mParm67);

                        SqlParameter mParm68 = new SqlParameter("@p_envi_off_chem", SqlDbType.Bit);
                        mParm68.Direction = ParameterDirection.Input;
                        mParm68.Value = clsQHH.P_envi_off_chem;
                        pCmd.Parameters.Add(mParm68);

                        SqlParameter mParm69 = new SqlParameter("@p_envi_off_pollen", SqlDbType.Bit);
                        mParm69.Direction = ParameterDirection.Input;
                        mParm69.Value = clsQHH.P_envi_off_pollen;
                        pCmd.Parameters.Add(mParm69);


                        SqlParameter mParm70 = new SqlParameter("@p_envi_off_pet", SqlDbType.Bit);
                        mParm70.Direction = ParameterDirection.Input;
                        mParm70.Value = clsQHH.P_envi_off_pet;
                        pCmd.Parameters.Add(mParm70);

                        SqlParameter mParm71 = new SqlParameter("@p_envi_off_other", SqlDbType.Bit);
                        mParm71.Direction = ParameterDirection.Input;
                        mParm71.Value = clsQHH.P_envi_off_other;
                        pCmd.Parameters.Add(mParm71);

                        SqlParameter mParm72 = new SqlParameter("@p_envi_dur", SqlDbType.Float);
                        mParm72.Direction = ParameterDirection.Input;
                        mParm72.Value = clsQHH.P_envi_dur;
                        pCmd.Parameters.Add(mParm72);

                        SqlParameter mParm73 = new SqlParameter("@p_envi_yrs", SqlDbType.Float);
                        mParm73.Direction = ParameterDirection.Input;
                        mParm73.Value = clsQHH.P_envi_yrs;
                        pCmd.Parameters.Add(mParm73);

                        SqlParameter mParm74 = new SqlParameter("@p_cur_ill_cough", SqlDbType.Bit);
                        mParm74.Direction = ParameterDirection.Input;
                        mParm74.Value = clsQHH.P_cur_ill_cough;
                        pCmd.Parameters.Add(mParm74);

                        SqlParameter mParm75 = new SqlParameter("@p_cur_ill_wcough", SqlDbType.Bit);
                        mParm75.Direction = ParameterDirection.Input;
                        mParm75.Value = clsQHH.P_cur_ill_wcough;
                        pCmd.Parameters.Add(mParm75);

                        SqlParameter mParm76 = new SqlParameter("@p_cur_ill_gcough", SqlDbType.Bit);
                        mParm76.Direction = ParameterDirection.Input;
                        mParm76.Value = clsQHH.P_cur_ill_gcough;
                        pCmd.Parameters.Add(mParm76);

                        SqlParameter mParm77 = new SqlParameter("@p_cur_ill_bcough", SqlDbType.Bit);
                        mParm77.Direction = ParameterDirection.Input;
                        mParm77.Value = clsQHH.P_cur_ill_bcough;
                        pCmd.Parameters.Add(mParm77);

                        SqlParameter mParm78 = new SqlParameter("@p_cou_per_morn", SqlDbType.Bit);
                        mParm78.Direction = ParameterDirection.Input;
                        mParm78.Value = clsQHH.P_cou_per_morn;
                        pCmd.Parameters.Add(mParm78);

                        SqlParameter mParm79 = new SqlParameter("@p_cou_per_aday", SqlDbType.Bit);
                        mParm79.Direction = ParameterDirection.Input;
                        mParm79.Value = clsQHH.P_cou_per_aday;
                        pCmd.Parameters.Add(mParm79);

                        SqlParameter mParm80 = new SqlParameter("@p_cou_per_night", SqlDbType.Bit);
                        mParm80.Direction = ParameterDirection.Input;
                        mParm80.Value = clsQHH.P_cou_per_night;
                        pCmd.Parameters.Add(mParm80);

                        SqlParameter mParm81 = new SqlParameter("@p_cou_per_rarely", SqlDbType.Bit);
                        mParm81.Direction = ParameterDirection.Input;
                        mParm81.Value = clsQHH.P_cou_per_rarely;
                        pCmd.Parameters.Add(mParm81);


                        SqlParameter mParm82 = new SqlParameter("@p_cou_per_nsure", SqlDbType.Bit);
                        mParm82.Direction = ParameterDirection.Input;
                        mParm82.Value = clsQHH.P_cou_per_nsure;
                        pCmd.Parameters.Add(mParm82);

                        SqlParameter mParm83 = new SqlParameter("@p_cur_ill_pant", SqlDbType.Bit);
                        mParm83.Direction = ParameterDirection.Input;
                        mParm83.Value = clsQHH.P_cur_ill_pant;
                        pCmd.Parameters.Add(mParm83);

                        SqlParameter mParm84 = new SqlParameter("@p_pat_per_morn", SqlDbType.Bit);
                        mParm84.Direction = ParameterDirection.Input;
                        mParm84.Value = clsQHH.P_pat_per_morn;
                        pCmd.Parameters.Add(mParm84);

                        SqlParameter mParm85 = new SqlParameter("@p_pat_per_aday", SqlDbType.Bit);
                        mParm85.Direction = ParameterDirection.Input;
                        mParm85.Value = clsQHH.P_pat_per_aday;
                        pCmd.Parameters.Add(mParm85);

                        SqlParameter mParm86 = new SqlParameter("@p_pat_per_night", SqlDbType.Bit);
                        mParm86.Direction = ParameterDirection.Input;
                        mParm86.Value = clsQHH.P_pat_per_night;
                        pCmd.Parameters.Add(mParm86);


                        SqlParameter mParm87 = new SqlParameter("@p_pat_per_rarely", SqlDbType.Bit);
                        mParm87.Direction = ParameterDirection.Input;
                        mParm87.Value = clsQHH.P_pat_per_rarely;
                        pCmd.Parameters.Add(mParm87);


                        SqlParameter mParm88 = new SqlParameter("@p_pat_per_nsure", SqlDbType.Bit);
                        mParm88.Direction = ParameterDirection.Input;
                        mParm88.Value = clsQHH.P_pat_per_nsure;
                        pCmd.Parameters.Add(mParm88);


                        SqlParameter mParm89 = new SqlParameter("@p_pat_freq", SqlDbType.Bit);
                        mParm89.Direction = ParameterDirection.Input;
                        mParm89.Value = clsQHH.P_pat_freq;
                        pCmd.Parameters.Add(mParm89);

                        SqlParameter mParm90 = new SqlParameter("@p_pat_exercise", SqlDbType.Bit);
                        mParm90.Direction = ParameterDirection.Input;
                        mParm90.Value = clsQHH.P_pat_exercise;
                        pCmd.Parameters.Add(mParm90);


                        SqlParameter mParm91 = new SqlParameter("@p_pat_still", SqlDbType.Bit);
                        mParm91.Direction = ParameterDirection.Input;
                        mParm91.Value = clsQHH.P_pat_still;
                        pCmd.Parameters.Add(mParm91);


                        SqlParameter mParm92 = new SqlParameter("@p_pat_pros", SqlDbType.Bit);
                        mParm92.Direction = ParameterDirection.Input;
                        mParm92.Value = clsQHH.P_pat_pros;
                        pCmd.Parameters.Add(mParm92);

                        SqlParameter mParm93 = new SqlParameter("@p_pat_nsure", SqlDbType.Bit);
                        mParm93.Direction = ParameterDirection.Input;
                        mParm93.Value = clsQHH.P_pat_nsure;
                        pCmd.Parameters.Add(mParm93);


                        SqlParameter mParm94 = new SqlParameter("@p_illness_others", SqlDbType.NVarChar);
                        mParm94.Direction = ParameterDirection.Input;
                        mParm94.Value = clsQHH.P_illness_others;
                        pCmd.Parameters.Add(mParm94);



                        SqlParameter mParm95 = new SqlParameter("@p_ill_cur_med", SqlDbType.Char);
                        mParm95.Direction = ParameterDirection.Input;
                        mParm95.Value = clsQHH.P_ill_cur_med;
                        pCmd.Parameters.Add(mParm95);


                        SqlParameter mParm96 = new SqlParameter("@p_ill_cmed_diab", SqlDbType.Bit);
                        mParm96.Direction = ParameterDirection.Input;
                        mParm96.Value = clsQHH.P_ill_cmed_diab;
                        pCmd.Parameters.Add(mParm96);

                        SqlParameter mParm97 = new SqlParameter("@p_ill_cmed_hyper", SqlDbType.Bit);
                        mParm97.Direction = ParameterDirection.Input;
                        mParm97.Value = clsQHH.P_ill_cmed_hyper;
                        pCmd.Parameters.Add(mParm97);

                        SqlParameter mParm98 = new SqlParameter("@p_ill_cmed_demia", SqlDbType.Bit);
                        mParm98.Direction = ParameterDirection.Input;
                        mParm98.Value = clsQHH.P_ill_cmed_demia;
                        pCmd.Parameters.Add(mParm98);

                        //SqlParameter mParm99 = new SqlParameter("@p_ill_cmed_demia", SqlDbType.Bit);
                        //mParm99.Direction = ParameterDirection.Input;
                        //mParm99.Value = clsQHH.P_ill_cmed_demia;
                        //pCmd.Parameters.Add(mParm99);


                        SqlParameter mParm100 = new SqlParameter("@p_ill_cmed_cardi", SqlDbType.Bit);
                        mParm100.Direction = ParameterDirection.Input;
                        mParm100.Value = clsQHH.P_ill_cmed_cardi;
                        pCmd.Parameters.Add(mParm100);

                        SqlParameter mParm101 = new SqlParameter("@p_ill_cmed_dysl", SqlDbType.Bit);
                        mParm101.Direction = ParameterDirection.Input;
                        mParm101.Value = clsQHH.P_ill_cmed_dysl;
                        pCmd.Parameters.Add(mParm101);

                        SqlParameter mParm102 = new SqlParameter("@p_ill_cmed_horm", SqlDbType.Bit);
                        mParm102.Direction = ParameterDirection.Input;
                        mParm102.Value = clsQHH.P_ill_cmed_horm;
                        pCmd.Parameters.Add(mParm102);

                        SqlParameter mParm103 = new SqlParameter("@p_ill_cmed_oth", SqlDbType.Bit);
                        mParm103.Direction = ParameterDirection.Input;
                        mParm103.Value = clsQHH.P_ill_cmed_oth;
                        pCmd.Parameters.Add(mParm103);

                        SqlParameter mParm104 = new SqlParameter("@p_ill_cmed_others", SqlDbType.NVarChar);
                        mParm104.Direction = ParameterDirection.Input;
                        mParm104.Value = clsQHH.P_ill_cmed_others;
                        pCmd.Parameters.Add(mParm104);

                        SqlParameter mParm105 = new SqlParameter("@p_ill_allergy", SqlDbType.Char);
                        mParm105.Direction = ParameterDirection.Input;
                        mParm105.Value = clsQHH.P_ill_allergy;
                        pCmd.Parameters.Add(mParm105);

                        SqlParameter mParm106 = new SqlParameter("@p_ill_drug_or_food", SqlDbType.NVarChar);
                        mParm106.Direction = ParameterDirection.Input;
                        mParm106.Value = clsQHH.P_ill_drug_or_food;
                        pCmd.Parameters.Add(mParm106);

                        SqlParameter mParm107 = new SqlParameter("@p_pill_adm", SqlDbType.Char);
                        mParm107.Direction = ParameterDirection.Input;
                        mParm107.Value = clsQHH.P_pill_adm;
                        pCmd.Parameters.Add(mParm107);

                        SqlParameter mParm108 = new SqlParameter("@p_pill_admission", SqlDbType.NVarChar);
                        mParm108.Direction = ParameterDirection.Input;
                        mParm108.Value = clsQHH.P_pill_admission;
                        pCmd.Parameters.Add(mParm108);

                        SqlParameter mParm109 = new SqlParameter("@p_pill_sur", SqlDbType.Char);
                        mParm109.Direction = ParameterDirection.Input;
                        mParm109.Value = clsQHH.P_pill_sur;
                        pCmd.Parameters.Add(mParm109);

                        SqlParameter mParm110 = new SqlParameter("@p_pill_surgery", SqlDbType.NVarChar);
                        mParm110.Direction = ParameterDirection.Input;
                        mParm110.Value = clsQHH.P_pill_surgery;
                        pCmd.Parameters.Add(mParm110);

                        SqlParameter mParm111 = new SqlParameter("@p_vinf_hepB_virus", SqlDbType.Char);
                        mParm111.Direction = ParameterDirection.Input;
                        mParm111.Value = clsQHH.P_vinf_hepB_virus;
                        pCmd.Parameters.Add(mParm111);

                        SqlParameter mParm112 = new SqlParameter("@p_vinf_hepA_virus", SqlDbType.Char);
                        mParm112.Direction = ParameterDirection.Input;
                        mParm112.Value = clsQHH.P_vinf_hepA_virus;
                        pCmd.Parameters.Add(mParm112);

                        SqlParameter mParm113 = new SqlParameter("@p_vinf_vaccine", SqlDbType.Char);
                        mParm113.Direction = ParameterDirection.Input;
                        mParm113.Value = clsQHH.P_vinf_vaccine;
                        pCmd.Parameters.Add(mParm113);

                        //father

                        #region

                        SqlParameter mParm114 = new SqlParameter("@p_fhis_f_disease", SqlDbType.Char);
                        mParm114.Direction = ParameterDirection.Input;
                        mParm114.Value = clsQHH.P_fhis_f_disease;
                        pCmd.Parameters.Add(mParm114);

                        SqlParameter mParm115 = new SqlParameter("@p_fhis_fdis_hyper", SqlDbType.Bit);
                        mParm115.Direction = ParameterDirection.Input;
                        mParm115.Value = clsQHH.P_fhis_fdis_hyper;
                        pCmd.Parameters.Add(mParm115);

                        SqlParameter mParm116 = new SqlParameter("@p_fhis_fdis_heart", SqlDbType.Bit);
                        mParm116.Direction = ParameterDirection.Input;
                        mParm116.Value = clsQHH.P_fhis_fdis_heart;
                        pCmd.Parameters.Add(mParm116);


                        SqlParameter mParm117 = new SqlParameter("@p_fhis_fdis_diab", SqlDbType.Bit);
                        mParm117.Direction = ParameterDirection.Input;
                        mParm117.Value = clsQHH.P_fhis_fdis_diab;
                        pCmd.Parameters.Add(mParm117);

                        SqlParameter mParm118 = new SqlParameter("@p_fhis_fdis_coro", SqlDbType.Bit);
                        mParm118.Direction = ParameterDirection.Input;
                        mParm118.Value = clsQHH.P_fhis_fdis_coro;
                        pCmd.Parameters.Add(mParm118);

                        SqlParameter mParm119 = new SqlParameter("@p_fhis_fdis_coro_cs", SqlDbType.Char);
                        mParm119.Direction = ParameterDirection.Input;
                        mParm119.Value = clsQHH.P_fhis_fdis_coro_cs;
                        pCmd.Parameters.Add(mParm119);

                        SqlParameter mParm120 = new SqlParameter("@p_fhis_fdis_dysl", SqlDbType.Bit);
                        mParm120.Direction = ParameterDirection.Input;
                        mParm120.Value = clsQHH.P_fhis_fdis_dysl;
                        pCmd.Parameters.Add(mParm120);

                        SqlParameter mParm121 = new SqlParameter("@p_fhis_fdis_gout", SqlDbType.Bit);
                        mParm121.Direction = ParameterDirection.Input;
                        mParm121.Value = clsQHH.P_fhis_fdis_gout;
                        pCmd.Parameters.Add(mParm121);

                        SqlParameter mParm122 = new SqlParameter("@p_fhis_fdis_pulm", SqlDbType.Bit);
                        mParm122.Direction = ParameterDirection.Input;
                        mParm122.Value = clsQHH.P_fhis_fdis_pulm;
                        pCmd.Parameters.Add(mParm122);

                        SqlParameter mParm123 = new SqlParameter("@p_fhis_fdis_para", SqlDbType.Bit);
                        mParm123.Direction = ParameterDirection.Input;
                        mParm123.Value = clsQHH.P_fhis_fdis_para;
                        pCmd.Parameters.Add(mParm123);


                        SqlParameter mParm124 = new SqlParameter("@p_fhis_fdis_putb", SqlDbType.Bit);
                        mParm124.Direction = ParameterDirection.Input;
                        mParm124.Value = clsQHH.P_fhis_fdis_putb;
                        pCmd.Parameters.Add(mParm124);

                        SqlParameter mParm125 = new SqlParameter("@p_fhis_fdis_stro", SqlDbType.Bit);
                        mParm125.Direction = ParameterDirection.Input;
                        mParm125.Value = clsQHH.P_fhis_fdis_stro;
                        pCmd.Parameters.Add(mParm125);

                        SqlParameter mParm126 = new SqlParameter("@p_fhis_fdis_pepu", SqlDbType.Bit);
                        mParm126.Direction = ParameterDirection.Input;
                        mParm126.Value = clsQHH.P_fhis_fdis_pepu;
                        pCmd.Parameters.Add(mParm126);

                        SqlParameter mParm127 = new SqlParameter("@p_fhis_fdis_asth", SqlDbType.Bit);
                        mParm127.Direction = ParameterDirection.Input;
                        mParm127.Value = clsQHH.P_fhis_fdis_asth;
                        pCmd.Parameters.Add(mParm127);

                        SqlParameter mParm128 = new SqlParameter("@p_fhis_fdis_alle", SqlDbType.Bit);
                        mParm128.Direction = ParameterDirection.Input;
                        mParm128.Value = clsQHH.P_fhis_fdis_alle;
                        pCmd.Parameters.Add(mParm128);

                        SqlParameter mParm129 = new SqlParameter("@p_fhis_fdis_canc", SqlDbType.Bit);
                        mParm129.Direction = ParameterDirection.Input;
                        mParm129.Value = clsQHH.P_fhis_fdis_canc;
                        pCmd.Parameters.Add(mParm129);

                        SqlParameter mParm130 = new SqlParameter("@p_fhis_fdis_canc_rmk", SqlDbType.NVarChar);
                        mParm130.Direction = ParameterDirection.Input;
                        mParm130.Value = clsQHH.P_fhis_fdis_canc_rmk;
                        pCmd.Parameters.Add(mParm130);


                        SqlParameter mParm131 = new SqlParameter("@p_fhis_fdis_oth", SqlDbType.Bit);
                        mParm131.Direction = ParameterDirection.Input;
                        mParm131.Value = clsQHH.P_fhis_fdis_oth;
                        pCmd.Parameters.Add(mParm131);


                        SqlParameter mParm133 = new SqlParameter("@p_fhis_fdis_others", SqlDbType.NVarChar);
                        mParm133.Direction = ParameterDirection.Input;
                        mParm133.Value = clsQHH.P_fhis_fdis_others;
                        pCmd.Parameters.Add(mParm133);



                        //end father
                        #endregion

                        //Mother

                        #region

                        SqlParameter mParm134 = new SqlParameter("@p_fhis_m_disease", SqlDbType.Char);
                        mParm134.Direction = ParameterDirection.Input;
                        mParm134.Value = clsQHH.P_fhis_m_disease;
                        pCmd.Parameters.Add(mParm134);

                        SqlParameter mParm135 = new SqlParameter("@p_fhis_mdis_hyper", SqlDbType.Bit);
                        mParm135.Direction = ParameterDirection.Input;
                        mParm135.Value = clsQHH.P_fhis_mdis_hyper;
                        pCmd.Parameters.Add(mParm135);

                        SqlParameter mParm136 = new SqlParameter("@p_fhis_mdis_heart", SqlDbType.Bit);
                        mParm136.Direction = ParameterDirection.Input;
                        mParm136.Value = clsQHH.P_fhis_mdis_heart;
                        pCmd.Parameters.Add(mParm136);


                        SqlParameter mParm137 = new SqlParameter("@p_fhis_mdis_diab", SqlDbType.Bit);
                        mParm137.Direction = ParameterDirection.Input;
                        mParm137.Value = clsQHH.P_fhis_mdis_diab;
                        pCmd.Parameters.Add(mParm137);

                        SqlParameter mParm138 = new SqlParameter("@p_fhis_mdis_coro", SqlDbType.Bit);
                        mParm138.Direction = ParameterDirection.Input;
                        mParm138.Value = clsQHH.P_fhis_mdis_coro;
                        pCmd.Parameters.Add(mParm138);

                        SqlParameter mParm139 = new SqlParameter("@p_fhis_mdis_coro_cs", SqlDbType.Char);
                        mParm139.Direction = ParameterDirection.Input;
                        mParm139.Value = clsQHH.P_fhis_mdis_coro_cs;
                        pCmd.Parameters.Add(mParm139);

                        SqlParameter mParm140 = new SqlParameter("@p_fhis_mdis_dysl", SqlDbType.Bit);
                        mParm140.Direction = ParameterDirection.Input;
                        mParm140.Value = clsQHH.P_fhis_mdis_dysl;
                        pCmd.Parameters.Add(mParm140);

                        SqlParameter mParm141 = new SqlParameter("@p_fhis_mdis_gout", SqlDbType.Bit);
                        mParm141.Direction = ParameterDirection.Input;
                        mParm141.Value = clsQHH.P_fhis_mdis_gout;
                        pCmd.Parameters.Add(mParm141);

                        SqlParameter mParm142 = new SqlParameter("@p_fhis_mdis_pulm", SqlDbType.Bit);
                        mParm142.Direction = ParameterDirection.Input;
                        mParm142.Value = clsQHH.P_fhis_mdis_pulm;
                        pCmd.Parameters.Add(mParm142);

                        SqlParameter mParm143 = new SqlParameter("@p_fhis_mdis_para", SqlDbType.Bit);
                        mParm143.Direction = ParameterDirection.Input;
                        mParm143.Value = clsQHH.P_fhis_mdis_para;
                        pCmd.Parameters.Add(mParm143);


                        SqlParameter mParm144 = new SqlParameter("@p_fhis_mdis_putb", SqlDbType.Bit);
                        mParm144.Direction = ParameterDirection.Input;
                        mParm144.Value = clsQHH.P_fhis_mdis_putb;
                        pCmd.Parameters.Add(mParm144);

                        SqlParameter mParm145 = new SqlParameter("@p_fhis_mdis_stro", SqlDbType.Bit);
                        mParm145.Direction = ParameterDirection.Input;
                        mParm145.Value = clsQHH.P_fhis_mdis_stro;
                        pCmd.Parameters.Add(mParm145);

                        SqlParameter mParm146= new SqlParameter("@p_fhis_mdis_pepu", SqlDbType.Bit);
                        mParm146.Direction = ParameterDirection.Input;
                        mParm146.Value = clsQHH.P_fhis_mdis_pepu;
                        pCmd.Parameters.Add(mParm146);

                        SqlParameter mParm147 = new SqlParameter("@p_fhis_mdis_asth", SqlDbType.Bit);
                        mParm147.Direction = ParameterDirection.Input;
                        mParm147.Value = clsQHH.P_fhis_mdis_asth;
                        pCmd.Parameters.Add(mParm147);

                        SqlParameter mParm148 = new SqlParameter("@p_fhis_mdis_alle", SqlDbType.Bit);
                        mParm148.Direction = ParameterDirection.Input;
                        mParm148.Value = clsQHH.P_fhis_mdis_alle;
                        pCmd.Parameters.Add(mParm148);

                        SqlParameter mParm149 = new SqlParameter("@p_fhis_mdis_canc", SqlDbType.Bit);
                        mParm149.Direction = ParameterDirection.Input;
                        mParm149.Value = clsQHH.P_fhis_mdis_canc;
                        pCmd.Parameters.Add(mParm149);

                        SqlParameter mParm150= new SqlParameter("@p_fhis_mdis_canc_rmk", SqlDbType.NVarChar);
                        mParm150.Direction = ParameterDirection.Input;
                        mParm150.Value = clsQHH.P_fhis_mdis_canc_rmk;
                        pCmd.Parameters.Add(mParm150);


                        SqlParameter mParm151= new SqlParameter("@p_fhis_mdis_oth", SqlDbType.Bit);
                        mParm151.Direction = ParameterDirection.Input;
                        mParm151.Value = clsQHH.P_fhis_mdis_oth;
                        pCmd.Parameters.Add(mParm151);

                        SqlParameter mParm152 = new SqlParameter("@p_fhis_mdis_others", SqlDbType.NVarChar);
                        mParm152.Direction = ParameterDirection.Input;
                        mParm152.Value = clsQHH.P_fhis_mdis_others;
                        pCmd.Parameters.Add(mParm152);

                   
                        //end father
                        #endregion


                        //relative
                        #region

                        SqlParameter mParm153 = new SqlParameter("@p_fhis_b_disease", SqlDbType.Char);
                        mParm153.Direction = ParameterDirection.Input;
                        mParm153.Value = clsQHH.P_fhis_b_disease;
                        pCmd.Parameters.Add(mParm153);

                        SqlParameter mParm154 = new SqlParameter("@p_fhis_bdis_hyper", SqlDbType.Bit);
                        mParm154.Direction = ParameterDirection.Input;
                        mParm154.Value = clsQHH.P_fhis_bdis_hyper;
                        pCmd.Parameters.Add(mParm154);

                        SqlParameter mParm155 = new SqlParameter("@p_fhis_bdis_heart", SqlDbType.Bit);
                        mParm155.Direction = ParameterDirection.Input;
                        mParm155.Value = clsQHH.P_fhis_bdis_heart;
                        pCmd.Parameters.Add(mParm155);


                        SqlParameter mParm156 = new SqlParameter("@p_fhis_bdis_diab", SqlDbType.Bit);
                        mParm156.Direction = ParameterDirection.Input;
                        mParm156.Value = clsQHH.P_fhis_bdis_diab;
                        pCmd.Parameters.Add(mParm156);

                        SqlParameter mParm157 = new SqlParameter("@p_fhis_bdis_coro", SqlDbType.Bit);
                        mParm157.Direction = ParameterDirection.Input;
                        mParm157.Value = clsQHH.P_fhis_bdis_coro;
                        pCmd.Parameters.Add(mParm157);

                        SqlParameter mParm158 = new SqlParameter("@p_fhis_bdis_coro_cs", SqlDbType.Char);
                        mParm158.Direction = ParameterDirection.Input;
                        mParm158.Value = clsQHH.P_fhis_bdis_coro_cs;
                        pCmd.Parameters.Add(mParm158);

                        SqlParameter mParm159 = new SqlParameter("@p_fhis_bdis_coro_bfm", SqlDbType.Bit);
                        mParm159.Direction = ParameterDirection.Input;
                        mParm159.Value = clsQHH.P_fhis_bdis_coro_bfm;
                        pCmd.Parameters.Add(mParm159);

                        SqlParameter mParm160 = new SqlParameter("@p_fhis_bdis_coro_afm", SqlDbType.Bit);
                        mParm160.Direction = ParameterDirection.Input;
                        mParm160.Value = clsQHH.P_fhis_bdis_coro_afm;
                        pCmd.Parameters.Add(mParm160);

                        SqlParameter mParm161 = new SqlParameter("@p_fhis_bdis_coro_nfm", SqlDbType.Bit);
                        mParm161.Direction = ParameterDirection.Input;
                        mParm161.Value = clsQHH.P_fhis_bdis_coro_nfm;
                        pCmd.Parameters.Add(mParm161);

                        SqlParameter mParm162 = new SqlParameter("@p_fhis_bdis_coro_bm", SqlDbType.Bit);
                        mParm162.Direction = ParameterDirection.Input;
                        mParm162.Value = clsQHH.P_fhis_bdis_coro_bm;
                        pCmd.Parameters.Add(mParm162);


                        SqlParameter mParm163 = new SqlParameter("@p_fhis_bdis_coro_am", SqlDbType.Bit);
                        mParm163.Direction = ParameterDirection.Input;
                        mParm163.Value = clsQHH.P_fhis_bdis_coro_am;
                        pCmd.Parameters.Add(mParm163);

                        SqlParameter mParm164 = new SqlParameter("@p_fhis_bdis_coro_nm", SqlDbType.Bit);
                        mParm164.Direction = ParameterDirection.Input;
                        mParm164.Value = clsQHH.P_fhis_bdis_coro_nm;
                        pCmd.Parameters.Add(mParm164);

                        SqlParameter mParm165 = new SqlParameter("@p_fhis_bdis_dysl", SqlDbType.Bit);
                        mParm165.Direction = ParameterDirection.Input;
                        mParm165.Value = clsQHH.P_fhis_bdis_dysl;
                        pCmd.Parameters.Add(mParm165);

                        SqlParameter mParm166 = new SqlParameter("@p_fhis_bdis_gout", SqlDbType.Bit);
                        mParm166.Direction = ParameterDirection.Input;
                        mParm166.Value = clsQHH.P_fhis_bdis_gout;
                        pCmd.Parameters.Add(mParm166);

                        SqlParameter mParm167 = new SqlParameter("@p_fhis_bdis_pulm", SqlDbType.Bit);
                        mParm167.Direction = ParameterDirection.Input;
                        mParm167.Value = clsQHH.P_fhis_bdis_pulm;
                        pCmd.Parameters.Add(mParm167);

                        SqlParameter mParm168 = new SqlParameter("@p_fhis_bdis_para", SqlDbType.Bit);
                        mParm168.Direction = ParameterDirection.Input;
                        mParm168.Value = clsQHH.P_fhis_bdis_para;
                        pCmd.Parameters.Add(mParm168);

                        SqlParameter mParm169 = new SqlParameter("@p_fhis_bdis_putb", SqlDbType.Bit);
                        mParm169.Direction = ParameterDirection.Input;
                        mParm169.Value = clsQHH.P_fhis_bdis_putb;
                        pCmd.Parameters.Add(mParm169);


                        SqlParameter mParm170 = new SqlParameter("@p_fhis_bdis_stro", SqlDbType.Bit);
                        mParm170.Direction = ParameterDirection.Input;
                        mParm170.Value = clsQHH.P_fhis_bdis_stro;
                        pCmd.Parameters.Add(mParm170);

                        SqlParameter mParm171 = new SqlParameter("@p_fhis_bdis_pepu", SqlDbType.Bit);
                        mParm171.Direction = ParameterDirection.Input;
                        mParm171.Value = clsQHH.P_fhis_bdis_pepu;
                        pCmd.Parameters.Add(mParm171);

                        SqlParameter mParm172 = new SqlParameter("@p_fhis_bdis_asth", SqlDbType.Bit);
                        mParm172.Direction = ParameterDirection.Input;
                        mParm172.Value = clsQHH.P_fhis_bdis_asth;
                        pCmd.Parameters.Add(mParm172);

                        SqlParameter mParm173 = new SqlParameter("@p_fhis_bdis_alle", SqlDbType.Bit);
                        mParm173.Direction = ParameterDirection.Input;
                        mParm173.Value = clsQHH.P_fhis_bdis_alle;
                        pCmd.Parameters.Add(mParm173);

                        SqlParameter mParm174 = new SqlParameter("@p_fhis_bdis_canc", SqlDbType.Bit);
                        mParm174.Direction = ParameterDirection.Input;
                        mParm174.Value = clsQHH.P_fhis_bdis_canc;
                        pCmd.Parameters.Add(mParm174);

                        SqlParameter mParm175 = new SqlParameter("@p_fhis_bdis_canc_rmk", SqlDbType.NVarChar);
                        mParm175.Direction = ParameterDirection.Input;
                        mParm175.Value = clsQHH.P_fhis_bdis_canc_rmk;
                        pCmd.Parameters.Add(mParm175);

                        SqlParameter mParm176 = new SqlParameter("@p_fhis_bdis_oth", SqlDbType.Bit);
                        mParm176.Direction = ParameterDirection.Input;
                        mParm176.Value = clsQHH.P_fhis_bdis_oth;
                        pCmd.Parameters.Add(mParm176);

                        SqlParameter mParm177 = new SqlParameter("@p_fhis_bdis_others", SqlDbType.NVarChar);
                        mParm177.Direction = ParameterDirection.Input;
                        mParm177.Value = clsQHH.P_fhis_bdis_others;
                        pCmd.Parameters.Add(mParm177);

                        SqlParameter mParm178 = new SqlParameter("@p_fhis_others", SqlDbType.NVarChar);
                        mParm178.Direction = ParameterDirection.Input;
                        mParm178.Value = clsQHH.P_fhis_others;
                        pCmd.Parameters.Add(mParm178);

                        //end relative
                        #endregion

                        #region
                        //Womem
                        SqlParameter mParm179 = new SqlParameter("@p_fwm_menopause", SqlDbType.Bit);
                        mParm179.Direction = ParameterDirection.Input;
                        mParm179.Value = clsQHH.P_fwm_menopause;
                        pCmd.Parameters.Add(mParm179);

                        SqlParameter mParm180 = new SqlParameter("@p_fwm_meno_start", SqlDbType.Char);
                        mParm180.Direction = ParameterDirection.Input;
                        mParm180.Value = clsQHH.P_fwm_meno_start;
                        pCmd.Parameters.Add(mParm180);

                        SqlParameter mParm181 = new SqlParameter("@p_fwm_lst_st_mens", SqlDbType.NVarChar);
                        mParm181.Direction = ParameterDirection.Input;
                        mParm181.Value = clsQHH.P_fwm_lst_st_mens;
                        pCmd.Parameters.Add(mParm181);

                        SqlParameter mParm182 = new SqlParameter("@p_fwm_lst_ed_mens", SqlDbType.NVarChar);
                        mParm182.Direction = ParameterDirection.Input;
                        mParm182.Value = clsQHH.P_fwm_lst_ed_mens;
                        pCmd.Parameters.Add(mParm182);

                        SqlParameter mParm183 = new SqlParameter("@p_fwm_character", SqlDbType.Char);
                        mParm183.Direction = ParameterDirection.Input;
                        mParm183.Value = clsQHH.P_fwm_character;
                        pCmd.Parameters.Add(mParm183);

                        SqlParameter mParm184 = new SqlParameter("@p_fwm_pregnancy", SqlDbType.Char);
                        mParm184.Direction = ParameterDirection.Input;
                        mParm184.Value = clsQHH.P_fwm_pregnancy;
                        pCmd.Parameters.Add(mParm184);

                        SqlParameter mParm185 = new SqlParameter("@p_fwm_over_weight", SqlDbType.Char);
                        mParm185.Direction = ParameterDirection.Input;
                        mParm185.Value = clsQHH.P_fwm_over_weight;
                        pCmd.Parameters.Add(mParm185);

                        SqlParameter mParm186 = new SqlParameter("@p_symp_faint", SqlDbType.Bit);
                        mParm186.Direction = ParameterDirection.Input;
                        mParm186.Value = clsQHH.P_symp_faint;
                        pCmd.Parameters.Add(mParm186);

                        SqlParameter mParm187 = new SqlParameter("@p_symp_shake", SqlDbType.Bit);
                        mParm187.Direction = ParameterDirection.Input;
                        mParm187.Value = clsQHH.P_symp_shake;
                        pCmd.Parameters.Add(mParm187);

                        SqlParameter mParm188 = new SqlParameter("@p_symp_wind", SqlDbType.Bit);
                        mParm188.Direction = ParameterDirection.Input;
                        mParm188.Value = clsQHH.P_symp_wind;
                        pCmd.Parameters.Add(mParm188);

                        SqlParameter mParm189 = new SqlParameter("@p_symp_breath", SqlDbType.Bit);
                        mParm189.Direction = ParameterDirection.Input;
                        mParm189.Value = clsQHH.P_symp_wind;
                        pCmd.Parameters.Add(mParm189);

                        SqlParameter mParm190 = new SqlParameter("@p_symp_vein", SqlDbType.Bit);
                        mParm190.Direction = ParameterDirection.Input;
                        mParm190.Value = clsQHH.P_symp_vein;
                        pCmd.Parameters.Add(mParm190);

                        SqlParameter mParm191 = new SqlParameter("@p_symp_paralysis", SqlDbType.Bit);
                        mParm191.Direction = ParameterDirection.Input;
                        mParm191.Value = clsQHH.P_symp_paralysis;
                        pCmd.Parameters.Add(mParm191);

                        SqlParameter mParm192 = new SqlParameter("@p_address", SqlDbType.NVarChar);
                        mParm192.Direction = ParameterDirection.Input;
                        mParm192.Value = clsQHH.P_address;
                        pCmd.Parameters.Add(mParm192);

                        SqlParameter mParm193 = new SqlParameter("@p_tumbon", SqlDbType.NVarChar);
                        mParm193.Direction = ParameterDirection.Input;
                        mParm193.Value = clsQHH.P_tumbon;
                        pCmd.Parameters.Add(mParm193);

                        SqlParameter mParm194 = new SqlParameter("@p_aumphur", SqlDbType.NVarChar);
                        mParm194.Direction = ParameterDirection.Input;
                        mParm194.Value = clsQHH.P_aumphur;
                        pCmd.Parameters.Add(mParm194);

                        SqlParameter mParm195 = new SqlParameter("@p_province", SqlDbType.NVarChar);
                        mParm195.Direction = ParameterDirection.Input;
                        mParm195.Value = clsQHH.P_province;
                        pCmd.Parameters.Add(mParm195);

                        SqlParameter mParm196 = new SqlParameter("@p_zip_code", SqlDbType.NVarChar);
                        mParm196.Direction = ParameterDirection.Input;
                        mParm196.Value = clsQHH.P_zip_code;
                        pCmd.Parameters.Add(mParm196);

                        SqlParameter mParm197 = new SqlParameter("@p_mobile", SqlDbType.NVarChar);
                        mParm197.Direction = ParameterDirection.Input;
                        mParm197.Value = clsQHH.P_mobile;
                        pCmd.Parameters.Add(mParm197);
                        //end women
                        #endregion


                        SqlParameter mParm198 = new SqlParameter("@p_create_by", SqlDbType.NVarChar);
                        mParm198.Direction = ParameterDirection.Input;
                        mParm198.Value = clsQHH.P_create_by;
                        pCmd.Parameters.Add(mParm198);


                        SqlParameter mParm199 = new SqlParameter("@p_create_date", SqlDbType.NVarChar);
                        mParm199.Direction = ParameterDirection.Input;
                        mParm199.Value = clsQHH.P_create_date;
                        pCmd.Parameters.Add(mParm199);

                        SqlParameter mParm200 = new SqlParameter("@p_update_by", SqlDbType.NVarChar);
                        mParm200.Direction = ParameterDirection.Input;
                        mParm200.Value = clsQHH.P_update_by;
                        pCmd.Parameters.Add(mParm200);


                        SqlParameter mParm201 = new SqlParameter("@p_update_date", SqlDbType.NVarChar);
                        mParm201.Direction = ParameterDirection.Input;
                        mParm201.Value = clsQHH.P_update_date;
                        pCmd.Parameters.Add(mParm201);

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }
           
         
        }

        public DataTable get_QuestionairePatiant(string HN)
        {
            using (SqlConnection pConn = new SqlConnection(_BHQ_ConnectionString))
            {
                DataTable pDs = new DataTable();
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }
                pConn.Open();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.CommandText = "selectQuestionairePatiant";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();
                        pCmd.Parameters.AddWithValue("@p_hn_no", HN);

                        SqlDataAdapter pDta = new SqlDataAdapter(pCmd);
                        pDta.Fill(pDs);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
                return pDs;
            }
        }

        public DataTable get_history_verify(string HN)
        {
            using (SqlConnection pConn = new SqlConnection(_DB_QuestionaireConnectionString))
            {
                DataTable pDs = new DataTable();
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }
                pConn.Open();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.CommandText = "SP_GET_VERIFY";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();
                        pCmd.Parameters.AddWithValue("@HN", HN);

                        SqlDataAdapter pDta = new SqlDataAdapter(pCmd);
                        pDta.Fill(pDs);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
                return pDs;
            }
        }

        //Aviation
        public DataTable get_DefaultPatientInfoAviation(string HN)
        {
            using (SqlConnection pConn = new SqlConnection(_BHQ_ConnectionString))
            {
                DataTable pDs = new DataTable();
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }
                pConn.Open();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.CommandText = "selectDefaultPatiantInfoAviation";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();
                        pCmd.Parameters.AddWithValue("@p_hn_no", HN);

                        SqlDataAdapter pDta = new SqlDataAdapter(pCmd);
                        pDta.Fill(pDs);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
                return pDs;
            }
        }
        public DataTable get_QuestionairePatientAviation(string HN)
        {
            using (SqlConnection pConn = new SqlConnection(_BHQ_ConnectionString))
            {
                DataTable pDs = new DataTable();
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }
                pConn.Open();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.CommandText = "selectQuestionairePatiantAviation";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();
                        pCmd.Parameters.AddWithValue("@p_hn_no", HN);

                        SqlDataAdapter pDta = new SqlDataAdapter(pCmd);
                        pDta.Fill(pDs);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
                return pDs;
            }
        }
        public void insert_trn_ques_aviation(clsQuestionaireAviation clsQHH)
        {
            using (SqlConnection pConn = new SqlConnection(_BHQ_ConnectionString))
            {
                if (pConn.State == ConnectionState.Open)
                {
                    pConn.Close();
                }

                pConn.Open();

                SqlTransaction pTrans;
                pTrans = pConn.BeginTransaction();
                try
                {
                    using (SqlCommand pCmd = new SqlCommand())
                    {
                        pCmd.Connection = pConn;
                        pCmd.Transaction = pTrans;
                        pCmd.CommandText = "insertQuestionairePatiantAviation";
                        pCmd.CommandType = CommandType.StoredProcedure;
                        pCmd.Parameters.Clear();

                        SqlParameter mParm1 = new SqlParameter("@p_hn_no", SqlDbType.VarChar);
                        mParm1.Direction = ParameterDirection.Input;
                        mParm1.Value = clsQHH.P_HN;
                        pCmd.Parameters.Add(mParm1);

                        SqlParameter mParm2 = new SqlParameter("@tqa_type", SqlDbType.Char);
                        mParm2.Direction = ParameterDirection.Input;
                        mParm2.Value = clsQHH.tqa_type == null ? DBNull.Value : (object)clsQHH.tqa_type;
                        pCmd.Parameters.Add(mParm2);

                        SqlParameter mParm3 = new SqlParameter("@tqa_confirm_doctor", SqlDbType.VarChar);
                        mParm3.Direction = ParameterDirection.Input;
                        mParm3.Value = clsQHH.tqa_confirm_doctor == null ? DBNull.Value : (object)clsQHH.tqa_confirm_doctor;
                        pCmd.Parameters.Add(mParm3);
                        
                        SqlParameter mParm4 = new SqlParameter("@tqa_avia_type", SqlDbType.Char);
                        mParm4.Direction = ParameterDirection.Input;
                        mParm4.Value = clsQHH.tqa_avia_type == null ? DBNull.Value : (object)clsQHH.tqa_avia_type;
                        pCmd.Parameters.Add(mParm4);

                        SqlParameter mParm5 = new SqlParameter("@tqa_avia_oths", SqlDbType.VarChar);
                        mParm5.Direction = ParameterDirection.Input;
                        mParm5.Value = clsQHH.tqa_avia_oths == null ? DBNull.Value : (object)clsQHH.tqa_avia_oths;
                        pCmd.Parameters.Add(mParm5);

                        SqlParameter mParm6 = new SqlParameter("@tqa_doc_type", SqlDbType.Char);
                        mParm6.Direction = ParameterDirection.Input;
                        mParm6.Value = clsQHH.tqa_doc_type == null ? DBNull.Value : (object)clsQHH.tqa_doc_type;
                        pCmd.Parameters.Add(mParm6);

                        SqlParameter mParm7 = new SqlParameter("@tqa_place_exam", SqlDbType.VarChar);
                        mParm7.Direction = ParameterDirection.Input;
                        mParm7.Value = clsQHH.tqa_place_exam == null ? DBNull.Value : (object)clsQHH.tqa_place_exam;
                        pCmd.Parameters.Add(mParm7);

                        SqlParameter mParm8 = new SqlParameter("@tqa_th_fullname", SqlDbType.VarChar);
                        mParm8.Direction = ParameterDirection.Input;
                        mParm8.Value = clsQHH.tqa_th_fullname == null ? DBNull.Value : (object)clsQHH.tqa_th_fullname;
                        pCmd.Parameters.Add(mParm8);

                        SqlParameter mParm9 = new SqlParameter("@tqa_th_nation", SqlDbType.VarChar);
                        mParm9.Direction = ParameterDirection.Input;
                        mParm9.Value = clsQHH.tqa_th_nation == null ? DBNull.Value : (object)clsQHH.tqa_th_nation;
                        pCmd.Parameters.Add(mParm9);

                        SqlParameter mParm10 = new SqlParameter("@tqa_en_fullname", SqlDbType.VarChar);
                        mParm10.Direction = ParameterDirection.Input;
                        mParm10.Value = clsQHH.tqa_en_fullname == null ? DBNull.Value : (object)clsQHH.tqa_en_fullname;
                        pCmd.Parameters.Add(mParm10);

                        SqlParameter mParm11 = new SqlParameter("@tqa_en_nation", SqlDbType.VarChar);
                        mParm11.Direction = ParameterDirection.Input;
                        mParm11.Value = clsQHH.tqa_en_nation == null ? DBNull.Value : (object)clsQHH.tqa_en_nation;
                        pCmd.Parameters.Add(mParm11);

                        SqlParameter mParm12 = new SqlParameter("@tqa_sex", SqlDbType.Char);
                        mParm12.Direction = ParameterDirection.Input;
                        mParm12.Value = clsQHH.tqa_sex == null ? DBNull.Value : (object)clsQHH.tqa_sex;
                        pCmd.Parameters.Add(mParm12);

                        SqlParameter mParm13 = new SqlParameter("@tqa_dob", SqlDbType.VarChar);
                        mParm13.Direction = ParameterDirection.Input;
                        mParm13.Value = clsQHH.tqa_dob == null ? DBNull.Value : (object)clsQHH.tqa_dob;
                        pCmd.Parameters.Add(mParm13);

                        SqlParameter mParm14 = new SqlParameter("@tqa_age_yrs", SqlDbType.Float);
                        mParm14.Direction = ParameterDirection.Input;
                        mParm14.Value = clsQHH.tqa_age_yrs == null ? DBNull.Value : (object)clsQHH.tqa_age_yrs;
                        pCmd.Parameters.Add(mParm14);

                        SqlParameter mParm15 = new SqlParameter("@tqa_age_month", SqlDbType.Float);
                        mParm15.Direction = ParameterDirection.Input;
                        mParm15.Value = clsQHH.tqa_age_month == null ? DBNull.Value : (object)clsQHH.tqa_age_month;
                        pCmd.Parameters.Add(mParm15);

                        SqlParameter mParm16 = new SqlParameter("@tqa_marital", SqlDbType.Char);
                        mParm16.Direction = ParameterDirection.Input;
                        mParm16.Value = clsQHH.tqa_marital == null ? DBNull.Value : (object)clsQHH.tqa_marital;
                        pCmd.Parameters.Add(mParm16);

                        SqlParameter mParm17 = new SqlParameter("@tqa_license_type", SqlDbType.Char);
                        mParm17.Direction = ParameterDirection.Input;
                        mParm17.Value = clsQHH.tqa_license_type == null ? DBNull.Value : (object)clsQHH.tqa_license_type;
                        pCmd.Parameters.Add(mParm17);

                        SqlParameter mParm18 = new SqlParameter("@tqa_license_no", SqlDbType.VarChar);
                        mParm18.Direction = ParameterDirection.Input;
                        mParm18.Value = clsQHH.tqa_license_no == null ? DBNull.Value : (object)clsQHH.tqa_license_no;
                        pCmd.Parameters.Add(mParm18);

                        SqlParameter mParm19 = new SqlParameter("@tqa_chge_address", SqlDbType.Char);
                        mParm19.Direction = ParameterDirection.Input;
                        mParm19.Value = clsQHH.tqa_chge_address == null ? DBNull.Value : (object)clsQHH.tqa_chge_address;
                        pCmd.Parameters.Add(mParm19);

                        SqlParameter mParm20 = new SqlParameter("@tqa_th_address", SqlDbType.VarChar);
                        mParm20.Direction = ParameterDirection.Input;
                        mParm20.Value = clsQHH.tqa_th_address == null ? DBNull.Value : (object)clsQHH.tqa_th_address;
                        pCmd.Parameters.Add(mParm20);

                        SqlParameter mParm21 = new SqlParameter("@tqa_th_moblie", SqlDbType.VarChar);
                        mParm21.Direction = ParameterDirection.Input;
                        mParm21.Value = clsQHH.tqa_th_moblie == null ? DBNull.Value : (object)clsQHH.tqa_th_moblie;
                        pCmd.Parameters.Add(mParm21);

                        SqlParameter mParm22 = new SqlParameter("@tqa_en_address", SqlDbType.VarChar);
                        mParm22.Direction = ParameterDirection.Input;
                        mParm22.Value = clsQHH.tqa_en_address == null ? DBNull.Value : (object)clsQHH.tqa_en_address;
                        pCmd.Parameters.Add(mParm22);

                        SqlParameter mParm23 = new SqlParameter("@tqa_en_mobile", SqlDbType.VarChar);
                        mParm23.Direction = ParameterDirection.Input;
                        mParm23.Value = clsQHH.tqa_en_mobile == null ? DBNull.Value : (object)clsQHH.tqa_en_mobile;
                        pCmd.Parameters.Add(mParm23);

                        SqlParameter mParm24 = new SqlParameter("@tqa_th_occupa", SqlDbType.VarChar);
                        mParm24.Direction = ParameterDirection.Input;
                        mParm24.Value = clsQHH.tqa_th_occupa == null ? DBNull.Value : (object)clsQHH.tqa_th_occupa;
                        pCmd.Parameters.Add(mParm24);

                        SqlParameter mParm25 = new SqlParameter("@tqa_th_comp", SqlDbType.VarChar);
                        mParm25.Direction = ParameterDirection.Input;
                        mParm25.Value = clsQHH.tqa_th_comp == null ? DBNull.Value : (object)clsQHH.tqa_th_comp;
                        pCmd.Parameters.Add(mParm25);

                        SqlParameter mParm26 = new SqlParameter("@tqa_en_occupa", SqlDbType.VarChar);
                        mParm26.Direction = ParameterDirection.Input;
                        mParm26.Value = clsQHH.tqa_en_occupa == null ? DBNull.Value : (object)clsQHH.tqa_en_occupa;
                        pCmd.Parameters.Add(mParm26);

                        SqlParameter mParm27 = new SqlParameter("@tqa_en_comp", SqlDbType.VarChar);
                        mParm27.Direction = ParameterDirection.Input;
                        mParm27.Value = clsQHH.tqa_en_comp == null ? DBNull.Value : (object)clsQHH.tqa_en_comp;
                        pCmd.Parameters.Add(mParm27);

                        SqlParameter mParm28 = new SqlParameter("@tqa_th_office", SqlDbType.VarChar);
                        mParm28.Direction = ParameterDirection.Input;
                        mParm28.Value = clsQHH.tqa_th_office == null ? DBNull.Value : (object)clsQHH.tqa_th_office;
                        pCmd.Parameters.Add(mParm28);

                        SqlParameter mParm29 = new SqlParameter("@tqa_th_of_mobile", SqlDbType.VarChar);
                        mParm29.Direction = ParameterDirection.Input;
                        mParm29.Value = clsQHH.tqa_th_of_mobile == null ? DBNull.Value : (object)clsQHH.tqa_th_of_mobile;
                        pCmd.Parameters.Add(mParm29);

                        SqlParameter mParm30 = new SqlParameter("@tqa_en_office", SqlDbType.VarChar);
                        mParm30.Direction = ParameterDirection.Input;
                        mParm30.Value = clsQHH.tqa_en_office == null ? DBNull.Value : (object)clsQHH.tqa_en_office;
                        pCmd.Parameters.Add(mParm30);

                        SqlParameter mParm31 = new SqlParameter("@tqa_en_of_mobile", SqlDbType.VarChar);
                        mParm31.Direction = ParameterDirection.Input;
                        mParm31.Value = clsQHH.tqa_en_of_mobile == null ? DBNull.Value : (object)clsQHH.tqa_en_of_mobile;
                        pCmd.Parameters.Add(mParm31);

                        SqlParameter mParm32 = new SqlParameter("@tqa_cont_address", SqlDbType.Char);
                        mParm32.Direction = ParameterDirection.Input;
                        mParm32.Value = clsQHH.tqa_cont_address == null ? DBNull.Value : (object)clsQHH.tqa_cont_address;
                        pCmd.Parameters.Add(mParm32);

                        SqlParameter mParm33 = new SqlParameter("@tqa_person_emer", SqlDbType.VarChar);
                        mParm33.Direction = ParameterDirection.Input;
                        mParm33.Value = clsQHH.tqa_person_emer == null ? DBNull.Value : (object)clsQHH.tqa_person_emer;
                        pCmd.Parameters.Add(mParm33);

                        SqlParameter mParm34 = new SqlParameter("@tqa_telep_emer", SqlDbType.VarChar);
                        mParm34.Direction = ParameterDirection.Input;
                        mParm34.Value = clsQHH.tqa_telep_emer == null ? DBNull.Value : (object)clsQHH.tqa_telep_emer;
                        pCmd.Parameters.Add(mParm34);

                        SqlParameter mParm35 = new SqlParameter("@tqa_prev_examined", SqlDbType.Char);
                        mParm35.Direction = ParameterDirection.Input;
                        mParm35.Value = clsQHH.tqa_prev_examined == null ? DBNull.Value : (object)clsQHH.tqa_prev_examined;
                        pCmd.Parameters.Add(mParm35);

                        SqlParameter mParm36 = new SqlParameter("@tqa_prev_exam_loc", SqlDbType.VarChar);
                        mParm36.Direction = ParameterDirection.Input;
                        mParm36.Value = clsQHH.tqa_prev_exam_loc == null ? DBNull.Value : (object)clsQHH.tqa_prev_exam_loc;
                        pCmd.Parameters.Add(mParm36);

                        SqlParameter mParm37 = new SqlParameter("@tqa_prev_exam_date", SqlDbType.VarChar);
                        mParm37.Direction = ParameterDirection.Input;
                        mParm37.Value = clsQHH.tqa_prev_exam_date == null ? DBNull.Value : (object)clsQHH.tqa_prev_exam_date;
                        pCmd.Parameters.Add(mParm37);

                        SqlParameter mParm38 = new SqlParameter("@tqa_prev_exam_deca", SqlDbType.Char);
                        mParm38.Direction = ParameterDirection.Input;
                        mParm38.Value = clsQHH.tqa_prev_exam_deca == null ? DBNull.Value : (object)clsQHH.tqa_prev_exam_deca;
                        pCmd.Parameters.Add(mParm38);

                        SqlParameter mParm39 = new SqlParameter("@tqa_med_waiver", SqlDbType.Char);
                        mParm39.Direction = ParameterDirection.Input;
                        mParm39.Value = clsQHH.tqa_med_waiver == null ? DBNull.Value : (object)clsQHH.tqa_med_waiver;
                        pCmd.Parameters.Add(mParm39);

                        SqlParameter mParm40 = new SqlParameter("@tqa_waiver_spec", SqlDbType.VarChar);
                        mParm40.Direction = ParameterDirection.Input;
                        mParm40.Value = clsQHH.tqa_waiver_spec == null ? DBNull.Value : (object)clsQHH.tqa_waiver_spec;
                        pCmd.Parameters.Add(mParm40);

                        SqlParameter mParm41 = new SqlParameter("@tqa_tot_fling_time", SqlDbType.Float);
                        mParm41.Direction = ParameterDirection.Input;
                        mParm41.Value = clsQHH.tqa_tot_fling_time == null ? DBNull.Value : (object)clsQHH.tqa_tot_fling_time;
                        pCmd.Parameters.Add(mParm41);

                        SqlParameter mParm42 = new SqlParameter("@tqa_last_six_time", SqlDbType.Float);
                        mParm42.Direction = ParameterDirection.Input;
                        mParm42.Value = clsQHH.tqa_last_six_time == null ? DBNull.Value : (object)clsQHH.tqa_last_six_time;
                        pCmd.Parameters.Add(mParm42);

                        SqlParameter mParm43 = new SqlParameter("@tqa_pres_aircraft", SqlDbType.VarChar);
                        mParm43.Direction = ParameterDirection.Input;
                        mParm43.Value = clsQHH.tqa_pres_aircraft == null ? DBNull.Value : (object)clsQHH.tqa_pres_aircraft;
                        pCmd.Parameters.Add(mParm43);

                        SqlParameter mParm44 = new SqlParameter("@tqa_aircraft_type", SqlDbType.Char);
                        mParm44.Direction = ParameterDirection.Input;
                        mParm44.Value = clsQHH.tqa_aircraft_type == null ? DBNull.Value : (object)clsQHH.tqa_aircraft_type;
                        pCmd.Parameters.Add(mParm44);

                        SqlParameter mParm45 = new SqlParameter("@tqa_aircraft_name", SqlDbType.VarChar);
                        mParm45.Direction = ParameterDirection.Input;
                        mParm45.Value = clsQHH.tqa_aircraft_name == null ? DBNull.Value : (object)clsQHH.tqa_aircraft_name;
                        pCmd.Parameters.Add(mParm45);

                        SqlParameter mParm46 = new SqlParameter("@tqa_aircraft_jet", SqlDbType.Bit);
                        mParm46.Direction = ParameterDirection.Input;
                        mParm46.Value = clsQHH.tqa_aircraft_jet == null ? DBNull.Value : (object)clsQHH.tqa_aircraft_jet;
                        pCmd.Parameters.Add(mParm46);

                        SqlParameter mParm47 = new SqlParameter("@tqa_aircraft_turbo", SqlDbType.Bit);
                        mParm47.Direction = ParameterDirection.Input;
                        mParm47.Value = clsQHH.tqa_aircraft_turbo == null ? DBNull.Value : (object)clsQHH.tqa_aircraft_turbo;
                        pCmd.Parameters.Add(mParm47);

                        SqlParameter mParm48 = new SqlParameter("@tqa_aircraft_heli", SqlDbType.Bit);
                        mParm48.Direction = ParameterDirection.Input;
                        mParm48.Value = clsQHH.tqa_aircraft_heli == null ? DBNull.Value : (object)clsQHH.tqa_aircraft_heli;
                        pCmd.Parameters.Add(mParm48);

                        SqlParameter mParm49 = new SqlParameter("@tqa_aircraft_piston", SqlDbType.Bit);
                        mParm49.Direction = ParameterDirection.Input;
                        mParm49.Value = clsQHH.tqa_aircraft_piston == null ? DBNull.Value : (object)clsQHH.tqa_aircraft_piston;
                        pCmd.Parameters.Add(mParm49);

                        SqlParameter mParm50 = new SqlParameter("@tqa_aircraft_other", SqlDbType.Bit);
                        mParm50.Direction = ParameterDirection.Input;
                        mParm50.Value = clsQHH.tqa_aircraft_other == null ? DBNull.Value : (object)clsQHH.tqa_aircraft_other;
                        pCmd.Parameters.Add(mParm50);

                        SqlParameter mParm51 = new SqlParameter("@tqa_aircraft_oth", SqlDbType.VarChar);
                        mParm51.Direction = ParameterDirection.Input;
                        mParm51.Value = clsQHH.tqa_aircraft_oth == null ? DBNull.Value : (object)clsQHH.tqa_aircraft_oth;
                        pCmd.Parameters.Add(mParm51);

                        SqlParameter mParm51_1 = new SqlParameter("@tqa_flying_status", SqlDbType.Char);
                        mParm51_1.Direction = ParameterDirection.Input;
                        mParm51_1.Value = clsQHH.tqa_flying_status == null ? DBNull.Value : (object)clsQHH.tqa_flying_status;
                        pCmd.Parameters.Add(mParm51_1);

                        SqlParameter mParm52 = new SqlParameter("@tqa_single_pilot", SqlDbType.Bit);
                        mParm52.Direction = ParameterDirection.Input;
                        mParm52.Value = clsQHH.tqa_single_pilot == null ? DBNull.Value : (object)clsQHH.tqa_single_pilot;
                        pCmd.Parameters.Add(mParm52);

                        SqlParameter mParm53 = new SqlParameter("@tqa_muti_pilot", SqlDbType.Bit);
                        mParm53.Direction = ParameterDirection.Input;
                        mParm53.Value = clsQHH.tqa_muti_pilot == null ? DBNull.Value : (object)clsQHH.tqa_muti_pilot;
                        pCmd.Parameters.Add(mParm53);

                        SqlParameter mParm54 = new SqlParameter("@tqa_smoking", SqlDbType.Char);
                        mParm54.Direction = ParameterDirection.Input;
                        mParm54.Value = clsQHH.tqa_smoking == null ? DBNull.Value : (object)clsQHH.tqa_smoking;
                        pCmd.Parameters.Add(mParm54);

                        SqlParameter mParm55 = new SqlParameter("@tqa_smoking_since", SqlDbType.VarChar);
                        mParm55.Direction = ParameterDirection.Input;
                        mParm55.Value = clsQHH.tqa_smoking_since == null ? DBNull.Value : (object)clsQHH.tqa_smoking_since;
                        pCmd.Parameters.Add(mParm55);

                        SqlParameter mParm56 = new SqlParameter("@tqa_smoking_type", SqlDbType.VarChar);
                        mParm56.Direction = ParameterDirection.Input;
                        mParm56.Value = clsQHH.tqa_smoking_type == null ? DBNull.Value : (object)clsQHH.tqa_smoking_type;
                        pCmd.Parameters.Add(mParm56);

                        SqlParameter mParm57 = new SqlParameter("@tqa_smoking_amt", SqlDbType.VarChar);
                        mParm57.Direction = ParameterDirection.Input;
                        mParm57.Value = clsQHH.tqa_smoking_amt == null ? DBNull.Value : (object)clsQHH.tqa_smoking_amt;
                        pCmd.Parameters.Add(mParm57);

                        SqlParameter mParm58 = new SqlParameter("@tqa_use_medicine", SqlDbType.Char);
                        mParm58.Direction = ParameterDirection.Input;
                        mParm58.Value = clsQHH.tqa_use_medicine == null ? DBNull.Value : (object)clsQHH.tqa_use_medicine;
                        pCmd.Parameters.Add(mParm58);

                        SqlParameter mParm59 = new SqlParameter("@tqa_med_name", SqlDbType.VarChar);
                        mParm59.Direction = ParameterDirection.Input;
                        mParm59.Value = clsQHH.tqa_med_name == null ? DBNull.Value : (object)clsQHH.tqa_med_name;
                        pCmd.Parameters.Add(mParm59);

                        SqlParameter mParm60 = new SqlParameter("@tqa_med_amount", SqlDbType.VarChar);
                        mParm60.Direction = ParameterDirection.Input;
                        mParm60.Value = clsQHH.tqa_med_amount == null ? DBNull.Value : (object)clsQHH.tqa_med_amount;
                        pCmd.Parameters.Add(mParm60);

                        SqlParameter mParm61 = new SqlParameter("@tqa_med_startdate", SqlDbType.VarChar);
                        mParm61.Direction = ParameterDirection.Input;
                        mParm61.Value = clsQHH.tqa_med_startdate == null ? DBNull.Value : (object)clsQHH.tqa_med_startdate;
                        pCmd.Parameters.Add(mParm61);

                        SqlParameter mParm62 = new SqlParameter("@tqa_med_reason", SqlDbType.VarChar);
                        mParm62.Direction = ParameterDirection.Input;
                        mParm62.Value = clsQHH.tqa_med_reason == null ? DBNull.Value : (object)clsQHH.tqa_med_reason;
                        pCmd.Parameters.Add(mParm62);

                        SqlParameter mParm63 = new SqlParameter("@tqa_avg_alcohal", SqlDbType.VarChar);
                        mParm63.Direction = ParameterDirection.Input;
                        mParm63.Value = clsQHH.tqa_avg_alcohal == null ? DBNull.Value : (object)clsQHH.tqa_avg_alcohal;
                        pCmd.Parameters.Add(mParm63);

                        SqlParameter mParm64 = new SqlParameter("@tqa_m20_exercise", SqlDbType.Char);
                        mParm64.Direction = ParameterDirection.Input;
                        mParm64.Value = clsQHH.tqa_m20_exercise == null ? DBNull.Value : (object)clsQHH.tqa_m20_exercise;
                        pCmd.Parameters.Add(mParm64);

                        SqlParameter mParm65 = new SqlParameter("@tqa_chis_freq", SqlDbType.Char);
                        mParm65.Direction = ParameterDirection.Input;
                        mParm65.Value = clsQHH.tqa_chis_freq == null ? DBNull.Value : (object)clsQHH.tqa_chis_freq;
                        pCmd.Parameters.Add(mParm65);

                        SqlParameter mParm66 = new SqlParameter("@tqa_chis_freq_rmk", SqlDbType.VarChar);
                        mParm66.Direction = ParameterDirection.Input;
                        mParm66.Value = clsQHH.tqa_chis_freq_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_freq_rmk;
                        pCmd.Parameters.Add(mParm66);

                        SqlParameter mParm67 = new SqlParameter("@tqa_chis_dizz", SqlDbType.Char);
                        mParm67.Direction = ParameterDirection.Input;
                        mParm67.Value = clsQHH.tqa_chis_dizz == null ? DBNull.Value : (object)clsQHH.tqa_chis_dizz;
                        pCmd.Parameters.Add(mParm67);

                        SqlParameter mParm68 = new SqlParameter("@tqa_chis_dizz_rmk", SqlDbType.VarChar);
                        mParm68.Direction = ParameterDirection.Input;
                        mParm68.Value = clsQHH.tqa_chis_dizz_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_dizz_rmk;
                        pCmd.Parameters.Add(mParm68);

                        SqlParameter mParm69 = new SqlParameter("@tqa_chis_unco", SqlDbType.Char);
                        mParm69.Direction = ParameterDirection.Input;
                        mParm69.Value = clsQHH.tqa_chis_unco == null ? DBNull.Value : (object)clsQHH.tqa_chis_unco;
                        pCmd.Parameters.Add(mParm69);

                        SqlParameter mParm70 = new SqlParameter("@tqa_chis_unco_rmk", SqlDbType.VarChar);
                        mParm70.Direction = ParameterDirection.Input;
                        mParm70.Value = clsQHH.tqa_chis_unco_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_unco_rmk;
                        pCmd.Parameters.Add(mParm70);

                        SqlParameter mParm71 = new SqlParameter("@tqa_chis_eyet", SqlDbType.Char);
                        mParm71.Direction = ParameterDirection.Input;
                        mParm71.Value = clsQHH.tqa_chis_eyet == null ? DBNull.Value : (object)clsQHH.tqa_chis_eyet;
                        pCmd.Parameters.Add(mParm71);

                        SqlParameter mParm72 = new SqlParameter("@tqa_chis_eyet_rmk", SqlDbType.VarChar);
                        mParm72.Direction = ParameterDirection.Input;
                        mParm72.Value = clsQHH.tqa_chis_eyet_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_eyet_rmk;
                        pCmd.Parameters.Add(mParm72);

                        SqlParameter mParm73 = new SqlParameter("@tqa_chis_hayf", SqlDbType.Char);
                        mParm73.Direction = ParameterDirection.Input;
                        mParm73.Value = clsQHH.tqa_chis_hayf == null ? DBNull.Value : (object)clsQHH.tqa_chis_hayf;
                        pCmd.Parameters.Add(mParm73);

                        SqlParameter mParm74 = new SqlParameter("@tqa_chis_hayf_rmk", SqlDbType.VarChar);
                        mParm74.Direction = ParameterDirection.Input;
                        mParm74.Value = clsQHH.tqa_chis_hayf_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_hayf_rmk;
                        pCmd.Parameters.Add(mParm74);

                        SqlParameter mParm75 = new SqlParameter("@tqa_chis_hert", SqlDbType.Char);
                        mParm75.Direction = ParameterDirection.Input;
                        mParm75.Value = clsQHH.tqa_chis_hert == null ? DBNull.Value : (object)clsQHH.tqa_chis_hert;
                        pCmd.Parameters.Add(mParm75);

                        SqlParameter mParm76 = new SqlParameter("@tqa_chis_hert_rmk", SqlDbType.VarChar);
                        mParm76.Direction = ParameterDirection.Input;
                        mParm76.Value = clsQHH.tqa_chis_hert_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_hert_rmk;
                        pCmd.Parameters.Add(mParm76);

                        SqlParameter mParm77 = new SqlParameter("@tqa_chis_chst", SqlDbType.Char);
                        mParm77.Direction = ParameterDirection.Input;
                        mParm77.Value = clsQHH.tqa_chis_chst == null ? DBNull.Value : (object)clsQHH.tqa_chis_chst;
                        pCmd.Parameters.Add(mParm77);

                        SqlParameter mParm78 = new SqlParameter("@tqa_chis_chst_rmk", SqlDbType.VarChar);
                        mParm78.Direction = ParameterDirection.Input;
                        mParm78.Value = clsQHH.tqa_chis_chst_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_chst_rmk;
                        pCmd.Parameters.Add(mParm78);

                        SqlParameter mParm79 = new SqlParameter("@tqa_chis_high", SqlDbType.Char);
                        mParm79.Direction = ParameterDirection.Input;
                        mParm79.Value = clsQHH.tqa_chis_high == null ? DBNull.Value : (object)clsQHH.tqa_chis_high;
                        pCmd.Parameters.Add(mParm79);

                        SqlParameter mParm80 = new SqlParameter("@tqa_chis_high_rmk", SqlDbType.VarChar);
                        mParm80.Direction = ParameterDirection.Input;
                        mParm80.Value = clsQHH.tqa_chis_high_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_high_rmk;
                        pCmd.Parameters.Add(mParm80);

                        SqlParameter mParm81 = new SqlParameter("@tqa_chis_stom", SqlDbType.Char);
                        mParm81.Direction = ParameterDirection.Input;
                        mParm81.Value = clsQHH.tqa_chis_stom == null ? DBNull.Value : (object)clsQHH.tqa_chis_stom;
                        pCmd.Parameters.Add(mParm81);

                        SqlParameter mParm82 = new SqlParameter("@tqa_chis_stom_rmk", SqlDbType.VarChar);
                        mParm82.Direction = ParameterDirection.Input;
                        mParm82.Value = clsQHH.tqa_chis_stom_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_stom_rmk;
                        pCmd.Parameters.Add(mParm82);

                        SqlParameter mParm83 = new SqlParameter("@tqa_chis_jaun", SqlDbType.Char);
                        mParm83.Direction = ParameterDirection.Input;
                        mParm83.Value = clsQHH.tqa_chis_jaun == null ? DBNull.Value : (object)clsQHH.tqa_chis_jaun;
                        pCmd.Parameters.Add(mParm83);

                        SqlParameter mParm84 = new SqlParameter("@tqa_chis_jaun_rmk", SqlDbType.VarChar);
                        mParm84.Direction = ParameterDirection.Input;
                        mParm84.Value = clsQHH.tqa_chis_jaun_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_jaun_rmk;
                        pCmd.Parameters.Add(mParm84);

                        SqlParameter mParm85 = new SqlParameter("@tqa_chis_kidn", SqlDbType.Char);
                        mParm85.Direction = ParameterDirection.Input;
                        mParm85.Value = clsQHH.tqa_chis_kidn == null ? DBNull.Value : (object)clsQHH.tqa_chis_kidn;
                        pCmd.Parameters.Add(mParm85);

                        SqlParameter mParm86 = new SqlParameter("@tqa_chis_kidn_rmk", SqlDbType.VarChar);
                        mParm86.Direction = ParameterDirection.Input;
                        mParm86.Value = clsQHH.tqa_chis_kidn_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_kidn_rmk;
                        pCmd.Parameters.Add(mParm86);


                        SqlParameter mParm87 = new SqlParameter("@tqa_chis_suga", SqlDbType.Char);
                        mParm87.Direction = ParameterDirection.Input;
                        mParm87.Value = clsQHH.tqa_chis_suga == null ? DBNull.Value : (object)clsQHH.tqa_chis_suga;
                        pCmd.Parameters.Add(mParm87);


                        SqlParameter mParm88 = new SqlParameter("@tqa_chis_suga_rmk", SqlDbType.VarChar);
                        mParm88.Direction = ParameterDirection.Input;
                        mParm88.Value = clsQHH.tqa_chis_suga_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_suga_rmk;
                        pCmd.Parameters.Add(mParm88);


                        SqlParameter mParm89 = new SqlParameter("@tqa_chis_epil", SqlDbType.Char);
                        mParm89.Direction = ParameterDirection.Input;
                        mParm89.Value = clsQHH.tqa_chis_epil == null ? DBNull.Value : (object)clsQHH.tqa_chis_epil;
                        pCmd.Parameters.Add(mParm89);

                        SqlParameter mParm90 = new SqlParameter("@tqa_chis_epil_rmk", SqlDbType.VarChar);
                        mParm90.Direction = ParameterDirection.Input;
                        mParm90.Value = clsQHH.tqa_chis_epil_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_epil_rmk;
                        pCmd.Parameters.Add(mParm90);


                        SqlParameter mParm91 = new SqlParameter("@tqa_chis_nurv", SqlDbType.Char);
                        mParm91.Direction = ParameterDirection.Input;
                        mParm91.Value = clsQHH.tqa_chis_nurv == null ? DBNull.Value : (object)clsQHH.tqa_chis_nurv;
                        pCmd.Parameters.Add(mParm91);


                        SqlParameter mParm92 = new SqlParameter("@tqa_chis_nurv_rmk", SqlDbType.VarChar);
                        mParm92.Direction = ParameterDirection.Input;
                        mParm92.Value = clsQHH.tqa_chis_nurv_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_nurv_rmk;
                        pCmd.Parameters.Add(mParm92);

                        SqlParameter mParm93 = new SqlParameter("@tqa_chis_temp", SqlDbType.Char);
                        mParm93.Direction = ParameterDirection.Input;
                        mParm93.Value = clsQHH.tqa_chis_temp == null ? DBNull.Value : (object)clsQHH.tqa_chis_temp;
                        pCmd.Parameters.Add(mParm93);

                        SqlParameter mParm94 = new SqlParameter("@tqa_chis_temp_rmk", SqlDbType.VarChar);
                        mParm94.Direction = ParameterDirection.Input;
                        mParm94.Value = clsQHH.tqa_chis_temp_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_temp_rmk;
                        pCmd.Parameters.Add(mParm94);

                        SqlParameter mParm95 = new SqlParameter("@tqa_chis_drug", SqlDbType.Char);
                        mParm95.Direction = ParameterDirection.Input;
                        mParm95.Value = clsQHH.tqa_chis_drug == null ? DBNull.Value : (object)clsQHH.tqa_chis_drug;
                        pCmd.Parameters.Add(mParm95);

                        SqlParameter mParm96 = new SqlParameter("@tqa_chis_drug_rmk", SqlDbType.VarChar);
                        mParm96.Direction = ParameterDirection.Input;
                        mParm96.Value = clsQHH.tqa_chis_drug_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_drug_rmk;
                        pCmd.Parameters.Add(mParm96);

                        SqlParameter mParm97 = new SqlParameter("@tqa_chis_suic", SqlDbType.Char);
                        mParm97.Direction = ParameterDirection.Input;
                        mParm97.Value = clsQHH.tqa_chis_suic == null ? DBNull.Value : (object)clsQHH.tqa_chis_suic;
                        pCmd.Parameters.Add(mParm97);

                        SqlParameter mParm98 = new SqlParameter("@tqa_chis_suic_rmk", SqlDbType.VarChar);
                        mParm98.Direction = ParameterDirection.Input;
                        mParm98.Value = clsQHH.tqa_chis_suic_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_suic_rmk;
                        pCmd.Parameters.Add(mParm98);

                        SqlParameter mParm99 = new SqlParameter("@tqa_chis_losw", SqlDbType.Char);
                        mParm99.Direction = ParameterDirection.Input;
                        mParm99.Value = clsQHH.tqa_chis_losw == null ? DBNull.Value : (object)clsQHH.tqa_chis_losw;
                        pCmd.Parameters.Add(mParm99);

                        SqlParameter mParm100 = new SqlParameter("@tqa_chis_losw_rmk", SqlDbType.VarChar);
                        mParm100.Direction = ParameterDirection.Input;
                        mParm100.Value = clsQHH.tqa_chis_losw_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_losw_rmk;
                        pCmd.Parameters.Add(mParm100);

                        SqlParameter mParm101 = new SqlParameter("@tqa_chis_moti", SqlDbType.Char);
                        mParm101.Direction = ParameterDirection.Input;
                        mParm101.Value = clsQHH.tqa_chis_moti == null ? DBNull.Value : (object)clsQHH.tqa_chis_moti;
                        pCmd.Parameters.Add(mParm101);

                        SqlParameter mParm102 = new SqlParameter("@tqa_chis_moti_rmk", SqlDbType.VarChar);
                        mParm102.Direction = ParameterDirection.Input;
                        mParm102.Value = clsQHH.tqa_chis_moti_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_moti_rmk;
                        pCmd.Parameters.Add(mParm102);

                        SqlParameter mParm103 = new SqlParameter("@tqa_chis_reje", SqlDbType.Char);
                        mParm103.Direction = ParameterDirection.Input;
                        mParm103.Value = clsQHH.tqa_chis_reje == null ? DBNull.Value : (object)clsQHH.tqa_chis_reje;
                        pCmd.Parameters.Add(mParm103);

                        SqlParameter mParm104 = new SqlParameter("@tqa_chis_reje_rmk", SqlDbType.VarChar);
                        mParm104.Direction = ParameterDirection.Input;
                        mParm104.Value = clsQHH.tqa_chis_reje_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_reje_rmk;
                        pCmd.Parameters.Add(mParm104);

                        SqlParameter mParm105 = new SqlParameter("@tqa_chis_adms", SqlDbType.Char);
                        mParm105.Direction = ParameterDirection.Input;
                        mParm105.Value = clsQHH.tqa_chis_adms == null ? DBNull.Value : (object)clsQHH.tqa_chis_adms;
                        pCmd.Parameters.Add(mParm105);

                        SqlParameter mParm106 = new SqlParameter("@tqa_chis_adms_rmk", SqlDbType.VarChar);
                        mParm106.Direction = ParameterDirection.Input;
                        mParm106.Value = clsQHH.tqa_chis_adms_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_adms_rmk;
                        pCmd.Parameters.Add(mParm106);

                        SqlParameter mParm107 = new SqlParameter("@tqa_chis_avia", SqlDbType.Char);
                        mParm107.Direction = ParameterDirection.Input;
                        mParm107.Value = clsQHH.tqa_chis_avia == null ? DBNull.Value : (object)clsQHH.tqa_chis_avia;
                        pCmd.Parameters.Add(mParm107);

                        SqlParameter mParm108 = new SqlParameter("@tqa_chis_avia_rmk", SqlDbType.VarChar);
                        mParm108.Direction = ParameterDirection.Input;
                        mParm108.Value = clsQHH.tqa_chis_avia_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_avia_rmk;
                        pCmd.Parameters.Add(mParm108);

                        SqlParameter mParm109 = new SqlParameter("@tqa_chis_otha", SqlDbType.Char);
                        mParm109.Direction = ParameterDirection.Input;
                        mParm109.Value = clsQHH.tqa_chis_otha == null ? DBNull.Value : (object)clsQHH.tqa_chis_otha;
                        pCmd.Parameters.Add(mParm109);

                        SqlParameter mParm110 = new SqlParameter("@tqa_chis_otha_rmk", SqlDbType.VarChar);
                        mParm110.Direction = ParameterDirection.Input;
                        mParm110.Value = clsQHH.tqa_chis_otha_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_otha_rmk;
                        pCmd.Parameters.Add(mParm110);

                        SqlParameter mParm111 = new SqlParameter("@tqa_chis_gyna", SqlDbType.Char);
                        mParm111.Direction = ParameterDirection.Input;
                        mParm111.Value = clsQHH.tqa_chis_gyna == null ? DBNull.Value : (object)clsQHH.tqa_chis_gyna;
                        pCmd.Parameters.Add(mParm111);

                        SqlParameter mParm112 = new SqlParameter("@tqa_chis_gyna_rmk", SqlDbType.VarChar);
                        mParm112.Direction = ParameterDirection.Input;
                        mParm112.Value = clsQHH.tqa_chis_gyna_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_gyna_rmk;
                        pCmd.Parameters.Add(mParm112);

                        SqlParameter mParm113 = new SqlParameter("@tqa_chis_othi", SqlDbType.Char);
                        mParm113.Direction = ParameterDirection.Input;
                        mParm113.Value = clsQHH.tqa_chis_othi == null ? DBNull.Value : (object)clsQHH.tqa_chis_othi;
                        pCmd.Parameters.Add(mParm113);

                        SqlParameter mParm114 = new SqlParameter("@tqa_chis_othi_rmk", SqlDbType.VarChar);
                        mParm114.Direction = ParameterDirection.Input;
                        mParm114.Value = clsQHH.tqa_chis_othi_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_othi_rmk;
                        pCmd.Parameters.Add(mParm114);

                        SqlParameter mParm115 = new SqlParameter("@tqa_chis_heth", SqlDbType.Char);
                        mParm115.Direction = ParameterDirection.Input;
                        mParm115.Value = clsQHH.tqa_chis_heth == null ? DBNull.Value : (object)clsQHH.tqa_chis_heth;
                        pCmd.Parameters.Add(mParm115);

                        SqlParameter mParm116 = new SqlParameter("@tqa_chis_heth_rmk", SqlDbType.VarChar);
                        mParm116.Direction = ParameterDirection.Input;
                        mParm116.Value = clsQHH.tqa_chis_heth_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_heth_rmk;
                        pCmd.Parameters.Add(mParm116);

                        SqlParameter mParm117 = new SqlParameter("@tqa_chis_lung", SqlDbType.Char);
                        mParm117.Direction = ParameterDirection.Input;
                        mParm117.Value = clsQHH.tqa_chis_lung == null ? DBNull.Value : (object)clsQHH.tqa_chis_lung;
                        pCmd.Parameters.Add(mParm117);

                        SqlParameter mParm118 = new SqlParameter("@tqa_chis_lung_rmk", SqlDbType.VarChar);
                        mParm118.Direction = ParameterDirection.Input;
                        mParm118.Value = clsQHH.tqa_chis_lung_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_lung_rmk;
                        pCmd.Parameters.Add(mParm118);

                        SqlParameter mParm119 = new SqlParameter("@tqa_chis_alco", SqlDbType.Char);
                        mParm119.Direction = ParameterDirection.Input;
                        mParm119.Value = clsQHH.tqa_chis_alco == null ? DBNull.Value : (object)clsQHH.tqa_chis_alco;
                        pCmd.Parameters.Add(mParm119);

                        SqlParameter mParm120 = new SqlParameter("@tqa_chis_alco_rmk", SqlDbType.VarChar);
                        mParm120.Direction = ParameterDirection.Input;
                        mParm120.Value = clsQHH.tqa_chis_alco_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_alco_rmk;
                        pCmd.Parameters.Add(mParm120);

                        SqlParameter mParm121 = new SqlParameter("@tqa_chis_ment", SqlDbType.Char);
                        mParm121.Direction = ParameterDirection.Input;
                        mParm121.Value = clsQHH.tqa_chis_ment == null ? DBNull.Value : (object)clsQHH.tqa_chis_ment;
                        pCmd.Parameters.Add(mParm121);

                        SqlParameter mParm122 = new SqlParameter("@tqa_chis_ment_rmk", SqlDbType.VarChar);
                        mParm122.Direction = ParameterDirection.Input;
                        mParm122.Value = clsQHH.tqa_chis_ment_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_ment_rmk;
                        pCmd.Parameters.Add(mParm122);

                        SqlParameter mParm123 = new SqlParameter("@tqa_chis_fam_his", SqlDbType.Char);
                        mParm123.Direction = ParameterDirection.Input;
                        mParm123.Value = clsQHH.tqa_chis_fam_his == null ? DBNull.Value : (object)clsQHH.tqa_chis_fam_his;
                        pCmd.Parameters.Add(mParm123);

                        SqlParameter mParm124 = new SqlParameter("@tqa_chis_fam_diab", SqlDbType.Bit);
                        mParm124.Direction = ParameterDirection.Input;
                        mParm124.Value = clsQHH.tqa_chis_fam_diab == null ? DBNull.Value : (object)clsQHH.tqa_chis_fam_diab;
                        pCmd.Parameters.Add(mParm124);

                        SqlParameter mParm125 = new SqlParameter("@tqa_chis_fam_card", SqlDbType.Bit);
                        mParm125.Direction = ParameterDirection.Input;
                        mParm125.Value = clsQHH.tqa_chis_fam_card == null ? DBNull.Value : (object)clsQHH.tqa_chis_fam_card;
                        pCmd.Parameters.Add(mParm125);

                        SqlParameter mParm126 = new SqlParameter("@tqa_chis_fam_ment", SqlDbType.Bit);
                        mParm126.Direction = ParameterDirection.Input;
                        mParm126.Value = clsQHH.tqa_chis_fam_ment == null ? DBNull.Value : (object)clsQHH.tqa_chis_fam_ment;
                        pCmd.Parameters.Add(mParm126);

                        SqlParameter mParm127 = new SqlParameter("@tqa_chis_conviction", SqlDbType.Char);
                        mParm127.Direction = ParameterDirection.Input;
                        mParm127.Value = clsQHH.tqa_chis_conviction == null ? DBNull.Value : (object)clsQHH.tqa_chis_conviction;
                        pCmd.Parameters.Add(mParm127);

                        SqlParameter mParm128 = new SqlParameter("@tqa_chis_conv_rmk", SqlDbType.VarChar);
                        mParm128.Direction = ParameterDirection.Input;
                        mParm128.Value = clsQHH.tqa_chis_conv_rmk == null ? DBNull.Value : (object)clsQHH.tqa_chis_conv_rmk;
                        pCmd.Parameters.Add(mParm128);

                        SqlParameter mParm129 = new SqlParameter("@tqa_remark", SqlDbType.VarChar);
                        mParm129.Direction = ParameterDirection.Input;
                        mParm129.Value = clsQHH.tqa_remark == null ? DBNull.Value : (object)clsQHH.tqa_remark;
                        pCmd.Parameters.Add(mParm129);
                                              

                        pCmd.ExecuteNonQuery();
                        pTrans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    pTrans.Rollback();
                    ex.Message.ToString();
                }
                finally
                {

                    if (pConn.State == ConnectionState.Open)
                    {
                        pConn.Close();
                        pConn.Dispose();
                    }
                }
            }


        }
    }
}
