/*
 * Constants.cs
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


namespace DeployR
{
    /// <summary>
    /// Constants used by this Class Library
    /// </summary>
    /// <remarks></remarks>
    sealed class Constants
    {

        public const String FORMAT_JSON = "format=json";

        public const String RUSERLOGIN = "/r/user/login";
        public const String RUSERABOUT = "/r/user/about";
        public const String RUSERAUTOSAVE = "/r/user/autosave";
        public const String RUSERLOGOUT = "/r/user/logout";
        public const String RUSERRELEASE = "/r/user/release";

        public const String RPROJECTLIST = "/r/project/list";
        public const String RPROJECTCREATE = "/r/project/create";
        public const String RPROJECTPOOL = "/r/project/pool";
        public const String RPROJECTRECYCLE = "/r/project/recycle";
        public const String RPROJECTCLOSE = "/r/project/close";
        public const String RPROJECTDELETE = "/r/project/delete";
        public const String RPROJECTPING = "/r/project/ping";
        public const String RPROJECTGRANT = "/r/project/grant";
        public const String RPROJECTABOUT = "/r/project/about";
        public const String RPROJECTABOUTUPDATE = "/r/project/about/update";
        public const String RPROJECTSAVE = "/r/project/save";
        public const String RPROJECTSAVEAS = "/r/project/saveas";
        public const String RPROJECTIMPORT = "/r/project/import";
        public const String RPROJECTEXPORT = "/r/project/export";

        public const String RPROJECTEXECUTECONSOLE = "/r/project/execute/console";
        public const String RPROJECTEXECUTECODE = "/r/project/execute/code";
        public const String RPROJECTEXECUTESCRIPT = "/r/project/execute/script";
        public const String RPROJECTEXECUTEINTERRUPT = "/r/project/execute/interrupt";
        public const String RPROJECTEXECUTEFLUSH = "/r/project/execute/flush";
        public const String RPROJECTEXECUTEHISTORY = "/r/project/execute/history";

        public const String RPROJECTEXECUTERESULTLIST = "/r/project/execute/result/list";
        public const String RPROJECTEXECUTERESULTPRINT = "/r/project/execute/result/print";
        public const String RPROJECTEXECUTERESULTDOWNLOAD = "/r/project/execute/result/download";
        public const String RPROJECTEXECUTERESULTDELETE = "/r/project/execute/result/delete";

        public const String RPROJECTWORKSPACELIST = "/r/project/workspace/list";
        public const String RPROJECTWORKSPACEGET = "/r/project/workspace/get";
        public const String RPROJECTWORKSPACEUPLOAD = "/r/project/workspace/upload";
        public const String RPROJECTWORKSPACETRANSFER = "/r/project/workspace/transfer";
        public const String RPROJECTWORKSPACEPUSH = "/r/project/workspace/push";
        public const String RPROJECTWORKSPACESAVE = "/r/project/workspace/save";
        public const String RPROJECTWORKSPACESTORE = "/r/project/workspace/store";
        public const String RPROJECTWORKSPACELOAD = "/r/project/workspace/load";
        public const String RPROJECTWORKSPACEDELETE = "/r/project/workspace/delete";

        public const String RPROJECTDIRECTORYLIST = "/r/project/directory/list";
        public const String RPROJECTDIRECTORYUPLOAD = "/r/project/directory/upload";
        public const String RPROJECTDIRECTORYTRANSFER = "/r/project/directory/transfer";
        public const String RPROJECTDIRECTORYWRITE = "/r/project/directory/write";
        public const String RPROJECTDIRECTORYUPDATE = "/r/project/directory/update";
        public const String RPROJECTDIRECTORYDOWNLOAD = "/r/project/directory/download";
        public const String RPROJECTDIRECTORYDELETE = "/r/project/directory/delete";
        public const String RPROJECTDIRECTORYSAVE = "/r/project/directory/save";
        public const String RPROJECTDIRECTORYSTORE = "/r/project/directory/store";
        public const String RPROJECTDIRECTORYLOAD = "/r/project/directory/load";

        public const String RPROJECTPACKAGELIST = "/r/project/package/list";
        public const String RPROJECTPACKAGEATTACH = "/r/project/package/attach";
        public const String RPROJECTPACKAGEDETACH = "/r/project/package/detach";

        public const String RREPOSITORYDIRECTORYARCHIVE = "/r/repository/directory/archive";
        public const String RREPOSITORYDIRECTORYCOPY = "/r/repository/directory/copy";
        public const String RREPOSITORYDIRECTORYCREATE = "/r/repository/directory/create";
        public const String RREPOSITORYDIRECTORYDELETE = "/r/repository/directory/delete";
        public const String RREPOSITORYDIRECTORYDOWNLOAD = "/r/repository/directory/download";
        public const String RREPOSITORYDIRECTORYLIST = "/r/repository/directory/list";
        public const String RREPOSITORYDIRECTORYMOVE = "/r/repository/directory/move";
        public const String RREPOSITORYDIRECTORYRENAME = "/r/repository/directory/rename";
        public const String RREPOSITORYDIRECTORYUPDATE = "/r/repository/directory/update";
        public const String RREPOSITORYDIRECTORYUPLOAD = "/r/repository/directory/upload";


        public const String RREPOSITORYFILELIST = "/r/repository/file/list";
        public const String RREPOSITORYFILEUPLOAD = "/r/repository/file/upload";
        public const String RREPOSITORYFILEWRITE = "/r/repository/file/write";
        public const String RREPOSITORYFILETRANSFER = "/r/repository/file/transfer";
        public const String RREPOSITORYFILEPUBLISH = "/r/repository/file/publish";
        public const String RREPOSITORYFILEUPDATE = "/r/repository/file/update";
        public const String RREPOSITORYFILEGRANT = "/r/repository/file/grant";
        public const String RREPOSITORYFILEREVERT = "/r/repository/file/revert";
        public const String RREPOSITORYFILEDOWNLOAD = "/r/repository/file/download";
        public const String RREPOSITORYFILEDIFF = "/r/repository/file/diff";
        public const String RREPOSITORYFILEDELETE = "/r/repository/file/delete";
        public const String RREPOSITORYFILEFETCH = "/r/repository/file/fetch";
        public const String RREPOSITORYFILECOPY = "/r/repository/file/copy";
        public const String RREPOSITORYFILEMOVE = "/r/repository/file/move";

        public const String RREPOSITORYSCRIPTLIST = "/r/repository/script/list";
        public const String RREPOSITORYSCRIPTEXECUTE = "/r/repository/script/execute";
        public const String RREPOSITORYSCRIPTRENDER = "/r/repository/script/render";
        public const String RREPOSITORYSCRIPTINTERRUPT = "/r/repository/script/interrupt";
        public const String RREPOSITORYSHELLEXECUTE = "/r/repository/shell/execute";

        public const String RJOBLIST = "/r/job/list";
        public const String RJOBSUBMIT = "/r/job/submit";
        public const String RJOBSCHEDULE = "/r/job/schedule";
        public const String RJOBQUERY = "/r/job/query";
        public const String RJOBCANCEL = "/r/job/cancel";
        public const String RJOBDELETE = "/r/job/delete";


        public const String RCLASS_MATRIX = "matrix";
        public const String RCLASS_NUMERIC = "numeric";
        public const String RCLASS_CHARACTER = "character";
        public const String RCLASS_BOOLEAN = "logical";
        public const String RCLASS_DATE = "date";
        public const String RCLASS_DATAFRAME = "data.frame";
        public const String RCLASS_LIST = "list";
        public const String RCLASS_FACTOR = "factor";
        public const String TYPE_VECTOR = "vector";
        public const String TYPE_MATRIX = "matrix";
        public const String TYPE_PRIMITIVE = "primitive";
        public const String TYPE_DATAFRAME = "dataframe";
        public const String TYPE_FACTOR = "factor";
        public const String TYPE_DATE = "date";
        public const String TYPE_LIST = "list";

        public const String SYSTEM_SHARED = "Shared";
        public const String SYSTEM_RESTRICTED = "Restricted";
        public const String SYSTEM_PUBLIC = "Public";

        public const int _NUMERIC = 0;
        public const int _CHARACTER = 1;
        public const int _LOGICAL = 2;

    }
}