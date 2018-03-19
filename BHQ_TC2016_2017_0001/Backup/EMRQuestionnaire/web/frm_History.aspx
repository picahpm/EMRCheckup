<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_History.aspx.cs" Inherits="EMRQuestionnaire.web.frm_History" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="../css/FontsThai/fontsthaisans_neueregular.css" rel="stylesheet" type="text/css" />
<link href="../css/maincss.css" rel="stylesheet" type="text/css" />
<link href="../css/maincss_en.css" rel="stylesheet" type="text/css" />
<link rel="shortcut icon" href="../images/quiz-games-300x300.png" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>History Questionaire</title>
</head>
<body>
<table border ="0" cellpadding ="0" cellspacing="0" width="100%" > <tr> <td  align ="center">
    <form id="frmHistory" runat="server">
    <div id="dash" class="header">
    <div id="left" style="float:left;"> 
        <img src="../images/header_logo.jpg" />  
        </div>
        <div id="right" 
            style="float:right;width:50%; height:22px; vertical-align:bottom;" 
            align="right">
            &nbsp;
                      
        </div>
          <div id="Div1" 
            style="float:right;width:50%; height:30px; vertical-align:bottom;" 
            align="right">
           
          </div> 
    
    
    </div>
    <div>
        <div id="Content" class="Content">
            <table cellpadding="0" cellspacing="0" border="0" id="table" class="sortable">
                <thead>
                    <tr>
                    <th>
                            <h3>
                                Name</h3>
                        </th>
                        <th>
                            <h3>
                                HN
                             </h3>
                        </th>
                         <th>
                            <h3>
                                Room
                             </h3>
                        </th>
                        <th>
                            <h3>
                                Physician</h3>
                        </th>
                        <th>
                            <h3>
                                Visit Date</h3>
                        </th>
                        <th>
                            <h3>
                                Department</h3>
                        </th>
                        
                        <th>
                            <h3>
                                Birth Date</h3>
                        </th>
                        <th>
                            <h3>
                                Age</h3>
                        </th>
                        <th>
                            <h3>
                                Sex</h3>
                        </th>
                        <th>
                            <h3>
                                Allergies</h3>
                        </th>
                        <th>
                            <h3>
                                Visit Date</h3>
                        </th>
                    </tr>
                </thead>
                <% System.Data.DataTable dtHistory = new System.Data.DataTable();
                   QuestionnaireWebSite.clsUtility.Utility ut = new QuestionnaireWebSite.clsUtility.Utility();
                   QuestionnaireWebSite.clsExecuteSQL.executeDC clsEX = new QuestionnaireWebSite.clsExecuteSQL.executeDC();
                   dtHistory = clsEX.get_history_questionaire("01-11-030389");
                %>
                <tbody>
                    <%for (int i = 0; i < dtHistory.Rows.Count;i++ )
                      { %>
                    <tr onclick="nextPage('<%=dtHistory.Rows[i]["HN"].ToString()%>','<%=dtHistory.Rows[i]["LANGUAGE"].ToString()%>');">  
                        <td>
                            <%=dtHistory.Rows[i]["FULL_NAME"].ToString()%>
                        </td>
                        <td>
                            <%=dtHistory.Rows[i]["HN"].ToString() %>
                        </td>
                         <td>
                            <%=dtHistory.Rows[i]["ROOM"].ToString()%>
                        </td>
                        <td>    
                              <%=dtHistory.Rows[i]["PHYSICIAN"].ToString()%>
                        </td>     
                         <td>    
                              <%=ut.ConvertDateToStringFormat(dtHistory.Rows[i]["VISIT_DATE"].ToString(), "dd/MM/yyyy")%>
                        </td>
                         <td>    
                              <%=dtHistory.Rows[i]["DEPARTMENT"].ToString()%>
                        </td>
                               <td>    
                              <%=ut.ConvertDateToStringFormat(dtHistory.Rows[i]["BIRTH_DATE"].ToString(), "dd/MM/yyyy")%>
                        </td>
                         <td>    
                              <%=dtHistory.Rows[i]["AGE"].ToString()%>
                        </td>
                         <td>    
                              <%=dtHistory.Rows[i]["SEX"].ToString()%>
                        </td>
                        <td>    
                              <%=dtHistory.Rows[i]["ALLERGIES"].ToString()%>
                        </td>
                         <td>    
                              <%= ut.ConvertDateToStringFormat(dtHistory.Rows[i]["CREATE_DATE"].ToString(),"dd/MM/yyyy")%>
                        </td>
                    </tr>
                     <%} %>
                </tbody>
            </table>
            <div id="controls">
                <div id="perpage">
                    <select onchange="sorter.size(this.value)">
                        <option value="5">5</option>
                        <option value="10" selected="selected">10</option>
                        <option value="20">20</option>
                        <option value="50">50</option>
                        <option value="100">100</option>
                    </select>
                    <span>Entries Per Page</span>
                </div>
                <div id="navigation">
                    <img src="../images/first.gif" width="16" height="16" alt="First Page" onclick="sorter.move(-1,true)" />
                    <img src="../images/previous.gif" width="16" height="16" alt="First Page" onclick="sorter.move(-1)" />
                    <img src="../images/next.gif" width="16" height="16" alt="First Page" onclick="sorter.move(1)" />
                    <img src="../images/last.gif" width="16" height="16" alt="Last Page" onclick="sorter.move(1,true)" />
                </div>
                <div id="text">
                    Displaying Page <span id="currentpage"></span>of <span id="pagelimit"></span>
                </div>
            </div>
            <script type="text/javascript" src="../scripts/script_History.js"></script>
            <script type="text/javascript">
                var sorter = new TINY.table.sorter("sorter");
                sorter.head = "head";
                sorter.asc = "asc";
                sorter.desc = "desc";
                sorter.even = "evenrow";
                sorter.odd = "oddrow";
                sorter.evensel = "evenselected";
                sorter.oddsel = "oddselected";
                sorter.paginate = true;
                sorter.currentid = "currentpage";
                sorter.limitid = "pagelimit";
                sorter.init("table", 1);
            </script>
             <div id="btnFooter" style="float:right;">   
                <a class="large button yellow" onclick="nextPage('');">&nbsp;&nbsp;&nbsp;&nbsp Next &nbsp;&nbsp;&nbsp;&nbsp </a>     
             </div>
        </div>
      
    </div>
    </form>
    </td>
    </tr>
    </table>
</body>
</html>
