using Dapper;
using Nota.Application.Common;
using Nota.Application.Interfaces;
using Nota.Domain.Entities;
using System.Data;

namespace Nota.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        IDbConnection _dbConnection;
        private readonly string? _tableName = string.Empty;
        private readonly List<string> _columnNames = [];

        public GenericRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;

            _tableName = typeof(T).Name + "s";// i need to handle this part using hmizer package
            _columnNames = typeof(T).GetProperties().Where(p => p.Name != "Id").Select(p => p.Name).ToList();
        }

        public async Task<PaginatedListResponse<T>> GetAllAsync(PaginatedRequest request, bool isDeleted = false)
        {
            // Base query
            var baseQuery = $"SELECT * FROM {_tableName}";
            var countQuery = $"SELECT COUNT(*) FROM {_tableName}";

            var whereClause = " WHERE IsDeleted = @IsDeleted";
            if (!string.IsNullOrEmpty(request.SearchTerm))
                whereClause += " AND Name LIKE @SearchTerm";

            // Pagination query
            var paginatedQuery = $@"
                    {baseQuery}
                    {whereClause}
                    ORDER BY Id
                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY";

            // Count query
            countQuery += whereClause;

            var parameters = new
            {
                SearchTerm = $"%{request.SearchTerm}%",
                Offset = (request.PageNumber - 1) * request.PageSize,
                PageSize = request.PageSize,
                IsDeleted = isDeleted
            };

            // Execute queries
            var data = await _dbConnection.QueryAsync<T>(paginatedQuery, parameters);
            var totalCount = await _dbConnection.ExecuteScalarAsync<int>(countQuery, parameters);

            return new PaginatedListResponse<T>(
                data: data,
                totalCount: totalCount,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize
            );
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool isDeleted = false)
        {
            var query = $@"SELECT * FROM {_tableName}
                            WHERE IsDeleted = @IsDeleted";

            return await _dbConnection.QueryAsync<T>(query, new { IsDeleted = isDeleted });
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var query = $@"SELECT * FROM {_tableName} 
                                WHERE Id = @Id";

            var entityInDb = await _dbConnection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
            return entityInDb;
        }

        public async Task<int> AddAsync(T entity)
        {
            var query = $@"INSERT INTO {_tableName} ({string.Join(',', _columnNames)}) 
                                            VALUES (@{string.Join(", @", _columnNames)});
                                            SELECT CAST(SCOPE_IDENTITY() as int)";

            return await _dbConnection.QuerySingleAsync<int>(query, entity);
        }

        public async Task<int> UpdateAsync(T entity)
        {
            entity.LastUpdate = DateTime.UtcNow;
            string[] blockedFields = ["CreatedAt", "DeletedAt", "IsDeleted"];
            var setValues = _columnNames
                                .Where(propName => !blockedFields.Contains(propName))
                                .Select(prop => $"{prop} = @{prop}");

            var query = $@"UPDATE {_tableName} SET {string.Join(", ", setValues)} WHERE id = @Id;";

            int rowsEffected = await _dbConnection.ExecuteAsync(query, entity);
            return rowsEffected;
        }

        public async Task<int> ToggleDeleteAsync(int id)
        {
            var query = @$"
                            UPDATE {_tableName}
                            SET 
                                IsDeleted = CASE 
                                                WHEN IsDeleted = 0 THEN 1 
                                                ELSE 0 
                                            END,
                                DeletedAt = CASE 
                                                WHEN IsDeleted = 0 THEN GETUTCDATE() 
                                                ELSE NULL 
                                            END
                            WHERE Id = @Id";

            return await _dbConnection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<bool> IsExistAsync(int id)
        {
            var query = @$"SELECT 1 FROM {_tableName} 
                            WHERE Id = @Id AND IsDeleted = 0";

            return id > 0 && await _dbConnection.QuerySingleOrDefaultAsync<int>(query, new { Id = id }) == 1;
        }
    }
}