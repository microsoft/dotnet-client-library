/*
 * RUserProjectImpl.cs
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

    internal class RUserProjectImpl
    {

        static public void autosaveProjects(Boolean save, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&enable=" + save.ToString().ToLower());
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);
        }

        static public void releaseProjects(RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);
        }

        static public RProject createProject(String name, String descr, ProjectCreationOptions options, RClient client, String uri)
        {
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&projectname=" + HttpUtility.UrlEncode(name));
            data.Append("&projectdescr=" + HttpUtility.UrlEncode(descr));

            if (!(options == null))
            {

                data.Append("&blackbox=" + options.blackbox.ToString());

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

                data.Append("&cluster=" + HttpUtility.UrlEncode(options.gridCluster));
            }

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

            RProject returnValue = new RProject(jresponse, client);

            return returnValue;
        }

        static public List<RProject> createProjectPool(int poolSize, ProjectCreationOptions options, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&poolsize=" + poolSize.ToString());

            if (!(options == null))
            {

                data.Append("&blackbox=" + options.blackbox.ToString());

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

                data.Append("&cluster=" + HttpUtility.UrlEncode(options.gridCluster));
            }

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

            List<RProject> returnValue = new List<RProject>();

            //Parse the response
            if (!(jresponse.JSONMarkup["projects"] == null))
            {
                JArray jvalues = jresponse.JSONMarkup["projects"].Value<JArray>();
                foreach (var j in jvalues)
                {
                    if (j.Type != JTokenType.Null)
                    {
                        returnValue.Add(new RProject(new JSONResponse(j.Value<JObject>(), true, "", 0), client));
                    }
                }
            }

            return returnValue;
        }

        static public RProject getProject(String name, RClient client, String uri)
        {

            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&project=" + HttpUtility.UrlEncode(name));
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

            RProject returnValue = new RProject(jresponse, client);

            return returnValue;
        }

        static public RProject importProject(String file, String descr, RClient client, String uri)
        {
            StringBuilder data = new StringBuilder();
            Dictionary<String, String> parameters = new Dictionary<String, String>();

            //create the input String
            parameters.Add("format", "json");
            parameters.Add("name", HttpUtility.UrlEncode(Path.GetFileName(file)));
            parameters.Add("descr", HttpUtility.UrlEncode(descr));
            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTFileUploadPost(uri, parameters, file, ref client);

            RProject returnValue = new RProject(jresponse, client);

            return returnValue;
        }

        static public List<RProject> listProjects(Boolean sortByLastModified, Boolean showPublicProjects, RClient client, String uri)
        {
            StringBuilder data = new StringBuilder();

            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&publicprojectsalso=" + showPublicProjects.ToString());
            data.Append("&publicprojectsonly=false");
            data.Append("&isordered=" + sortByLastModified.ToString());

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref client);

            List<RProject> returnValue = new List<RProject>();

            //Parse the response
            if (!(jresponse.JSONMarkup["projects"] == null))
            {
                JArray jvalues = jresponse.JSONMarkup["projects"].Value<JArray>();
                foreach (var j in jvalues)
                {
                    if (j.Type != JTokenType.Null)
                    {
                        returnValue.Add(new RProject(new JSONResponse(j.Value<JObject>(), true, "", 0), client));
                    }
                }
            }

            return returnValue;
        }
    }
}