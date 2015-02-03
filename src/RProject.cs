/*
 * RProject.cs
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

namespace DeployR
{
/// <summary>
/// Represents a Project in RevoDeployR
/// </summary>
/// <remarks></remarks>
    public class RProject
    {

        private RProjectDetails m_projectDetails;
        private RClient m_client;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks></remarks>
        protected RProject()
        {

        }

        internal RProject(JSONResponse jresponse, RClient client)
        {

            m_client = client;
            if (!(jresponse == null))
            {
                parseProject(jresponse, ref m_projectDetails);
            }

        }

        /// <summary>
        /// Gets the details associated with this project
        /// </summary>
        /// <returns>RProjectDetails object</returns>
        /// <remarks></remarks>
        public RProjectDetails about()
        {
            RProjectDetails returnValue = default(RProjectDetails);

            returnValue = RProjectBaseImpl.about(m_projectDetails, m_client, Constants.RPROJECTABOUT);

            return returnValue;
        }

        /// <summary>
        /// Closes the project
        /// </summary>
        /// <remarks></remarks>
        public void close()
        {

            RProjectBaseImpl.close(m_projectDetails, null, m_client, Constants.RPROJECTCLOSE);

        }

        /// <summary>
        /// Closes the project
        /// </summary>
        /// <param name="options">ProjectCloseOptions object describing additional actions upon closing the project</param>
        /// <remarks></remarks>
        public void close(ProjectCloseOptions options)
        {

            RProjectBaseImpl.close(m_projectDetails, options, m_client, Constants.RPROJECTCLOSE);

        }

        /// <summary>
        /// Deletes the project
        /// </summary>
        /// <remarks></remarks>
        public void delete()
        {

            RProjectBaseImpl.delete(m_projectDetails, m_client, Constants.RPROJECTDELETE);

        }

        /// <summary>
        /// Exports the project
        /// </summary>
        /// <returns>A URL to the project location</returns>
        /// <remarks></remarks>
        public String export()
        {
            String returnValue = System.Convert.ToString(RProjectBaseImpl.export(m_projectDetails, m_client, Constants.RPROJECTEXPORT));

            return returnValue;
        }


        /// <summary>
        /// Grant authorship rights on the project
        /// </summary>
        /// <param name="author">The author that is being granted permissions</param>
        /// <returns>A URL to the project location</returns>
        /// <remarks></remarks>
        public RProjectDetails grant(String author)
        {
            RProjectDetails returnValue = RProjectBaseImpl.grant(author, m_projectDetails, m_client, Constants.RPROJECTGRANT);

            return returnValue;
        }

        /// <summary>
        /// Pings the project
        /// </summary>
        /// <returns>true if project is alive</returns>
        /// <remarks></remarks>
        public Boolean ping()
        {
            Boolean returnValue = System.Convert.ToBoolean(RProjectBaseImpl.ping(m_projectDetails, m_client, Constants.RPROJECTPING));

            return returnValue;
        }

        /// <summary>
        /// Recycles the R session associated with the project by deleting all
        /// R objects from the workspace and all files from the working directory.
        /// Recycling a project is a convenient and efficient alternative to
        /// starting over by closing an existing project and then creating a new project.
        ///
        /// Recommended for temporary and blackbox projects. Recycle persistent projects
        /// with caution as this operation can not be reversed.
        /// </summary>
        /// <returns>RProjectDetails object</returns>
        /// <remarks></remarks>
        public RProjectDetails recycle()
        {
            RProjectDetails returnValue = RProjectBaseImpl.recycle(m_projectDetails, m_client, Constants.RPROJECTRECYCLE);

            return returnValue;
        }

        /// <summary>
        /// Saves the project
        /// </summary>
        /// <returns>RProjectDetails object</returns>
        /// <remarks></remarks>
        public RProjectDetails save()
        {
            m_projectDetails = RProjectBaseImpl.save(m_projectDetails, null, m_client, Constants.RPROJECTSAVE);
            
            return m_projectDetails;

        }

        /// <summary>
        /// Saves the project
        /// </summary>
        /// <param name="details">RProjectDetails object describing the project</param>
        /// <returns>RProjectDetails object</returns>
        /// <remarks></remarks>
        public RProjectDetails save(RProjectDetails details)
        {

            m_projectDetails = RProjectBaseImpl.save(details, null, m_client, Constants.RPROJECTSAVE);
            
            return m_projectDetails;
        }

        /// <summary>
        /// Saves the project
        /// </summary>
        /// <param name="details">RProjectDetails object describing the project</param>
        /// <param name="dropOptions">ProjectDropOptions object describing what to drop from the project</param>
        /// <returns>RProjectDetails object</returns>
        /// <remarks></remarks>
        public RProjectDetails save(RProjectDetails details, ProjectDropOptions dropOptions)
        {
            m_projectDetails = RProjectBaseImpl.save(details, dropOptions, m_client, Constants.RPROJECTSAVE);
            
            return m_projectDetails;
        }

        /// <summary>
        /// Saves a copy of the project
        /// </summary>
        /// <param name="details">RProjectDetails object describing the project</param>
        /// <returns>RProject object</returns>
        /// <remarks></remarks>
        public RProject saveAs(RProjectDetails details)
        {
            RProject returnValue = RProjectBaseImpl.saveAs(details, null, m_client, Constants.RPROJECTSAVEAS);

            return returnValue;
        }

        /// <summary>
        /// Saves a copy the project
        /// </summary>
        /// <param name="details">RProjectDetails object describing the project</param>
        /// <param name="dropOptions">ProjectDropOptions object describing what to drop from the project</param>
        /// <returns>RProject object</returns>
        /// <remarks></remarks>
        public RProject saveAs(RProjectDetails details, ProjectDropOptions dropOptions)
        {
            RProject returnValue = RProjectBaseImpl.saveAs(details, dropOptions, m_client, Constants.RPROJECTSAVEAS);
            
            return returnValue;
        }


        /// <summary>
        /// Updates the project
        /// </summary>
        /// <param name="details">RProjectDetails object describing the project</param>
        /// <returns>A RProjectDetails object with the updated project information</returns>
        /// <remarks></remarks>
        public RProjectDetails update(RProjectDetails details)
        {
            m_projectDetails = RProjectBaseImpl.update(details, m_client, Constants.RPROJECTABOUTUPDATE);
            
            return m_projectDetails;
        }

        /// <summary>
        /// Download all files from project directory
        /// </summary>
        /// <returns>URL corresponding to zip file containing all files</returns>
        /// <remarks></remarks>
        public String downloadFiles()
        {
            String returnValue = System.Convert.ToString(RProjectDirectoryImpl.downloadFiles(m_projectDetails, null, m_client, Constants.RPROJECTDIRECTORYDOWNLOAD));
            
            return returnValue;
        }

        /// <summary>
        /// Download specified files from project directory
        /// </summary>
        /// <param name="files">List of file names to download</param>
        /// <returns>URL corresponding to zip file containing file specified</returns>
        /// <remarks></remarks>
        public String downloadFiles(List<String> files)
        {
            String returnValue = System.Convert.ToString(RProjectDirectoryImpl.downloadFiles(m_projectDetails, files, m_client, Constants.RPROJECTDIRECTORYDOWNLOAD));
            
            return returnValue;
        }

        /// <summary>
        /// List files in the project directory
        /// </summary>
        /// <returns>List of corresponding RProjectFile objects</returns>
        /// <remarks></remarks>
        public List<RProjectFile> listFiles()
        {
            List<RProjectFile>  returnValue = RProjectDirectoryImpl.listFiles(m_projectDetails, m_client, Constants.RPROJECTDIRECTORYLIST);

            return returnValue;
        }

        /// <summary>
        /// Load file from user repository into project directory
        /// </summary>
        /// <param name="file">RRepositoryFile object to load</param>
        /// <returns>RProjectFile object created</returns>
        /// <remarks></remarks>
        public RProjectFile loadFile(RRepositoryFile file)
        {
            RProjectFile returnValue = RProjectDirectoryImpl.loadFile(m_projectDetails, file, m_client, Constants.RPROJECTDIRECTORYLOAD);

            return returnValue;
        }

        /// <summary>
        /// Transfer file to project directory
        /// </summary>
        /// <param name="url">url of file to transfer</param>
        /// <param name="options">DirectoryUploadOptions object specifying behavior of the transfer</param>
        /// <returns>RProjectFile object created</returns>
        /// <remarks></remarks>
        public RProjectFile transferFile(String url, DirectoryUploadOptions options)
        {
            RProjectFile returnValue = RProjectDirectoryImpl.transferFile(m_projectDetails, url, options, m_client, Constants.RPROJECTDIRECTORYTRANSFER);

            return returnValue;
        }

        /// <summary>
        /// Upload a file to project directory
        /// </summary>
        /// <param name="file">complete path to the file to upload</param>
        /// <param name="options">DirectoryUploadOptions object specifying behavior of the transfer</param>
        /// <returns>RProjectFile object created</returns>
        /// <remarks></remarks>
        public RProjectFile uploadFile(String file, DirectoryUploadOptions options)
        {
            RProjectFile returnValue = RProjectDirectoryImpl.uploadFile(m_projectDetails, file, options, m_client, Constants.RPROJECTDIRECTORYUPLOAD);

            return returnValue;
        }

        /// <summary>
        /// Write a file to project directory
        /// </summary>
        /// <param name="text">text to write to a file in the project directory</param>
        /// <param name="options">DirectoryUploadOptions object specifying behavior of the transfer</param>
        /// <returns>RProjectFile object created</returns>
        /// <remarks></remarks>
        public RProjectFile writeFile(String text, DirectoryUploadOptions options)
        {
            RProjectFile returnValue = RProjectDirectoryImpl.writeFile(m_projectDetails, text, options, m_client, Constants.RPROJECTDIRECTORYWRITE);

            return returnValue;
        }

        /// <summary>
        /// Delete execution results on project
        /// </summary>
        /// <remarks></remarks>
        public void deleteResults()
        {

            RProjectExecuteImpl.deleteResults(m_projectDetails, m_client, Constants.RPROJECTEXECUTERESULTDELETE);

        }

        /// <summary>
        /// Download all results on a project.
        /// </summary>
        /// <returns>List of URLs corresponding to the results</returns>
        /// <remarks></remarks>
        public String downloadResults()
        {
            String returnValue = System.Convert.ToString(RProjectExecuteImpl.downloadResults(m_projectDetails, m_client, Constants.RPROJECTEXECUTERESULTDOWNLOAD));

            return returnValue;
        }

        /// <summary>
        /// Execute R code on a project
        /// </summary>
        /// <param name="code">R code to be executed</param>
        /// <returns>RProjectExecution object</returns>
        /// <remarks></remarks>
        public RProjectExecution executeCode(String code)
        {
            RProjectExecution returnValue = RProjectExecuteImpl.executeCode(m_projectDetails, code, null, m_client, Constants.RPROJECTEXECUTECODE);

            return returnValue;
        }

        /// <summary>
        /// Execute R code on a project
        /// </summary>
        /// <param name="code">R code to be executed</param>
        /// <param name="options">Options for this execution</param>
        /// <returns>RProjectExecution object</returns>
        /// <remarks></remarks>
        public RProjectExecution executeCode(String code, ProjectExecutionOptions options)
        {
            RProjectExecution returnValue = RProjectExecuteImpl.executeCode(m_projectDetails, code, options, m_client, Constants.RPROJECTEXECUTECODE);

            return returnValue;
        }

        /// <summary>
        /// Execute a single repository-managed script or a chain of repository-managed scripts
        /// on the current project.
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
        /// <returns>RProjectExecution object</returns>
        /// <remarks></remarks>
        public RProjectExecution executeScript(String scriptName, String scriptAuthor, String scriptVersion)
        {
            RProjectExecution returnValue = RProjectExecuteImpl.executeScript(m_projectDetails, scriptName, "root", scriptAuthor, scriptVersion, null, null, m_client, Constants.RPROJECTEXECUTESCRIPT);

            return returnValue;
        }

        /// <summary>
        /// Execute a single repository-managed script or a chain of repository-managed scripts
        /// on the current project.
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
        /// <returns>RProjectExecution object</returns>
        /// <remarks></remarks>
        public RProjectExecution executeScript(String scriptName, String scriptDirectory, String scriptAuthor, String scriptVersion)
        {
            RProjectExecution returnValue = RProjectExecuteImpl.executeScript(m_projectDetails, scriptName, scriptDirectory, scriptAuthor, scriptVersion, null, null, m_client, Constants.RPROJECTEXECUTESCRIPT);

            return returnValue;
        }

        /// <summary>
        /// Execute a single repository-managed script or a chain of repository-managed scripts
        /// on the current project.
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
        /// <param name="options">Options for this execution</param>
        /// <returns>RProjectExecution object</returns>
        /// <remarks></remarks>
        public RProjectExecution executeScript(String scriptName, String scriptDirectory, String scriptAuthor, String scriptVersion, ProjectExecutionOptions options)
        {
            RProjectExecution returnValue = RProjectExecuteImpl.executeScript(m_projectDetails, scriptName, scriptDirectory, scriptAuthor, scriptVersion, null, options, m_client, Constants.RPROJECTEXECUTESCRIPT);

            return returnValue;
        }

        /// <summary>
        /// Execute a single repository-managed script or a chain of repository-managed scripts
        /// on the current project.
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
        /// <param name="options">Options for this execution</param>
        /// <returns>RProjectExecution object</returns>
        /// <remarks></remarks>
        public RProjectExecution executeScript(String scriptName, String scriptAuthor, String scriptVersion, ProjectExecutionOptions options)
        {
            RProjectExecution returnValue = RProjectExecuteImpl.executeScript(m_projectDetails, scriptName, "root", scriptAuthor, scriptVersion, null, options, m_client, Constants.RPROJECTEXECUTESCRIPT);

            return returnValue;
        }

        /// <summary>
        /// Execute a single script found on a URL/path or a chain of scripts found on a set of URLs/paths
        /// on the current project.
        ///
        /// To execute a chain of repository-managed scripts on this call provide a comma-separated
        /// list of values on the externalSource parameter.
        /// Chained execution executes each of the scripts identified on the call in a sequential
        /// fashion on the R session, with execution occuring in the order specified on the parameter list.
        ///
        /// POWER_USER privileges are required for this call.
        /// </summary>
        /// <param name="externalSource">RScript represented as a URL or DeployR file path</param>
        /// <param name="options">Options for this execution</param>
        /// <returns>RProjectExecution object</returns>
        /// <remarks></remarks>
        public RProjectExecution executeExternal(String externalSource, ProjectExecutionOptions options)
        {
            RProjectExecution returnValue = RProjectExecuteImpl.executeScript(m_projectDetails, null, null, null, null, externalSource, options, m_client, Constants.RPROJECTEXECUTESCRIPT);

            return returnValue;
        }

        /// <summary>
        /// Flush execution history on project
        /// </summary>
        /// <remarks></remarks>
        public void flushHistory()
        {

            RProjectExecuteImpl.flushHistory(m_projectDetails, m_client, Constants.RPROJECTEXECUTEFLUSH);

        }

        /// <summary>
        /// Execute R script from the repository on a project
        /// </summary>
        /// <returns>String containing console text</returns>
        /// <remarks></remarks>
        public String getConsole()
        {
            String returnValue = System.Convert.ToString(RProjectExecuteImpl.getConsole(m_projectDetails, m_client, Constants.RPROJECTEXECUTECONSOLE));

            return returnValue;
        }

        /// <summary>
        /// Retrieve execution history on project
        /// </summary>
        /// <param name="options">Options for returning the History</param>
        /// <returns>List of RProjectExecution objects</returns>
        /// <remarks></remarks>
        public List<RProjectExecution> getHistory(ProjectHistoryOptions options)
        {
            List<RProjectExecution>  returnValue = RProjectExecuteImpl.getHistory(m_projectDetails, options, m_client, Constants.RPROJECTEXECUTEHISTORY);

            return returnValue;
        }

        /// <summary>
        /// Interrupt execution on project
        /// </summary>interruptExecution
        /// <remarks></remarks>
        public void interruptExecution()
        {

            RProjectExecuteImpl.interruptExecution(m_projectDetails, m_client, Constants.RPROJECTEXECUTEINTERRUPT);

        }

        /// <summary>
        ///  Retreive execution result list on project
        /// </summary>
        /// <returns>List of RProjectResult objects</returns>
        /// <remarks></remarks>
        public List<RProjectResult> listResults()
        {
            List<RProjectResult> returnValue = RProjectExecuteImpl.listResults(m_projectDetails, m_client, Constants.RPROJECTEXECUTERESULTLIST);

            return returnValue;
        }

        /// <summary>
        /// Attach one or more R packages to a project
        /// </summary>
        /// <param name="packageNames">List of R package names to attach</param>
        /// <param name="repo">Name of package repository to use</param>
        /// <returns>List of RProjectPackage objects</returns>
        /// <remarks></remarks>
        public List<RProjectPackage> attachPackage(List<String> packageNames, String repo)
        {
            List<RProjectPackage> returnValue = RProjectPackageImpl.attachPackage(m_projectDetails, packageNames, repo, m_client, Constants.RPROJECTPACKAGEATTACH);

            return returnValue;
        }

        /// <summary>
        /// Attach a single R package to a project
        /// </summary>
        /// <param name="packageName">R package name to attach</param>
        /// <param name="repo">Name of package repository to use</param>
        /// <returns>List of RProjectPackage objects</returns>
        /// <remarks></remarks>
        public List<RProjectPackage> attachPackage(String packageName, String repo)
        {
            List<String> packageNames = new List<String>();
            packageNames.Add(packageName);

            List<RProjectPackage> returnValue = RProjectPackageImpl.attachPackage(m_projectDetails, packageNames, repo, m_client, Constants.RPROJECTPACKAGEATTACH);

            return returnValue;
        }

        /// <summary>
        /// Detach one or more R packages from a project
        /// </summary>
        /// <param name="packageNames">List of R package names to detach</param>
        /// <returns>List of RProjectPackage objects</returns>
        /// <remarks></remarks>
        public List<RProjectPackage> detachPackage(List<String> packageNames)
        {
            List<RProjectPackage> returnValue = RProjectPackageImpl.detachPackage(m_projectDetails, packageNames, m_client, Constants.RPROJECTPACKAGEDETACH);

            return returnValue;
        }

        /// <summary>
        /// Detach a single R package from a project
        /// </summary>
        /// <param name="packageName">R package name to detach</param>
        /// <returns>List of RProjectPackage objects</returns>
        /// <remarks></remarks>
        public List<RProjectPackage> detachPackage(String packageName)
        {
            List<String> packageNames = new List<String>();
            packageNames.Add(packageName);

            List<RProjectPackage> returnValue = RProjectPackageImpl.detachPackage(m_projectDetails, packageNames, m_client, Constants.RPROJECTPACKAGEDETACH);

            return returnValue;
        }

        /// <summary>
        /// List R packages associated with a project
        /// </summary>
        /// <param name="installed">flag indicating if list should contain only packages that have been installed</param>
        /// <returns>List of RProjectPackage objects</returns>
        /// <remarks></remarks>
        public List<RProjectPackage> listPackages(Boolean installed)
        {
            List<RProjectPackage> returnValue = RProjectPackageImpl.listPackages(m_projectDetails, installed, m_client, Constants.RPROJECTPACKAGELIST);

            return returnValue;
        }

        /// <summary>
        /// Delete one or more objects from the R workspace
        /// </summary>
        /// <param name="objectNames">List of R objects to delete</param>
        /// <remarks></remarks>
        public void deleteObject(List<String> objectNames)
        {

            RProjectWorkspaceImpl.deleteObject(m_projectDetails, objectNames, m_client, Constants.RPROJECTWORKSPACEDELETE);

        }

        /// <summary>
        /// Delete an object from the R workspace
        /// </summary>
        /// <param name="objectName">Name of R obejct to delete</param>
        /// <remarks></remarks>
        public void deleteObject(String objectName)
        {

            List<String> objectNames = new List<String>();
            objectNames.Add(objectName);

            RProjectWorkspaceImpl.deleteObject(m_projectDetails, objectNames, m_client, Constants.RPROJECTWORKSPACEDELETE);

        }

        /// <summary>
        /// get the value of one or more objects from an R workspace
        /// </summary>
        /// <param name="objectNames">List of R objects to get</param>
        /// <param name="encodeDataFramePrimitiveAsVector">data.frame encoding preference</param>
        /// <returns>List of RData objects</returns>
        /// <remarks></remarks>
        public List<RData> getObjects(List<String> objectNames, Boolean encodeDataFramePrimitiveAsVector)
        {
            List<RData> returnValue = RProjectWorkspaceImpl.getObject(m_projectDetails, objectNames, encodeDataFramePrimitiveAsVector, m_client, Constants.RPROJECTWORKSPACEGET);

            return returnValue;
        }

        /// <summary>
        /// get the value of an object from an R workspace
        /// </summary>
        /// <param name="objectName">R objects to get</param>
        /// <returns>List of RData objects</returns>
        /// <remarks></remarks>
        public RData getObject(String objectName)
        {
            return getObject(objectName, false);
        }

        /// <summary>
        /// get the value of an object from an R workspace
        /// </summary>
        /// <param name="objectName">R objects to get</param>
        /// <param name="encodeDataFramePrimitiveAsVector">data.frame encoding preference</param>
        /// <returns>List of RData objects</returns>
        /// <remarks></remarks>
        public RData getObject(String objectName, Boolean encodeDataFramePrimitiveAsVector)
        {
            RData returnValue = default(RData);

            List<String> objectNames = new List<String>();
            objectNames.Add(objectName);

            List<RData> objects = RProjectWorkspaceImpl.getObject(m_projectDetails, objectNames, encodeDataFramePrimitiveAsVector, m_client, Constants.RPROJECTWORKSPACEGET);
            if (!(objects == null))
            {
                if (objects.Count > 0)
                {
                    returnValue = objects[0];
                    return returnValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// get all objects from an R workspace
        /// </summary>
        /// <returns>List of RData objects</returns>
        /// <remarks></remarks>
        public List<RData> listObjects()
        {
            List<RData> returnValue = RProjectWorkspaceImpl.listObjects(m_projectDetails, null, m_client, Constants.RPROJECTWORKSPACELIST);

            return returnValue;
        }

        /// <summary>
        /// get list of objects from an R workspace
        /// </summary>
        /// <param name="options">ProjectWorkspaceOptions object</param>
        /// <returns>List of RData objects</returns>
        /// <remarks></remarks>
        public List<RData> listObjects(ProjectWorkspaceOptions options)
        {
            List<RData> returnValue = RProjectWorkspaceImpl.listObjects(m_projectDetails, options, m_client, Constants.RPROJECTWORKSPACELIST);

            return returnValue;
        }

        /// <summary>
        /// load an object into the R workspace from a file in the repository
        /// </summary>
        /// <param name="file">RRepository object representing the file to load</param>
        /// <remarks></remarks>
        public void loadObject(RRepositoryFile file)
        {

            RProjectWorkspaceImpl.loadObject(m_projectDetails, file, m_client, Constants.RPROJECTWORKSPACELOAD);

        }

        /// <summary>
        /// push an object into the R workspace
        /// </summary>
        /// <param name="inputs">A list of RData objects to load</param>
        /// <remarks></remarks>
        public void pushObject(List<RData> inputs)
        {

            RProjectWorkspaceImpl.pushObject(m_projectDetails, inputs, m_client, Constants.RPROJECTWORKSPACEPUSH);

        }

        /// <summary>
        /// push an object into the R workspace
        /// </summary>
        /// <param name="input">An RData object to load</param>
        /// <remarks></remarks>
        public void pushObject(RData input)
        {

            List<RData> inputs = new List<RData>();
            inputs.Add(input);

            RProjectWorkspaceImpl.pushObject(m_projectDetails, inputs, m_client, Constants.RPROJECTWORKSPACEPUSH);

        }

        /// <summary>
        /// save an object from the workspace to a project file
        /// </summary>
        /// <param name="name">name of the object to save</param>
        /// <param name="descr">description of the object to save</param>
        /// <param name="versioning">indicate if versioning of the file should be enabled</param>
        /// <returns>RProjectFile object</returns>
        /// <remarks></remarks>
        public RProjectFile saveObject(String name, String descr, Boolean versioning)
        {
            RProjectFile returnValue = RProjectWorkspaceImpl.saveObject(m_projectDetails, name, descr, versioning, m_client, Constants.RPROJECTWORKSPACESAVE);

            return returnValue;
        }

        /// <summary>
        /// save an object from the workspace to a repository file
        /// </summary>
        /// <param name="name">name of the object to save</param>
        /// <param name="descr">description of the object to save</param>
        /// <param name="versioning">indicate if versioning of the file should be enabled</param>
        /// <param name="restricted">indicates if file is restricted</param>
        /// <param name="sharedUser">indicates if file is shared</param>
        /// <param name="published">indicates if file is published</param>
        /// <returns>RRepositoryFile object</returns>
        /// <remarks></remarks>
        public RRepositoryFile storeObject(String name, String descr, Boolean versioning, Boolean sharedUser, Boolean published, String restricted)
        {
            RRepositoryFile returnValue = RProjectWorkspaceImpl.storeObject(m_projectDetails, name, sharedUser, published, restricted, descr, versioning, m_client, Constants.RPROJECTWORKSPACESTORE);

            return returnValue;
        }

        /// <summary>
        /// transfer an file from a URL into the R workspace
        /// </summary>
        /// <param name="name">name of the object to transfer</param>
        /// <param name="url">url of the object to transfer</param>
        /// <remarks></remarks>
        public void transferObject(String name, String url)
        {

            RProjectWorkspaceImpl.transferObject(m_projectDetails, name, url, m_client, Constants.RPROJECTWORKSPACETRANSFER);

        }

        /// <summary>
        /// upload a file into the R workspace
        /// </summary>
        /// <param name="name">name of the object to transfer</param>
        /// <param name="filename">full path name of the file to transfer</param>
        /// <remarks></remarks>
        public void uploadObject(String name, String filename)
        {

            RProjectWorkspaceImpl.uploadObject(m_projectDetails, name, filename, m_client, Constants.RPROJECTWORKSPACEUPLOAD);

        }

        private void parseProject(JSONResponse jresponse, ref RProjectDetails m_projectDetails)
        {

            RProjectBaseImpl.parseProject(jresponse, ref m_projectDetails);

        }

    }
}