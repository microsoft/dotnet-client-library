/*
 * RUser.cs
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


namespace DeployR
{
/// <summary>
/// Represents an authenticated User in RevoDeployR
/// </summary>
/// <remarks></remarks>
    public class RUser
    {

        private RClient m_client;
        private RUserDetails m_userDetails;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RUser()
        {

        }

        internal RUser(JSONResponse jresponse, RClient client)
        {

            m_client = client;
            if (!(jresponse == null))
            {
                parseUser(jresponse, ref m_userDetails);
            }

        }

        /// <summary>
        /// Gets the details associated with this user
        /// </summary>
        /// <returns>RUserDetails object</returns>
        /// <remarks></remarks>
        public RUserDetails about()
        {
            StringBuilder data = new StringBuilder();
            RUserDetails userDetails = null;

            //set the url
            String uri = Constants.RUSERABOUT;
            //create the input String
            data.Append(Constants.FORMAT_JSON);

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref m_client);

            parseUser(jresponse, ref userDetails);
            return userDetails;
        }

        /// <summary>
        /// List jobs
        /// </summary>
        /// <returns>List of RJob objects</returns>
        /// <remarks></remarks>
        public List<RJob> listJobs()
        {
            List<RJob> returnValue = default(List<RJob>);

            returnValue = RUserJobImpl.listJobs(m_client, Constants.RJOBLIST);

            return returnValue;
        }

        /// <summary>
        /// Submit a job based on block of R code
        /// </summary>
        /// <param name="name">Name of the job to schedule</param>
        /// <param name="descr">Description of the job</param>
        /// <param name="code">R code associated with the job</param>
        /// <returns>RJob object</returns>
        /// <remarks></remarks>
        public RJob submitJobCode(String name, String descr, String code)
        {
            RJob returnValue = RUserJobImpl.callJob(name, descr, code, "", "", "", "", null, null, m_client, Constants.RJOBSUBMIT);

            return returnValue;
        }

        /// <summary>
        /// Submit a job based on block of R code
        /// </summary>
        /// <param name="name">Name of the job to schedule</param>
        /// <param name="descr">Description of the job</param>
        /// <param name="code">R code associated with the job</param>
        /// <param name="options">options associated with job</param>
        /// <returns>RJob object</returns>
        /// <remarks></remarks>
        public RJob submitJobCode(String name, String descr, String code, JobExecutionOptions options)
        {
            String uri = Constants.RJOBSUBMIT;
            if (!(options == null))
            {
                if (!(options.schedulingOptions == null))
                {
                    uri = Constants.RJOBSCHEDULE;
                }
            }
            RJob returnValue = RUserJobImpl.callJob(name, descr, code, "", "", "", "", null, options, m_client, uri);

            return returnValue;
        }

        /// <summary>
        /// Submit a single repository-managed script or a chain of repository-managed scripts
        /// to execute as a job.
        ///
        /// To submit a chain of repository-managed scripts on this call provide a comma-separated
        /// list of values on the scriptName, scriptAuthor and optionally scriptVersion parameters.
        /// Chained execution executes each of the scripts identified on the call in a sequential
        /// fashion on the R session, with execution occuring in the order specified on the parameter list.
        ///
        /// Deprecated. As of release 7.1, use submitJobScript method that supports scriptDirectory parameter. This deprecated call assumes each script is found in the root directory.
        ///
        /// </summary>
        /// <param name="name">Name of the job to schedule</param>
        /// <param name="descr">Description of the job</param>
        /// <param name="scriptName">name of valid R Script</param>
        /// <param name="scriptAuthor">author of the R Script</param>
        /// <param name="scriptVersion">version of the R Script to execute</param>
        /// <returns>RJob object</returns>
        /// <remarks></remarks>
        public RJob submitJobScript(String name, String descr, String scriptName, String scriptAuthor, String scriptVersion)
        {
            RJob returnValue = RUserJobImpl.callJob(name, descr, "", scriptName, "root", scriptAuthor, scriptVersion, null, null, m_client, Constants.RJOBSUBMIT);

            return returnValue;
        }
        /// <summary>
        /// Submit a single repository-managed script or a chain of repository-managed scripts
        /// to execute as a job.
        ///
        /// To submit a chain of repository-managed scripts on this call provide a comma-separated
        /// list of values on the scriptName, scriptAuthor and optionally scriptVersion parameters.
        /// Chained execution executes each of the scripts identified on the call in a sequential
        /// fashion on the R session, with execution occuring in the order specified on the parameter list.
        ///
        /// </summary>
        /// <param name="name">Name of the job to schedule</param>
        /// <param name="descr">Description of the job</param>
        /// <param name="scriptName">name of valid R Script</param>
        /// <param name="scriptDirectory">directory containing R Script.</param>
        /// <param name="scriptAuthor">author of the R Script</param>
        /// <param name="scriptVersion">version of the R Script to execute</param>
        /// <returns>RJob object</returns>
        /// <remarks></remarks>
        public RJob submitJobScript(String name, String descr, String scriptName, String scriptDirectory, String scriptAuthor, String scriptVersion)
        {
            RJob returnValue = RUserJobImpl.callJob(name, descr, "", scriptName, scriptDirectory, scriptAuthor, scriptVersion, null, null, m_client, Constants.RJOBSUBMIT);

            return returnValue;
        }

        /// <summary>
        /// Submit a single repository-managed script or a chain of repository-managed scripts
        /// to execute as a job.
        ///
        /// To submit a chain of repository-managed scripts on this call provide a comma-separated
        /// list of values on the scriptName, scriptAuthor and optionally scriptVersion parameters.
        /// Chained execution executes each of the scripts identified on the call in a sequential
        /// fashion on the R session, with execution occuring in the order specified on the parameter list.
        ///
        /// Deprecated. As of release 7.1, use submitJobScript method that supports scriptDirectory parameter. This deprecated call assumes each script is found in the root directory.
        ///
        /// </summary>
        /// <param name="name">Name of the job to schedule</param>
        /// <param name="descr">Description of the job</param>
        /// <param name="scriptName">name of valid R Script</param>
        /// <param name="scriptAuthor">author of the R Script</param>
        /// <param name="scriptVersion">version of the R Script to execute</param>
        /// <param name="options">options associated with job</param>
        /// <returns>RJob object</returns>
        /// <remarks></remarks>
        public RJob submitJobScript(String name, String descr, String scriptName, String scriptAuthor, String scriptVersion, JobExecutionOptions options)
        {

            String uri = Constants.RJOBSUBMIT;
            if (!(options == null))
            {
                if (!(options.schedulingOptions == null))
                {
                    uri = Constants.RJOBSCHEDULE;
                }
            }
            RJob returnValue = RUserJobImpl.callJob(name, descr, "", scriptName, "root", scriptAuthor, scriptVersion, null, options, m_client, uri);

            return returnValue;
        }

        /// <summary>
        /// Submit a single repository-managed script or a chain of repository-managed scripts
        /// to execute as a job.
        ///
        /// To submit a chain of repository-managed scripts on this call provide a comma-separated
        /// list of values on the scriptName, scriptAuthor and optionally scriptVersion parameters.
        /// Chained execution executes each of the scripts identified on the call in a sequential
        /// fashion on the R session, with execution occuring in the order specified on the parameter list.
        ///
        /// </summary>
        /// <param name="name">Name of the job to schedule</param>
        /// <param name="descr">Description of the job</param>
        /// <param name="scriptName">name of valid R Script</param>
        /// <param name="scriptDirectory">directory containing R Script.</param>
        /// <param name="scriptAuthor">author of the R Script</param>
        /// <param name="scriptVersion">version of the R Script to execute</param>
        /// <param name="options">options associated with job</param>
        /// <returns>RJob object</returns>
        /// <remarks></remarks>
        public RJob submitJobScript(String name, String descr, String scriptName, String scriptDirectory, String scriptAuthor, String scriptVersion, JobExecutionOptions options)
        {

            String uri = Constants.RJOBSUBMIT;
            if (!(options == null))
            {
                if (!(options.schedulingOptions == null))
                {
                    uri = Constants.RJOBSCHEDULE;
                }
            }
            RJob returnValue = RUserJobImpl.callJob(name, descr, "", scriptName, scriptDirectory, scriptAuthor, scriptVersion, null, options, m_client, uri);

            return returnValue;
        }


        /// <summary>
        /// Submit a single script found on a URL/path or a chain of scripts found on a set of URLs/paths
        /// on the current project.
        ///
        /// To submit a chain of repository-managed scripts on this call provide a comma-separated
        /// list of values on the externalSource parameter.
        ///
        /// POWER_USER privileges are required for this call.
        ///
        /// </summary>
        /// <param name="name">Name of the job to schedule</param>
        /// <param name="descr">Description of the job</param>
        /// <param name="externalSource">RScript represented as a URL or DeployR file path</param>
        /// <param name="options">options associated with job</param>
        /// <returns>RJob object</returns>
        /// <remarks></remarks>
        public RJob submitJobExternal(String name, String descr, String externalSource, JobExecutionOptions options)
        {

            String uri = Constants.RJOBSUBMIT;
            if (!(options == null))
            {
                if (!(options.schedulingOptions == null))
                {
                    uri = Constants.RJOBSCHEDULE;
                }
            }
            RJob returnValue = RUserJobImpl.callJob(name, descr, "", null, null, null, null, externalSource, options, m_client, uri);

            return returnValue;
        }

        /// <summary>
        /// Gets an RJob object for a give job identifier
        /// </summary>
        /// <param name="jobId">job identifier</param>
        /// <returns>RJob object</returns>
        /// <remarks></remarks>
        public RJob queryJob(String jobId)
        {
            StringBuilder data = new StringBuilder();

            //set the url
            String uri = Constants.RJOBQUERY;
            //create the input String
            data.Append(Constants.FORMAT_JSON);
            data.Append("&job=" + HttpUtility.UrlEncode(jobId));

            //call the server
            JSONResponse jresponse = HTTPUtilities.callRESTGet(uri, data.ToString(), ref m_client);

            return new RJob(jresponse, m_client);
            
        }
        /// <summary>
        /// Enabled/disable project auto-save semantics for duration of user session
        /// </summary>
        /// <param name="save">True to enable auto-save of projects on close or logout</param>
        /// <remarks></remarks>
        public void autosaveProjects(Boolean save)
        {

            RUserProjectImpl.autosaveProjects(save, m_client, Constants.RUSERAUTOSAVE);

        }

        /// <summary>
        /// Releases all of the projects on the grid for the user
        /// </summary>
        /// <remarks></remarks>
        public void releaseProjects()
        {
            RUserProjectImpl.releaseProjects(m_client, Constants.RUSERRELEASE);
        }

        /// <summary>
        /// Create a temporary project
        /// </summary>
        /// <returns>RProject object</returns>
        /// <remarks></remarks>
        public RProject createProject()
        {
            RProject returnValue = RUserProjectImpl.createProject("", "", null, m_client, Constants.RPROJECTCREATE);

            return returnValue;
        }

        /// <summary>
        /// Create a temporary project with options
        /// </summary>
        /// <param name="options">ProjectCreationOptions that specifies what options the projects should be created with</param>
        /// <returns>RProject object</returns>
        /// <remarks></remarks>
        public RProject createProject(ProjectCreationOptions options)
        {
            RProject returnValue = RUserProjectImpl.createProject("", "", options, m_client, Constants.RPROJECTCREATE);

            return returnValue;
        }

        /// <summary>
        /// Create a pool of temporary projects
        /// </summary>
        /// <param name="poolSize">Number of temporary projects to create</param>
        /// <param name="options">ProjectCreationOptions that specifies what options the projects should be created with</param>
        /// <returns>List of RProject objects</returns>
        /// <remarks></remarks>

        public List<RProject> createProjectPool(int poolSize, ProjectCreationOptions options)
        {
            List<RProject> returnValue = RUserProjectImpl.createProjectPool(poolSize, options, m_client, Constants.RPROJECTPOOL);

            return returnValue;
        }

        /// <summary>
        /// Create a named persistent project
        /// </summary>
        /// <param name="name">Name of project</param>
        /// <param name="descr">Description of project</param>
        /// <returns>RProject object</returns>
        /// <remarks></remarks>
        public RProject createProject(String name, String descr)
        {
            RProject returnValue = RUserProjectImpl.createProject(name, descr, null, m_client, Constants.RPROJECTCREATE);

            return returnValue;
        }

        /// <summary>
        /// Create a named persistent project with options
        /// </summary>
        /// <param name="name">Name of project</param>
        /// <param name="descr">Description of project</param>
        /// <param name="options">ProjectCreationOptions that specifies what options the projects should be created with</param>
        /// <returns>RProject object</returns>
        /// <remarks></remarks>
        public RProject createProject(String name, String descr, ProjectCreationOptions options)
        {
            RProject returnValue = RUserProjectImpl.createProject(name, descr, options, m_client, Constants.RPROJECTCREATE);

            return returnValue;
        }

        /// <summary>
        /// Retrieve project reference
        /// </summary>
        /// <param name="name">Name of project</param>
        /// <returns>RProject object</returns>
        /// <remarks></remarks>
        public RProject getProject(String name)
        {
            RProject returnValue = RUserProjectImpl.getProject(name, m_client, Constants.RPROJECTABOUT);

            return returnValue;
        }

        /// <summary>
        /// Import project from a file
        /// </summary>
        /// <param name="file">Full path to File to upload and import</param>
        /// <param name="descr">Description of project</param>
        /// <returns>RProject object</returns>
        /// <remarks></remarks>
        public RProject importProject(String file, String descr)
        {
            RProject returnValue = RUserProjectImpl.importProject(file, descr, m_client, Constants.RPROJECTIMPORT);

            return returnValue;
        }

        /// <summary>
        /// List projects
        /// </summary>
        /// <returns>List of RProject object</returns>
        /// <remarks></remarks>
        public List<RProject> listProjects()
        {
            List<RProject> returnValue = RUserProjectImpl.listProjects(false, true, m_client, Constants.RPROJECTLIST);

            return returnValue;
        }

        /// <summary>
        /// List projects
        /// </summary>
        /// <param name="sortByLastModified">True to sort by last modified date</param>
        /// <param name="showPublicProjects">True to return public projects as well as private ones</param>
        /// <returns>List of RProject objects</returns>
        /// <remarks></remarks>
        public List<RProject> listProjects(Boolean sortByLastModified, Boolean showPublicProjects)
        {
            List<RProject> returnValue = RUserProjectImpl.listProjects(sortByLastModified, showPublicProjects, m_client, Constants.RPROJECTLIST);

            return returnValue;
        }

        /// <summary>
        /// List files in user repository
        /// </summary>
        /// <returns>List of RRepositoryFile objects</returns>
        /// <remarks></remarks>
        public List<RRepositoryFile> listFiles()
        {
            List<RRepositoryFile> returnValue = RUserRepositoryFileImpl.listFiles("", "", false, false, false, false, "", m_client, Constants.RREPOSITORYFILELIST);

            return returnValue;
        }

        /// <summary>
        /// List files in user repository
        /// </summary>
        /// <param name="archived">True to show files that have been archived</param>
        /// <param name="sharedUsers">True to show both shared and restricted files</param>
        /// <param name="published">True to show both public</param>
        /// <returns>List of RRepositoryFile objects</returns>
        /// <remarks></remarks>
        public List<RRepositoryFile> listFiles(Boolean archived, Boolean sharedUsers, Boolean published)
        {
            List<RRepositoryFile> returnValue = RUserRepositoryFileImpl.listFiles("", "", archived, sharedUsers, published, false, "", m_client, Constants.RREPOSITORYFILELIST);

            return returnValue;
        }

        /// <summary>
        /// List files in user List versions of named file in user repository-managed directory
        /// </summary>
        /// <param name="filename">Name of file to retreive versions</param>
        /// <param name="directory">Name of directory containing the file</param>
        /// <returns>List of RRepositoryFile objects</returns>
        /// <remarks></remarks>
        public List<RRepositoryFile> listFiles(String filename, String directory)
        {
            List<RRepositoryFile> returnValue = RUserRepositoryFileImpl.listFiles(filename, directory, false, false, false, false, "", m_client, Constants.RREPOSITORYFILELIST);

            return returnValue;
        }

        /// <summary>
        /// List files in the user's default repository using filters to constrain the files in the response markup.
        /// If categoryFilter is specified only files matching the Category indicated will be included in the response.
        /// If directoryFilter is specified then only files found in the directory indicated will be included in the response.
        /// If both categoryFilter and directoryFilter are specified then only files matching the Category within the directory indicated will be included in the response
        /// </summary>
        /// <param name="categoryFilter">Value of RRepositoryFile.Category to specify the types of files returned</param>
        /// <param name="directoryFilter">Name of directory containing the files</param>
        /// <returns>List of RRepositoryFile objects</returns>
        /// <remarks></remarks>
        public List<RRepositoryFile> listFiles(RRepositoryFile.Category categoryFilter, String directoryFilter)
        {
            List<RRepositoryFile> returnValue = RUserRepositoryFileImpl.listFiles("", directoryFilter, false, false, false, false, categoryFilter.ToString(), m_client, Constants.RREPOSITORYFILELIST);

            return returnValue;
        }

        /// <summary>
        /// List files in user's external repository
        /// </summary>
        /// <returns>List of RRepositoryFile objects</returns>
        /// <remarks></remarks>
        public List<RRepositoryFile> listExternalFiles()
        {
            List<RRepositoryFile> returnValue = RUserRepositoryFileImpl.listFiles("", "", false, false, false, true, "", m_client, Constants.RREPOSITORYFILELIST);

            return returnValue;
        }

        /// <summary>
        /// List files in user's external repository
        /// </summary>
        /// <param name="sharedUsers">True to show both shared and restricted files</param>
        /// <param name="published">True to show both public</param>
        /// <returns>List of RRepositoryFile objects</returns>
        /// <remarks></remarks>
        public List<RRepositoryFile> listExternalFiles(Boolean sharedUsers, Boolean published)
        {
            List<RRepositoryFile> returnValue = RUserRepositoryFileImpl.listFiles("", "", false, sharedUsers, published, true, "", m_client, Constants.RREPOSITORYFILELIST);

            return returnValue;
        }

        /// <summary>
        /// List files in the user's external repository using filters to constrain the files in the response markup.
        /// If categoryFilter is specified only files matching the Category indicated will be included in the response.
        /// If directoryFilter is specified then only files found in the directory indicated will be included in the response.
        /// If both categoryFilter and directoryFilter are specified then only files matching the Category within the directory indicated will be included in the response
        /// </summary>
        /// <param name="categoryFilter">Value of RRepositoryFile.Category to specify the types of files returned</param>
        /// <param name="directoryFilter">Name of directory containing the files</param>
        /// <returns>List of RRepositoryFile objects</returns>
        /// <remarks></remarks>
        public List<RRepositoryFile> listExternalFiles(RRepositoryFile.Category categoryFilter, String directoryFilter)
        {
            List<RRepositoryFile> returnValue = RUserRepositoryFileImpl.listFiles("", directoryFilter, false, false, false, true, categoryFilter.ToString(), m_client, Constants.RREPOSITORYFILELIST);

            return returnValue;
        }

        /// <summary>
        /// Transfer file to the user repository
        /// </summary>
        /// <param name="url">Full qualified URL of file to transfer to repository</param>
        /// <param name="options">Repository upload options object </param>
        /// <returns>RRepositoryFile object</returns>
        /// <remarks></remarks>
        public RRepositoryFile transferFile(String url, RepoUploadOptions options)
        {
            RRepositoryFile returnValue = RUserRepositoryFileImpl.transferFile(url, options, m_client, Constants.RREPOSITORYFILETRANSFER);

            return returnValue;
        }

        /// <summary>
        /// Upload a file to the user repository
        /// </summary>
        /// <param name="file">Full path to File to upload and import</param>
        /// <param name="options">Repository upload options object </param>
        /// <returns>RRepositoryFile object</returns>
        /// <remarks></remarks>
        public RRepositoryFile uploadFile(String file, RepoUploadOptions options)
        {
            RRepositoryFile returnValue = RUserRepositoryFileImpl.uploadFile(file, options, m_client, Constants.RREPOSITORYFILEUPLOAD);

            return returnValue;
        }

        /// <summary>
        /// Write a file to the user repository
        /// </summary>
        /// <param name="text">text to write to the repository</param>
        /// <param name="options">Repository upload options object </param>
        /// <returns>RRepositoryFile object</returns>
        /// <remarks></remarks>
        public RRepositoryFile writeFile(String text, RepoUploadOptions options)
        {
            RRepositoryFile returnValue = RUserRepositoryFileImpl.writeFile(text, options, m_client, Constants.RREPOSITORYFILEWRITE);

            return returnValue;
        }

        /// <summary>
        /// Fetch the latest meta-data for a repository file
        /// </summary>
        /// <param name="filename">Repository filename to fetch</param>
        /// <param name="author">Author of the file</param>
        /// <param name="directory">Repository directory containing the file</param>
        /// <param name="version">Optional version of the file</param>
        /// <returns>RRepositoryFile object</returns>
        /// <remarks></remarks>
        public RRepositoryFile fetchFile(String filename, String author, String directory, String version)
        {
            RRepositoryFile returnValue = RUserRepositoryFileImpl.fetchFile(filename, author, directory, version, m_client, Constants.RREPOSITORYFILEFETCH);

            return returnValue;
        }

        /// <summary>
        /// Copy one or more files to a repository-managed directory
        /// </summary>
        /// <param name="destination">Repository destination directory for copy</param>
        /// <param name="files">List of Repository file to copy </param>
        /// <remarks></remarks>
        public void copyFiles(String destination, List<RRepositoryFile> files)
        {

            RUserRepositoryFileImpl.copyFiles(destination, files, m_client, Constants.RREPOSITORYFILECOPY);

        }

        /// <summary>
        /// Move one or more files to a repository-managed directory
        /// </summary>
        /// <param name="destination">Repository destination directory for move</param>
        /// <param name="files">List of Repository file to move </param>
        /// <remarks></remarks>
        public void moveFiles(String destination, List<RRepositoryFile> files)
        {

            RUserRepositoryFileImpl.moveFiles(destination, files, m_client, Constants.RREPOSITORYFILEMOVE);

        }

        /// <summary>
        /// List scripts in user repository
        /// </summary>
        /// <returns>List of RRepositoryScript objects</returns>
        /// <remarks></remarks>
        public List<RRepositoryScript> listScripts()
        {
            List<RRepositoryScript> returnValue = RUserRepositoryScriptImpl.listScripts("", "", false, false, false, false, "", m_client, Constants.RREPOSITORYSCRIPTLIST);

            return returnValue;
        }


        /// <summary>
        /// List scripts in user repository
        /// </summary>
        /// <param name="archived">True to show scripts that have been archived</param>
        /// <param name="sharedUsers">True to show both shared and restricted scripts</param>
        /// <param name="published">True to show public scripts</param>
        /// <returns>List of RRepositoryScript objects</returns>
        /// <remarks></remarks>
        public List<RRepositoryScript> listScripts(Boolean archived, Boolean sharedUsers, Boolean published)
        {
            List<RRepositoryScript> returnValue = RUserRepositoryScriptImpl.listScripts("", "", archived, sharedUsers, published, false, "", m_client, Constants.RREPOSITORYSCRIPTLIST);

            return returnValue;
        }

        /// <summary>
        /// List scripts in user repository
        /// </summary>
        /// <param name="filename">Name of script to retreive versions</param>
        /// <param name="directory">Name of directory containing the script</param>
        /// <returns>List of RRepositoryScript objects</returns>
        /// <remarks></remarks>
        public List<RRepositoryScript> listScripts(String filename, String directory)
        {
            List<RRepositoryScript> returnValue = RUserRepositoryScriptImpl.listScripts(filename, directory, false, false, false, false, "", m_client, Constants.RREPOSITORYSCRIPTLIST);

            return returnValue;
        }

        /// <summary>
        /// List scripts in user's external repository
        /// </summary>
        /// <returns>List of RRepositoryScript objects</returns>
        /// <remarks></remarks>
        public List<RRepositoryScript> listExternalScripts()
        {
            List<RRepositoryScript> returnValue = RUserRepositoryScriptImpl.listScripts("", "", false, false, false, true, "", m_client, Constants.RREPOSITORYSCRIPTLIST);

            return returnValue;
        }


        /// <summary>
        /// List scripts in user's external repository
        /// </summary>
        /// <param name="sharedUsers">True to show both shared and restricted scripts</param>
        /// <param name="published">True to show public scripts</param>
        /// <returns>List of RRepositoryScript objects</returns>
        /// <remarks></remarks>
        public List<RRepositoryScript> listExternalScripts(Boolean sharedUsers, Boolean published)
        {
            List<RRepositoryScript> returnValue = RUserRepositoryScriptImpl.listScripts("", "", false, sharedUsers, published, true, "", m_client, Constants.RREPOSITORYSCRIPTLIST);

            return returnValue;
        }

        
        /// <summary>
        /// Copies one or more repository-managed files from a source user directory to a destination user directory. If the files parameter is null, all files in the source directory will be copied to the destination directory
        /// </summary>
        /// <param name="source">Repository source directory for copy</param>
        /// <param name="destination">Repository destination directory for copy</param>
        /// <param name="files">List of Repository file to copy </param>
        /// <remarks></remarks>
        public void copyDirectory(String source, String destination, List<RRepositoryFile> files)
        {

            RUserRepositoryDirectoryImpl.copyDirectory(source, destination, files, m_client, Constants.RREPOSITORYDIRECTORYCOPY);

        }

        /// <summary>
        /// Creates a new repository-managed custom user directory.
        /// </summary>
        /// <param name="directory">Name of new directory to create</param>
        /// <returns>RRepositoryDirectory object</returns>
        /// <remarks></remarks>
        public RRepositoryDirectory createDirectory(String directory)
        {
            RRepositoryDirectory returnValue = RUserRepositoryDirectoryImpl.createDirectory(directory, m_client, Constants.RREPOSITORYDIRECTORYCREATE);

            return returnValue;
        }

        /// <summary>
        /// Moves one or more repository-managed files from a source user directory to a destination user directory. If the files parameter is null, all files in the source directory will be moved to the destination directory.
        /// </summary>
        /// <param name="source">Repository source directory for move</param>
        /// <param name="destination">Repository destination directory for move</param>
        /// <param name="files">List of Repository file to move </param>
        /// <remarks></remarks>
        public void moveDirectory(String source, String destination, List<RRepositoryFile> files)
        {

            RUserRepositoryDirectoryImpl.moveDirectory(source, destination, files, m_client, Constants.RREPOSITORYDIRECTORYMOVE);

        }

        /// <summary>
        /// Uploads a set of files in a single zip archive into an existing repository-managed user directory. The files are extracted from the zip archive and placed file-by-file into the directory. The options.filename property is ignored on this call and can be left blank.
        /// </summary>
        /// <param name="file">Name of zip file to upload</param>
        /// <param name="options">Repository upload options object</param>
        /// <remarks></remarks>
        public void uploadDirectory(String file, RepoUploadOptions options)
        {

            RUserRepositoryDirectoryImpl.uploadDirectory(file, options, m_client, Constants.RREPOSITORYDIRECTORYUPLOAD);

        }

        /// <summary>
        /// List repository-managed directories
        /// </summary>
        /// <returns>List of RRepositoryDirectory objects</returns>
        /// <remarks></remarks>
        public List<RRepositoryDirectory> listDirectories()
        {
            List<RRepositoryDirectory> returnValue = RUserRepositoryDirectoryImpl.listDirectories(false, false, false, false, false, "", "", m_client, Constants.RREPOSITORYDIRECTORYLIST);

            return returnValue;
        }

        /// <summary>
        /// List repository-managed directories. If the archived parameter is enabled, then files in the user archive directories are included in the response. If the shared parameter is enabled, then files in the system shared directory is included in the response. If the published parameter is enabled, then files in the system published directory is included in the response.
        /// </summary>
        /// <param name="userfiles">Flag indicating if user directories should be included</param>
        /// <param name="archived">Flag indicating if archived directories should be included</param>
        /// <param name="sharedUsers">Flag indicating if shared/restricted directories should be included</param>
        /// <param name="published">Flag indicating if public directories should be included</param>
        /// <param name="directory">If specified, only files in the user directory indicated are listed</param>
        /// <returns>List of RRepositoryDirectory objects</returns>
        /// <remarks></remarks>
        public List<RRepositoryDirectory> listDirectories(Boolean userfiles, Boolean archived, Boolean sharedUsers, Boolean published, String directory)
        {
            List<RRepositoryDirectory> returnValue = RUserRepositoryDirectoryImpl.listDirectories(userfiles, archived, sharedUsers, published, false, directory, "", m_client, Constants.RREPOSITORYDIRECTORYLIST);

            return returnValue;
        }

        /// <summary>
        ///  List directories in the user's default repository using filters to constrain the files in the response markup.
        /// If categoryFilter is specified only files matching the Category indicated will be included in the response.
         /// If directoryFilter is specified then only files found in the directory indicated will be included in the response.
         /// If both categoryFilter and directoryFilter are specified then only files matching the Category within the directory indicated will be included in the response.        
        /// </summary>
        /// <param name="categoryFilter">Value of RRepositoryFile.Category to specify the types of files returned</param>
        /// <param name="directoryFilter">If specified, only files in the user directory indicated are listed</param>
        /// <returns>List of RRepositoryDirectory objects</returns>
        /// <remarks></remarks>
        public List<RRepositoryDirectory> listDirectories(String directoryFilter, RRepositoryFile.Category categoryFilter)
        {
            List<RRepositoryDirectory> returnValue = RUserRepositoryDirectoryImpl.listDirectories(false, false, false, false, false, directoryFilter, categoryFilter.ToString(), m_client, Constants.RREPOSITORYDIRECTORYLIST);

            return returnValue;
        }

        /// <summary>
        /// List external repository-managed directories
        /// </summary>
        /// <returns>List of RRepositoryDirectory objects</returns>
        /// <remarks></remarks>
        public List<RRepositoryDirectory> listExternalDirectories()
        {
            List<RRepositoryDirectory> returnValue = RUserRepositoryDirectoryImpl.listDirectories(false, false, false, false, true, "", "", m_client, Constants.RREPOSITORYDIRECTORYLIST);

            return returnValue;
        }

        /// <summary>
        /// List external repository-managed directories. If the shared parameter is enabled, then files in the system shared directory is included in the response. If the published parameter is enabled, then files in the system published directory is included in the response.
        /// </summary>
        /// <param name="userfiles">Flag indicating if user directories should be included</param>
        /// <param name="sharedUsers">Flag indicating if shared/restricted directories should be included</param>
        /// <param name="published">Flag indicating if public directories should be included</param>
        /// <returns>List of RRepositoryDirectory objects</returns>
        /// <remarks></remarks>
        public List<RRepositoryDirectory> listExternalDirectories(Boolean userfiles, Boolean sharedUsers, Boolean published)
        {
            List<RRepositoryDirectory> returnValue = RUserRepositoryDirectoryImpl.listDirectories(userfiles, false, sharedUsers, published, true, "", "", m_client, Constants.RREPOSITORYDIRECTORYLIST);

            return returnValue;
        }

        /// <summary>
        ///  List directories in the user's external repository using filters to constrain the files in the response markup.
        /// If categoryFilter is specified only files matching the Category indicated will be included in the response.
        /// If directoryFilter is specified then only files found in the directory indicated will be included in the response.
        /// If both categoryFilter and directoryFilter are specified then only files matching the Category within the directory indicated will be included in the response.        
        /// </summary>
        /// <param name="categoryFilter">Value of RRepositoryFile.Category to specify the types of files returned</param>
        /// <param name="directoryFilter">If specified, only files in the user directory indicated are listed</param>
        /// <returns>List of RRepositoryDirectory objects</returns>
        /// <remarks></remarks>
        public List<RRepositoryDirectory> listExternalDirectories(String directoryFilter, RRepositoryFile.Category categoryFilter)
        {
            List<RRepositoryDirectory> returnValue = RUserRepositoryDirectoryImpl.listDirectories(false, false, false, false, true, directoryFilter, categoryFilter.ToString(), m_client, Constants.RREPOSITORYDIRECTORYLIST);

            return returnValue;
        }

        private void parseUser(JSONResponse jresponse, ref RUserDetails userDetails)
        {
            RUserLimitDetails limitDetails = null;

            if (!(jresponse.JSONMarkup["user"] == null))
            {
                JObject juser = jresponse.JSONMarkup["user"].Value<JObject>();

                String username = JSONUtilities.trimXtraQuotes(juser["username"].Value<String>());
                String displayname = JSONUtilities.trimXtraQuotes(juser["displayname"].Value<String>());
                String cookie = JSONUtilities.trimXtraQuotes(juser["cookie"].Value<String>());
                JObject jlimits = jresponse.JSONMarkup["limits"].Value<JObject>();
                parseLimits(jlimits, ref limitDetails);

                userDetails = new RUserDetails(username, displayname, cookie, limitDetails);

            }

        }

        private void parseLimits(JObject jlimits, ref RUserLimitDetails limitDetails)
        {

            if (!(jlimits == null))
            {
                int maxConcurrentLiveProjectCount = jlimits["maxConcurrentLiveProjectCount"].Value<int>();
                int maxFileUploadSize = jlimits["maxFileUploadSize"].Value<int>();
                int maxIdleLiveProjectTimeout = jlimits["maxIdleLiveProjectTimeout"].Value<int>();

                limitDetails = new RUserLimitDetails(maxConcurrentLiveProjectCount,
                    maxFileUploadSize,
                    maxIdleLiveProjectTimeout);
            }

        }
    }
}