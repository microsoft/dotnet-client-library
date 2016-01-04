/*
 * RUserRepositoryScriptImpl.cs
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace DeployR
{

    internal class RUserRepositoryScriptImpl
    {

        static public List<RRepositoryScript> listScripts(String filename, String directory, Boolean archived, Boolean sharedUsers, Boolean published, Boolean external, String categoryFilter, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&filename=" + HttpUtility.UrlEncode(filename));
            data.Append("&directory=" + HttpUtility.UrlEncode(directory));
            data.Append("&archived=" + archived.ToString());
            data.Append("&shared=" + sharedUsers.ToString());
            data.Append("&published=" + published.ToString());
            data.Append("&external=" + external.ToString());
            data.Append("&categoryFilter=" + HttpUtility.UrlEncode(categoryFilter));

            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref client);

            List<RRepositoryScript> returnValue = new List<RRepositoryScript>();

            if (!(jresponse.JSONMarkup["repository"] == null))
            {
                JObject jrepo = jresponse.JSONMarkup["repository"].Value<JObject>();
                if (!(jrepo["scripts"] == null))
                {
                    JArray jvalues = jrepo["scripts"].Value<JArray>();
                    foreach (var j in jvalues)
                    {
                        if (j.Type != JTokenType.Null)
                        {
                            returnValue.Add(new RRepositoryScript(new JSONResponse(j.Value<JObject>(), true, "", 0), client));
                        }
                    }
                }
            }


            return returnValue;
        }
    }
}