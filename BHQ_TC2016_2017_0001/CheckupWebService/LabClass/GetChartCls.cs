using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.LabClass
{
    public class GetChartCls
    {
        public ChartResult GetID(string labcode, double? chartmin, double? chartmax, double? normalmin, double? normalmax, double? value, string displayvalue, string summary)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    var mlch = cdc.mst_lab_charts
                                  .Where(x => x.mlch_lab_code == labcode &&
                                              x.mlch_min_value == chartmin &&
                                              x.mlch_max_value == chartmax &&
                                              x.mlch_min_normal == normalmin &&
                                              x.mlch_max_normal == normalmax &&
                                              x.mlch_summary == summary &&
                                              x.mlch_lab_value == value)
                                  .FirstOrDefault();
                    if (mlch != null && mlch.mlch_active == true)
                    {
                        return new ChartResult { chartid = mlch.mlch_id, chartpath = mlch.mlch_file_path };
                    }
                    else
                    {
                        DateTime dateNow = Class.globalCls.GetServerDateTime();
                        if (mlch == null)
                        {
                            mlch = new mst_lab_chart
                            {
                                mlch_lab_code = labcode,
                                mlch_min_value = chartmin,
                                mlch_max_value = chartmax,
                                mlch_min_normal = normalmin,
                                mlch_max_normal = normalmax,
                                mlch_lab_value = value,
                                mlch_summary = summary,
                                mlch_create_date = dateNow
                            };
                            cdc.mst_lab_charts.InsertOnSubmit(mlch);
                        }
                        mlch.mlch_update_date = dateNow;

                        Image img;
                        if (chartmin == null || chartmax == null || normalmin == null || normalmax == null || value == null)
                        {
                            img = Properties.Resources.Error;
                            mlch.mlch_active = false;
                        }
                        else
                        {
                            var genrs = new LabClass.GenerateChartCls().Generate(chartmin, chartmax, normalmin, normalmax, value, displayvalue, summary);
                            mlch.mlch_active = genrs.active;
                            img = genrs.img;

                        }
                        string dirLab = @"\" + (string.IsNullOrEmpty(labcode) ? "xxxx" : labcode);
                        string dirMinMax = @"\LR" + (chartmin == null ? "xxxx" : chartmin.ToString()) + "_" + (chartmax == null ? "xxxx" : chartmax.ToString());
                        string dirRange = @"\NLR" + (normalmin == null ? "xxxx" : normalmin.ToString()) + "_" + (normalmax == null ? "xxxx" : normalmax.ToString());
                        string path = Class.GetDBConfigCls.GetConfig("PathChartImage") + dirLab + dirMinMax + dirRange;
                        if (!System.IO.Directory.Exists(path))
                        {
                            System.IO.Directory.CreateDirectory(path);
                        }
                        string filename = @"\V" + (value == null ? "xxxx" : value.ToString()) + ".png";
                        img.Save(path + filename, System.Drawing.Imaging.ImageFormat.Png);
                        mlch.mlch_file_path = path + filename;
                    }
                    cdc.SubmitChanges();
                    return new ChartResult { chartid = mlch.mlch_id, chartpath = mlch.mlch_file_path };
                }
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("MstLabChartCls", "GetID(double? min, double? max, double? normal_min, double? normal_max, string value)", ex.Message);
                return new ChartResult { chartid = null, chartpath = null };
            }
        }
        //    public ChartResult GetID_Royal(string labcode, double? chartmin, double? chartmax, double? normalmin, double? normalmax, double? value, string displayvalue, string summary)
        //    {
        //        try
        //        {
        //            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
        //            {
        //                var mlch = cdc.mst_lab_charts
        //                              .Where(x => x.mlch_lab_code == labcode &&
        //                                          x.mlch_min_value == chartmin &&
        //                                          x.mlch_max_value == chartmax &&
        //                                          x.mlch_min_normal == normalmin &&
        //                                          x.mlch_max_normal == normalmax &&
        //                                          x.mlch_summary == summary &&
        //                                          x.mlch_lab_value == value)
        //                              .FirstOrDefault();
        //                if (mlch != null && mlch.mlch_active == true)
        //                {
        //                    return new ChartResult { chartid = mlch.mlch_id, chartpath = mlch.mlch_file_path };
        //                }
        //                else
        //                {
        //                    DateTime dateNow = Class.globalCls.GetServerDateTime();
        //                    if (mlch == null)
        //                    {
        //                        mlch = new mst_lab_chart
        //                        {
        //                            mlch_lab_code = labcode,
        //                            mlch_min_value = chartmin,
        //                            mlch_max_value = chartmax,
        //                            mlch_min_normal = normalmin,
        //                            mlch_max_normal = normalmax,
        //                            mlch_lab_value = value,
        //                            mlch_summary = summary,
        //                            mlch_create_date = dateNow
        //                        };
        //                        cdc.mst_lab_charts.InsertOnSubmit(mlch);
        //                    }
        //                    mlch.mlch_update_date = dateNow;

        //                    Image img;
        //                    if (chartmin == null || chartmax == null || normalmin == null || normalmax == null || value == null)
        //                    {
        //                        img = Properties.Resources.Error;
        //                        mlch.mlch_active = false;
        //                    }
        //                    else
        //                    {
        //                        //var genrs = new LabClass.GenerateChartCls().Generate(chartmin, chartmax, normalmin, normalmax, value, displayvalue, summary);
        //                        //mlch.mlch_active = genrs.active;
        //                        //img = genrs.img;
        //                        //value = null;
        //                        // displayvalue = null;
        //                        var genrs = new LabClass.GenerateChartCls().GenerateRoyalChart(chartmin, chartmax, normalmin, normalmax, value, displayvalue, summary);
        //                        mlch.mlch_active = genrs.active;
        //                        img = genrs.img;

        //                    string dirLab = @"\Royal" + (string.IsNullOrEmpty(labcode) ? "xxxx" : labcode);
        //                    string dirMinMax = @"\LR" + (chartmin == null ? "xxxx" : chartmin.ToString()) + "_" + (chartmax == null ? "xxxx" : chartmax.ToString());
        //                    string dirRange = @"\NLR" + (normalmin == null ? "xxxx" : normalmin.ToString()) + "_" + (normalmax == null ? "xxxx" : normalmax.ToString());
        //                    string path = Class.GetDBConfigCls.GetConfig("PathChartImage") + dirLab + dirMinMax + dirRange;
        //                    if (!System.IO.Directory.Exists(path))
        //                    {
        //                        System.IO.Directory.CreateDirectory(path);
        //                    }
        //                    string filename = @"\V" + (value == null ? "xxxx" : value.ToString()) + ".png";
        //                    img.Save(path + filename, System.Drawing.Imaging.ImageFormat.Png);
        //                    mlch.mlch_file_path = path + filename;
        //                }
        //                cdc.SubmitChanges();
        //                return new ChartResult { chartid = mlch.mlch_id, chartpath = mlch.mlch_file_path };
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Class.globalCls.MessageError("MstLabChartCls", "GetID(double? min, double? max, double? normal_min, double? normal_max, string value)", ex.Message);
        //            return new ChartResult { chartid = null, chartpath = null };
        //        }
        //    }
        //}
    }
    public class ChartResult
    {
        public int? chartid { get; set; }
        public string chartpath { get; set; }
    }
}