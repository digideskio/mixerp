/********************************************************************************
Copyright (C) MixERP Inc. (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, version 2 of the License.


MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MixERP.Net.DbFactory;
using MixERP.Net.Framework;
using Npgsql;
using PetaPoco;
using Serilog;

namespace MixERP.Net.Schemas.Core.Data
{
    /// <summary>
    /// Provides simplified data access features to perform SCRUD operation on the database table "core.sales_tax_details".
    /// </summary>
    public class SalesTaxDetail : DbAccess
    {
        /// <summary>
        /// The schema of this table. Returns literal "core".
        /// </summary>
	    public override string ObjectNamespace => "core";

        /// <summary>
        /// The schema unqualified name of this table. Returns literal "sales_tax_details".
        /// </summary>
	    public override string ObjectName => "sales_tax_details";

        /// <summary>
        /// Login id of application user accessing this table.
        /// </summary>
		public long LoginId { get; set; }

        /// <summary>
        /// The name of the database on which queries are being executed to.
        /// </summary>
        public string Catalog { get; set; }

		/// <summary>
		/// Performs SQL count on the table "core.sales_tax_details".
		/// </summary>
		/// <returns>Returns the number of rows of the table "core.sales_tax_details".</returns>
		public long Count()
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return 0;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to count entity \"SalesTaxDetail\" was denied to the user with Login ID {LoginId}", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			const string sql = "SELECT COUNT(*) FROM core.sales_tax_details;";
			return Factory.Scalar<long>(this.Catalog, sql);
		}

		/// <summary>
		/// Executes a select query on the table "core.sales_tax_details" with a where filter on the column "sales_tax_detail_id" to return a single instance of the "SalesTaxDetail" class. 
		/// </summary>
		/// <param name="salesTaxDetailId">The column "sales_tax_detail_id" parameter used on where filter.</param>
		/// <returns>Returns a non-live, non-mapped instance of "SalesTaxDetail" class mapped to the database row.</returns>
		public MixERP.Net.Entities.Core.SalesTaxDetail Get(int salesTaxDetailId)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return null;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to the get entity \"SalesTaxDetail\" filtered by \"SalesTaxDetailId\" with value {SalesTaxDetailId} was denied to the user with Login ID {LoginId}", salesTaxDetailId, this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			const string sql = "SELECT * FROM core.sales_tax_details WHERE sales_tax_detail_id=@0;";
			return Factory.Get<MixERP.Net.Entities.Core.SalesTaxDetail>(this.Catalog, sql, salesTaxDetailId).FirstOrDefault();
		}

        /// <summary>
        /// Displayfields provide a minimal name/value context for data binding the row collection of core.sales_tax_details.
        /// </summary>
        /// <returns>Returns an enumerable name and value collection for the table core.sales_tax_details</returns>
		public IEnumerable<DisplayField> GetDisplayFields()
		{
			List<DisplayField> displayFields = new List<DisplayField>();

			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return displayFields;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to get display field for entity \"SalesTaxDetail\" was denied to the user with Login ID {LoginId}", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			const string sql = "SELECT sales_tax_detail_id AS key, sales_tax_detail_code || ' (' || sales_tax_detail_name || ')' as value FROM core.sales_tax_details;";
			using (NpgsqlCommand command = new NpgsqlCommand(sql))
			{
				using (DataTable table = DbOperation.GetDataTable(this.Catalog, command))
				{
					if (table?.Rows == null || table.Rows.Count == 0)
					{
						return displayFields;
					}

					foreach (DataRow row in table.Rows)
					{
						if (row != null)
						{
							DisplayField displayField = new DisplayField
							{
								Key = row["key"].ToString(),
								Value = row["value"].ToString()
							};

							displayFields.Add(displayField);
						}
					}
				}
			}

			return displayFields;
		}

		/// <summary>
		/// Inserts the instance of SalesTaxDetail class on the database table "core.sales_tax_details".
		/// </summary>
		/// <param name="salesTaxDetail">The instance of "SalesTaxDetail" class to insert.</param>
		public void Add(MixERP.Net.Entities.Core.SalesTaxDetail salesTaxDetail)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Create, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to add entity \"SalesTaxDetail\" was denied to the user with Login ID {LoginId}. {SalesTaxDetail}", this.LoginId, salesTaxDetail);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			Factory.Insert(this.Catalog, salesTaxDetail);
		}

		/// <summary>
		/// Updates the row of the table "core.sales_tax_details" with an instance of "SalesTaxDetail" class against the primary key value.
		/// </summary>
		/// <param name="salesTaxDetail">The instance of "SalesTaxDetail" class to update.</param>
		/// <param name="salesTaxDetailId">The value of the column "sales_tax_detail_id" which will be updated.</param>
		public void Update(MixERP.Net.Entities.Core.SalesTaxDetail salesTaxDetail, int salesTaxDetailId)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Edit, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to edit entity \"SalesTaxDetail\" with Primary Key {PrimaryKey} was denied to the user with Login ID {LoginId}. {SalesTaxDetail}", salesTaxDetailId, this.LoginId, salesTaxDetail);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			Factory.Update(this.Catalog, salesTaxDetail, salesTaxDetailId);
		}

		/// <summary>
		/// Deletes the row of the table "core.sales_tax_details" against the primary key value.
		/// </summary>
		/// <param name="salesTaxDetailId">The value of the column "sales_tax_detail_id" which will be deleted.</param>
		public void Delete(int salesTaxDetailId)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Delete, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to delete entity \"SalesTaxDetail\" with Primary Key {PrimaryKey} was denied to the user with Login ID {LoginId}.", salesTaxDetailId, this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			const string sql = "DELETE FROM core.sales_tax_details WHERE sales_tax_detail_id=@0;";
			Factory.NonQuery(this.Catalog, sql, salesTaxDetailId);
		}

		/// <summary>
		/// Performs a select statement on table "core.sales_tax_details" producing a paged result of 25.
		/// </summary>
		/// <returns>Returns the first page of collection of "SalesTaxDetail" class.</returns>
		public IEnumerable<MixERP.Net.Entities.Core.SalesTaxDetail> GetPagedResult()
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return null;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to the first page of the entity \"SalesTaxDetail\" was denied to the user with Login ID {LoginId}.", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			const string sql = "SELECT * FROM core.sales_tax_details ORDER BY sales_tax_detail_id LIMIT 25 OFFSET 0;";
			return Factory.Get<MixERP.Net.Entities.Core.SalesTaxDetail>(this.Catalog, sql);
		}

		/// <summary>
		/// Performs a select statement on table "core.sales_tax_details" producing a paged result of 25.
		/// </summary>
		/// <param name="pageNumber">Enter the page number to produce the paged result.</param>
		/// <returns>Returns collection of "SalesTaxDetail" class.</returns>
		public IEnumerable<MixERP.Net.Entities.Core.SalesTaxDetail> GetPagedResult(long pageNumber)
		{
			if(string.IsNullOrWhiteSpace(this.Catalog))
			{
				return null;
			}

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to Page #{Page} of the entity \"SalesTaxDetail\" was denied to the user with Login ID {LoginId}.", pageNumber, this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
	
			long offset = (pageNumber -1) * 25;
			const string sql = "SELECT * FROM core.sales_tax_details ORDER BY sales_tax_detail_id LIMIT 25 OFFSET @0;";
				
			return Factory.Get<MixERP.Net.Entities.Core.SalesTaxDetail>(this.Catalog, sql, offset);
		}
	}
}