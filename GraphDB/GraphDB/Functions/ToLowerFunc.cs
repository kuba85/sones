/*
* sones GraphDB - Open Source Edition - http://www.sones.com
* Copyright (C) 2007-2010 sones GmbH
*
* This file is part of sones GraphDB Open Source Edition (OSE).
*
* sones GraphDB OSE is free software: you can redistribute it and/or modify
* it under the terms of the GNU Affero General Public License as published by
* the Free Software Foundation, version 3 of the License.
* 
* sones GraphDB OSE is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
* GNU Affero General Public License for more details.
*
* You should have received a copy of the GNU Affero General Public License
* along with sones GraphDB OSE. If not, see <http://www.gnu.org/licenses/>.
* 
*/


#region Usings

using System;
using sones.GraphDB.Managers.Structures;
using sones.GraphDB.TypeManagement;
using sones.GraphDB.TypeManagement.BasicTypes;
using sones.Lib.ErrorHandling;
using sones.GraphDBInterface.TypeManagement;


#endregion

namespace sones.GraphDB.Functions
{

    /// <summary>
    /// This function represents the well-known ToLower function.
    /// </summary>
    public class ToLowerFunc : ABaseFunction
    {

        public override string FunctionName
        {
            get { return "TOLOWER"; }
        }

        #region GetDescribeOutput()

        public override String GetDescribeOutput()
        {
            return "Returns a copy of this attribute value converted to lowercase.";
        }

        #endregion


        public ToLowerFunc()
        {
        }

        public override bool ValidateWorkingBase(IObject workingBase, DBTypeManager typeManager)
        {

            if (workingBase != null)
            {
                if ((workingBase is DBTypeAttribute) && (workingBase as DBTypeAttribute).GetValue().GetDBType(typeManager).IsUserDefined)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

            throw new NotImplementedException();

        }

        public override Exceptional<FuncParameter> ExecFunc(DBContext dbContext, params FuncParameter[] myParams)
        {
            if (CallingObject is ADBBaseObject)
                return new Exceptional<FuncParameter>(new FuncParameter(new DBString((CallingObject as ADBBaseObject).Value.ToString().ToLower())));
            else
                return new Exceptional<FuncParameter>(new Errors.Error_NotImplemented(new System.Diagnostics.StackTrace(true)));
        }

    }

}