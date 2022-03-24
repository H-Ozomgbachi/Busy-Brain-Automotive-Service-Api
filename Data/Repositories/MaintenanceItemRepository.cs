using Common.Data.Domain.RepairLabourTime;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Common.Data.Repositories
{
    public class MaintenanceItemRepository : IMaintenanceItemRepository
    {
        private readonly IConfiguration _config;

        public MaintenanceItemRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<int> CreateMaintenanceItem(MaintenanceItem maintenanceItem)
        {
            int newId = 0;

            using (var connection = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                using var cmd = new SqlCommand();
                await connection.OpenAsync();
                SqlTransaction transaction = connection.BeginTransaction("maintenance_item_add");

                try
                {
                    cmd.Connection = connection;
                    cmd.Transaction = transaction;

                    cmd.CommandText = "usp_maintenance_item_add";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = maintenanceItem.Title;
                    cmd.Parameters.Add("@code", SqlDbType.NVarChar).Value = maintenanceItem.Code;
                    cmd.Parameters.Add("@labour_time_hours", SqlDbType.Int).Value = maintenanceItem.LabourTimeHours;
                    cmd.Parameters.Add("@truck_model", SqlDbType.NVarChar).Value = maintenanceItem.TruckModel;
                    cmd.Parameters.Add("@cost_per_hour", SqlDbType.Decimal).Value = maintenanceItem.CostPerHour;
                    cmd.Parameters.Add("@failure_component_id", SqlDbType.Int).Value = maintenanceItem.FailureComponentId;
                    cmd.Parameters.Add("@created_at", SqlDbType.DateTime).Value = maintenanceItem.CreatedAt;
                    cmd.Parameters.Add("@modified_at", SqlDbType.DateTime).Value = maintenanceItem.ModifiedAt;
                    cmd.Parameters.Add("@modified_by", SqlDbType.UniqueIdentifier).Value = maintenanceItem.ModifiedBy;

                    newId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    transaction.Dispose();
                }
            }
            return newId;
        }

        public async Task<MaintenanceItem> GetMaintenanceItem(int failureComponentId, int id)
        {
            MaintenanceItem maintenanceItem = null;

            using (SqlConnection connection = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                using SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "usp_get_maintenance_item";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@failure_component_id", SqlDbType.Int).Value = failureComponentId;
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                await connection.OpenAsync();

                using var dr = await command.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    maintenanceItem = new MaintenanceItem(
                        dr.GetInt32(dr.GetOrdinal("id")),
                        dr.GetString(dr.GetOrdinal("title")),
                        dr.GetString(dr.GetOrdinal("code")),
                        dr.GetInt32(dr.GetOrdinal("labour_time_hours")),
                        dr.GetString(dr.GetOrdinal("truck_model")),
                        dr.GetDecimal(dr.GetOrdinal("cost_per_hour")),
                        dr.GetInt32(dr.GetOrdinal("failure_component_id")),
                        dr.GetDateTime(dr.GetOrdinal("created_at")),
                        dr.GetDateTime(dr.GetOrdinal("modified_at")),
                        dr.GetGuid(dr.GetOrdinal("modified_by")));
                }
            }
            return maintenanceItem;
        }

        public async Task<MaintenanceItem> GetMaintenanceItemByCode(string code)
        {
            MaintenanceItem maintenanceItem = null;

            using (SqlConnection connection = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                using SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "usp_get_maintenance_item_by_code";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@code", SqlDbType.NVarChar).Value = code;

                await connection.OpenAsync();

                using var dr = await command.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    maintenanceItem = new MaintenanceItem(
                        dr.GetInt32(dr.GetOrdinal("id")),
                        dr.GetString(dr.GetOrdinal("title")),
                        dr.GetString(dr.GetOrdinal("code")),
                        dr.GetInt32(dr.GetOrdinal("labour_time_hours")),
                        dr.GetString(dr.GetOrdinal("truck_model")),
                        dr.GetDecimal(dr.GetOrdinal("cost_per_hour")),
                        dr.GetInt32(dr.GetOrdinal("failure_component_id")),
                        dr.GetDateTime(dr.GetOrdinal("created_at")),
                        dr.GetDateTime(dr.GetOrdinal("modified_at")),
                        dr.GetGuid(dr.GetOrdinal("modified_by")));
                }
            }
            return maintenanceItem;
        }

        public async Task<int> UpdateMaintenanceItem(MaintenanceItem maintenanceItem)
        {
            int databaseResponse = 0;
            using SqlConnection connection = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);
            using SqlCommand command = new SqlCommand();
            await connection.OpenAsync();

            SqlTransaction transaction = connection.BeginTransaction("maintenance_item_update");
            try
            {
                command.Connection = connection;
                command.Transaction = transaction;
                command.CommandText = "usp_maintenance_item_update";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@id", SqlDbType.Int).Value = maintenanceItem.Id;
                command.Parameters.Add("@title", SqlDbType.NVarChar).Value = maintenanceItem.Title;
                command.Parameters.Add("@code", SqlDbType.NVarChar).Value = maintenanceItem.Code;
                command.Parameters.Add("@labour_time_hours", SqlDbType.Int).Value = maintenanceItem.LabourTimeHours;
                command.Parameters.Add("@truck_model", SqlDbType.NVarChar).Value = maintenanceItem.TruckModel;
                command.Parameters.Add("@cost_per_hour", SqlDbType.Decimal).Value = maintenanceItem.CostPerHour;
                command.Parameters.Add("@failure_component_id", SqlDbType.Int).Value = maintenanceItem.FailureComponentId;
                command.Parameters.Add("@created_at", SqlDbType.DateTime).Value = maintenanceItem.CreatedAt;
                command.Parameters.Add("@modified_at", SqlDbType.DateTime).Value = maintenanceItem.ModifiedAt;
                command.Parameters.Add("@modified_by", SqlDbType.UniqueIdentifier).Value = maintenanceItem.ModifiedBy;

                databaseResponse = await command.ExecuteNonQueryAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
            finally
            {
                transaction.Dispose();
            }
            return databaseResponse;
        }
    }
}
