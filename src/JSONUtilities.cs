/*
 * JSONUtilities.cs
 *
 * Copyright (C) 2010-2014 by Revolution Analytics Inc.
 *
 * This program is licensed to you under the terms of Version 2.0 of the
 * Apache License. This program is distributed WITHOUT
 * ANY EXPRESS OR IMPLIED WARRANTY, INCLUDING THOSE OF NON-INFRINGEMENT,
 * MERCHANTABILITY OR FITNESS FOR A PARTICULAR PURPOSE. Please refer to the
 * Apache License 2.0 (http://www.apache.org/licenses/LICENSE-2.0) for more details.
 *
 */

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace DeployR
{
/// <summary>
/// A collection of utility functions mainly for parsing JSON code and creating deployR objects from them.  These functions use the json.net 3rd party library
/// </summary>
/// <remarks></remarks>
    sealed class JSONUtilities
    {

        /// <summary>
        /// Trim extra quotes from a String
        /// </summary>
        /// <param name="value">String to trim</param>
        /// <returns>String</returns>
        /// <remarks></remarks>
        public static String trimXtraQuotes(String value)
        {
            if ((value == null) || (value.Length == 0))
            {
                return value;
            }

            String tmp = value;
            try
            {
                if (tmp.Substring(0, 1) == "\"")
                {
                    tmp = tmp.Substring(tmp.Length - (tmp.Length - 1), tmp.Length - 1);
                }
                if (tmp.Substring(tmp.Length - 1, 1) == "\"")
                {
                    tmp = tmp.Substring(0, tmp.Length - 1);
                }
            }
            catch
            {
                return value;
            }
            return tmp;
        }

        /// <summary>
        /// Replace a "\n" String with a real end of line
        /// </summary>
        /// <param name="value">String to replace</param>
        /// <returns>String</returns>
        /// <remarks></remarks>
        public static String replaceEndOfLine(String value)
        {
            return value.Replace("\\n", "\r\n"); ;
        }

        /// <summary>
        /// Take the response from an HTTP POST/GET and check the "success" and "error" properties,  then create a 'JSONResponse instance
        /// </summary>
        /// <param name="responseText">JSON markup to parse</param>
        /// <returns>JSONRepsonse object</returns>
        /// <remarks></remarks>
        public static JSONResponse checkForSuccess(String responseText)
        {

            JObject jresponse = default(JObject);
            String sSuccess = "";
            String errormsg = "";
            String console = "";
            int errorcode = 0;
            Boolean success = false;

            JObject deployrRoot = null;

            try
            {
                //let json.net parse the response
                jresponse = JObject.Parse(responseText);
                //get the deployr/response tree
                deployrRoot = jresponse["deployr"]["response"].Value<JObject>();

                //check for success
                sSuccess = deployrRoot["success"].Value<String>();
                if (sSuccess.ToLower() == "false")
                {
                    errormsg = deployrRoot["error"].Value<string>();
                    errorcode = deployrRoot["errorCode"].Value<int>();
                    success = false;
                }
                else
                {
                    success = true;
                }
                if (!(deployrRoot["execution"] == null))
                {
                    JObject jscriptexec = deployrRoot["execution"].Value<JObject>();
                    console = jscriptexec["console"].Value<string>();
                }

            }
            catch
            {
            }

            //create the JSONResponse class
            JSONResponse returnValue = new JSONResponse(deployrRoot, success, errormsg, console, errorcode);

            return returnValue;
        }

        /// <summary>
        /// Parse "robjects" object
        /// </summary>
        /// <param name="jroot">JObject to parse</param>
        /// <returns>Dictionary of RData objects (ByRef)</returns>
        /// <remarks></remarks>
        public static List<RData> parseRObjects(JObject jroot)
        {
            RData data = default(RData);
            List<RData> robjects = new List<RData>();


            if (!(jroot["workspace"] == null))
            {
                JObject jworkspace = jroot["workspace"].Value<JObject>();
                if (!(jworkspace["objects"] == null))
                {
                    JArray jvalues = jworkspace["objects"].Value<JArray>();
                    foreach (var jtok in jvalues)
                    {
                        data = JSONSerialize.parseRObject(jtok);
                        robjects.Add(data);
                    }
                }
                else if (!(jworkspace["object"] == null))
                {
                    JToken jtok = jworkspace["object"].Value<JToken>();
                    data = JSONSerialize.parseRObject(jtok);
                    robjects.Add(data);
                }
            }

            return robjects;
        }

    }
}