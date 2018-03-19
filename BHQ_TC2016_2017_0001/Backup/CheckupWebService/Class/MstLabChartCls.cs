using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.Class
{
    public class MstLabChartCls
    {
        public int GetID(string mlch_lab_code, double? mlch_min_value, double? mlch_max_value, double? mlch_min_normal, double? mlch_max_normal, string normal_range, string mlch_lab_value, string ShowValue)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    bool error = false;
                    DateTime dateNow = globalCls.GetServerDateTime();
                    double min = 0;
                    double max = 0;
                    double min_normal = 0;
                    double max_normal = 0;
                    double value = 0;
                    try
                    {
                        min = (double)mlch_min_value;
                        max = (double)mlch_max_value;
                        min_normal = (double)mlch_min_normal;
                        max_normal = (double)mlch_max_normal;
                        value = Convert.ToDouble(mlch_lab_value);
                    }
                    catch
                    {
                        error = true;
                    }
                    mst_lab_chart mlch = cdc.mst_lab_charts
                                            .Where(x => x.mlch_lab_code == mlch_lab_code &&
                                                        x.mlch_min_value == min &&
                                                        x.mlch_max_value == max &&
                                                        x.mlch_min_normal == min_normal &&
                                                        x.mlch_max_normal == max_normal &&
                                                        x.mlch_lab_value == value)
                                            .FirstOrDefault();
                    if (mlch == null || mlch.mlch_active == false)
                    {
                        if (mlch == null)
                        {
                            mlch = new mst_lab_chart
                            {
                                mlch_lab_code = mlch_lab_code,
                                mlch_summary = value >= min_normal && value <= max_normal ? "N" : "A",
                                mlch_min_value = min,
                                mlch_max_value = max,
                                mlch_min_normal = min_normal,
                                mlch_max_normal = max_normal,
                                mlch_lab_value = value,
                                mlch_active = true,
                                mlch_create_date = dateNow
                            };
                            cdc.mst_lab_charts.InsertOnSubmit(mlch);
                        }
                        mlch.mlch_normal_range = normal_range.Trim();
                        mlch.mlch_summary = value >= min_normal && value <= max_normal ? "N" : "A";
                        mlch.mlch_update_date = dateNow;
                        Image img;
                        if (!error)
                        {
                            try
                            {
                                img = new GenerateChartCls().Generate(min, max, min_normal, max_normal, value, ShowValue);
                                mlch.mlch_active = true;
                            }
                            catch (Exception ex)
                            {
                                img = Properties.Resources.Error;
                                mlch.mlch_active = false;
                                globalCls.MessageError("MstLabChartCls", "GetID(double? min, double? max, double? normal_min, double? normal_max, string value)", ex.Message);
                            }
                        }
                        else
                        {
                            img = Properties.Resources.Error;
                            mlch.mlch_active = false;
                        }
                        string dirLab = @"\" + mlch.mlch_lab_code;
                        string dirMinMax = @"\LR" + min.ToString() + "_" + max.ToString();
                        string dirRange = @"\NLR" + min_normal.ToString() + "_" + max_normal.ToString();
                        string path = GetDBConfigCls.GetConfig("PathChartImage") + dirLab + dirMinMax + dirRange;
                        if (!System.IO.Directory.Exists(path))
                        {
                            System.IO.Directory.CreateDirectory(path);
                        }
                        try
                        {
                            string filename = @"\V" + value.ToString() + ".png";
                            img.Save(path + filename, System.Drawing.Imaging.ImageFormat.Png);
                            mlch.mlch_file_path = path + filename;
                        }
                        catch (Exception ex)
                        {
                            mlch.mlch_file_path = "";
                            mlch.mlch_active = false;
                            globalCls.MessageError("MstLabChartCls", "GetID(double? min, double? max, double? normal_min, double? normal_max, string value)", ex.Message);
                        }
                        cdc.SubmitChanges();
                    }
                    return mlch.mlch_id;
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("MstLabChartCls", "GetID(double? min, double? max, double? normal_min, double? normal_max, string value)", ex.Message);
                return 1;
            }
        }
    }
}