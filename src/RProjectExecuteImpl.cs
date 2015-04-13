/*
 * RProjectExecuteImpl.cs
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
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DeployR
{

    internal class RProjectExecuteImpl
    {

        static public void deleteResults(RProjectDetails details, RClient client, String uri)
        {
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

        }

        static public String downloadResults(RProjectDetails details, RClient client, String uri)
        {
            String returnValue = client.URL + uri + "/" + details.id + ";jsessionid=" + client.Cookie.Value;

            return returnValue;
        }

        static public RProjectExecution executeCode(RProjectDetails details, String code, ProjectExecutionOptions options, RClient client, String uri)
        {
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            data.Append("&code=" + HttpUtility.UrlEncode(code));

            if (!(options == null))
            {

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
                    data.Append("&preloadfiledirectory=" + HttpUtility.UrlEncode(options.preloadDirectory.directory));
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
                data.Append("&graphicswidth=" + HttpUtility.UrlEncode(options.graphicsWidth.ToString()));
                data.Append("&graphicsheight=" + HttpUtility.UrlEncode(options.graphicsHeight.ToString()));
                data.Append("&nan=" + HttpUtility.UrlEncode(options.nan));
                data.Append("&infinity=" + HttpUtility.UrlEncode(options.infinity));
                data.Append("&encodeDataFramePrimitiveAsVector=" + options.encodeDataFramePrimitiveAsVector.ToString());
                data.Append("&enableConsoleEvents=" + options.enableConsoleEvents.ToString());
                data.Append("&preloadbydirectory=" + HttpUtility.UrlEncode(options.preloadByDirectory));

            }

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

            RProjectExecution returnValue = new RProjectExecution(jresponse, client);

            return returnValue;
        }

        static public RProjectExecution executeScript(RProjectDetails details, String scriptName, String scriptDirectory, String scriptAuthor, String scriptVersion, String ExternalSource, ProjectExecutionOptions options, RClient client, String uri)
        {
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));

            if (ExternalSource == null)
            {
                data.Append("&filename=" + HttpUtility.UrlEncode(scriptName));
                data.Append("&directory=" + HttpUtility.UrlEncode(scriptDirectory));
                data.Append("&author=" + HttpUtility.UrlEncode(scriptAuthor));
                data.Append("&version=" + HttpUtility.UrlEncode(scriptVersion));
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
                    data.Append("&preloadfiledirectory=" + HttpUtility.UrlEncode(options.preloadDirectory.directory));
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
                data.Append("&graphicswidth=" + HttpUtility.UrlEncode(options.graphicsWidth.ToString()));
                data.Append("&graphicsheight=" + HttpUtility.UrlEncode(options.graphicsHeight.ToString()));
                data.Append("&nan=" + HttpUtility.UrlEncode(options.nan));
                data.Append("&infinity=" + HttpUtility.UrlEncode(options.infinity));
                data.Append("&encodeDataFramePrimitiveAsVector=" + options.encodeDataFramePrimitiveAsVector.ToString());
                data.Append("&enableConsoleEvents=" + options.enableConsoleEvents.ToString());
                data.Append("&preloadbydirectory=" + HttpUtility.UrlEncode(options.preloadByDirectory));
            }


            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

            RProjectExecution returnValue = new RProjectExecution(jresponse, client);

            return returnValue;
        }

        static public void flushHistory(RProjectDetails details, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

        }

        static public String getConsole(RProjectDetails details, RClient client, String uri)
        {
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref client);


            String console = "";
            parseConsole(jresponse, ref console);
            return console;
        }

        static public List<RProjectExecution> getHistory(RProjectDetails details, ProjectHistoryOptions options, RClient client, String uri)
        {
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            if (!(options == null))
            {
                if (options.depthFilter < 1 || options.depthFilter > 500)
                {
                    options.depthFilter = 250;
                }
                data.Append("&filterdepth=" + options.depthFilter.ToString());
                data.Append("&filtertag=" + HttpUtility.UrlEncode(options.tagfilter));
                data.Append("&reversed=" + options.reversed.ToString());
            }
            else
            {
                data.Append("&filterdepth=250");
            }
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref client);

            List<RProjectExecution> returnValue = new List<RProjectExecution>();

            if (!(jresponse.JSONMarkup["history"] == null))
            {
                JArray jvalues = jresponse.JSONMarkup["history"].Value<JArray>();
                foreach (var j in jvalues)
                {
                    if (j.Type != JTokenType.Null)
                    {
                        returnValue.Add(new RProjectExecution(new JSONResponse(j.Value<JObject>(), true, "", 0), client));
                    }
                }
            }

            return returnValue;
        }

        static public void interruptExecution(RProjectDetails details, RClient client, String uri)
        {
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

        }

        static public List<RProjectResult> listResults(RProjectDetails details, RClient client, String uri)
        {
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref client);

            List<RProjectResult> returnValue = new List<RProjectResult>();

            if (!(jresponse.JSONMarkup["results"] == null))
            {
                JArray jvalues = jresponse.JSONMarkup["results"].Value<JArray>();
                foreach (var j in jvalues)
                {
                    if (j.Type != JTokenType.Null)
                    {
                        returnValue.Add(new RProjectResult(new JSONResponse(j.Value<JObject>(), true, "", 0), client));
                    }
                }
            }

            return returnValue;
        }

        static public void parseConsole(JSONResponse jresponse, ref String console)
        {

            if (!(jresponse.JSONMarkup["execution"] == null))
            {
                JObject jscriptexec = jresponse.JSONMarkup["execution"].Value<JObject>();
                console = JSONUtilities.trimXtraQuotes(jscriptexec["console"].Value<string>());
            }

        }
    }
}