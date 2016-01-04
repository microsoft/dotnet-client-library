/*
 * RProjectPackageImpl.cs
 *
 * Copyright (C) 2010-2015 by Microsoft Corporation
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
using System.Net;
using System.Web;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DeployR
{

    internal class RProjectPackageImpl
    {

        static public List<RProjectPackage> attachPackage(RProjectDetails details, List<String> packageNames, String repo, RClient client, String uri)
        {
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            data.Append("&repo=" + HttpUtility.UrlEncode(repo));
            if (!(packageNames == null))
            {
                if (packageNames.Count > 0)
                {
                    data.Append("&name=");
                    foreach (var s in packageNames)
                    {
                        data.Append(HttpUtility.UrlEncode(s) + ",");
                    }
                    data.Remove(data.Length - 1, 1);
                }
            }

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

            List<RProjectPackage> returnValue = new List<RProjectPackage>();

            if (!(jresponse.JSONMarkup["packages"] == null))
            {
                JArray jvalues = jresponse.JSONMarkup["packages"].Value<JArray>();
                foreach (var j in jvalues)
                {
                    if (j.Type != JTokenType.Null)
                    {
                        returnValue.Add(new RProjectPackage(new JSONResponse(j.Value<JObject>(), true, "", 0), client));
                    }
                }
            }

            return returnValue;
        }

        static public List<RProjectPackage> detachPackage(RProjectDetails details, List<String> packageNames, RClient client, String uri)
        {
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            if (!(packageNames == null))
            {
                if (packageNames.Count > 0)
                {
                    data.Append("&name=");
                    foreach (var s in packageNames)
                    {
                        data.Append(HttpUtility.UrlEncode(s) + ",");
                    }
                    data.Remove(data.Length - 1, 1);
                }
            }

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

            List<RProjectPackage> returnValue = new List<RProjectPackage>();

            if (!(jresponse.JSONMarkup["packages"] == null))
            {
                JArray jvalues = jresponse.JSONMarkup["packages"].Value<JArray>();
                foreach (var j in jvalues)
                {
                    if (j.Type != JTokenType.Null)
                    {
                        returnValue.Add(new RProjectPackage(new JSONResponse(j.Value<JObject>(), true, "", 0), client));
                    }
                }
            }

            return returnValue;
        }

        static public List<RProjectPackage> listPackages(RProjectDetails details, Boolean installed, RClient client, String uri)
        {
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            data.Append("&installed=" + installed.ToString());

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref client);

            List<RProjectPackage> returnValue = new List<RProjectPackage>();

            if (!(jresponse.JSONMarkup["packages"] == null))
            {
                JArray jvalues = jresponse.JSONMarkup["packages"].Value<JArray>();
                foreach (var j in jvalues)
                {
                    if (j.Type != JTokenType.Null)
                    {
                        returnValue.Add(new RProjectPackage(new JSONResponse(j.Value<JObject>(), true, "", 0), client));
                    }
                }
            }

            return returnValue;
        }

    }
}