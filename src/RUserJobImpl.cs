/*
 * RUserJobImpl.cs
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
using System.Net;
using System.Web;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace DeployR
{
    internal class RUserJobImpl
    {

        static public RJob callJob(String name, String descr, String code, String scriptName, String scriptDirectory, String scriptAuthor, String scriptVersion, String ExternalSource, JobExecutionOptions options, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&name=" + HttpUtility.UrlEncode(name));
            data.Append("&descr=" + HttpUtility.UrlEncode(descr));
            data.Append("&code=" + HttpUtility.UrlEncode(code));

            if (ExternalSource == null)
            {
                data.Append("&rscriptname=" + HttpUtility.UrlEncode(scriptName));
                data.Append("&rscriptdirectory=" + HttpUtility.UrlEncode(scriptDirectory));
                data.Append("&rscriptauthor=" + HttpUtility.UrlEncode(scriptAuthor));
                data.Append("&rscriptversion=" + HttpUtility.UrlEncode(scriptVersion));
            }
            else
            {
                data.Append("&externalsource=" + HttpUtility.UrlEncode(ExternalSource));
            }

            if (!(options == null))
            {

                data.Append("&csvinputs=" + HttpUtility.UrlEncode(options.csvrinputs));

                if (!(options.rinputs == null))
                {
                    if (options.rinputs.Count > 0)
                    {
                        data.Append("&inputs=");
                        String sJSON = JSONSerialize.createJSONfromRData(options.rinputs);
                        data.Append(HttpUtility.UrlEncode(sJSON));
                        if (HTTPUtilities.DEBUGMODE == true)
                        {
                            Console.Write(sJSON);
                        }
                    }
                }

                if (!(options.preloadDirectory == null))
                {
                    data.Append("&preloadfilename=" + HttpUtility.UrlEncode(options.preloadDirectory.filename));
                    data.Append("&preloaddirectory=" + HttpUtility.UrlEncode(options.preloadDirectory.directory));
                    data.Append("&preloadfileauthor=" + HttpUtility.UrlEncode(options.preloadDirectory.author));
                    data.Append("&preloadfileversion=" + HttpUtility.UrlEncode(options.preloadDirectory.version));
                }

                if (!(options.preloadWorkspace == null))
                {
                    data.Append("&preloadobjectname=" + HttpUtility.UrlEncode(options.preloadWorkspace.filename));
                    data.Append("&preloadobjectdirectory=" + HttpUtility.UrlEncode(options.preloadWorkspace.directory));
                    data.Append("&preloadobjectauthor=" + HttpUtility.UrlEncode(options.preloadWorkspace.author));
                    data.Append("&preloadobjectversion=" + HttpUtility.UrlEncode(options.preloadWorkspace.version));
                }

                if (!(options.adoptionOptions == null))
                {
                    data.Append("&adoptworkspace=" + HttpUtility.UrlEncode(options.adoptionOptions.adoptWorkspace));
                    data.Append("&adoptdirectory=" + HttpUtility.UrlEncode(options.adoptionOptions.adoptDirectory));
                    data.Append("&adoptpackages=" + HttpUtility.UrlEncode(options.adoptionOptions.adoptPackages));
                }

                if (!(options.storageOptions == null))
                {
                    data.Append("&storefile=" + HttpUtility.UrlEncode(options.storageOptions.files));
                    data.Append("&storedirectory=" + HttpUtility.UrlEncode(options.storageOptions.directory));
                    data.Append("&storeobject=" + HttpUtility.UrlEncode(options.storageOptions.objects));
                    data.Append("&storeworkspace=" + HttpUtility.UrlEncode(options.storageOptions.workspace));
                    data.Append("&storenewversion=" + options.storageOptions.newVersion.ToString());
                    data.Append("&storepublic=" + options.storageOptions.published.ToString());
                }

                if (!(options.routputs == null))
                {
                    if (options.routputs.Count > 0)
                    {
                        data.Append("&robjects=");
                        foreach (var s in options.routputs)
                        {
                            data.Append(HttpUtility.UrlEncode(s) + ",");
                        }
                        data.Remove(data.Length - 1, 1);
                    }
                }

                data.Append("&echooff=" + options.echooff.ToString());
                data.Append("&consoleoff=" + options.consoleoff.ToString());
                data.Append("&tag=" + HttpUtility.UrlEncode(options.tag));
                data.Append("&graphics=" + HttpUtility.UrlEncode(options.graphicsDevice));
                data.Append("&graphicswidth=" + options.graphicsWidth.ToString());
                data.Append("&graphicsheight=" + options.graphicsHeight.ToString());
                data.Append("&nan=" + HttpUtility.UrlEncode(options.nan));
                data.Append("&infinity=" + HttpUtility.UrlEncode(options.infinity));
                data.Append("&encodeDataFramePrimitiveAsVector=" + options.encodeDataFramePrimitiveAsVector.ToString());
                data.Append("&enableConsoleEvents=" + options.enableConsoleEvents.ToString());
                data.Append("&preloadbydirectory=" + HttpUtility.UrlEncode(options.preloadByDirectory));

                if (!(options.schedulingOptions == null))
                {
                    data.Append("&schedstart=" + options.schedulingOptions.startTime.ToString());
                    data.Append("&schedrepeat=" + options.schedulingOptions.repeatCount.ToString());
                    data.Append("&schedinterval=" + options.schedulingOptions.repeatInterval.ToString());
                }

                data.Append("&storenoproject=" + options.noProject.ToString());
                data.Append("&priority=" + HttpUtility.UrlEncode(options.priority));
                data.Append("&cluster=" + HttpUtility.UrlEncode(options.gridCluster));
            }

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);
            RJob returnValue = new RJob(jresponse, client);

            return returnValue;
        }

        static public List<RJob> listJobs(RClient client, String uri)
        {
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref client);

            List<RJob> returnValue = new List<RJob>();

            if (!(jresponse.JSONMarkup["jobs"] == null))
            {
                JArray jvalues = jresponse.JSONMarkup["jobs"].Value<JArray>();
                foreach (var j in jvalues)
                {
                    if (j.Type != JTokenType.Null)
                    {
                        returnValue.Add(new RJob(new JSONResponse(j.Value<JObject>(), true, "", 0), client));
                    }
                }
            }


            return returnValue;
        }
    }
}