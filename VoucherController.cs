using AnnualLeaveScheduleV1.ViewModels.Voucher;
using AnnualLeaveScheduleV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gibsin_Sys_CommonModel;
using System.Web.Script.Serialization;
//using AnnualLeaveScheduleV1.ViewModels;
//using Omu.ValueInjecter;
//using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace AnnualLeaveScheduleV1.Controllers
{
    public class VoucherController : BaseController
    {
        GWEB_TESTEntities db = new GWEB_TESTEntities();

        //this design to be PUBLIC for json return to HTML side//
        //[HttpPost]
        public ActionResult Index()
        {
            //
            //IndexViewModel model = new IndexViewModel();
            //try
            //{
            //    //model.TPID = "現支";
            //    //int SJcounts = (from a in db.ACHD
            //    //                where System.Data.Entity.DbFunctions.TruncateTime(a.DATE) == System.Data.Entity.DbFunctions.TruncateTime(DateTime.Today) && a.TPID == "現支"
            //    //                select a.SQID).ToList().Count() + 1;
            //    //model.SQID = (DateTime.Today.Year - 1911).ToString() + (DateTime.Today.Month.ToString("00")) + SJcounts.ToString("000");

            //    //QueryViewModel qViewModel = new QueryViewModel();

            //    if (model.DATE == DateTime.MinValue)
            //    {
            //        model.DATE = DateTime.Today;
            //    }
            //    List<ACHD> result = new JavaScriptSerializer().Deserialize<List<ACHD>>(this.QueryACHD(new PageInfoModel(), new ACHDViewModel()));
            //}
            //catch (Exception)
            //{
            //    //TODO:
            //    throw;
            //}
            //return View(model);
            Response.RedirectToRoute("Index", new { TPID = "", SQID = "" });
            return Json("");
        }

        [HttpGet]
        public ActionResult Index(string TPID, string SQID)
        {
            if (string.IsNullOrEmpty(TPID) || string.IsNullOrEmpty(SQID))
            {
                IndexViewModel model = new IndexViewModel();
                try
                {
                    //model.TPID = "現支";
                    //int SJcounts = (from a in db.ACHD
                    //                where System.Data.Entity.DbFunctions.TruncateTime(a.DATE) == System.Data.Entity.DbFunctions.TruncateTime(DateTime.Today) && a.TPID == "現支"
                    //                select a.SQID).ToList().Count() + 1;
                    //model.SQID = (DateTime.Today.Year - 1911).ToString() + (DateTime.Today.Month.ToString("00")) + SJcounts.ToString("000");

                    //QueryViewModel qViewModel = new QueryViewModel();

                    if (model.DATE == DateTime.MinValue)
                    {
                        model.DATE = DateTime.Today;
                    }
                    List<ACHD> result = new JavaScriptSerializer().Deserialize<List<ACHD>>(this.QueryACHD(new PageInfoModel(), new ACHDViewModel()));
                }
                catch (Exception)
                {
                    //TODO:
                    throw;
                }
                return View(model);
            }
            else
            {
                IndexViewModel outputModel = new IndexViewModel();
                try
                {
                    //ACHDViewModel aCHDViewModel = JsonConvert.DeserializeObject<ACHDViewModel>(JsonConvert.SerializeObject(model));
                    ACHD achdModel = new JavaScriptSerializer().Deserialize<List<ACHD>>(this.QueryACHD(new PageInfoModel(), new ACHDViewModel() { TPID = TPID, SQID = SQID })).FirstOrDefault();
                    //outputModel.ACHDList = achdList;
                    outputModel = JsonConvert.DeserializeObject<IndexViewModel>(JsonConvert.SerializeObject(achdModel), new JsonSerializerSettings() { DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind});
                    IndexViewModel outputModel2 = JsonConvert.DeserializeObject<IndexViewModel>(JsonConvert.SerializeObject(achdModel), new JsonSerializerSettings() { DateTimeZoneHandling = DateTimeZoneHandling.Local });
                    IndexViewModel outputModel3 = JsonConvert.DeserializeObject<IndexViewModel>(JsonConvert.SerializeObject(achdModel), new JsonSerializerSettings() { DateTimeZoneHandling = DateTimeZoneHandling.Unspecified });

                    outputModel.ACBDList = JsonConvert.DeserializeObject<List<ACBD>>(this.QueryACBD(new PageInfoModel(), new ACBDViewModel() { TPID = outputModel.TPID, SQID = outputModel.SQID }),new JsonSerializerSettings() {  DateTimeZoneHandling = DateTimeZoneHandling.Local});

                    if (outputModel.ACBDList != null && outputModel.ACBDList.Count() > 0)
                    {
                        var tmp = outputModel.ACBDList.First();
                        outputModel.ACBJList = JsonConvert.DeserializeObject<List<ACBJ>>(this.QueryACBJ(new PageInfoModel(), new ACBJViewModel() { TPID = tmp.TPID, SQID = tmp.SQID, ACID = tmp.ACID, SBID = tmp.SBID, SQNO = tmp.SQNO }), new JsonSerializerSettings() { DateTimeZoneHandling = DateTimeZoneHandling.Utc });
                    }
                }
                catch (Exception ex)
                {
                    //TODO:
                    throw;
                }
                return View(outputModel);
            }
        }

        [ValidateAntiForgeryToken]
        //public ActionResult Create(CreateViewModel model, string DATE, List<ACBD> ACBDList, List<ACBJ> ACBJList, string ACBJListStr, JsonResult ACBJListJson, FormCollection form)
        public ActionResult Create(CreateViewModel model, string DATE, List<ACBD> ACBDList, List<ACBJ> ACBJList, List<string> ACBJListStr, JsonResult ACBJListJson, FormCollection form)
        {
            this.db = new GWEB_TESTEntities();
            IndexViewModel outputModel = new IndexViewModel();
            //if (DATE != "" && DATE != null)
            //{
            //    model.DATE = DateTime.ParseExact(DATE, "yyyyMMdd", null);
            //}
            //else
            //{
            //    model.DATE = DateTime.Today.Date;
            //}

            //////ACBJList[0].IVID\":\"15\"},{\"ACBJList[1].PJID\
            //////ACBJList[0].IVID\":\"15\"},{\"ACBJList[1].PJID\
            //////ACBJListStr = ACBJListStr.Replace("\"","");
            //////Regex pattern = new Regex(/);
            //////string result = Regex.Replace(ACBJListStr, "", "($1)");
            ////object aa = new JavaScriptSerializer().DeserializeObject(ACBJListStr);
            ////string[] arr = ((FormCollection)aa).Cast<Dictionary<string,string>>()
            ////                                 .Select(x => x.ToString())
            ////                                 .ToArray();
            //////ACBJ aa2 = JsonConvert.DeserializeObject<ACBJ>(ACBJListStr);
            ////List<ACBJ> aCBJs = new JavaScriptSerializer().Deserialize<List<ACBJ>>(ACBJListStr);
            ////ACBJListStr = ACBJListStr.Replace("ACBJList", "");
            ////for (int i = 0; i< ((Dictionary<string,string>)aa).Count(); i++)
            ////{

            ////}

            try
            {
                //TEST MODE...//
                if (model.TPID == null | model.SQID == null)
                {
                    return View("Index", outputModel);
                }

                //DEFAULT VALUE//
                if (model.PSID == null || model.PSID == "")
                {
                    model.PSID = "X";
                }

                //REMOVE EMPTY DATA//
                ACBDList.RemoveAll(itm =>
                    itm.ACID == null
                );

                #region des


                List<ACBJ> totalACBJList = new List<ACBJ>();
                if (ACBJListStr != null && ACBJListStr.Count() > 0) foreach (var item in ACBJListStr)
                {
                    var jsonObj3 = JsonConvert.DeserializeObject<JObject>(item);


                    List<ACBJ> partACBJList = new List<ACBJ>();
                    int rows = (jsonObj3.Count / 6);


                    
                    for (int i = 0; i < rows; i++)
                    {
                        partACBJList.Add(new ACBJ());
                    }
                    //foreach(var item in totalACBJList.Select((x,i)=> new {idx = i, x = x}))
                    //{

                    //}
                    int currentRowIndex = 0;
                    //foreach(var item in item.Select((x,i)=> new {index = i, v = x}))
                    foreach (var itm in partACBJList)
                    {
                        string keyNmePre = "";
                        keyNmePre = string.Format("ACBJList[{0}].{1}", currentRowIndex, "PJID");
                        partACBJList[currentRowIndex].PJID = jsonObj3[keyNmePre].ToString();

                        keyNmePre = string.Format("ACBJList[{0}].{1}", currentRowIndex,"PJSQ");
                        partACBJList[currentRowIndex].PJSQ = jsonObj3[keyNmePre].ToString();

                        keyNmePre = string.Format("ACBJList[{0}].{1}", currentRowIndex, "DAMT");
                        decimal dDamt = 0m;
                        if(Decimal.TryParse(jsonObj3[keyNmePre].ToString(), out dDamt))
                            partACBJList[currentRowIndex].DAMT = (dDamt);

                        keyNmePre = string.Format("ACBJList[{0}].{1}", currentRowIndex, "CAMT");
                        //partACBJList[currentRowIndex].CAMT = jsonObj3[keyNmePre].ToString();
                        decimal dCamt = 0m;
                        if (Decimal.TryParse(jsonObj3[keyNmePre].ToString(), out dCamt))
                            partACBJList[currentRowIndex].CAMT = (dCamt);

                        keyNmePre = string.Format("ACBJList[{0}].{1}", currentRowIndex, "IVAM");
                        //partACBJList[currentRowIndex].IVAM = jsonObj3[keyNmePre].ToString();
                        decimal dIvam = 0m;
                        if (Decimal.TryParse(jsonObj3[keyNmePre].ToString(), out dIvam))
                            partACBJList[currentRowIndex].IVAM = (dIvam);

                        keyNmePre = string.Format("ACBJList[{0}].{1}", currentRowIndex, "IVID");
                        partACBJList[currentRowIndex].IVID = jsonObj3[keyNmePre].ToString();


                        //////partACBJList[currentRowIndex].IVAM = jsonObj3[string.Format("ACBJList[{0}].{1}", currentRowIndex,"IVAM")];
                        //////partACBJList[currentRowIndex].IVID = jsonObj3[string.Format("ACBJList[{0}].{1}", currentRowIndex,"IVAM")];
                        ////
                        ////
                        ////string IVAM = string.Format("{0}PJID",keyNmePre);
                        ////partACBJList[currentRowIndex].PJID = jsonObj3[keyname];
                        ////
                        ////string  = string.Format("{0}PJID",keyNmePre);

                        currentRowIndex++;
                    }
                    totalACBJList.AddRange(partACBJList);
                }

               
                #endregion

                //this.db.ACHD.Add(new ACHD() {
                //    ACID = model.ACID,
                //    CKAM = Convert.ToDecimal(model.CKAM ?? 0),
                //    CKID = model.CKID,
                //    CKYM = model.CKYM,
                //    DATE = model.DATE,
                //    JSKD = model.JSKD,
                //    NIKD = model.NIKD,
                //    NOTE = model.NOTE,
                //    PSID = model.PSID,
                //    REM1 = model.REM1,
                //    REM2 = model.REM2,
                //    SBID = model.SBID,
                //    SQID = model.SQID,
                //    TPID = model.TPID,
                //    TPNM = model.TPNM,
                //    YYMM = model.YYMM
                //});
                //this.db.SaveChanges();

                ACBDList.ForEach(x => {
                    x.DATE = model.DATE;
                    x.SQID = model.SQID;
                    x.TPID = model.TPID;
                });
                this.createVoucher(new ACHD
                {
                    ACID = model.ACID,
                    CKAM = Convert.ToDecimal(model.CKAM ?? 0),
                    CKID = model.CKID,
                    CKYM = model.CKYM,
                    DATE = model.DATE,
                    JSKD = model.JSKD,
                    NIKD = model.NIKD,
                    NOTE = model.NOTE,
                    PSID = model.PSID,
                    REM1 = model.REM1,
                    REM2 = model.REM2,
                    SBID = model.SBID,
                    SQID = model.SQID,
                    TPID = model.TPID,
                    TPNM = model.TPNM,
                    YYMM = model.YYMM
                }
                , ACBDList
                , totalACBJList);
                ViewBag.Msg = "新增完成";
                
                //保存舊有資料打回//
                outputModel.ACID = model.ACID;
                outputModel.CKAM = model.CKAM;
                outputModel.CKID = model.CKID;
                outputModel.CKYM = model.CKYM;
                outputModel.DATE = model.DATE;
                outputModel.JSKD = model.JSKD;
                outputModel.NIKD = model.NIKD;
                outputModel.NOTE = model.NOTE;
                outputModel.PSID = model.PSID;
                outputModel.REM1 = model.REM1;
                outputModel.REM2 = model.REM2;
                outputModel.SBID = model.SBID;
                outputModel.SQID = model.SQID;
                outputModel.TPID = model.TPID;
                outputModel.TPNM = model.TPNM;
                outputModel.YYMM = model.YYMM;

                //KEEP LIST//
                outputModel.ACBDList = ACBDList;
                outputModel.ACBDListJson = Json(ACBDList, "application/json", System.Text.Encoding.UTF8,JsonRequestBehavior.AllowGet);
                outputModel.ACBDListJsStr = new JavaScriptSerializer().Serialize(ACBDList);


                outputModel.ACBJList = ACBJList;
                outputModel.ACBJListJson = Json(ACBJList, "application/json", System.Text.Encoding.UTF8,JsonRequestBehavior.AllowGet);
                outputModel.ACBJListJsStr = new JavaScriptSerializer().Serialize(ACBJList);
            }
            catch (Exception)
            {
                //TODO:
                throw;
            }
            return View("Index", outputModel);
        }

        private RModel createVoucher(ACHD achd, List<ACBD> acbdList, List<ACBJ> acbjList)
        {
            //for (int i = 0; i < acbdList.Count(); i++)
            //{
            //    if (acbdList[i].SQNO == null || acbdList[i].SQNO == "")
            //    {
            //        acbdList[i].SQNO = (i + 1).ToString();
            //    };
            //}
            //for (int i = 0; i < acbjList.Count(); i++)
            //{
            //    acbjList[i].SQNO = (i + 1).ToString();
            //}
            RModel rModel = new RModel(false, 1, string.Empty);
            this.db = new GWEB_TESTEntities();
            try
            {
                //if (achd.TPID == "現入" && achd.TPID == "現出")
                    
                //{
                //    achd.SBID = "1103";

                //}
                //this.db.ACSB.Where(p=>p. == "")
                this.db.ACHD.Add(achd);
                if (acbdList.Count() > 0)
                {
                    foreach (var item in acbdList)
                    {
                        if (item.SBID == null || item.SBID == "")
                        {
                            item.SBID = "";
                        }
                    }
                    this.db.ACBD.AddRange(acbdList);
                }

                acbjList.RemoveAll(p =>
                    p.PJID == "" &&
                    p.CAMT == null &&
                    p.DAMT == null &&
                    p.IVAM == null &&
                    p.IVID == ""
                );
                if (acbjList.Count() > 0)
                {
                    acbjList.ForEach(x => {
                        x.DATE = achd.DATE;
                        x.SQID = achd.SQID;
                        x.TPID = achd.TPID;

                        //TT://錯誤資料測試中...//
                        //TODO:...
                        //x.PJID = model.ACID;
                        //x.PJSQ = model.ACID;
                    });
                    this.db.ACBJ.AddRange(acbjList);
                }
                db.SaveChanges();
                rModel.Code = 0;
                rModel.Success = true;
                rModel.Msg = string.Empty;
            }
            catch (Exception ex)
            {
                rModel.Code = 1;
                rModel.Success = false;
                rModel.Msg = ex.Message;

                //TODO:
                throw;
            }
            finally { db.Dispose(); }
            return rModel;
        }
        //[ValidateAntiForgeryToken]
        [HttpGet]
        public ActionResult Query(PageInfoModel pgingModel, ACHDViewModel model)
        {
            string result = string.Empty;
            IndexViewModel outputModel = new IndexViewModel();
            try
            {
                List<ACHD> queryResult = new JavaScriptSerializer().Deserialize<List<ACHD>>(this.QueryACHD(pgingModel, model));
                //outputModel 

                if (queryResult != null && queryResult.Count() > 0)
                {
                    //this.QueryACBD
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Json( new { data = result} ,JsonRequestBehavior.AllowGet);
            //return View("Index", outputModel);
        }
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(EditViewModel model)
        //{
        //    EditViewModel outputModel = new VoucherViewModel();
        //    //this.db = new GWEB_TESTEntities();
        //    //ACHD achd = new ACHD();
        //    //achd.ACID = model.ACID;
        //    //achd.CKAM = Convert.ToDecimal(model.CKAM);
        //    //achd.CKID = model.CKID;
        //    //achd.CKYM = model.CKYM;
        //    //achd.DATE = model.DATE;
        //    //achd.JSKD = model.JSKD;
        //    //achd.NIKD = model.NIKD;
        //    //achd.NOTE = model.NOTE;
        //    //achd.PSID = model.PSID;
        //    //achd.REM1 = model.REM1;
        //    //achd.REM2 = model.REM2;
        //    //achd.SBID = model.SBID;
        //    //achd.SQID = model.SQID;
        //    //achd.TPID = model.TPID;
        //    //achd.TPNM = model.TPNM;
        //    //achd.YYMM = model.YYMM;
        //    //db.ACHD.Add(achd);
        //    //db.SaveChanges();
        //    return View(outputModel);
        //}

        //Query ACHD >> using ACHD MODEL//
        //[ValidateAntiForgeryToken]
        //[NonAction]
        public string QueryACHD(PageInfoModel pgingModel, ACHDViewModel model)
        {
            string result = string.Empty;
            this.db = new GWEB_TESTEntities();
            try
            {
                //PAGING//
                //var queryResult = (from p in db.ACHD select p).OrderBy(x=>x.ACID).Skip(pgingModel.PageNum * pgingModel.PageSize).Take(pgingModel.PageSize);
                
                //NO PAGING//
                var queryResult = (from p in db.ACHD select p);

                if (model.ACID != "" & model.ACID != null)
                    queryResult = queryResult.Where(x => x.ACID == model.ACID);

                if (model.CKAM != "" & model.CKAM != null)
                {
                    //decimal ckam = 0m;
                    //if (Decimal.TryParse(model.CKAM, out ckam))
                    //{
                    //   queryResult = queryResult.Where(x => x.CKAM == ckam);
                    queryResult = queryResult.Where(x => x.CKAM == Convert.ToDecimal(model.CKAM));
                    //}
                    //else
                    //{
                    //    //TODO:
                    //    //NOTHING//
                    //}
                }

                if (model.CKID != "" & model.CKID != null)
                    queryResult = queryResult.Where(x => x.CKID == model.CKID);

                if (model.CKYM != "" & model.CKYM != null)
                    queryResult = queryResult.Where(x => x.CKYM == model.CKYM);


                if (model.DATE != "" & model.DATE != null)
                {
                    DateTime date = CcDevConst.DATEINIVALUE;
                    if (DateTime.TryParse(model.DATE, out date))
                    {
                        queryResult = queryResult.Where(x => x.DATE == date);
                    }
                    else
                    {
                        //TODO:
                        //NOTHING//
                    }
                }

                if (model.JSKD != "" & model.JSKD != null)
                    queryResult = queryResult.Where(x => x.JSKD == model.JSKD);

                if (model.NIKD != "" & model.NIKD != null)
                    queryResult = queryResult.Where(x => x.NIKD == model.NIKD);

                if (model.NOTE != "" & model.NOTE != null)
                    queryResult = queryResult.Where(x => x.NOTE == model.NOTE);

                if (model.PSID != "" & model.PSID != null)
                    queryResult = queryResult.Where(x => x.PSID == model.PSID);

                if (model.REM1 != "" & model.REM1 != null)
                    queryResult = queryResult.Where(x => x.REM1 == model.REM1);

                if (model.REM2 != "" & model.REM2 != null)
                    queryResult = queryResult.Where(x => x.REM2 == model.REM2);

                if (model.SBID != "" & model.SBID != null)
                    queryResult = queryResult.Where(x => x.SBID == model.SBID);

                if (model.SQID != "" & model.SQID != null)
                    queryResult = queryResult.Where(x => x.SQID == model.SQID);

                if (model.TPID != "" & model.TPID != null)
                    queryResult = queryResult.Where(x => x.TPID == model.TPID);

                if (model.TPNM != "" & model.TPNM != null)
                    queryResult = queryResult.Where(x => x.TPNM == model.TPNM);

                if (model.YYMM != "" & model.YYMM != null)
                    queryResult = queryResult.Where(x => x.YYMM == model.YYMM);

                result = new JavaScriptSerializer().Serialize(queryResult.OrderBy(x=>x.TPID).OrderBy(x=>x.SQID).ToList());
            }
            catch (Exception)
            {
                //TODO:
                throw;
            }
            finally { db.Dispose(); }

            return result;
        }

        //[ValidateAntiForgeryToken]
        public string QueryACBD(PageInfoModel pgingModel, ACBDViewModel model)
        {
            string result = string.Empty;
            this.db = new GWEB_TESTEntities();
            try
            {
                //PAGING//
                //var queryResult = (from p in db.ACBD select p).OrderBy(x => x.ACID).Skip(pgingModel.PageNum * pgingModel.PageSize).Take(pgingModel.PageSize);

                //NOPAGING//
                var queryResult = (from p in db.ACBD select p);

                if (model.ACID != "" & model.ACID != null)
                    queryResult = queryResult.Where(x => x.ACID == model.ACID);

                if (model.TPID != "" & model.TPID != null)
                    queryResult = queryResult.Where(x => x.TPID == model.TPID);

                if (model.SQID != "" & model.SQID != null)
                    queryResult = queryResult.Where(x => x.SQID == model.SQID);

                if (model.SBID != "" & model.SBID != null)
                    queryResult = queryResult.Where(x => x.SBID == model.SBID);

                if (model.DATE != "" & model.DATE != null)
                    queryResult = queryResult.Where(x => x.DATE == Convert.ToDateTime(model.DATE));
                    //queryResult = queryResult.Where(x => x.DATE == DateTime.ParseExact(model.DATE, "yyyyMMdd", null));

                if (model.SQNO != "" & model.SQNO != null)
                    queryResult = queryResult.Where(x => x.SQNO == model.SQNO);

                if (model.CAMT != null)
                    queryResult = queryResult.Where(x => x.CAMT == model.CAMT);

                if (model.DAMT != null)
                    queryResult = queryResult.Where(x => x.DAMT == model.DAMT);

                if (model.TPNM != "" & model.TPNM != null)
                    queryResult = queryResult.Where(x => x.TPNM == model.TPNM);

                if (model.YYMM != "" & model.YYMM != null)
                    queryResult = queryResult.Where(x => x.YYMM == model.YYMM);

                if (model.ITID != "" & model.ITID != null)
                    queryResult = queryResult.Where(x => x.ITID == model.ITID);

                result = new JavaScriptSerializer().Serialize(queryResult.ToList());
            }
            catch (Exception)
            {
                //TODO:
                throw;
            }
            finally { db.Dispose(); }
            return result;
        }

        //[ValidateAntiForgeryToken]
        public string QueryACBJ(PageInfoModel pgingModel, ACBJViewModel model)
        {
            string result = string.Empty;
            this.db = new GWEB_TESTEntities();

            try
            {
                //PAGING//
                //var queryResult = (from p in db.ACBJ select p).OrderBy(x => x.TPID).Skip(pgingModel.PageNum * pgingModel.PageSize).Take(pgingModel.PageSize);

                //NO PAGING//
                var queryResult = (from p in db.ACBJ select p);

                //FILTER ONLY WITH PRIMARY K,.. NOT REALLY EXACTLY/FULLY RIGHT//
                if (model.TPID != "" & model.TPID != null)
                    queryResult = queryResult.Where(x => x.TPID == model.TPID);

                if (model.SQID != "" & model.SQID != null)
                    queryResult = queryResult.Where(x => x.SQID == model.SQID);

                if (model.ACID != "" & model.ACID != null)
                    queryResult = queryResult.Where(x => x.ACID == model.ACID);

                if (model.SBID != "" & model.SBID != null)
                    queryResult = queryResult.Where(x => x.SBID == model.SBID);

                if (model.SQNO != "" & model.SQNO != null)
                    queryResult = queryResult.Where(x => x.SQNO == model.SQNO);

                if (model.PJID != "" & model.PJID != null)
                    queryResult = queryResult.Where(x => x.PJID == model.PJID);

                if (model.PJSQ != "" & model.PJSQ != null)
                    queryResult = queryResult.Where(x => x.PJSQ == model.PJSQ);

                result = new JavaScriptSerializer().Serialize(queryResult.ToList());
            }
            catch (Exception)
            {
                //TODO:
                throw;
            }
            finally { db.Dispose(); }
            return result;
        }

        //[ValidateAntiForgeryToken]
        [HttpGet]
        public ActionResult QueryACNM(ACMS model)
        {
            string result = string.Empty;
            this.db = new GWEB_TESTEntities();
            try
            {
                var queryResult = (from p in db.ACMS where p.ACID == model.ACID select p.ACNM).FirstOrDefault();
                if (queryResult != null)
                {
                    result = queryResult.ToString();
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception)
            {
                //TODO:
                throw;
            }
            finally { db.Dispose(); }
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        //[ValidateAntiForgeryToken]
        [HttpGet]
        public ActionResult QueryLatestTic_A()
        {
            string result = string.Empty;
            this.db = new GWEB_TESTEntities();
            try
            {
                var queryResult = (from p in db.ACHD where p.TPID == "現入" select p).OrderByDescending(p=>p.DATE).OrderByDescending(q=>q.SQID).Take(1).FirstOrDefault();
                if (queryResult != null)
                {
                    result = string.Format("現入: {0} - {1}", queryResult.SQID, queryResult.DATE.Day.ToString("00"));
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception)
            {
                //TODO:
                throw;
            }
            finally { db.Dispose(); }
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult QueryLatestTic_B(ACMS model)
        {
            string result = string.Empty;
            this.db = new GWEB_TESTEntities();
            try
            {
                var queryResult = (from p in db.ACHD where p.TPID == "現支" select p).OrderByDescending(p => p.DATE).OrderByDescending(q => q.SQID).Take(1).FirstOrDefault();
                if (queryResult != null)
                {
                    result = string.Format("現支: {0} - {1}", queryResult.SQID, queryResult.DATE.Day.ToString("00"));
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception)
            {
                //TODO:
                throw;
            }
            finally { db.Dispose(); }
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult QueryLatestTic_C(ACMS model)
        {
            string result = string.Empty;
            this.db = new GWEB_TESTEntities();
            try
            {
                var queryResult = (from p in db.ACHD where p.TPID == "轉帳" select p).OrderByDescending(p => p.DATE).OrderByDescending(q => q.SQID).Take(1).FirstOrDefault();
                if (queryResult != null)
                {
                    result = string.Format("轉帳: {0} - {1}", queryResult.SQID, queryResult.DATE.Day.ToString("00"));
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception)
            {
                //TODO:
                throw;
            }
            finally { db.Dispose(); }
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        //[ValidateAntiForgeryToken]
        [HttpGet]
        public ActionResult QuerySBNM(ACSB model)
        {
            string result = string.Empty;
            this.db = new GWEB_TESTEntities();
            try
            {
                var queryResult = (from p in db.ACSB where p.ACID == model.ACID && p.SBID == model.SBID select p.SBNM).FirstOrDefault();
                if (queryResult != null)
                {
                    result = queryResult.ToString();
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception)
            {
                //TODO:
                throw;
            }
            finally { db.Dispose(); }
            return Json(new { data = result, ACID = model.ACID }, JsonRequestBehavior.AllowGet);
        }
        //[ValidateAntiForgeryToken]
        [HttpGet]
        public ActionResult QueryACHD_SQID(ACHD model)
        {
            //查詢傳票號碼//
            string result = string.Empty;
            this.db = new GWEB_TESTEntities();
            try
            {
                var queryResult = (from p in db.ACHD
                                  where 
                                    p.DATE.Year == model.DATE.Year
                                    && p.DATE.Month == model.DATE.Month
                                    && p.TPID == model.TPID
                                  select p).Count();
                string SQID = (queryResult + 1).ToString("000");
                result = (model.DATE.Year - 1911).ToString() + model.DATE.Month.ToString("00") + SQID;
            }
            catch (Exception)
            {
                //TODO:
                throw;
            }
            finally { db.Dispose(); }
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        ////[ValidateAntiForgeryToken]
        //[HttpGet]
        //public ActionResult QueryACBJ_PJSQ(ACHD model)
        //{
        //    //查詢傳票號碼//
        //    string result = string.Empty;
        //    this.db = new GWEB_TESTEntities();
        //    try
        //    {
        //        var queryResult = (from p in db.ACHD
        //                           where
        //                             p.DATE.Year == model.DATE.Year
        //                             && p.DATE.Month == model.DATE.Month
        //                             && p.TPID == model.TPID
        //                           select p).Count();
        //        string SQID = (queryResult + 1).ToString("000");
        //        result = (model.DATE.Year - 1911).ToString() + model.DATE.Month.ToString("00") + SQID;
        //    }
        //    catch (Exception)
        //    {
        //        //TODO:
        //        throw;
        //    }
        //    finally { db.Dispose(); }
        //    return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public ActionResult QueryITNM(ACIT model)
        {
            string result = string.Empty;
            this.db = new GWEB_TESTEntities();
            try
            {
                var queryResult = (from p in db.ACIT
                                   where p.ACID == model.ACID 
                                       && p.SBID == model.SBID 
                                       && p.ITID == model.ITID
                                   select p.ITNM).FirstOrDefault();
                if (queryResult != null)
                {
                    result = queryResult.ToString();
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception)
            {
                //TODO:
                throw;
            }
            finally { db.Dispose(); }
            return Json(new { data = result}, JsonRequestBehavior.AllowGet);
        }
        [NonAction]
        private string GetSqno(DateTime date,string tpid, string sqid)
        {
            //像是在計算 該日 該類 的傳票有幾張...//
            this.db = new GWEB_TESTEntities();
            string result = "";
            try
            {
                var queryResult = 
                    this.db.ACBD.Where(x => 
                       x.DATE == date
                    && x.TPID == tpid
                    //&& x.SQID == sqid//
                    ).Count();
                string temp = queryResult.ToString().PadLeft(3, '0');
                result = temp.Substring(temp.Length-1,-3);
            }
            catch (Exception)
            {
                //TODO:
                throw;
            }
            finally { db.Dispose(); }
            return result;
        }
    }
}