/*
 * RClient.cs
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
/// <summary>
/// Represents a live RevoDeployR client connection. Supports top level RevoDeployR API calls and provides access to the RevoDeployR Service Management objects.
/// </summary>
/// <remarks></remarks>
    public class RClient
    {

        private String m_url = "";
        private int m_concurrent = 10;
        private Cookie m_cookie;
        private Boolean m_allowSelfSignedSSLCert = false;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RClient()
        {

        }

        internal RClient(String url, int concurrent)
        {
            m_url = url;
            m_concurrent = concurrent;

        }
        /// <summary>
        /// Gets the cookie associated with this RClient
        /// </summary>
        /// <value></value>
        /// <returns>Cookie object</returns>
        /// <remarks></remarks>
        public Cookie Cookie
        {
            get
            {
                return m_cookie;
            }
            set
            {
                m_cookie = value;
            }
        }
        /// <summary>
        /// Gets the base URL used for all the API calls
        /// </summary>
        /// <value></value>
        /// <returns>String containg URL</returns>
        /// <remarks></remarks>
        public String URL
        {
            get
            {
                return m_url;
            }
            set
            {
                m_url = value;
            }
        }
        /// <summary>
        /// Get/Set the flag that allows Self-Signed SSL Certificates to be use on HTTPS Web Requests to the DeployR API
        /// </summary>
        /// <value></value>
        /// <returns>Self-Signed SSL Certificate flag</returns>
        /// <remarks></remarks>
        public Boolean allowSelfSignedSSLCert
        {
            get
            {
                return m_allowSelfSignedSSLCert;
            }
            set
            {
                m_allowSelfSignedSSLCert = value;
            }
        }
        /// <summary>
        /// Authenticate with the RevoDeployR server
        /// 
        /// Auto save is defaulted to false
        /// </summary>
        /// <param name="authentication">A valid RAuthentication object</param>
        /// <returns>RResponse object</returns>
        /// <remarks></remarks>
        public RUser login(RAuthentication authentication)
        {
            return login(authentication, false);
        }

        /// <summary>
        /// Authenticate with the RevoDeployR server
        /// </summary>
        /// <param name="authentication">A valid RAuthentication object</param>
        /// <param name="autosave">(optional) Flag indicating that autosave should be turned on/off for the user</param>
        /// <returns>RResponse object</returns>
        /// <remarks></remarks>
        public RUser login(RAuthentication authentication, Boolean autosave)
        {
            StringBuilder data = new StringBuilder();

            String uri = Constants.RUSERLOGIN;
            //create the input String
            data.Append(Constants.FORMAT_JSON + "&username=" + HttpUtility.UrlEncode(authentication.Username));
            data.Append("&password=" + HttpUtility.UrlEncode(authentication.Password));
            data.Append("&save=" + HttpUtility.UrlEncode(autosave.ToString()));
            //call the server
            RClient client = this;
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);
            RUser returnValue = new RUser(jresponse, this);

            return returnValue;
        }

        /// <summary>
        /// Logout from the RevoDeployR server.
        /// </summary>
        /// <param name="user">The RUser object representing the user to logout</param>
        /// <remarks></remarks>
        public void logout(RUser user)
        {
            StringBuilder data = new StringBuilder();

            String uri = Constants.RUSERLOGOUT;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            //call the server
            RClient client = this;
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

        }

        /// <summary>
        /// Execute a single repository-managed script or a chain of repository-managed scripts
        /// on an anonymous project.
        ///
        /// To execute a chain of repository-managed scripts on this call provide a comma-separated
        /// list of values on the scriptName, scriptAuthor and optionally scriptVersion parameters.
        /// Chained execution executes each of the scripts identified on the call in a sequential
        /// fashion on the R session, with execution occuring in the order specified on the parameter list.
        ///
        /// Deprecated. As of release 7.1, use executeScript method that supports scriptDirectory parameter. This deprecated call assumes each script is found in the root directory.
        ///
        /// </summary>
        /// <param name="scriptName">name of valid R Script</param>
        /// <param name="scriptAuthor">author of the R Script</param>
        /// <param name="scriptVersion">version of the R Script to execute</param>
        /// <param name="options">execute options associated with the R Script</param>
        /// <returns>RScriptExecution object</returns>
        /// <remarks></remarks>
        public RScriptExecution executeScript(String scriptName, String scriptAuthor, String scriptVersion, AnonymousProjectExecutionOptions options)
        {
            RScriptExecution returnValue = executeScriptImp(scriptName, "root", scriptAuthor, scriptVersion, null, options);

            return returnValue;
        }

        /// <summary>
        /// Execute a single repository-managed script or a chain of repository-managed scripts
        /// on an anonymous project.
        ///
        /// To execute a chain of repository-managed scripts on this call provide a comma-separated
        /// list of values on the scriptName, scriptAuthor and optionally scriptVersion parameters.
        /// Chained execution executes each of the scripts identified on the call in a sequential
        /// fashion on the R session, with execution occuring in the order specified on the parameter list.
        ///
        /// </summary>
        /// <param name="scriptName">name of valid R Script</param>
        /// <param name="scriptDirectory">directory containing R Script.</param>
        /// <param name="scriptAuthor">author of the R Script</param>
        /// <param name="scriptVersion">version of the R Script to execute</param>
        /// <param name="options">execute options associated with the R Script</param>
        /// <returns>RScriptExecution object</returns>
        /// <remarks></remarks>
        public RScriptExecution executeScript(String scriptName, String scriptDirectory, String scriptAuthor, String scriptVersion, AnonymousProjectExecutionOptions options)
        {
            RScriptExecution returnValue = executeScriptImp(scriptName, scriptDirectory, scriptAuthor, scriptVersion, null, options);

            return returnValue;
        }

        /// <summary>
        /// Execute a single repository-managed script or a chain of repository-managed scripts
        /// on an anonymous project.
        ///
        /// To execute a chain of repository-managed scripts on this call provide a comma-separated
        /// list of values on the scriptName, scriptAuthor and optionally scriptVersion parameters.
        /// Chained execution executes each of the scripts identified on the call in a sequential
        /// fashion on the R session, with execution occuring in the order specified on the parameter list.
        ///
        /// Deprecated. As of release 7.1, use executeScript method that supports scriptDirectory parameter. This deprecated call assumes each script is found in the root directory.
        ///
        /// </summary>
        /// <param name="scriptName">RRepositoryScript object identifying the script to execute</param>
        /// <param name="scriptAuthor">author of the R Script</param>
        /// <param name="scriptVersion">version of the R Script to execute</param>
        /// <returns>RScriptExecution object</returns>
        /// <remarks></remarks>
        public RScriptExecution executeScript(String scriptName, String scriptAuthor, String scriptVersion)
        {
            RScriptExecution returnValue = executeScriptImp(scriptName, "root", scriptAuthor, scriptVersion, null, null);

            return returnValue;
        }

        /// <summary>
        /// Execute a single repository-managed script or a chain of repository-managed scripts
        /// on an anonymous project.
        ///
        /// To execute a chain of repository-managed scripts on this call provide a comma-separated
        /// list of values on the scriptName, scriptAuthor and optionally scriptVersion parameters.
        /// Chained execution executes each of the scripts identified on the call in a sequential
        /// fashion on the R session, with execution occuring in the order specified on the parameter list.
        ///
        /// </summary>
        /// <param name="scriptName">RRepositoryScript object identifying the script to execute</param>
        /// <param name="scriptDirectory">directory containing R Script.</param>
        /// <param name="scriptAuthor">author of the R Script</param>
        /// <param name="scriptVersion">version of the R Script to execute</param>
        /// <returns>RScriptExecution object</returns>
        /// <remarks></remarks>
        public RScriptExecution executeScript(String scriptName, String scriptDirectory, String scriptAuthor, String scriptVersion)
        {
            RScriptExecution returnValue = executeScriptImp(scriptName, scriptDirectory, scriptAuthor, scriptVersion, null, null);

            return returnValue;
        }

        /// <summary>
        /// Execute a single script found on a URL/path or a chain of scripts found on a set of URLs/paths
        /// on an anonymous project.
        ///
        /// To execute a chain of repository-managed scripts on this call provide a comma-separated
        /// list of values on the externalSource parameter.
        /// Chained execution executes each of the scripts identified on the call in a sequential
        /// fashion on the R session, with execution occuring in the order specified on the parameter list.
        ///
        /// POWER_USER privileges are required for this call.
        /// </summary>
        /// <param name="externalSource">URL or DeployR file path</param>
        /// <param name="options">execute options associated with the external script</param>
        /// <returns>RScriptExecution object</returns>
        /// <remarks></remarks>
        public RScriptExecution executeExternal(String externalSource, AnonymousProjectExecutionOptions options)
        {
            RScriptExecution returnValue = executeScriptImp(null, null, null, null, externalSource, options);

            return returnValue;
        }

        /// <summary>
        /// Execute a single repository-managed script or a chain of repository-managed scripts
        /// on an anonymous project and render the outputs to a HTML page.
        ///
        /// To execute a chain of repository-managed scripts on this call provide a comma-separated
        /// list of values on the scriptName, scriptAuthor and optionally scriptVersion parameters.
        /// Chained execution executes each of the scripts identified on the call in a sequential
        /// fashion on the R session, with execution occuring in the order specified on the parameter list.
        /// </summary>
        /// <param name="scriptName">name of valid R Script</param>
        /// <param name="scriptDirectory">directory containing R Script.</param>
        /// <param name="scriptAuthor">author of the R Script</param>
        /// <param name="scriptVersion">version of the R Script to execute</param>
        /// <param name="options">execute options associated with the R Script</param>
        /// <returns>URL to HTML page with outputs</returns>
        /// <remarks></remarks>
        public String renderScript(String scriptName, String scriptDirectory, String scriptAuthor, String scriptVersion, ref AnonymousProjectExecutionOptions options)
        {
            String returnValue = renderScriptImp(scriptName, scriptDirectory, scriptAuthor, scriptVersion, options);

            return returnValue;
        }

        /// <summary>
        /// Execute a repository-managed shell script. Execution occurs on the
        /// DeployR server. The response captures the line-by-line console output
        /// generated by the execution of the shell script on the server.
        /// Only shell scripts created by an ADMIN user on the DeployR server
        /// can be executed on this call. All attempts to execute shell scripts
        /// not created by an ADMIN user will be rejected. Access to shell
        /// scripts is also governed by standard repository access controls.
        /// </summary>
        /// <param name="shellName">name of valid shell script</param>
        /// <param name="shellDirectory">directory containing shell script.</param>
        /// <param name="shellAuthor">author of the shell script</param>
        /// <param name="shellVersion">version of the shell script to execute</param>
        /// <param name="args">execute arguments passed to the shell script</param>
        /// <returns>List of strings</returns>
        /// <remarks></remarks>
        public List<String> executeShell(String shellName, String shellDirectory, String shellAuthor, String shellVersion, String args)
        {
            return executeShellImp(shellName, shellDirectory, shellAuthor, shellVersion, args);
        }

        /// <summary>
        ///  Interrupts the current execution on the HTTP blackbox project associated
        /// with the current HTTP session.
        /// </summary>

        public void interruptScript()
        {
            StringBuilder data = new StringBuilder();

            String uri = Constants.RREPOSITORYSCRIPTINTERRUPT;
            data.Append(Constants.FORMAT_JSON);

            //call the server
            RClient client = this;
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref client);

        }

        private RScriptExecution executeScriptImp(String scriptName, String scriptDirectory, String scriptAuthor, String scriptVersion, String ExternalSource, AnonymousProjectExecutionOptions options)
        {
            StringBuilder data = new StringBuilder();

            String uri = Constants.RREPOSITORYSCRIPTEXECUTE;
            //create the input String
            data.Append(Constants.FORMAT_JSON);

            if (ExternalSource == null)
            {
                data.Append("&filename=" + HttpUtility.UrlEncode(scriptName));
                data.Append("&author=" + HttpUtility.UrlEncode(scriptAuthor));
                data.Append("&directory=" + HttpUtility.UrlEncode(scriptDirectory));
                data.Append("&version=" + HttpUtility.UrlEncode(scriptVersion));
            }
            else
            {
                data.Append("&externalsource=" + HttpUtility.UrlEncode(ExternalSource));
            }


            if (!(options == null))
            {

                data.Append("&blackbox=" + options.blackbox.ToString());
                data.Append("&recycle=" + options.recycle.ToString());
                data.Append("&csvinputs=" + HttpUtility.UrlEncode(options.csvrinputs));
                data.Append("&cluster=" + HttpUtility.UrlEncode(options.gridCluster));

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
                data.Append("&graphicswidth=" + HttpUtility.UrlEncode(options.graphicsWidth.ToString()));
                data.Append("&graphicsheight=" + HttpUtility.UrlEncode(options.graphicsHeight.ToString()));
                data.Append("&nan=" + HttpUtility.UrlEncode(options.nan));
                data.Append("&infinity=" + HttpUtility.UrlEncode(options.infinity));
                data.Append("&encodeDataFramePrimitiveAsVector=" + options.encodeDataFramePrimitiveAsVector.ToString());
                data.Append("&enableConsoleEvents=" + options.enableConsoleEvents.ToString());
                data.Append("&preloadbydirectory=" + HttpUtility.UrlEncode(options.preloadByDirectory));

            }

            //call the server
            RClient client = this;
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);
            RScriptExecution returnValue = new RScriptExecution(jresponse, this);

            return returnValue;
        }

        private String renderScriptImp(String scriptName, String scriptDirectory, String scriptAuthor, String scriptVersion, AnonymousProjectExecutionOptions options)
        {
            StringBuilder data = new StringBuilder();

            //set the url
            String uri = Constants.RREPOSITORYSCRIPTRENDER;
            //create the input String
            data.Append(Constants.FORMAT_JSON);

            data.Append("&filename=" + HttpUtility.UrlEncode(scriptName));
            data.Append("&directory=" + HttpUtility.UrlEncode(scriptDirectory));
            data.Append("&author=" + HttpUtility.UrlEncode(scriptAuthor));
            data.Append("&version=" + HttpUtility.UrlEncode(scriptVersion));

            if (!(options == null))
            {

                data.Append("&blackbox=" + options.blackbox.ToString());
                data.Append("&recycle=" + options.recycle.ToString());
                data.Append("&csvinputs=" + HttpUtility.UrlEncode(options.csvrinputs));
                data.Append("&cluster=" + HttpUtility.UrlEncode(options.gridCluster));

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
                data.Append("&graphicswidth=" + HttpUtility.UrlEncode(options.graphicsWidth.ToString()));
                data.Append("&graphicsheight=" + HttpUtility.UrlEncode(options.graphicsHeight.ToString()));
                data.Append("&nan=" + HttpUtility.UrlEncode(options.nan));
                data.Append("&infinity=" + HttpUtility.UrlEncode(options.infinity));
                data.Append("&encodeDataFramePrimitiveAsVector=" + options.encodeDataFramePrimitiveAsVector.ToString());
                data.Append("&enableConsoleEvents=" + options.enableConsoleEvents.ToString());
                data.Append("&preloadbydirectory=" + HttpUtility.UrlEncode(options.preloadByDirectory));


            }

            //call the server
            RClient client = this;
            String returnValue = HTTPUtilities.callRESTHTMLGet(uri + "/" + "jsessionid=" + m_cookie.Value, data.ToString(), ref client);
            return returnValue;
        }

        private List<String> executeShellImp(String shellName, String shellDirectory, String shellAuthor, String shellVersion, String args)
        {
            StringBuilder data = new StringBuilder();

            String uri = Constants.RREPOSITORYSHELLEXECUTE;
            //create the input String
            data.Append(Constants.FORMAT_JSON);

            data.Append("&filename=" + HttpUtility.UrlEncode(shellName));
            data.Append("&author=" + HttpUtility.UrlEncode(shellAuthor));
            data.Append("&directory=" + HttpUtility.UrlEncode(shellDirectory));
            data.Append("&version=" + HttpUtility.UrlEncode(shellVersion));
            data.Append("&args=" + HttpUtility.UrlEncode(args));

            //call the server
            RClient client = this;
            JSONResponse jresponse = HTTPUtilities.callRESTPost(uri, data.ToString(), ref client);

            List<String> console = new List<String>();
            JArray jvalues;

            if (!(jresponse.JSONMarkup["repository"] == null))
            {
                JObject jrepo = jresponse.JSONMarkup["repository"].Value<JObject>();
                if (!(jrepo["shell"] == null))
                {
                    JObject jshell = jrepo["shell"].Value<JObject>();;

                    if (!(jshell["console"] == null))
                    {
                        jvalues = jshell["console"].Value<JArray>();
                        foreach (var j in jvalues)
                        {
                            if (j.Type != JTokenType.Null)
                            {
                                console.Add(j.Value<string>());
                            }
                        }
                    }
                }
            }
            return console;
        }

    }


}