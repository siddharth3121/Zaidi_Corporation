using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zaidi_Corporation.DataManager;
using Zaidi_Corporation.Models;

namespace Zaidi_Corporation.Controllers
{
    public class CrudController : Controller
    {
        /// <summary>
        /// Get Main page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ModelState.Clear();
            return View();
        }


        /// <summary>
        /// Post information of User
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(CrudModel objData)
        {
            if (objData != null)
            {
                try
                {
                    var objInsertData = new CrudManager();
                    bool bResult = objInsertData.InsertDetails(objData);
                }
                catch (Exception)
                {
                    throw;
                }


            }
            //ModelState.Clear();
            return View("Index");
        }

        /// <summary>
        /// Return Batch Datatable
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetails()
        {
            List<CrudModel> lstData = new List<CrudModel>();
            try
            {
                CrudManager objTable = new CrudManager();
                lstData = objTable.GetDetails();
            }
            catch (Exception)
            {

                throw;
            }
            return Json(new { data = lstData }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Edit User Details
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit1(CrudModel objData)
        {            
            try
            {
                CrudManager objCrudmanager = new CrudManager();
                bool bResult = objCrudmanager.EditDetails(objData);
            }
            catch (Exception)
            {

                throw;
            }
            return View("Index");
        }
        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public ActionResult Delete(String Name)
        {
            try
            {
                CrudManager objCrudmanager = new CrudManager();
                bool bResult = objCrudmanager.DeleteDetails(Name);
            }
            catch (Exception)
            {
                throw;
            }
            return View("Index");
        }


        /// <summary>
        ///Edit Details
        /// </summary>
        /// <returns></returns>        
        public ActionResult Edit(string sBatchName, string sBatchLoaction)
        {
            CrudModel objData = null;
            try
            {
                CrudManager objCrudmanager = new CrudManager();
                objData = objCrudmanager.GetSpecificUser(sBatchName);
            }
            catch (Exception)
            {

                throw;
            }
            return PartialView("Edit" , objData);
        }
                     
    }
}