﻿using System;
using System.Web.Mvc;
using CalculationCSharp.Areas.Scheme.Models;
using CalculationCSharp.Controllers;
using CalculationCSharp.Models.Calculation;
using System.Collections.Generic;

namespace CalculationCSharp.Areas.Scheme.Controllers

{
    public class DeferredController : CalculationBaseController
    {
        public Deferred InputForm = new Deferred();
        public new CalculationCSharp.Models.XMLFunctions.XMLFunctions xmlfunction = new CalculationCSharp.Models.XMLFunctions.XMLFunctions();

        // GET: Scheme/Deferred
        [HttpGet()]
        public ActionResult Input()
        {
            ViewData["H1"] = "Scheme " + this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewData["P1"] = "";
            InputForm.CalcReference = "1";
            InputForm.CalcDate = Convert.ToDateTime("17/07/2015");
            InputForm.DOL = Convert.ToDateTime("17/07/2015");
            InputForm.APP = 23000;
            InputForm.Contributions = 1200;
            InputForm.PIDateOverride = Convert.ToDateTime("30/04/2013");
            InputForm.DOB = Convert.ToDateTime("30/09/1973");
            InputForm.MarStat = "Married";
            InputForm.DJS = Convert.ToDateTime("03/04/1991");
            InputForm.Grade = "Firefighter";
            

            return View("Input", InputForm);
        }

        //POST: Calculation
        [HttpPost()]
        public ActionResult Input(Deferred InputForm, string ButtonType)
        {

            String Scheme = this.Request.RequestContext.RouteData.DataTokens["area"].ToString();
            String CalcType = this.ControllerContext.RouteData.Values["controller"].ToString();

            object Calculation = Calculate(InputForm, Scheme, CalcType, InputForm.CalcReference, false);
            string InputXML = xmlfunction.XMLStringBuilder(InputForm);
            string OutputXML = xmlfunction.XMLStringBuilder(Calculation);

            if (ButtonType == "Download")
            {
                List<OutputList> Output = new List<OutputList>();
                Output = Calculation as List<OutputList>;
                return new DownloadFileActionResult(Output, "Output.xls");
            }
            else if (ButtonType == "Regression")
            {
                CalculationRegressionAdd(InputXML, OutputXML, InputForm.CalcReference, Scheme, CalcType, false);
                return RedirectToAction("Input");
            }
            else
            {
                return PartialView("_Output", Calculation);
            }
            
        }

    }
}