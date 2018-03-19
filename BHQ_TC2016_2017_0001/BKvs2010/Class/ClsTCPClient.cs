using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using DBCheckup;
using System.IO;

namespace BKvs2010
{
    public class ClsTCPClient : IDisposable
    {
        public StatusTransaction sendCallUnitDisplay()
        {
            try
            {
                try
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        var useDis = cdc.mst_project_configs.Where(x => x.mpc_code == "UseUnitDisplay").Select(x => x.mpc_value).FirstOrDefault();
                        if (!string.IsNullOrEmpty(useDis)) PrePareData.StaticDataCls.UseUnitDisplay = useDis == "true" ? true : false;
                    }
                }
                catch
                {

                }
                if (PrePareData.StaticDataCls.UseUnitDisplay)
                {
                    int tpr_id = Program.CurrentRegis.tpr_id;
                    int mhs_id = Program.CurrentSite.mhs_id;
                    StatusTransaction chkVip = new Class.FunctionDataCls().chkVipType(tpr_id, mhs_id);
                    if (chkVip == StatusTransaction.False)
                    {
                        int mrd_id = Program.CurrentRoom.mrd_id;
                        int? mvt_id = Program.CurrentPatient_queue.mvt_id;
                        return sendCallUnitDisplay(tpr_id, mhs_id, mrd_id, (int)mvt_id);
                    }
                    else if (chkVip == StatusTransaction.True)
                    {
                        return StatusTransaction.False;
                    }
                    else
                    {
                        return chkVip;
                    }
                }
                else
                {
                    return StatusTransaction.False;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("ClsTCPClient", "sendCallUnitDisplay()", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction sendCallUnitDisplay(int tpr_id, int mhs_id, int mrd_id, int mvt_id)
        {
            try
            {
                string ipAddress = "";
                string stringPort = "";
                StatusTransaction getIp = getConfigMessage(ref ipAddress, mhs_id, "IUD");
                StatusTransaction getPort = getConfigMessage(ref stringPort, mhs_id, "PUD");
                int port = Convert.ToInt32(stringPort);

                double loopConfig = 0;
                StatusTransaction getLoopConfig = getConfigMessage(ref loopConfig, mhs_id, "RDQ");
                int loop = 0;
                if (loopConfig != 0)
                {
                    try
                    {
                        loop = Convert.ToInt32(loopConfig);
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError("ClsTCPClient", "sendCallUnitDisplay", ex, false);
                    }
                }
                if (getIp == StatusTransaction.True && getPort == StatusTransaction.True)
                {
                    if (!string.IsNullOrEmpty(ipAddress) && !string.IsNullOrEmpty(stringPort))
                    {
                        ipAddress = ipAddress.Replace(@"\\", "");
                        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                        {
                            trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                            List<string> list_temp_message = new List<string>();
                            StatusTransaction getMessage = getMessageUnitDisplay(mhs_id, mrd_id, mvt_id, ref list_temp_message);
                            foreach (string message in list_temp_message)
                            {
                                string msg = tpr.trn_patient.tpt_nation_code + message + tpr.tpr_queue_no;
                                if (loop == 0)
                                {
                                    sendMessageTCPServer(msg);
                                }
                                else
                                {
                                    sendMessageTCPServer(msg, loop);
                                }
                            }
                            return StatusTransaction.True;
                            //if (getMessage == StatusTransaction.True)
                            //{
                            //    List<string> list_message = new List<string>();
                            //    foreach (string msg in list_temp_message)
                            //    {
                            //        list_message.Add(tpr.trn_patient.tpt_nation_code + msg + tpr.tpr_queue_no);
                            //    }
                            //    return sendMessageToUnitDisplay(ipAddress, port, list_message);
                            //}
                            //else
                            //{
                            //    return StatusTransaction.Error;
                            //}
                        }
                    }
                    else
                    {
                        return StatusTransaction.Error;
                    }
                }
                else
                {
                    return StatusTransaction.Error;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("ClsTCPClient", "sendCallUnitDisplay", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction sendClearUnitDisplay()
        {
            try
            {
                if (PrePareData.StaticDataCls.UseUnitDisplay)
                {
                    int tpr_id = 0;
                    if (Program.CurrentRegis != null) tpr_id = Program.CurrentRegis.tpr_id;
                    int mhs_id = Program.CurrentSite.mhs_id;
                    StatusTransaction chkVip = new Class.FunctionDataCls().chkVipType(tpr_id, mhs_id);
                    if (chkVip == StatusTransaction.False)
                    {
                        int mrd_id = 0;
                        if (Program.CurrentRoom != null) mrd_id = Program.CurrentRoom.mrd_id;
                        StatusTransaction isCheckP = isCheckPoint(mrd_id);
                        if (isCheckP == StatusTransaction.False)
                        {
                            int? mvt_id = Program.CurrentPatient_queue.mvt_id;
                            return sendClearUnitDisplay(tpr_id, mhs_id, mrd_id, (int)mvt_id);
                        }
                        else if (isCheckP == StatusTransaction.True)
                        {
                            return StatusTransaction.False;
                        }
                        else
                        {
                            return isCheckP;
                        }
                    }
                    else if (chkVip == StatusTransaction.True)
                    {
                        return StatusTransaction.False;
                    }
                    else
                    {
                        return chkVip;
                    }
                }
                else
                {
                    return StatusTransaction.False;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("ClsTCPClient", "sendClearUnitDisplay()", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction sendClearUnitDisplay(int tpr_id, int mhs_id, int mrd_id, int mvt_id)
        {
            try
            {
                string ipAddress = "";
                string stringPort = "";
                StatusTransaction getIp = getConfigMessage(ref ipAddress, mhs_id, "IUD");
                StatusTransaction getPort = getConfigMessage(ref stringPort, mhs_id, "PUD");
                int port = Convert.ToInt32(stringPort);

                double loopConfig = 0;
                StatusTransaction getLoopConfig = getConfigMessage(ref loopConfig, mhs_id, "RDQ");
                int loop = 0;
                if (loopConfig != 0)
                {
                    try
                    {
                        loop = Convert.ToInt32(loopConfig);
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError("ClsTCPClient", "sendCallUnitDisplay", ex, false);
                    }
                }
                if (getIp == StatusTransaction.True && getPort == StatusTransaction.True)
                {
                    if (!string.IsNullOrEmpty(ipAddress) && !string.IsNullOrEmpty(stringPort))
                    {
                        ipAddress = ipAddress.Replace(@"\\", "");
                        List<string> list_temp_message = new List<string>();
                        StatusTransaction getMessage = getMessageUnitDisplay(mhs_id, mrd_id, mvt_id, ref list_temp_message);
                        if (getMessage == StatusTransaction.True)
                        {
                            if (tpr_id != 0)
                            {
                                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                                {
                                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();

                                    foreach (string message in list_temp_message)
                                    {
                                        string msg = tpr.trn_patient.tpt_nation_code + message + "00000";
                                        if (loop == 0)
                                        {
                                            sendMessageTCPServer(msg);
                                        }
                                        else
                                        {
                                            sendMessageTCPServer(msg, loop);
                                        }
                                    }
                                    return StatusTransaction.True;

                                    //List<string> list_message = new List<string>();
                                    //foreach (string msg in list_temp_message)
                                    //{
                                    //    list_message.Add(tpr.trn_patient.tpt_nation_code + msg + "00000");
                                    //}
                                    //return sendMessageToUnitDisplay(ipAddress, port, list_message);
                                }
                            }
                            else
                            {
                                foreach (string message in list_temp_message)
                                {
                                    string msg = "TH" + message + "00000";
                                    if (loop == 0)
                                    {
                                        sendMessageTCPServer(msg);
                                    }
                                    else
                                    {
                                        sendMessageTCPServer(msg, loop);
                                    }
                                }
                                return StatusTransaction.True;
                                //List<string> list_message = new List<string>();
                                //foreach (string msg in list_temp_message)
                                //{
                                //    list_message.Add("TH" + msg + "00000");
                                //}
                                //return sendMessageToUnitDisplay(ipAddress, port, list_message);
                            }
                        }
                        else
                        {
                            return StatusTransaction.Error;
                        }
                    }
                    else
                    {
                        return StatusTransaction.Error;
                    }
                }
                else
                {
                    return StatusTransaction.Error;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("ClsTCPClient", "sendClearUnitDisplay", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction sendMessageTCPServer(string message, int limitLoop = 10)
        {
            try
            {
                Socket m_socClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                RemoteIPAddress objRemoteIPAddress = new RemoteIPAddress();
                System.Net.IPEndPoint remoteEndPoint = new System.Net.IPEndPoint(objRemoteIPAddress.GetObjectIPAddress(), GetPort());

                byte[] byData = System.Text.Encoding.ASCII.GetBytes(message);

                int loopSend = 0;
                do
                {
                    try
                    {
                        loopSend++;
                        m_socClient.Connect(remoteEndPoint);
                        if (m_socClient.Connected)
                        {
                            m_socClient.Send(byData);
                            return StatusTransaction.True;
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                    catch
                    {

                    }
                    finally
                    {
                        if (m_socClient.Connected) m_socClient.Close();
                    }
                }
                while (loopSend < limitLoop);
                return StatusTransaction.False;
            }
            catch (Exception ex)
            {
                Program.MessageError("ClsTCPClient", "sendMessageTCPServer", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction sendMessageToUnitDisplay(string ipAddress, int port, List<string> list_message)
        {
            try
            {
                Socket m_socClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                RemoteIPAddress objRemoteIPAddress = new RemoteIPAddress();
                System.Net.IPAddress remoteIPAddress = System.Net.IPAddress.Parse(ipAddress);
                System.Net.IPEndPoint remoteEndPoint = new System.Net.IPEndPoint(remoteIPAddress, port);

                m_socClient.Connect(remoteEndPoint);
                foreach (string msg in list_message)
                {
                    byte[] byData = System.Text.Encoding.ASCII.GetBytes(msg);
                    m_socClient.Send(byData);
                    if (list_message.IndexOf(msg) != list_message.Count() - 1) System.Threading.Thread.Sleep(500);
                }
                m_socClient.Close();
                return StatusTransaction.True;
            }
            catch (Exception ex)
            {
                Program.MessageError("ClsTCPClient", "sendMessegeToUnitDisplay", ex, false);
                return StatusTransaction.Error;
            }
        }
        private StatusTransaction getConfigMessage(ref string configResult, int mhs_id, string configCode)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    DateTime dateNow = Program.GetServerDateTime();
                    string ip = cdc.mst_config_dtls
                                   .Where(x => x.mst_config_hdr.mfh_code == configCode &&
                                               x.mst_config_hdr.mhs_id == mhs_id &&
                                               x.mst_config_hdr.mfh_status == 'A' &&
                                               x.mst_config_hdr.mfh_effective_date.Value.Date <= dateNow &&
                                               ((x.mst_config_hdr.mfh_expire_date == null) ? true : x.mst_config_hdr.mfh_expire_date.Value.Date >= dateNow) &&
                                               x.mfd_status == 'A' &&
                                               x.mfd_effective_date.Value.Date <= dateNow &&
                                               ((x.mfd_expire_date == null) ? true : x.mfd_expire_date.Value.Date >= dateNow))
                                   .Select(x => x.mfd_text).FirstOrDefault();
                    configResult = ip;
                    return StatusTransaction.True;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("ClsTCPClient", "getConfigMessage", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction getConfigMessage(ref double configResult, int mhs_id, string configCode)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    DateTime dateNow = Program.GetServerDateTime();
                    double result = cdc.mst_config_dtls
                                       .Where(x => x.mst_config_hdr.mfh_code == configCode &&
                                                   x.mst_config_hdr.mhs_id == mhs_id &&
                                                   x.mst_config_hdr.mfh_status == 'A' &&
                                                   x.mst_config_hdr.mfh_effective_date.Value.Date <= dateNow &&
                                                   ((x.mst_config_hdr.mfh_expire_date == null) ? true : x.mst_config_hdr.mfh_expire_date.Value.Date >= dateNow) &&
                                                   x.mfd_status == 'A' &&
                                                   x.mfd_effective_date.Value.Date <= dateNow &&
                                                   ((x.mfd_expire_date == null) ? true : x.mfd_expire_date.Value.Date >= dateNow))
                                       .Select(x => (double)x.mfd_value).FirstOrDefault();
                    configResult = result;
                    return StatusTransaction.True;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("ClsTCPClient", "getConfigMessage", ex, false);
                return StatusTransaction.Error;
            }
        }
        private StatusTransaction getMessageUnitDisplay(int mhs_id, int mrd_id, int mvt_id, ref List<string> list_message)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    mst_room_dtl mrd = cdc.mst_room_dtls.Where(x => x.mrd_id == mrd_id).FirstOrDefault();

                    string mvt_code = cdc.mst_events.Where(x => x.mvt_id == mvt_id).Select(x => x.mvt_code).FirstOrDefault();
                    string mrm_code = cdc.mst_room_hdrs.Where(x => x.mrm_id == mrd.mrm_id).Select(x => x.mrm_code).FirstOrDefault();
                    List<string> list_zone = cdc.mst_room_screens
                                                .Where(x => x.mrm_code == mrm_code &&
                                                            x.mvt_code == mvt_code &&
                                                            x.mhs_id == mhs_id &&
                                                            x.mrs_status == 'A' &&
                                                            x.mrs_effective_date.Value.Date <= DateTime.Now.Date &&
                                                            ((x.mrs_expire_date == null) ? true : x.mrs_expire_date.Value.Date >= DateTime.Now.Date))
                                                .Select(x => x.mze_code).ToList();
                    foreach (string zone in list_zone)
                    {
                        list_message.Add("_" + mhs_id.ToString() + zone + "_" + mrd.mrd_room_no + "_");
                    }
                    return StatusTransaction.True;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("ClsTCPClient", "getMessageUnitDisplay", ex, false);
                return StatusTransaction.Error;
            }
        }
        private StatusTransaction isCheckPoint(int mrd_id)
        {
            try
            {
                if (mrd_id != 0)
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        mst_room_dtl mrd = cdc.mst_room_dtls.Where(x => x.mrd_id == mrd_id).FirstOrDefault();
                        if (mrd.mst_room_hdr.mrm_code == "CB" || mrd.mst_room_hdr.mrm_code == "CC")
                        {
                            return StatusTransaction.True;
                        }
                        else
                        {
                            return StatusTransaction.False;
                        }
                    }
                }
                else
                {
                    return StatusTransaction.True;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("ClsTCPClient", "isCheckPoint", ex, false);
                return StatusTransaction.Error;
            }
        }



        public StatusTransaction sendMessegeToUnitDisplay(List<string> list_message)
        {
            Socket socket = null;
            //try
            //{
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, false);

                string ipAddress = new RemoteIPAddress().GetRemoteIPAddress();
                IAsyncResult result = socket.BeginConnect(ipAddress, GetPort(), null, null);
                bool success = result.AsyncWaitHandle.WaitOne(new TimeSpan(0, 0, 30), true);

                if (success)
                {
                    if (socket.Connected)
                    {
                        foreach (string msg in list_message)
                        {
                            byte[] byData = System.Text.Encoding.ASCII.GetBytes(msg);
                            socket.Send(byData);
                        }
                    }
                }
                else
                {
                    Program.MessageError("ClsTCPClient", "sendMessegeToUnitDisplay", "connect port not success", false);
                }
                if (null != socket) socket.Close();
                return StatusTransaction.True;
            //}
            //catch (Exception ex)
            //{
            //    Program.MessageError("ClsTCPClient", "sendMessegeToUnitDisplay", ex.Message, false);
            //    return StatusTransaction.Error;
            //}
            //finally
            //{
            //    if (null != socket) socket.Close();
            //}
        }

        public void sendMessegeToUnitDisplay(string Messege)
         {
            Socket m_socClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            RemoteIPAddress objRemoteIPAddress = new RemoteIPAddress();
            System.Net.IPEndPoint remoteEndPoint = new System.Net.IPEndPoint(objRemoteIPAddress.GetObjectIPAddress(), GetPort());

            sendNew:
            try
            {
                m_socClient.Connect(remoteEndPoint);
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(Messege);
                m_socClient.Send(byData);
                m_socClient.Close();
            }
            catch
            {
                goto sendNew;
                //Program.MessageError("ClsTCPClient", "sendMessegeToUnitDisplay", ex.Message, false);
                //if (m_socClient.Connected == false) { return; };

            }
        }

        public void SendClearUnitDisplay()
        {
            if (PrePareData.StaticDataCls.UseUnitDisplay)
            {
                if (!Program.chkVIP())
                {
                    List<string> message = Program.getMessegeClearDisplay();
                    if (message != null)
                    {
                        message.ForEach(x => sendMessegeToUnitDisplay(x));
                    }
                }
            }
            //sendMessegeToUnitDisplay(Program.getMessegeClearDisplay());
        }

        public void SendQueueUnitDisplay()
        {
            if (PrePareData.StaticDataCls.UseUnitDisplay)
            {
                if (!Program.chkVIP())
                {
                    List<string> messege = Program.getMessegeCallQueue();
                    if (messege != null)
                    {
                        messege.ForEach(x => sendMessegeToUnitDisplay(x));
                    }
                }
            }
            //sendMessegeToUnitDisplay(Program.getMessegeCallQueue());


                //Socket m_socClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //RemoteIPAddress objRemoteIPAddress = new RemoteIPAddress();

                //System.Net.IPEndPoint remoteEndPoint = new System.Net.IPEndPoint(objRemoteIPAddress.GetRemoteIPAddress(), GetPort());

                ////do
                ////{
                ////    try
                ////    {
                ////        m_socClient.Connect(remoteEndPoint);
                ////        if (m_socClient.Connected == true) { break; };
                ////    }
                ////    catch
                ////    {

                ////    }
                ////} while (m_socClient.Connected == false);
                //try
                //{
                //    m_socClient.Connect(remoteEndPoint);
                //}
                //catch
                //{
                //    if (m_socClient.Connected == false) { return; };
                //}

                //byte[] byData = System.Text.Encoding.ASCII.GetBytes(Program.getMessegeCallQueue());
                ////byte[] byData = System.Text.Encoding.ASCII.GetBytes("TH_1_A01_" + Program.CurrentRegis.tpr_queue_no);
                //m_socClient.Send(byData);
                //m_socClient.Close();
        }

        private int GetPort()
        {
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                int objip = Convert1.ToInt32((from mfh in dbc.mst_config_hdrs join mfd in dbc.mst_config_dtls on mfh.mfh_id equals mfd.mfh_id 
                                            where mfh.mfh_code == "PUD" 
                                            && mfh.mhs_id == Program.CurrentSite.mhs_id 
                                            && mfh.mfh_status == 'A' 
                                            && mfh.mfh_effective_date.Value.Date <= DateTime.Now.Date 
                                            && (mfh.mfh_expire_date == null ? DateTime.Now.Date : mfh.mfh_expire_date.Value.Date) >= DateTime.Now.Date 
                                            && mfd.mfd_status == 'A'
                                            && mfd.mfd_effective_date.Value.Date <= DateTime.Now.Date
                                            && (mfd.mfd_expire_date == null ? DateTime.Now.Date : mfd.mfd_expire_date.Value.Date) >= DateTime.Now.Date 
                                            select mfd.mfd_text).FirstOrDefault());
                return objip;
            }
        }

        public void Dispose()
        {
            //this.Dispose();
        }
    }

    public class RemoteIPAddress
    {
        public System.Net.IPAddress GetObjectIPAddress()
        {
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                string objip = (from mfh in dbc.mst_config_hdrs
                                join mfd in dbc.mst_config_dtls on mfh.mfh_id equals mfd.mfh_id
                                where mfh.mfh_code == "IUD"
                                && mfh.mhs_id == Program.CurrentSite.mhs_id
                                && mfh.mfh_status == 'A'
                                && mfh.mfh_effective_date.Value.Date <= DateTime.Now.Date
                                && (mfh.mfh_expire_date == null ? DateTime.Now.Date : mfh.mfh_expire_date.Value.Date) >= DateTime.Now.Date
                                && mfd.mfd_status == 'A'
                                && mfd.mfd_effective_date.Value.Date <= DateTime.Now.Date
                                && (mfd.mfd_expire_date == null ? DateTime.Now.Date : mfd.mfd_expire_date.Value.Date) >= DateTime.Now.Date
                                select mfd.mfd_text).FirstOrDefault();
                string ip = objip.Substring(2, objip.Length - 2);
                System.Net.IPAddress remoteIPAddress = System.Net.IPAddress.Parse(ip);
                return remoteIPAddress;
            }
        }

        public string GetRemoteIPAddress()
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    string objip = (from mfh in dbc.mst_config_hdrs
                                    join mfd in dbc.mst_config_dtls on mfh.mfh_id equals mfd.mfh_id
                                    where mfh.mfh_code == "IUD"
                                    && mfh.mhs_id == Program.CurrentSite.mhs_id
                                    && mfh.mfh_status == 'A'
                                    && mfh.mfh_effective_date.Value.Date <= DateTime.Now.Date
                                    && (mfh.mfh_expire_date == null ? DateTime.Now.Date : mfh.mfh_expire_date.Value.Date) >= DateTime.Now.Date
                                    && mfd.mfd_status == 'A'
                                    && mfd.mfd_effective_date.Value.Date <= DateTime.Now.Date
                                    && (mfd.mfd_expire_date == null ? DateTime.Now.Date : mfd.mfd_expire_date.Value.Date) >= DateTime.Now.Date
                                    select mfd.mfd_text).FirstOrDefault();
                    string ip = objip.Substring(2, objip.Length - 2);
                    return ip;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("ClsTCPClient", "GetRemoteIPAddress", ex, false);
                return null;
            }
        }
    }
}
