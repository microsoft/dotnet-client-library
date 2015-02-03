/*
 * RProjectBaseImpl.cs
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

    internal class RProjectBaseImpl
    {

        static public RProjectDetails about(RProjectDetails details, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref client);

            RProjectDetails returnValue = default(RProjectDetails);
            parseProject(jresponse, ref returnValue);
            return returnValue;
        }

        static public void close(RProjectDetails details, ProjectCloseOptions options, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            if (!(options == null))
            {
                if (!(options.dropOptions == null))
                {
                    data.Append("&dropworkspace=" + options.dropOptions.dropWorkspace.ToString());
                    data.Append("&dropdirectory=" + options.dropOptions.dropDirectory.ToString());
                    data.Append("&drophistory=" + options.dropOptions.dropHistory.ToString());
                }
                data.Append("&flushhistory=" + options.flushHistory.ToString());
                data.Append("&disableautosave=" + options.disableAutosave.ToString());
                data.Append("&projectcookie=" + options.cookie);
            }
            
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

        }

        static public void delete(RProjectDetails details, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

        }

        static public String export(RProjectDetails details, RClient client, String uri)
        {
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref client);

            String returnValue = System.Convert.ToString(HttpUtility.UrlEncode(uri + "/" + details.id + ";jsessionid=" + client.Cookie.Value));
            return returnValue;
        }

        static public String ping(RProjectDetails details, RClient client, String uri)
        {
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref client);

            RProjectDetails projectDetails = default(RProjectDetails);
            RProjectBaseImpl.parseProject(jresponse, ref projectDetails);
            String returnValue = "";
            if (!(projectDetails == null))
            {
                returnValue = System.Convert.ToString(projectDetails.islive);
            }
            else
            {
                returnValue = System.Convert.ToString(false);
            }

            return returnValue;
        }

        static public RProjectDetails recycle(RProjectDetails details, RClient client, String uri)
        {
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);


            RProjectDetails returnValue = default(RProjectDetails);
            parseProject(jresponse, ref returnValue);
            return returnValue;
        }

        static public RProjectDetails save(RProjectDetails details, ProjectDropOptions options, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            data.Append("&name=" + HttpUtility.UrlEncode(details.name));
            data.Append("&descr=" + HttpUtility.UrlEncode(details.descr));
            data.Append("&longdescr=" + HttpUtility.UrlEncode(details.longdescr));
            data.Append("&projectcookie=" + HttpUtility.UrlEncode(details.cookie));
            data.Append("&shared=" + details.sharedUsers.ToString());

            if (!(options == null))
            {
                data.Append("&dropworkspace=" + options.dropWorkspace.ToString());
                data.Append("&dropdirectory=" + options.dropDirectory.ToString());
                data.Append("&drophistory=" + options.dropHistory.ToString());
            }

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

            RProjectDetails returnValue = default(RProjectDetails);
            parseProject(jresponse, ref returnValue);
            return returnValue;
        }

        static public RProject saveAs(RProjectDetails details, ProjectDropOptions options, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            data.Append("&name=" + HttpUtility.UrlEncode(details.name));
            data.Append("&descr=" + HttpUtility.UrlEncode(details.descr));
            data.Append("&longdescr=" + HttpUtility.UrlEncode(details.longdescr));
            data.Append("&projectcookie=" + HttpUtility.UrlEncode(details.cookie));
            data.Append("&shared=" + details.sharedUsers.ToString());

            if (!(options == null))
            {
                data.Append("&dropworkspace=" + options.dropWorkspace.ToString());
                data.Append("&dropdirectory=" + options.dropDirectory.ToString());
                data.Append("&drophistory=" + options.dropHistory.ToString());
            }

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

            RProject returnValue = default(RProject);
            returnValue = new RProject(jresponse, client);
            return returnValue;
        }

        static public RProjectDetails grant(String author, RProjectDetails details, RClient client, String uri)
        {
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            data.Append("&newauthor=" + HttpUtility.UrlEncode(author));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

            RProjectDetails returnValue = default(RProjectDetails);
            parseProject(jresponse, ref returnValue);
            return returnValue;
        }

        static public RProjectDetails update(RProjectDetails details, RClient client, String uri)
        {
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(details.id));
            data.Append("&name=" + HttpUtility.UrlEncode(details.name));
            data.Append("&descr=" + HttpUtility.UrlEncode(details.descr));
            data.Append("&longdescr=" + HttpUtility.UrlEncode(details.longdescr));
            data.Append("&projectcookie=" + HttpUtility.UrlEncode(details.cookie));
            data.Append("&shared=" + details.sharedUsers.ToString());

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

            RProjectDetails returnValue = default(RProjectDetails);
            parseProject(jresponse, ref returnValue);
            return returnValue;
        }

        static public void parseProject(JSONResponse jresponse, ref RProjectDetails projectDetails)
        {

            List<String> authors = new List<String>();
            JObject jproject = default(JObject);

            if (jresponse.JSONMarkup["project"].Type == JTokenType.Object)
            {
                jproject = jresponse.JSONMarkup["project"].Value<JObject>();
            }
            else
            {
                jproject = jresponse.JSONMarkup;
            }
            if (!(jproject == null))
            {
                String cookie = JSONUtilities.trimXtraQuotes(jproject["cookie"].Value<string>());
                String descr = JSONUtilities.trimXtraQuotes(jproject["descr"].Value<string>());
                String id = JSONUtilities.trimXtraQuotes(jproject["project"].Value<string>());
                Boolean live = jproject["live"].Value<bool>();
                Boolean sharedUsers = jproject["shared"].Value<bool>();
                String longdescr = JSONUtilities.trimXtraQuotes(jproject["longdescr"].Value<string>());
                String modified = JSONUtilities.trimXtraQuotes(jproject["lastmodified"].Value<string>());
                String name = JSONUtilities.trimXtraQuotes(jproject["name"].Value<string>());
                String origin = JSONUtilities.trimXtraQuotes(jproject["origin"].Value<string>());

                if (!(jproject["authors"] == null))
                {
                    JArray jvalues = jproject["authors"].Value<JArray>();
                    foreach (var j in jvalues)
                    {
                        if (j.Type != JTokenType.Null)
                        {
                            authors.Add(j.Value<String>());
                        }
                    }
                }

                projectDetails = new RProjectDetails(cookie, descr, id, live, longdescr, modified, name, origin, sharedUsers, authors);

            }

        }

        static public void parseProjectExecution(JSONResponse jresponse, ref RProjectExecutionDetails executionDetails, ref RProjectDetails projectDetails, RClient client)
        {

            List<RProjectFile> projectfiles = new List<RProjectFile>();
            List<RRepositoryFile> repositoryFiles = new List<RRepositoryFile>();
            List<RProjectResult> results = new List<RProjectResult>();
            List<String> warnings = new List<String>();
            List<RData> workspaceObjects = new List<RData>();
            JArray jvalues;
            JObject jrepo;

            if (!(jresponse.JSONMarkup["execution"] == null))
            {
                JObject jscriptexec = jresponse.JSONMarkup["execution"].Value<JObject>();
                
                String code = JSONUtilities.trimXtraQuotes(jscriptexec["code"].Value<string>());
                long timeStart = jscriptexec["timeStart"].Value<long>();
                long timeCode = jscriptexec["timeCode"].Value<long>();
                long timeTotal = jscriptexec["timeTotal"].Value<long>();
                String tag = JSONUtilities.trimXtraQuotes(jscriptexec["tag"].Value<string>());
                String console = JSONUtilities.trimXtraQuotes(jscriptexec["console"].Value<string>());
                String errorDescr = jresponse.ErrorMsg;
                int errorCode = jresponse.ErrorCode;
                String id = JSONUtilities.trimXtraQuotes(jscriptexec["execution"].Value<string>());
                Boolean interrupted = Convert.ToBoolean(jscriptexec["interrupted"].Value<string>());

                if (!(jscriptexec["results"] == null))
                {
                    jvalues = jscriptexec["results"].Value<JArray>();
                    foreach (var j in jvalues)
                    {
                        if (j.Type != JTokenType.Null)
                        {
                            results.Add(new RProjectResult(new JSONResponse(j.Value<JObject>(), true, "", 0), client));
                        }
                    }
                }

                if (!(jscriptexec["artifacts"] == null))
                {
                    jvalues = jscriptexec["artifacts"].Value<JArray>();
                    foreach (var j in jvalues)
                    {
                        if (j.Type != JTokenType.Null)
                        {
                            projectfiles.Add(new RProjectFile(new JSONResponse(j.Value<JObject>(), true, "", 0), client, id));
                        }
                    }
                }

                if (!(jscriptexec["warnings"] == null))
                {
                    jvalues = jscriptexec["warnings"].Value<JArray>();
                    foreach (var j in jvalues)
                    {
                        if (j.Type != JTokenType.Null)
                        {
                            warnings.Add(j.Value<string>());
                        }
                    }
                }

                if (!(jscriptexec["repository"] == null))
                {
                    jrepo = jscriptexec["repository"].Value<JObject>();
                    
                    if (!(jrepo["files"] == null))
                    {
                        jvalues = jrepo["files"].Value<JArray>();
                        foreach (var j in jvalues)
                        {
                            if (j.Type != JTokenType.Null)
                            {
                                repositoryFiles.Add(new RRepositoryFile(new JSONResponse(j.Value<JObject>(), true, "", 0), client));
                            }
                        }
                    }
                }

                workspaceObjects = JSONUtilities.parseRObjects(jresponse.JSONMarkup);
                parseProject(jresponse, ref projectDetails);

                executionDetails = new RProjectExecutionDetails(projectfiles, code, timeStart, timeCode, timeTotal, tag, console, errorDescr, errorCode, id, interrupted, null, results, warnings, workspaceObjects);

            }
        }

    }
}